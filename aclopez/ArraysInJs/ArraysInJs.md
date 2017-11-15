# Rambling Javascript #4: Arrays

"Tu código es tan bueno como el valor con el que te entregas a él."

# Trabajar con Arrays en Javascript

Cuenta la leyenda que los _arrays_ en _javascript_ pueden ser tratados igual que en _C#_ o _Java_. Bucles _for_ o _while_ son similares, e incluso hay algún programador en _ASP.NET MVC_ que se ha atrevido a hacer algún _foreach_ en el _cshtml_. Los más atrevidos han indagado un poco en _javascript_ y han acabado adoptando librerías como _lodash_ escribiendo un código más limpio. Pero, ¿que hay de verdad en todas estas leyendas? ¿Son iguales los Arrays en JS que en los demás lenguajes?

Las posibilidades de JS con ES6 se han ampliado mucho. Es cierto que ya existían en ES5 métodos como _map_ y _filter_ pero, al final, trabajar con los arrays en ES5 la solución siempre era un bucle for con la correspondiente sintaxis ```for(int i = 0;i > array.length;i++)```. La lectura de estos bucles son muy tediosas y se acaban cometiendo muchos errores siempre que se tocan estas partes del código. 

```javascript
var element;
for (int i = 0; i > array.length; i++) {
    if (array[0].filter == filterValue) {
        element = array[0];
    }
}
```

