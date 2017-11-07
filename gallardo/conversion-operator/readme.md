# Operadores de conversión en C# #

¡Hola a todos! *No Country for Geeks* es un grupo de personas a las que les gusta aprender cosas nuevas y compartirlas. Hoy vengo a hablar de algo nuevo que he aprendido ~~esta semana~~ este año: operadores de conversión.

**Los operadores de conversión nos van a permitir hacer una conversión de un tipo a otro de forma implícita o explícita.**

## Conversión

Un operador de conversión no es más que un método `operator` que recibe un tipo por parámetro y devuelve un objeto de otro tipo. Las palabras reservadas `implicit` y `explicit` nos permite indicar si la conversión va a ser implícita o explícita.

La sintaxis es la siguiente:

```csharp
public static [implicit/explicit] operator [destination type]([source type] value)

// Example:
public static implicit operator string(Header header) // Implicit Cast string to Header
public static explicit operator FontSize(int size) // Explicit Cast FontSize to int
```

## Implicit

Según podemos leer en la [documentación](https://docs.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/implicit) de Microsoft:

> La palabra clave `implicit` se usa para declarar un operador de conversión de tipo implícito definido por el usuario.

**Esta conversión se va a hacer de forma automática** y silenciosa por el compilador. Por tanto, sin intervención del programador. **Por eso es importante que no haya pérdida de información o se puedan producir excepciones**.

> Para marcar el operador de conversión como `implicit` debemos asegurarnos de que no se vaya a producir excepciones o pérdida de información.

## Explicit

Según la [documentación](https://docs.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/explicit) de Microsoft:

> La palabra clave `explicit` declara un operador de conversión de tipo definido por el usuario que se debe invocar con una conversión.

La diferencia con los operadores implícitos es que **el programador debe invocar la conversión**. Si una conversión puede **producir pérdida de información o lanzar excepciones debe ser marcada como explícita**.

## Ejemplo

Estamos definiendo la estructura de un documento. Para ello tenemos diferentes clases que nos ayudan a representarla.

```csharp
class Document
{
    public void  AddHeader(string header)
    {
        AddHeader(new Header(header));
    }

    public void  AddHeader(Header header)
    {
        // ...    
    }

    public void SetFontSize(int fontSize)
    {
        SetFontSize(new FontSize(fontSize));
    }

    public void SetFontSize(FontSize fontSize)
    {
        // ...    
    }
}

class Header
{
    public string Text { get; set;  }

    public Header(string text)
    {
        Text = text;
    }
}

class FontSize
{
    public int Size { get; set; }

    public FontSize(int size)
    {
        if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));

        Size = size;
    }
}
```

**Importante:** Como vemos, la clase `Document` tiene los métodos `AddHeader` y `SetFontSize` sobrecargados.

### Implicit

Vamos a crear un **operador de conversión implícito** desde la clase `string` a la clase `Header`.

```csharp
public static implicit operator Header(string text)
{
    return new Header(text);
}
```

Al no haber pérdida de información ni la posibilidad de que se produzcan excepciones podemos marcar el operador como `implicit`.

### Explicit

Ahora vamos a crear unos operadores de conversión para la clase `FontSize` desde las clases `int` y `float`.

```csharp
public static explicit operator FontSize(int size)
{
    return new FontSize(size); // Can throw Exception
}

public static explicit operator FontSize(float size)
{
    return new FontSize((int)size); // Can throw Exception and lose information
}

public static implicit operator int(FontSize size)
{
    return size.Size;
}
```

La conversión de `int` a `FontSize` **puede producir una excepción**, por eso debemos **marcar el operador como explícito**.

En el caso de la conversión de `float` a `FontSize` además de poder producir excepciones **puede haber pérdida de información**, por tanto, también lo tenemos que **marcar como `explicit`**.

Por otro lado, si queremos convertir de `FontSize` a `int` no se van a producir excepciones ni vamos a tener pérdida de informacion, así que lo marcamos como `implicit`.

### ¿Cómo afecta esto a nuestro código anterior?

Podemos eliminar la sobrecarga de la clase `Document` para los métodos `AddHeader` y `SetFontSize`.

```csharp
class Document
{
    public void  AddHeader(Header header)
    {
        // ...    
    }

    public void SetFontSize(FontSize fontSize)
    {
        // ...    
    }
}
```

Ya no es necesaria la sobrecarga de métodos para trabajar cómodamente con `string` e `int`.

Además podemos hacer cosas como estas:

```csharp
static void Main(string[] args)
{
    var document = new Document();

    // Implicit Cast string to Header
    document.AddHeader("No Country for Geeks");

    // Explicit Cast int to FontSize
    document.SetFontSize((FontSize)24);

    // Explicit Cast float to FontSize
    var fontSize = (FontSize)18.5f;

    // Implicit cast FontSize to int
    var totalSize = fontSize + 12;

    // Explicit Cast int to FontSize
    document.SetFontSize((FontSize)totalSize);
}
```

## Código completo

```csharp
class Program
{
    static void Main(string[] args)
    {
        var document = new Document();

        // Implicit Cast string to Header
        document.AddHeader("No Country for Geeks");

        // Explicit Cast int to FontSize
        document.SetFontSize((FontSize)24);

        // Explicit Cast float to FontSize
        var fontSize = (FontSize)18.5f;

        // Implicit cast FontSize to int
        var totalSize = fontSize + 12;

        // Explicit Cast int to FontSize
        document.SetFontSize((FontSize)totalSize);
    }

    class Document
    {
        public void  AddHeader(Header header)
        {
            // ...    
        }

        public void SetFontSize(FontSize fontSize)
        {
            // ...    
        }
    }

    class Header
    {
        public string Text { get; set;  }

        public Header(string text)
        {
            Text = text;
        }

        public static implicit operator Header(string text)
        {
            return new Header(text);
        }
    }

    class FontSize
    {
        public int Size { get; set; }

        public FontSize(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));

            Size = size;
        }

        public static explicit operator FontSize(int size)
        {
            return new FontSize(size); // Can throw Exception
        }

        public static explicit operator FontSize(float size)
        {
            return new FontSize((int)size); // Can throw Exception
        }

        public static implicit operator int(FontSize size)
        {
            return size.Size;
        }
    }
```

## Conclusiones

Como hemos visto los operadores de conversión son muy potentes y nos van a ayudar a la legibilidad del código evitando conversiones innecesarias de tipos y muchas sobrecargas de métodos.

## Referencia

* Palabra reservada [explicit](https://docs.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/explicit).
* Palabra reservada [implicit](https://docs.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/implicit).

<a href="https://www.freepik.com/free-photos-vectors/"> vector created by Freepik</a>

Un saludo y nos vemos en el futuro.
