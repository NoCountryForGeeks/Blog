# [Micropost] F12 en Visual Studio

¡Hola a todos! Visual Studio tiene una gran cantidad de atajos de teclado. Hoy vengo a hablar del **F12** porque no todo el mundo sabe el partido que se le puede sacar a este atajo.

## Código de ejemplo

Para ver la funcionalidad que nos ofrece el atajo F12 vamos a trabajar sobre un pequeño código de ejemplo en el que tenemos una clase y su interfaz.

```csharp
interface IPost
{
    void Publish();
    void Schedule(DateTime publishDate);
}

class Post : IPost
{
    public void Publish()
    {
      Console.WriteLine($"Published post");
    }

    public void Schedule(DateTime publishDate)
    {
      Console.WriteLine($"Scheduled post: {publishDate.ToString()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
      var postA = new Post();
      IPost postB = new Post();

      postA.Publish();
      postB.Schedule(DateTime.Now.AddDays(1));
    }
}
```

## Go To Definition

En general **el atajo F12 nos permite navegar a la definición del ítem sobre el que estemos situados**. Por ejemplo, estando sobre una variable nos lleva a dónde está declarada. Pero vamos a ver más en detalle alguna de la funcionalidad que ofrece.

**Nota:** los atajos que vamos a ver funcionan en Visual Studio 2017. No podemos asegurarlo para versiones anteriores.

## Navegación al método

El atajo **F12 nos sirve para navegar a diferentes partes del código: Go to Definition**.

Si colocamos el cursor sobre un método y pulsamos F12 nos llevara a la definición de ese método.

**Nota:** Si la referencia con la que estamos trabajando es **una interfaz nos llevará a la firma del método y no a la implementación**.

De este modo al colocar el cursor sobre `Publish()` de la variable `postA` y pulsar F12 nos llevará a la implementación en la clase `Post`.

Por el contrario si pulsamos F12 sobre `Schedule(...)` de `postB` navegaremos a la definición del método en la interfaz ya que `postB` es una referencia de `IPost`.

### Ctrl + F12

**La mayoría de veces trabajamos con referencias a interfaces** ya que hacemos uso de la inyección de dependencias.

Por eso el atajo F12 no siempre es tan útil porque **muchas veces queremos navegar a la implementación** de nuestro método.

Para solucionar esto tenemos el comando **Ctrl + F12 que siempre navega a la implementación de método**.

Por tanto si estando sobre `Schedule(...)` de `postB` navegaríamos a la implementación de `Schedule` en la clase `Post`.

**Nota:** En caso de tener **más de una implementación de una interfaz, Visual Studio nos muestra una lista con todas ellas** para elegir a cual queremos navegar.

## Navegar a un tipo

Otra de las opciones que nos ofrece **el atajo F12 es la de navegar a un tipo**. Al igual que pasa con la navegación a un método si colocamos el cursor sobre un tipo o interfaz nos navega a dónde está definida.

Si pulsamos F12 sobre `Post` navegaremos a la definición de la clase y lo mismo sucede si lo hacemos sobre `IPost` que nos navegará a la interfaz.

### var

Esta atajo es mucho más potente ya que nos permite navegar al tipo que infiere `var`.

Por tanto, en nuestro ejemplo, **si nos colocamos sobre `var` y pulsamos F12 navegaremos a la clase `Post`**.

**Importante:** Hay que colocar el cursor sobre la palabra reservada `var`.

### Ctrl + F12

Al igual que pasaba con los métodos, al estar sobre una interfaz y pulsar **Ctrl + F12** nos navegará a la implementación de la interfaz, en este caso a `Post` estando sobre `IPost`.

## Más atajos

Esto es lo más interesante del los atajos **F12** y **Ctrl + F12**. Como hemos comentado al principio, Visual Studio tiene muchos otros atajos que nos ayudan en el día a día a trabajar más fácilmente.

Podéis dejar en comentarios qué atajos de teclado os parecen más interesantes y los veremos en siguientes *Microposts*.

Un saludo y ¡nos vemos en el futuro!  