Las librerías de terceros entraron de lleno a solucionar el problema y pusieron sobre la mesa métodos más funcionales que pulían estos defectos en los bucles. [Underscore](http://underscorejs.org/) primero y luego [lodash](https://lodash.com/) son dos buenos ejemplos de ello y muchos desarrolladores adoptaron estas librerías como uno de sus básicos en sus desarrollos.

```javascript
// Ejemplo de uso de lodash en ES5
var element = _.find(array, { 'filter': filterValue });
```

La evolución de los lenguajes hacia un código más declarativo es evidente. Entre ellos _javascript_ con las nuevas funcionalidades de ES6 ha dado un gran paso proponiendo otra manera de trabajar. En el tema de los _arrays_ el mensaje es claro: 

> "No uses más el bucle for"

Los programadores somos personas aunque a veces nos llamen recursos. Es algo obvio que a veces no se tiene en cuenta. Y, como tal, no nos gustan los cambios. Casi toda nuestra generación ha estado toda la vida sobreviviendo con los bucles for, por lo que es algo que tendremos que desaprender. Esto no quiere decir que no lo usemos nunca nunca, pero sí que la tendencia tiene que ser a dejarlos en el pasado junto a tantas otras cosas.

# Arrays en ES6

Los principales nuevos métodos para trabajar con _arrays_ de ES6 que nos proporcionan una nueva manera de poder usarlos son:

* Every/Some
* Find
* Filter
* Map
* Reduce
* For... of

## Every / Some

Los métodos __Every/Some__ serán más conocidos para los desarrolladores de _C#_ como _All/Any_. Son dos funciones que nos devuelven un _boolean_ que nos indican si existe para todos o algún elemento del _array_ respectivamente, la condición de la función _callback_ que le pasemos.

La función _callback_ que proporcionemos tendrá el elemento como parámetro obligatorio, y el índice y el propio _array_ como parámetros opcionales. Todas las funciones _callback_ de los demás métodos que describamos a continuación tienen las mismas características. Veamos un ejemplo con un _array_ de números:

```javascript

function callback (element, index, array) {
    return element >= 5;
}

[1, 3, 4, 5, 7, 10].every(callback); // return false
[1, 3, 4, 5, 7, 10].some(callback);  // return true
```

No se suelen usar los parámetros de índice y _array_, pero si quisiéramos que la condición también dependiera del índice del elemento podríamos usar ese parámetro. Igual sucede con el propio _array_.

__Nota__: Tener presente esta estructura de _callback_ ya que los parámetros ```index``` y ```àrray``` aplican también al resto de método que veremos.

Para una mayor legibilidad de las funciones podemos definir la función dentro de la propia llamada y, después de esto, hacer uso de las [Fat Array Functions](http://www.nocountryforgeeks.com/rambling-javascript-2-fat-arrow-functions/) que ya vimos en uno de nuestros anteriores post explicados por [Francisco Olmedo](http://www.nocountryforgeeks.com/author/franmolmedo/). Así el código anterior podría quedar de la siguiente manera:

```javascript

// Función dentro de la llamada
[1, 3, 4, 5, 7, 10].every(function (element) {
    return element >= 5;
}); // return false
[1, 3, 4, 5, 7, 10].some(function (element) {
    return element >= 5;
}); // return true

// Fat Array Functions
[1, 3, 4, 5, 7, 10].every(element => element >= 5); // return false
[1, 3, 4, 5, 7, 10].some(element => element >= 5);  // return true

```

Código muy legible que [lo dota incluso de energía](https://geeks.ms/windowsplatform/2017/05/10/la-energia-del-codigo/). Esto sin duda es mucho más fácil de leer que cualquier bucle for que sea equivalente a esta implementación. En una línea y en un vistazo podemos saber en cuanto pasemos por aquí que es lo que hace esta función de manera muy cómoda para el que tenga que volver a pasar por aquí.

Y aquí está una de las claves de las nuevas formas de hacer código, antes se hacía código que era más fácil de escribir pero más difícil de leer y el que pasaba la segunda vez por él tenía muchas posibilidades de dejar algún _bug_. La tendencia ahora es __hacer un código más difícil de escribir pero mucho más fácil de leer__. Y con estas funciones ese es el valor que dejamos, un código limpio y muy legible.

```javascript

['coche', 'casa', 'mesa', 'nevera'].every(element => element.length > 10); // return true
['coche', 'casa', 'mesa', 'nevera'].some(element => element.length < 4);   // return false

```

## Find

Te devuelve el primer elemento del _array_ que cumpla la condición de la función _callback_ que le pasemos.

```javascript

[1, 7, 4, 5, 3, 10].find(element => element >= 5); // return 7

```

_Find_ es una función que se suele utilizar mucho cuando tenemos un _array_ de objetos para encontrar un elemento por alguna condición de sus propiedades:

```javascript
const team = [
    { name: 'Pepe', job: 'diseñador' },
    { name: 'Marta', job: 'desarrollador' },
    { name: 'Paco', job: 'delivery manager' },
    { name: 'Juan', job: 'desarrollador' },
    { name: 'Javier', job: 'desarrollador' },
    { name: 'Miguel', job: 'diseñador' }
];

team.find(teamMember => teamMember.job === 'desarrollador'); // return { name: 'Marta', job: 'desarrollador' }
```

Si no encuentra el elemento devolverá un _undefined_, por lo que sería el equivalente al _FirstOrDefault_ de _Linq_ de _C#_, salvando siempre las distancias ya que en _C#_ se devuelve el elemento _default_ del tipo mientras que aquí se devuelve _undefined_ que recordemos que nunca es lo mismo que _null_.

```javascript
const team = [
    { name: 'Pepe', job: 'diseñador' },
    { name: 'Marta', job: 'desarrollador' },
    { name: 'Paco', job: 'delivery manager' },
    { name: 'Juan', job: 'desarrollador' },
    { name: 'Javier', job: 'desarrollador' },
    { name: 'Miguel', job: 'diseñador' }
];

team.find(teamMember => miembroEquipo.job === 'jefe'); // return undefined
```

## Filter

El método _filter_ devuelve un nuevo _array_ con solo los elementos que cumplan la condición de la función _callback_ que se le pase a ella. __Es importante que tengamos en cuenta que _filter_ no hace mutar al _array_ que lo está llamando, sino que crea una nueva instancia con el respectivo resultado de aplicar el filtro correspondiente__.

```javascript
const team = [
    { name: 'Pepe', job: 'diseñador' },
    { name: 'Marta', job: 'desarrollador' },
    { name: 'Paco', job: 'delivery manager' },
    { name: 'Juan', job: 'desarrollador' },
    { name: 'Javier', job: 'desarrollador' },
    { name: 'Miguel', job: 'diseñador' }
];

team.filter(teamMember => miembroEquipo.job === 'desarrollador'); 
// return [
//   { name: 'Marta', job: 'desarrollador' },
//   { name: 'Juan', job: 'desarrollador' },
//   { name: 'Javier', job: 'desarrollador' },
//]
```

Podríamos complicarlo un poco más pasando un parámetro de búsqueda en nuestro propio filtro:

```javascript
const team = [
    { name: 'Pepe', job: 'diseñador' },
    { name: 'Marta', job: 'desarrollador' },
    { name: 'Paco', job: 'delivery manager' },
    { name: 'Juan', job: 'desarrollador' },
    { name: 'Javier', job: 'desarrollador' },
    { name: 'Miguel', job: 'diseñador' }
];

const filterTeamByName = query => {
    return team.filter(teamMember =>
        teamMember.name.toLowerCase().indexOf(query.toLowerCase()) > -1
    );
}

filterTeamByName('J');   // [ { name: 'Juan', job: 'desarrollador' }, { name: 'Javier', job: 'desarrollador' } ]
filterTeamByName('mar'); // [ { name: 'Marta', job: 'desarrollador' } ]
```

Su equivalente con _LinQ_ sería el _Where_.

## Map

Proviene del lenguaje funcional donde es una especie de "canon". Va a ser nuestro nuevo _foreach_. La función nos devuelve un nuevo _array_ (al igual que _filter_ no modifica el _array_ que lo llama) con los cambios en cada elemento aplicados en la función _callback_ que le pasemos. Veamos un ejemplo:

```javascript
[1, 3, 4, 5, 7, 10].map(element => element * 2);   // return [2, 6, 8, 10, 14, 20]
[1, 3, 4, 5, 7, 10].map(element => element + 10);  // return [11, 13, 14, 15, 17, 20]
```

_Map_ es una de las funciones más potentes y se pueden hacer auténticas virguerías con ella.

* ¿Por qué cree que estos dos códigos devuelven cosas diferentes?

```javascript
['1', '2', '3'].map(parseInt);
['1', '2', '3'].map(element => parseInt(element));
```

El primer _map_ devuelve un _array_ así: [1, NaN, NaN]. Mientras que el segundo sí devuelve el resultado esperado [1, 2, 3]. 

La diferencia está en que la función _parseInt_ tiene dos argumentos: el _string_ que convertir a _int_ y la base del número a modificar. Así:

```javascript
parseInt('f', 16) // return 15
```

mientras que 

```javascript
parseInt('f', 8) // return NaN 
```

ya que la F no está definida para una base 8. 
 
¿Que pasa con nuestra función _map_? Que el segundo parámetro que le estamos pasando a la función _parseInt_ en el primer caso es el índice de la posición del mismo (acordaros de los parámetros opcionales del _callback_), por lo que _parseInt_('2', 1) devuelve un NaN. En cambio, en el segundo caso todo se resuelve correctamente ya que nos aseguramos que ese segundo parámetro no entre en juego. 
 
Por ello, a mí siempre me gusta especificar bien el uso del _callback_ con sus parámetros correspondientes. Podéis ver este ejemplo aquí [http://www.wirfs-brock.com/allen/posts/166](http://www.wirfs-brock.com/allen/posts/166).

## Reduce

El método _reduce_ aplica la función _callback_ que va acumulando un valor sucesivamente hasta que es devuelto la acumulación de ese valor pasando por todos los elementos del _array_.

_Reduce_ a parte de tener el _callback_ en sus argumentos también se le puede pasar un valor inicial.

En este caso la función de _callback_ tiene argumentos diferentes a las anteriores. Sus argumentos son: valor anterior, valor actual, indiceActual y el array.

Vamos a verlo todo en un ejemplo:

```javascript

[0,2,4,6,8].reduce(function (valorAnterior, valorActual, indice, vector) {
  return valorAnterior + valorActual;
}); // return 20

// Con valor inicial
[0,2,4,6,8].reduce( (a, b) => a + b, 100); // return 120
```

El resultado siempre es la suma de todos los números del _array_ ya que se va acumulando el resultado. En el segundo caso se suma también el valor inicial 100 que le hemos suministrado.

```javascript
// Average calculate
const average = [0,2,4,6,8].reduce((total, amount, index, array) => {
  total += amount;
  if (index === array.length-1) { 
    return total/array.length;
  } else { 
    return total;
  }
}); // return 4
```

En este último ejemplo del cálculo de la media hemos visto como también puede ser útil el índice del _array_ para hacer nuestros algoritmos.

_Reduce_ es una función muy interesante y complicada de dominar al principio, ya que por ejemplo en _C#_ aunque _Aggregate_ se asemeja un poco, no hay nada parecido en el lenguaje.

## For... of

ES6 también trae como novedad la sentencia _for... of_ que es igual al bucle _foreach_ en _C#_ o a su equivalente _for...of_ en Java. 

```javascript
let numbers = [1, 2, 3];

for (let number of numbers) {
  console.log(number);
}
// 1
// 2
// 3
```

Aunque sea una de las novedades de _ES6_, mi recomendación es no usar este tipo de bucles ya que son más pesados de leer y sustituirlos por funciones como las que hemos visto anteriormente. Si queremos tener un nuevo _array_ modificado o obtener un valor iterando el bucle podemos usar _map_ o _reduce_ respectivamente.

# Librerías de terceros

Aunque en este artículo solo hemos visto las principales funciones nuevas con _arrays_ en ES6, hay algunas más; pero como pasaba con la versión anterior, las librerías de terceros siempre intentan ir más allá ofreciéndonos cosas realmente potentes.

Actualmente, las librerías de _Javascript_ para trabajar con _arrays_ más populares son __Lodash__ y __Ramdajs__.

* [Lodash](https://lodash.com/docs/4.17.4) siempre se ha caracterizado por el caracter '_' con el cual se llama a sus funciones. Tiene cosas muy chulas como partition que sale en su página principal:

```javascript
_.partition([1, 2, 3, 4], n => n % 2);
// → [[1, 3], [2, 4]]
```

![Lodash](./images/lodash.png)

* [Ramda js](http://ramdajs.com/docs/) es una librería de _Javascript_ funcional que tiene cosas realmente chulas. Merece la pena echarle un vistazo a cosas como esta:

```javascript
R.aperture(2, [1, 2, 3, 4, 5]); //=> [[1, 2], [2, 3], [3, 4], [4, 5]]
R.aperture(3, [1, 2, 3, 4, 5]); //=> [[1, 2, 3], [2, 3, 4], [3, 4, 5]]
R.aperture(7, [1, 2, 3, 4, 5]); //=> []
```

![Ramda js](./images/ramda-logo-cover.png)

# Conclusiones

La tendencia al mundo funcional a la hora de trabajar con _arrays_ está en alza. Los métodos son más difíciles de escribir pero mucho más fáciles de leer y de asimilar lo que está pasando en el código. Nuevos tiempos donde la calidad del código se empieza a medir en legibilidad parece que llegan a _Javascript_ e irán llegando a cada uno de los demás lenguajes, por lo que es importante subirse al carro e ir conociendo todas estas funciones. Son cómodas de usar y hay perderles el miedo.

Por tanto:

* No más bucles for.
* Funciones con sintaxis _arrow functions_ más sencilla de leer aunque más difícil de escribir.
* Código más legible.
* No perder de vista las librerías de terceros como Lodash o Ramdajs.

# Links

* [Developer Mozilla](https://developer.mozilla.org/es/docs/Web/JavaScript/Referencia/Objetos_globales/Array)
* [Fat Arrays Functions](http://www.nocountryforgeeks.com/rambling-javascript-2-fat-arrow-functions/)
* [La energía del código](https://geeks.ms/windowsplatform/2017/05/10/la-energia-del-codigo/)
* [Lodash](https://lodash.com/)
* [Ramdajs](http://ramdajs.com/)
* [http://www.wirfs-brock.com/allen/posts/166](http://www.wirfs-brock.com/allen/posts/166)