![Header](images/header.jpg)

# No más condicionales anidados, abraza el patrón estrategia

[English version](https://www.carlosjdelgado.com/no-more-nested-conditionals-embrace-the-strategy-pattern/)

Hace algunos días un compañero de equipo formuló la siguiente pregunta en nuestro chat:

> Tengo un controller que llama a una clase, dependiendo del valor del parámetro que se le pasa al controller el método al que se llama puede hacer una cosa u otra.

El código que quería refactorizar era algo como esto (solo un ejemplo):

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

//METODOS PARA SUMAR, MULTIPLICAR Y DIVIDIR
```
¿Quien no se ha encontrado nunca en esta situación? El product owner necesita una nueva funcionalidad para la aplicación, pero esta funcionalidad tiene algunas excepciones que cambian todo el comportamiento para algunos casos. Por ejemplo, tienes un servicio de búsqueda de vuelos que usa un servicio externo para devolver todos los vuelos disponibles en un día, pero hay algunas rutas que usan otro servicio para obtener esos vuelos, en este caso el comportamiento cambia completamente.

Otro caso puede ser si cambias una funcionalidad, pero el product owner quiere que este inactiva en el entorno de producción hasta que el departamento de marketing (por ejemplo) lo apruebe, en ese caso querrás usar una feature flag en tus settings que cambian el comportamiento.

Volviendo a nuestro ejemplo anterior es obvio que hay más de una implementación de una operación matemática con 2 operadores, el código anterior no respeta la S de los principios SOLID porque está asumiendo más de una responsabilidad al mismo tiempo (sumar, multiplicar y dividir), puede ser refactorizado separándolo para tener una implementación por operación matematica usando una interfaz como molde.

Esta es la interfaz base de nuestro refactor:

```csharp
public interface IMathematicalOperation 
{
    int DoMathematicalOperation(int a, int b);
}
```

Ahora tenemos estas 3 implementaciones:

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
Sobre este escenario es fácil de implementar un [patrón estrategia](https://en.wikipedia.org/wiki/Strategy_pattern), recuerda que cada operación depende del símbolo de operador que se le pase (+, *, /) entonces, ¿porque no presentar el operador como una nueva propiedad en cada implementación?

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

Es el momento de añadir una nueva clase en la ecuación: el resolver.

El resolver de este caso tiene la responsabilidad de devolver la implementación correcta de una operación matemática a partir del símbolo de operador pasado como parámetro.

```csharp
public class MathematicalOperationResolver
{
    private List<IMathematicalOperation> _mathematicalOperationImplementations;

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

Ten en cuenta que esto es solo un ejemplo, puedes pasar todas las implementaciones en una lista usando tu sistema favorito de DI.

Con tu resolver implementado entonces puedes refactorizar el código original, y el resultado quedaría tal que así:

```csharp
public int DoMathematicalOperation(string @operator, int a, int b)
{
    var resolver = new MathematicalOperationResolver();
    var mathematicalOperation = resolver.Resolve(@operator);
    return mathematicalOperation.DoMathematicalOperation(a, b);
}
```

¿Has visto? todos los condicionales anidados se han eliminado y el código ahora es más mantenible, ten esto en cuenta cuando vuelvas a encontrarte con esta situación.

Happy coding!
