![Header](images/header.jpg)

# Atributos en C#

Muchas veces hemos utilizado atributos en C# para miembros o clases. En este post vamos a ver cómo crear nuestros propios atributos que nos van a permitir añadir información extra a algunos elementos.

Vamos a trabajar con atributos para enriquecer un poco a los enumerados, que muchas veces se nos quedan un poco cortos.

## Algunos ejemplos

Hay una gran cantidad de ejemplos de atributos que hemos podido utilizar alguna vez: Newtonsoft Json, Entity Framework, xUnit, etc.

```csharp
// Newtonsoft Json
[JsonIgnore]
public string InternalId {get;set;}

[JsonProperty(“display_name”)]
public string DisplayName {get; set;}

// Entity Framework
[Key]
public int Id {get;set;}

[MaxLength(10),MinLength(5)]
public string BloggerName { get; set; }

// xUnit
[Fact]
public void WhenTest_ItShouldBeReturnTrue(){}

[Fact(Skip=”Skip because yes”)]
public void WhenTest_ItShouldBeReturnTrue(){}
```

## Antes de empezar…

Para crear un atributo debemos crear una clase que herede de `Attribute`. Además podemos especificar algunas propiedades utilizando el atributo `AttributeUsage`.

Con este atributo podemos indicar:

* **ValidOn:** targets válidos para el atributo que vamos a crear. [Aquí](https://msdn.microsoft.com/en-us/library/system.attributetargets.aspx) podemos consultar los posibles valores.
* **AllowMultiple:** nos indica si el atributo se puede especificar más de una vez para un mismo elemento. Por defecto `false`.
* **Inherited:** nos indica si el atributo lo pueden heredar las clases derivadas. Por defecto `false`.

Antes de seguir debemos hacer una diferenciación entre parámetros *posicionales* y parámetros con nombre.

**Los parámetros *posicionales* son obligatorios y los especificaremos en el constructor** del atributo mientras que **los parámetros con nombre, que son opcionales, los especificaremos como propiedades** y deberemos nombrarlos al usar el atributo.

**Nota:** Los parámetros de los atributos deben ser un valor constante de un tipo simple (`string`, `enum`, `Type`...).

[Aquí](https://msdn.microsoft.com/es-es/library/z0w1kczw%28VS.80%29.aspx) puedes leer la documentación de los atributos.

## Creando nuestro primer atributo: Display Name

Uno de los problemas habituales con los enumerados es cuando los quieres mostrar por pantalla para, por ejemplo, que el usuario pueda filtrar los datos por ese valor del enumerado.

Probablemente todos hayamos hecho algo como un switch para mostrar un texto u otro. Pues bien, esto lo podemos resolver fácilmente con un atributo.

En este ejemplo vamos a tener un solo parámetro *posicional* llamado `DisplayName` que será el texto a mostrar.

Para crear nuestro atributo `DisplayName` **debemos crear una clase que herede de `Attribute`**. Como vamos a añadirle este atributo a cada valor de enumerado le indicaremos que el **target es Field**.

```csharp
[AttributeUsage(AttributeTargets.Field)]
public class DisplayNameAttribute : Attribute
{
  public readonly string DisplayName;

  public DisplayNameAttribute(string displayName)
  {
      DisplayName = displayName;
  }
}
```

```csharp
public enum Fruit
{
   [DisplayName("Lemon")]
   Lemon,
   [DisplayName("Watermelon")]
   Watermelon,
   [DisplayName("Orange")]
   Orange,
   [DisplayName("Blood Orange")]
   BloodOrange,
   [DisplayName("Kiwi")]
   Kiwi,
   [DisplayName("Banana")]
   Banana
}
```

Código de ejemplo [GitHub](https://github.com/maktub82/Samples/blob/master/Attributes/Maktub82.Samples.Attributes/Attributes/DisplayNameAttribute.cs).

## Consultando atributos gracias a la reflexión

Ahora que hemos enriquecido nuestro enumerado necesitamos tener una forma de poder consultar la nueva información. Para ello vamos a utilizar reflexión.

Con el siguiente método **obtenemos todos los atributos de un determinado tipo del enumerado** pasado por parámetro.

```csharp
private static IEnumerable<T> GetAttributes<T>(Enum enumValue) where T : Attribute
{
  // Obtenemos el tipo
  var type = enumValue.GetType();
  // La información del valor concreto del enumerado
  var memberInfo = type.GetMember(enumValue.ToString());
  // Obtenemos todos los atributos del miembro
  var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

  return attributes.Cast<T>();
} 

private static T GetFirstOrDefaultAttribute<T>(Enum enumValue) where T : Attribute
{
  var attributes = GetAttributes<T>(enumValue);
  return attributes.FirstOrDefault() as T;
}
```

Para que sea más sencillo e intuitivo de utilizar vamos a crear un método de extensión que nos devuelva el display name. Simplemente una vez tenemos el atributo obtenemos el miembro público `DisplayName`.

```csharp
public static string GetDisplayName(this Enum enumValue)
{
  var attribute = GetFirstOrDefaultAttribute<DisplayNameAttribute>(enumValue);
  return attribute != null ? attribute.DisplayName : string.Empty;
}
```

Ahora podemos utilizar el método `GetDisplayName` para obtener el `DisplayName` de un enumerado.

```csharp
Fruit.BloodOrange.GetDisplayName();
Fruit.Watermelon.GetDisplayName();
Fruit.Lemon.GetDisplayName();
```

Código de ejemplo [GitHub](https://github.com/maktub82/Samples/blob/master/Attributes/Maktub82.Samples.Attributes/Extensions/EnumExtensions.cs).

## Parámetros nombrados

En este ejemplo vemos cómo definir atributos con nombre y cómo especificarlos a la hora de usarlo.

```csharp
[AttributeUsage(AttributeTargets.Field)]
public class CenturyDataAttribute : Attribute
{
  private int startYear;

  public int StartYear
  {
      get { return startYear; }
      set { startYear = value; }
  }

  private int endYear;

  public int EndYear
  {
      get { return endYear; }
      set { endYear = value; }
  }

  public readonly string DisplayName;

  public CenturyDataAttribute(string displayName)
  {
      DisplayName = displayName;
  }
}
```

Como vemos, basta con añadir una propiedad en el atributo para definir un parámetro con nombre.

A la hora de usarlo simplemente nombramos los parámetros y le asignamos un valor después de los parámetros *posicionales*.

```csharp
public enum Century
{
  [CenturyData("15th", StartYear = 1401, EndYear = 1500)]
  XV,
  [CenturyData("16th", StartYear = 1501, EndYear = 1600)]
  XVI,
  [CenturyData("17th", StartYear = 1601, EndYear = 1700)]
  XVII,
  [CenturyData("18th", StartYear = 1701, EndYear = 1800)]
  XVIII,
  [CenturyData("19th", StartYear = 1801, EndYear = 1900)]
  XIX,
  [CenturyData("20th", StartYear = 1901, EndYear = 2000)]
  XX
}
```
**Importante:** Este **código es un ejemplo** para mostrar los parámetros con nombre en un atributo. **Hay que tener cuidado a la hora de desarrollar y saber cuándo es necesario o no el uso de los atributos.**

Código de ejemplo [GitHub](https://github.com/maktub82/Samples/blob/master/Attributes/Maktub82.Samples.Attributes/Attributes/CenturyDataAttribute.cs).

## Múltiples atributos

En este caso queremos mostrar por pantalla unas categorías para el filtrado de datos. Una vez el usuario seleccione una categoría se debe hacer una llamada a una API.

Surgen dos problemas: por un lado por pantalla se debe ver un texto amigable para cada categoría y por otro lado necesitamos saber el valor de cada categoría en el API (que además es un valor múltiple).

Para poder mostrar un texto amigable por pantalla para cada categoría basta con añadir el atributo `DisplayName` que hemos creado antes. Y para obtener el valor que tiene en el API crearemos otro atributo llamado `ApiValue`. En este caso **el atributo `ApiValue` se podrá asignar varias veces a un elemento**.

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ApiValueAttribute : Attribute
{
  public readonly string ApiValue;

  public ApiValueAttribute(string apiValue)
  {
    ApiValue = apiValue;
  }
}
```

Ahora simplemente en nuestro enumerado ya podemos especificar el nombre a mostrar y los diferentes valores que tienen las categorías en el API.

```csharp
public enum Category
{
  [DisplayName("Series")]
  [ApiValue("series")]
  [ApiValue("tv-series")]
  [ApiValue("tv-vod")]
  Series,
  [DisplayName("Films and Movies")]
  [ApiValue("movies")]
  [ApiValue("films")]
  Films,
  [DisplayName("Documentary")]
  [ApiValue("tv-documentary")]
  Documentary
}
```

Al igual que antes obtenemos la información del enumerado gracias a su atributo.

```csharp
public static string GenerateQuery(this Enum enumValue)
{
  var attributes = GetAttributes<ApiValueAttribute>(enumValue);
  IEnumerable<string> values = attributes.Select(attribute => attribute.ApiValue);

  return $"{string.Join(",", values)}";
}
```

Aquí podemos ver un ejemplo de ejecución:

```csharp
$"To Search by {Category.Films.GetDisplayName()} category:
http://example.api.fake?query=the&category={Category.Films.GenerateQuery()"

// Result
To Search by Films and Movies category: http://example.api.fake?query=the&category=movies,films

```

Código de ejemplo [GitHub](https://github.com/maktub82/Samples/blob/master/Attributes/Maktub82.Samples.Attributes/Attributes/ApiValueAttribute.cs)

## Conclusiones

Como hemos visto, los atributos son muy potentes para añadir información a elementos. En nuestro caso lo hemos visto para trabajar con enumerados pero como hemos visto en los ejemplos del principio en C# se pueden utilizar para muchos usos.

Por otro lado **no debemos abusar de su uso y siempre debemos plantearnos si tienen sentido o no**. Como con todo, hay que tener cuidado al usarlos ya que **tenemos que tener en cuenta el abuso de la reflexión, además del abuso decoradores que dificultan la legibilidad del código**.

La otra cosa a tener en cuenta es que **los parámetros de los atributos solo pueden ser valores constantes y en muchos casos nos puede limitar**.

## Referencia

* [Código de ejemplo](https://github.com/maktub82/Samples/tree/master/Attributes)
* [AttributeTargets Enumeration](
https://msdn.microsoft.com/en-us/library/system.attributetargets.aspx)

Free Vector Graphics by [vecteezy.com](https://www.vecteezy.com/vector-art/139604-pen-holder-desk-vector)

Un saludo y... ¡Nos vemos en el futuro!
