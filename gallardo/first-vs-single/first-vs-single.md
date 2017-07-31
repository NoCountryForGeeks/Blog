![Header](images/first-vs-single.jpg)

# First VS Single

¡Hola a todos! Linq se ha convertido en nuestro mejor amigo cuando trabajamos con colecciones de datos. Pese a la comodidad que nos ofrece, Linq no es solo azúcar sintáctico: es mucho más, pero ¿siempre usamos Linq correctamente?

Hoy vamos a comparar `Single` y `First` para ver las diferencias y si los usamos correctamente.

**Nota:** Para hacer la comparativa vamos a utilizar tanto en el método `Single` como en el `First` la sobrecarga que recibe un predicado con una condición para buscar el elemento.

## Principales diferencias

La principal diferencia es que **`Single` lanzará una excepción si existe más de un ítem en la colección que cumpla la condición del predicado**.

Esto ya nos está indicando que **`Single`, por norma general, realizará más iteraciones sobre la colección** porque no le basta con encontrar un ítem que cumpla el predicado, sino que tiene que seguir hasta encontrar otro ítem que cumpla la condición, para lanzar la excepción, o hasta llegar al final de la colección.

**`First` únicamente devuelve el primer ítem que encuentre que cumple con el predicado.** En ese momento dejará de iterar sobre los elementos de la colección.

Por suerte Linq es Open Source y lo tenemos disponible en [GitHub]( https://github.com/dotnet/corefx/tree/master/src/System.Linq/src/System/Linq) para poder ver las *entrañas* y comprender mejor cómo funciona. Vamos a ver la implementación de estos dos métodos.

## First

He extraído parte del código de la familia de métodos de extensión de [`First`]( https://github.com/dotnet/corefx/blob/master/src/System.Linq/src/System/Linq/First.cs) de GitHub para poder analizarlo mejor.


```csharp
foreach (TSource element in source)
{
    if (predicate(element))
    {
        found = true;
        return element;
    }
}
```

La parte del `First` que se encarga de encontrar el primer ítem que cumpla con el predicado no es más que un `foreach` que evalúa el predicado y en caso de cumplirse devuelve el ítem.

Por tanto, en cuando haya encontrado el ítem dejará de iterar sobre la colección y devolverá el resultado. Por otro lado, si no encuentra el ítem lanzará una excepción en el caso de `First` o `default(T)` en el caso de `FirstOrDefault`.

## Single

El código de [`Single`](https://github.com/dotnet/corefx/blob/master/src/System.Linq/src/System/Linq/Single.cs) es un poco más complejo. Para empezar, trabajamos con el `Enumerator` que nos va a permitir navegar por la colección.

```csharp
using (IEnumerator<TSource> e = source.GetEnumerator())
{
    while (e.MoveNext())
    {
        TSource result = e.Current;
        if (predicate(result))
        {
            while (e.MoveNext())
            {
                if (predicate(e.Current))
                {
                    throw Error.MoreThanOneMatch();
                }
            }

            return result;
        }
    }
}
```

**Nota:** El método `e.MoveNext()` avanzará en la colección y devolverá `true` en caso de que haya podido realizarse y `false` en caso contrario, es decir, no quedan ítems en la colección.

Por tanto, lo que tenemos es un *mientras queden ítems en la colección* que evalúa el predicado a ver si se cumple. En el caso de que se cumpla entramos en otro *mientras queden ítems en la colección* que de nuevo evalúa el predicado. En este caso si se cumple lanzamos una excepción, ya que hemos encontrado más de un ítem.

En caso de no haber encontrado otro elemento que cumpla el predicado devolvemos el que ya habíamos encontrado.

## ¿Cuál utilizar?

Como hemos podido comprobar `First`, por normal general, ejecuta menos iteraciones sobre la colección.

En un caso habitual en el que el predicado sólo lo cumpla un ítem, `First` recorrerá la colección hasta que lo encuentre, mientras que `Single` recorrerá toda la colección para asegurar que solo un ítem cumple el predicado.

Es evidente que **si no tenemos necesidad de comprobar que el predicado solo lo cumple un elemento la mejor opción es utilizar `First`** que va a iterar sobre la lista sólo hasta que encuentre el ítem.

## Bonus Track: Find

Existe otro método, disponible para las listas, llamado `Find` que también acepta un predicado. Podemos consultar el [código de `List`]( https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/Generic/List.cs) en Github.

```csharp
public T Find(Predicate<T> match)

...

for (int i = 0; i < _size; i++)
{
    if (match(_items[i]))
    {
        return _items[i];
    }
}
return default(T);
```

Como vemos, **`Find` utiliza un `for` para recorrer la colección, a diferencia de `First` que utilizaba un `foreach`**. Por tanto, podemos concluir que **`Find` es más rápido recorriendo los ítems**, pero no podemos utilizarlo en todo tipo de colecciones.

**Nota:** Además, si `Find` no encuentra ningún ítem que cumpla con el predicado, devolverá `default(T)`, teniendo el mismo comportamiento que `FirstOrDefault`.

## Conclusiones

Hemos comprobado que es mejor utilizar `First` que `Single` salvo que queramos asegurarnos que no existen más ítems que cumplen el predicado. Además, lo hemos comprobado leyendo el código de Linq.

Es interesante tener disponible el código de Linq porque nos ayuda a comprender mejor los diferentes métodos de extensión que nos ofrece para trabajar con las colecciones.

Escribí este post para investigar y sopesar si era verdad que no estaba utilizando `Single` en ocasiones que tendría que haberlo utilizado y creo que la respuesta es sí. En algunos desarrollos tendría que haberlo añadido y, si es el caso, controlar la excepción que lanza para ver cómo actuar al respecto.

## Referencias

* [Código]( https://github.com/dotnet/corefx/tree/master/src/System.Linq/src/System/Linq) de Linq.

* [Código]( https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/Generic/List.cs) de List.

Free Vector Graphics by [Vecteezy.com](https://vecteezy.com)

¡Nos vemos en el futuro!
