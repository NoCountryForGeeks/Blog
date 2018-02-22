# Tips para mejorar Bases de Datos SQL Server

![Tips para mejorar Bases de Datos SQL Server](images/header.jpg)

> Available in English [here](http://www.danielmartingonzalez.com/tips-to-improve-sql-server-databases-readme.html)

Hace unos días aprendí una valiosa lección acerca de lo que hay que hacer y no en bases de datos. Unos pequeños *tips* que nos ayudarán a optimizar nuestras bases de datos **SQL Server** y con ello a realizar consultas de manera más rápida.

## Nunca uses palabras reservadas

Hay ciertas palabras que suelen estar reservadas en **SQL Server**. Palabras como `Id` o `Name` pueden usarse como nombre de columnas, pero no está recomendado, la razón, aparte de que por convención no se deben usar, una más práctica es la velocidad a la que hacemos consultas manuales con *joins* de por medio.

Pongamos por ejemplo que queremos hacer un *Join* completo de dos tablas, vamos a hacerlo sencillo y no vamos a recortar columnas a mostrar. Supongamos que existe una relación `1 a N` entre *Table1* y *Table2*

```SQL
SELECT * FROM Table1 JOIN Table2 ON Table1.Table1Id = Table2.Table1Id
```

En el caso de no usar palabras reservadas, el *Join* de tablas quedaría así de simple, puesto que *Table1* tendría su clave primaria *Table1Id*, y *Table2* la suya *Table2Id*.

Si usásemos la palabra reservada `Id` y `Name`, la consulta daría error ya que no sabría si nos referimos al *Id* y *Name* de *Table1* o de *Table2*. Para arreglarlo tendríamos que hacer un **Select** por cada columna que queramos mostrar. Contando con solo tener las columnas `Id` y `Name` en ambas tablas quedaría de la siguiente forma:

```SQL
SELECT Table1.Id, Table1.Name, Table2.Id, Table2.Name
FROM Table1 JOIN Table2 ON Table1.Id = Table2.Table1Id
```

Todo esto incrementa lo que tardamos en redactar las consultas, sobre todo aquellas que queremos hacer de manera rápida para realizar pequeñas comprobaciones. Imaginaos si ambas tablas tienen 15 columnas y queremos mostrar todas ellas, tendríamos que hacer un **SELECT** indicando cada una de ellas por separado.

## Usa varchar en lugar de char

Seguramente existirán bases de datos antiguas donde varchar no podrá usarse, pero lo más seguro es que tengas acceso a este tipo de datos para tus columnas.

Siempre que puedas utiliza **varchar** en lugar de **char**. La razón es el tamaño que ocupan ambas. Mientras que **char** ocupa todo el tamaño que indiquemos de manera fija, **varchar** ocupará solo el tamaño que necesitemos en cada momento.

## Cuidado con la n

Mucha gente suele incluir la **n** en todos los **char**/**varchar** por defecto, muchas veces sin saber su significado o *por que todo el mundo lo hace*.

La diferencia entre **varchar** y **nvarchar**, radica en que **nvarchar** utiliza caracteres *unicode*, esto quiere decir que ocupará **EL DOBLE** que **varchar** pero nos dará compatibilidad con diferentes idiomas y caracteres especiales. Si la base de datos solo va a usarse en una región local y no tenemos intención de expandirlo al resto del mundo, conviene utilizar **varchar** para reducir hasta la mitad el espacio utilizado en todas estas columnas.

## Indica el tamaño de las columnas cuando sea posible

Otro punto donde podemos optimizar es indicando el tamaño que van a tener nuestras columnas. En el caso de **char**/**nchar**, será el tamaño que ocupen siempre, en el caso de **varchar**/**nvarchar**, el tamaño máximo que van a ocupar. Vamos con unos ejemplos:

- Si vamos a guardar **Urls** en nuestra columna, lo propio, dada la especificación de una *Url*, es que el tipo de dato de la columna sea `nvarchar(255)`.
- Si vamos a guardar una **descripción completa**, podemos utilizar `nvarchar(MAX)` para que el usuario escriba todo lo que crea necesario.
- Para los **nombres** lo usual es usar `nvarchar(100)` o `nvarchar(255)`.

## Guarda la relación de enumerados también en Base de Datos

Si vas a tener una columna de tipos, por ejemplo, la moneda utilizada en una tabla de transacciones, no guardes el tipo como `Euros` o `Dolares`. Guarda un **byte** con el valor numérico referenciado a otra tabla donde tengas todas las monedas guardadas. De esta manera consultar la lista de tipos de monedas es muy rápido y tenemos una referencia directa ocupando un mínimo espacio.

## Conclusión

Con estos breves tips mejoraremos el rendimiento de nuestras bases de datos y optimizaremos el espacio para que las consultas sean menos costosas y más rápidas.