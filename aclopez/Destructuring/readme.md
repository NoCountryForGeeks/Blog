# Destructuring en Javascript

> "Todo es muy difícil antes de ser sencillo" Thomas Fuller

La programación funcional viene de camino. Ya hemos hablado de ello en post como [La energía del código](https://geeks.ms/windowsplatform/2017/05/10/la-energia-del-codigo/) y el __destructuring__ de Javascript es una de las nuevas características de ES6 que recuerda mucho al [Pattern matching de Haskell](http://learnyouahaskell.com/syntax-in-functions). Programación funcional o no, lo que está claro es que esta nueva opción del lenguaje nos permite escribir código mucho más limpio y claro, y es nuestra obligación hacer uso del _destructuring_ siempre que nos permita dejar un código con más energía.

## Definición

_Destructuring_, o destructuración en un lenguaje más hispanizado, es una nueva expresión de ES6 para Javascript que nos da la posibilidad de poder coger los datos de objetos o _arrays_ directamente y de manera múltiple, extrayéndolos directamente de los mismos.

## Sintaxis

La sintaxis del _destructuring_ es muy sencilla. Por un lado tenemos el objeto que queremos destructurar. Para extraer sus propiedades usamos las __{ }__, metiendo dentro de ellas sus respectivos nombres y con esto tenemos nuevas variables que contienen estas propiedades:

```javascript

const person = {
    name: 'Pepe',
    age: 26,
    hobbies: ['chess', 'running', 'basket']
}

const { name, age, hobbies } = person;

console.log(name); // result => Pepe

```

Si queremos poner nombres específicos para estas nuevas variables bastará con poner __:__ seguido del nuevo nombre de variable que queramos asignar en las propiedades destructurada:

```javascript

const person = {
    name: 'Pepe',
    age: 26,
    hobbies: ['chess', 'running', 'basket']
}

const { name: personName, age: personAge, hobbies } = person;

console.log(name); // result => undefined
console.log(personName); // result => Pepe


```

Y de igual manera podemos destructurar los arrays:

```javascript

const person = {
    name: 'Pepe',
    age: 26,
    hobbies: ['chess', 'running', 'basket']
}

const { name: personName, age: personAge, hobbies } = person;

const [ firstHobbie, secondHobbie, thirdHobbie ] = hobbies;

console.log(secondHobbie); // result => running

```

## Uso Práctico

Después de a ver visto la sintaxis del _destructuring_ seguro que os queda la duda de: ¿donde podría aplicar yo esto para que mi código fuese más limpio y legible? La destructuración se puede hacer en muchos sitios, pero los más beneficiosos suelen ser:

* Retornos de funciones
* Parámetros en las funciones
* Funciones de trabajo con _arrays_
* Destructuring múltiple
* Destructuring en React

### Retornos de funciones.

Cuando recuperamos datos de una función solemos recuperar un objeto con propiedades que nos devuelve el resultado de la misma. Para tener que interaccionar con sus propiedades tenemos que ir hasta ellas desde el objeto. Algo que podemos evitar si usamos adecuadamente el _destructuring_.

```javascript

function getPerson() {
    return const person = {
        name: 'Pepe',
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    }
}

const { name } = getPerson();

console.log(name); // result => Pepe.

```

Como se puede ver podemos cazar al vuelo las propiedades devueltas por una función. Esto hace que el código sea mucho más limpio y que podamos atacar a las propiedades que de verdad nos interesan de los objetos que nos devuelven las funciones. Como podéis observar no he destructurado ni ```age``` ni ```hobbies``` ya que son propiedades con las que no voy a trabajar.

### Parámetros en las funciones