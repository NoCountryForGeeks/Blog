# No more nested conditionals, embrace the strategy pattern

Some days ago, a team partner asked in our chat how to resolve the following situation:

"I have a controller calling a class, depending of the value of parameters passed to the controller the called method can make an action or another"

The code that him want to refactor was something like this (is just an example) :


```csharp
public int DoMathematicalOperation (string @operator, int a, int b)
{
    if (@operator == "+")
    {
        return Add(a, b);
    }

    if (@operator == "*")
    {
        return Multiply(a, b);
    }

    if (@operator == "/")
    {
        return Divide(a, b);
    }

    throw Exception("You must pass a valid operator");
}

//METHODS FOR ADD, MULTIPLY AND DIVIDE
```

Who had never been in this situation? The product owner needs a new feature for the application, but this feature has some exceptions that changes all over the behavior in some cases. For example, you have a flight search service that uses an external service that gives all the flights available on a date, but have some routes that uses another service for getting these flights, in this case the behavior changes completely.

Another case can be  if you change a functionality but the product owner wants to be inactive in production enviroments until marketing department (for example) approves it, in this case you want to use a feature flag in your settings file that changes the behavior.

Going back to our previous example is obvious that there are more than one implementation of a mathematical operation with two operators, The previous code does not respect the S of the SOLID principles  because it is taken more than one resposability at once (Add, Multiply and divide), it can be refactored isolating to an implementation by mathematical operation and using an interface as shape.

There is the base interface of our refactor:

```csharp
public interface IMathematicalOperation 
{
    int DoMathematicalOperation(int a, int b);
}
```

Now, there is the 3 implementations:

```csharp
public class AddOperation : IMathematicalOperation
{
    public int DoMathematicalOperation(int a, int b)
    {
        return a + b;
    }
}
```

```csharp
public class MultiplyOperation : IMathematicalOperation
{
    public int DoMathematicalOperation(int a, int b)
    {
        return a * b;
    }
}
```

```csharp
public class DivideOperation : IMathematicalOperation
{
    public int DoMathematicalOperation(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return a / b;
    }
}
```

Having this scenario is easy to implement the strategy pattern, remember that an operation depends of operator symbol passed (+, *, /) so, why not to present the operator as a new property in each implementation?

```csharp
public interface IMathematicalOperation 
{
    string Operator { get; }
    int DoMathematicalOperation(int a, int b);
}
```

```csharp
public class AddOperation : IMathematicalOperation
{
    public string Operator => "+";

    public int DoMathematicalOperation(int a, int b)
    {
        return a + b;
    }
}
```

```csharp
public class MultiplyOperation : IMathematicalOperation
{
    public string Operator => "*";

    public int DoMathematicalOperation(int a, int b)
    {
        return a * b;
    }
}
```

```csharp
public class DivideOperation : IMathematicalOperation
{
    public string Operator => "/";

    public int DoMathematicalOperation(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return a / b;
    }
}
```

It's time to add a new class in the ecuation; the resolver.

The resolver of this case has the responsibility to give the correct implementation of an mathematical operation taking the operator symbol as parameter.

```csharp
public class MathematicalOperationResolver
{
    private List&lt;IMathematicalOperation> _mathematicalOperationImplementations;

    public MathematicalOperationResolver()
    {
        _mathematicalOperationImplementations = new List&lt;IMathematicalOperation>
        {
            new AddOperation(),
            new MultiplyOperation(),
            new DivideOperation()
        };
    }

    public IMathematicalOperation Resolve (string @operator)
    {
        return _mathematicalOperationImplementations.Single(x => x.Operator == @operator);
    }
}
```

Note that this is just an example, you can pass all the mathematical implementations as a list using your favorite DI system.

With your resolver implemented then you can refactor the original code and the result would be like this:

```csharp
public int DoMathematicalOperation(string @operator, int a, int b)
{
    var resolver = new MathematicalOperationResolver();
    var mathematicalOperation = resolver.Resolve(@operator);
    return mathematicalOperation.DoMathematicalOperation(a, b);
}
```

You see? All the nested conditionals has been removed and the code has become more maintainable, take in mind when you find this situation the next time!

Happy coding!
