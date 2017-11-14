# Sobrecarga de Constructores en C#

> Available in English [here](http://www.danielmartingonzalez.com/2017/06/constructor-overloading-in-csharp-readme.html)

Todos conocemos las ventajas de la sobrecarga de métodos en nuestro código.

> La sobrecarga de métodos es la creación de varios métodos con el mismo nombre, pero con diferentes firmas y definiciones. Se utiliza el número y tipo de argumentos para seleccionar qué definición de método ejecutar.

Esto ofrece flexibilidad a la hora de llamar a un método usando un número diferente de parámetros sin por ello, tener que replicar código. Por ejemplo:

```C#
public Task<IUICommand> ShowDialog(string title, string content)
{
    var dialog = new Windows.UI.Popups.MessageDialog(content, title);

    return dialog.ShowAsync().AsTask();
}

public Task<IUICommand> ShowDialog(string title)
{
    return ShowDialog(title, string.Empty);
}
```

Pero, ¿qué pasa si queremos usar estas ventajas en constructores?

Imaginemos por un momento que tenemos los siguientes constructores:

```C#
public MainViewModel(object parameter)
{
    Parameter = parameter;

    LoadData();
}

public MainViewModel()
{
    LoadData();
}
```

Se puede ver a simple vista que estamos duplicando código.

En este caso puede parecer algo que no afecta mucho a la solidez de nuestro código, pero tanto en el caso de la sobrecarga de métodos como en la sobrecarga de constructores, este código puede escalar y convertirse en un verdadero problema.

Una solución sencilla podría ser, como se puede ver en el ejemplo que acabamos de poner, crear un método que encapsule toda la funcionalidad común. El problema de usar esta técnica es que en el caso de asignar valores a variables ``readonly``, esta asignación solo puede realizarse a través del constructor, por tanto, este código tendrí­a que estar duplicado si o si.

``C#`` propone una solución mucho más limpia y elegante:

```C#
public MainViewModel(object parameter, IList<Checks> checks) : this()
{
    Parameter = parameter;
    Checks = checks;
}

public MainViewModel(IList<Checks> checks) : this(null, checks)
{
}

public MainViewModel(object parameter) : this(parameter, null)
{
}

public MainViewModel()
{
    LoadData();
}
```

Con ``this()`` podemos llamar a otro constructor que está definido en nuestra clase y ejecutar el código que lo contiene además del nuestro.

Gracias a esta técnica podremos usar las ventajas de la **sobrecarga de métodos en constructores** y mejorar nuestro código de una manera muy simple.

Nos vemos en un nuevo post.
Daniel '[danimart1991](http://danielmartingonzalez.com)' Martín González