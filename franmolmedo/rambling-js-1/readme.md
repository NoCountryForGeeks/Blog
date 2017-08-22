![Header](images/js.png)

# Rambling Javascript #1: Let y const

La idea con esta serie de entradas es ir comentando las nuevas características que se han ido introduciendo en el lenguaje. A partir de ES6 (o EcmaScript 2015) se han introducido importantes cambios de sintaxis o añadidos que pueden ser muy útiles a la hora de generar un código más estructurado, entendible y fácilmente mantenible.

## Preparativos
No queremos entrar a comentar la historia del lenguaje y como ha ido evolucionando a lo largo del tiempo (al final dejaré una serie de enlaces donde lo detallan perfectamente), pero sí es conveniente señalar el punto donde nos encontramos y cómo se plantea el futuro. Actualmente la versión del lenguaje soportada por la gran mayoría de navegadores es ES5 (que data del año 2009). En los últimos tiempos se ha abandonado la idea de crear grandes revisiones e ir actualizando el lenguaje paulativamente con nuevos añadidos. De ahí que hayan surgido ES2015, ES2016 o incluso ES2017. Esto permite que el lenguaje vaya avanzando de forma más dinámica y que los cambios lleguen antes a los desarrolladores.
La contrapartida a ello es el soporte que los navegadores dan a todos estos cambios. Por ello, con el fin de asegurar que tu código pueda ejecutarse de forma adecuada en la totalidad de entornos, se hace necesario un proceso previo de transpilación, que consiste en convertir el código escrito en ES2015 o posteriores, a código ES5. Esto se puede llevar a cabo utilizando alguna de las herramientas destinadas a esta tarea, siendo la más famosa de todas ellas [Babel](https://babeljs.io/).
De hecho, recomiendo utilizar la [herramienta](https://babeljs.io/repl/) de edición en vivo que proporcionan, donde se nos permite elegir la versión del lenguaje que vamos a utilizar para escribir el código (entre es2015, es2016, es2017, preset de react e incluso código con las últimas novedades: stage-0 en adelante), para poder ejecutar los ejemplos que iremos añadiendo tanto en ésta como en sucesivas entradas. Además de ver el resultado de ejecución del mismo, esta herramienta nos muestra el resultado de convertir dicho código a ES5, que sería realmente el que ejecutaría nuestro navegador.

Entre las caracterísitcas que se han incluído en EcmaScript2015 podemos destacar la forma de declarar nuestras variables y constantes, la existencia de clases, módulos, las fat arrow functions, los generadores, el uso de template strings... Iremos comentado muchas de estas nuevas características en detalle. Para esta primera entrada nos centraremos en la nueva forma de declarar variables y constantes

## Let y Const
Son dos nuevas palabras reservadas que se introducen en es2015 y que complementan a var, que ya se usaba en es5. Nos proporcionan una idea del carácter inmutable o no (aunque lo vamos a matizar posteriormente) del valor al que referencian. Importante el hecho de que const exige que el valor se le asigne inmediatamente.

Un aspecto interesante a comentar es que tienen un scope a nivel de bloque, entendiendo por bloque como aquel fragmento de código que encontramos entre dos llaves. Lo vemos
en el siguiente ejemplo:

```javascript
if (true){
  var age = 30;
}

console.log(age);
```

Por consola obtenemos el valor de 30.

Sin embargo, si modificamos el código anterior de la siguiente forma:

```javascript
if (true){
  let age = 30;
}

console.log(age);
```

El resultado sería que la variable age no estaría definida. Esto ocurre por lo que hemos comentado, que las variables definidas utilizando let sólo tienen como scope el bloque
de código en el que se definen.

Esto se pone de manifiesto, igualmente, en el siguiente código:

```javascript
let age = 21;

if (true){
  let age = 30;
  console.log('first: ' + age);
}

console.log('second: ' + age);
```

Donde el valor de la variable age se corresponde al que se asigna en su bloque correspondiente. Así mismo, se enmascara el valor de la declaración externa si la variable interna coincide en nombre.

Vamos a hacer ahora un inciso acerca de la palabra reservada const. Revisemos el siguiente ejemplo:

```javascript
const PERSONS = ['pedro', 'carlos'];
console.log(PERSONS);
PERSONS.push('pablo');
console.log(PERSONS);
```

Este código es perfectamente correcto. ¿Pero cómo, si hemos modificado el valor de PERSONS declarado como const?. La razón es que const crea variables inmutables pero no valores inmutables.
Es decir, en este caso nuestra const PERSONS siempre tiene el mismo valor (apunta al array que le indicamos), pero el array en sí mismo puede cambiar. Otro caso sería que a PERSONS, le asignaramos un array diferente, entonces sí nos produciría un error:

```javascript
const PERSONS = ['fran', 'manuel', 'antonio'];

if (true){
  const PERSONS = ['pedro', 'carlos'];
  console.log(PERSONS);
  PERSONS.push('pablo');
  console.log(PERSONS);
  PERSONS = [];
}

console.log(PERSONS);
```

Entonces, ¿cómo aseguramos que el valor sea inmutable? Podemos recurrir a ```Object.freeze()```, teniendo siempre en cuenta que lo que hace inmutable son las propiedades de su argumento, no los objetos almacenados en dichas propiedades. Lo ilustramos con el siguiente ejemplo:

```javascript
const obj = Object.freeze({ foo: {} });
obj.foo.bar = 10;
console.log(obj);
```

Este código sería perfectamente correcto, mientras que el siguiente:

```javascript
const obj = Object.freeze({ foo: {} });
obj.bar = 3;
console.log(obj);
```

No lo sería, teniendo el siguiente error: `Can't add property bar, object is not extensible`.

### Conclusiones
¿Cuáles de las formas para declarar una variable debemos usar?
* Por defecto tu elección debería ser const, a no ser que tengas la necesidad de reasignarle el valor más adelante.
* En ese último caso, deberás de usar let.
* Nunca deberías usar var. La razón es clara. Si declaramos una variable usando var, podemos hablar de constantes o variables, con lo que perdemos significado e información simplemente por el hecho de usarla.
* Ten en cuenta lo comentado sobre la inmutabilidad, a la hora de modificar o no los valores de tus variables o añadir nuevas propiedades a tus objetos.

## Links
* [A brief history of javascript by auth0](https://auth0.com/blog/a-brief-history-of-javascript/)
* [Javascript language resources](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Language_Resources)
