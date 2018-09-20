# Destructuring en Javascript

> "Todo es muy difícil antes de ser sencillo" Thomas Fuller

Hay una nueva programación que viene de camino. Una programación que da prioridad a una lectura fácil sacrificando un poco de dificultad a la hora de escribir el código. Ya hemos hablado de ello en posts como [La energía del código](https://geeks.ms/windowsplatform/2017/05/10/la-energia-del-codigo/) y el __destructuring__ de Javascript es una de las nuevas características de ES6 que sigue esa tendencia. Recuerda mucho al [Pattern matching de Haskell](http://learnyouahaskell.com/syntax-in-functions); pero programación funcional o no, lo que está claro es que esta nueva opción del lenguaje nos permite escribir código mucho más limpio y claro, y es nuestra obligación hacer uso del _destructuring_ siempre que nos permita dejar un código con más energía.

## Definición

_Destructuring_, o destructuración en un lenguaje más hispanizado, es una nueva característica de ES6 para Javascript que nos da la posibilidad de poder coger los datos de objetos o _arrays_ directamente y de manera múltiple, para extraerlos a variables o constantes.

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

Después de haber visto la sintaxis del _destructuring_ seguro que os queda la duda de: ¿dónde podría aplicar yo esto para que mi código fuese más limpio y legible? La destructuración se puede hacer en muchos sitios, pero los más beneficiosos suelen ser:

* Retornos de funciones
* Parámetros en las funciones
* Funciones de trabajo con _arrays_
* Destructuring múltiple
* Importación de objetos
* Destructuring en React

### Retornos de funciones.

Cuando recuperamos datos de una función solemos recuperar un objeto con propiedades que nos devuelve el resultado de la misma. Para tener que interaccionar con sus propiedades tenemos que ir hasta ellas desde el objeto. Algo que podemos evitar si usamos adecuadamente el _destructuring_.

```javascript

function getPerson() {
    return {
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

Muchas veces no usamos todos los parámetros de nuestras funciones y solo necesitamos un par de propiedades. En estos casos _destructuring_ también nos puede ayudar a dejar un código más claro. Pongamos una función _filter_ para poder filtrar las personas que tenemos en un _array_. Esta función _filter_ recibe un objeto con las opciones de filtrado de nombre y edad. 

```javascript

const people = [
    {
        name: 'Pepe',
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    },
    {
        name: 'Juan',
        age: 32,
        hobbies: [ 'basket' ]
    },
    {
        name: 'Paco',
        age: 45,
        hobbies: ['running']
    }
]

function getPeople(filter) {
    return people.map(person => (nameFilter || personName === name) && (personAge || personAge > minAge));
}

const peopleBiggerThan40 = getPeople({ age: 40 });

console.log(peopleBiggerThan40); 
// result => [
//    {
//        name: 'Paco',
//        age: 45,
//        hobbies: ['running']
//    }
// ]

```

Esta función queda más limpia aplicando el _destructuring_ en los parámetros de la misma:

```javascript

const people = [
    {
        name: 'Pepe',
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    },
    {
        name: 'Juan',
        age: 32,
        hobbies: [ 'basket' ]
    },
    {
        name: 'Paco',
        age: 45,
        hobbies: ['running']
    }
]

function getPeople({ name, minAge }) {
    return people.map(person => (nameFilter || personName === name) && (personAge || personAge > minAge));
}

const peopleBiggerThan40 = getPeople({ age: 40 });

console.log(peopleBiggerThan40); 
// result => [
//    {
//        name: 'Paco',
//        age: 45,
//        hobbies: ['running']
//    }
// ]

```

También podemos añadir a los parámetros destructurados valores por defecto si nos interesase:

```javascript

const people = [
    {
        name: 'Pepe',
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    },
    {
        name: 'Juan',
        age: 32,
        hobbies: [ 'basket' ]
    },
    {
        name: 'Paco',
        age: 45,
        hobbies: ['running']
    }
]

function getPeople({ name, minAge = 30 }) {
    return people.map(person => (nameFilter || personName === name) && (personAge || personAge > minAge));
}

const peopleBiggerThan40 = getPeople({});

console.log(peopleBiggerThan40); 
// result => [
//    {
//        name: 'Juan',
//        age: 32,
//        hobbies: [ 'basket' ]
//    },
//    {
//        name: 'Paco',
//        age: 45,
//        hobbies: ['running']
//    }
// ]

```

### Funciones de trabajo con _arrays_

También es interesante usar _destructuring_ en las funciones para trabajar con _arrays_. Podéis verlas [en nuestro post de arrays](http://www.nocountryforgeeks.com/rambling-javascript-3-arrays/). En estas funciones la combinación de _arrows functions_ con el _destructuring_ suele generar un código muy legible:

Por ejemplo la función anterior podría quedar reducida a:

```javascript

const people = [
    {
        name: 'Pepe',
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    },
    {
        name: 'Juan',
        age: 32,
        hobbies: [ 'basket' ]
    },
    {
        name: 'Paco',
        age: 45,
        hobbies: ['running']
    }
]

function getPeople({ name, minAge }) {
    return people.map({ name: personName, age: personAge } => (nameFilter || personName === name) && (personAge || personAge > minAge));
}

const peopleBiggerThan40 = getPeople({ age: 40 });

console.log(peopleBiggerThan40); 
// result => [
//    {
//        name: 'Paco',
//        age: 45,
//        hobbies: ['running']
//    }
// ]

```

Hemos destructurado el objeto _person_ dentro de la _arrow function_ del _map_. Así todo queda mucho más conciso y claro y no hace falta usar _person._ para poder acceder a las propiedades de _person_.

### Destructuring múltiple

Podemos llevar el _destructuring_ de objetos hasta su máxima expresión, es decir, podemos hacer _destructuring_ dentro de nuestro propio _destructuring_ hecho.

```javascript

const people = [
    {
        names: {
            name: 'Pepe',
            surname: 'Gonzalez'
        },
        age: 26,
        hobbies: ['chess', 'running', 'basket']
    },
    {
        names: {
            name: 'Pepe',
            surname: 'Gonzalez'
        },
        age: 32,
        hobbies: [ 'basket' ]
    },
    {
        names: {
            name: 'Pepe',
            surname: 'Gonzalez'
        },
        age: 45,
        hobbies: ['running']
    }
]

function getPeople({ nameFilter, minAge }) {
    return people.map({ ({ name: personName }), age: personAge } => (nameFilter || personName === name) && (personAge || personAge > minAge));
}

const peopleBiggerThan40 = getPeople({ age: 40 });

console.log(peopleBiggerThan40); 
// result => [
//    {
//        name: 'Paco',
//        age: 45,
//        hobbies: ['running']
//    }
// ]

```

Esta vez el parámetro _name_ ha sido cogido por una doble destructuración. Podemos tener todas las _destructuring_ que tengamos siempre que no lo mezclemos con el _destructuring de arrays_, ya que no serían compatibles uno con otro.

En cambio, si realizamos el _destructuring_ solo de _arrays_ también lo podemos realizar de manera múltiple:

```javascript

const [first, [[second], third]] = ['apple', [['banana'], 'orange']];

console.log(first);
// apple
console.log(second);
// banana
console.log(third);
// orange

```

Este _array_ de _arrays_ ha sido destructurado. Si lo analizamos el elemento _'apple'_ ha sido destructurado de una manera simple. En cambio _'banana'_ está dentro de un _array_ que a la vez está dentro de un _array_ y hemos podido hacer el _destructuring_ múltiple sin ningún problema. _'orange'_ finalmente es un elemento dentro de un _array_ por lo que su nivel de destructuración ha sido de 2.

### Destructuring en React

_Destructuring_ es una técnica usada mucho en los componentes React ya que les da mucha más legibilidad. Las _props_ suelen ser un blanco fácil para poder realizar el _destructuring_ siempre que se use más de una.

Si comparados dos códigos con o sin _destructuring_ podemos ver la diferencia entre ellos.

Sin _destructuring_:

```

import React from 'react';

import { SearchPanel } from './MasterPage/SearchPanel.js';
import { SearchResult } from './MasterPage/SearchResults.js';

import { root } from './masterPage.scss';

const MasterPage = (props) => (
	<main className={root}>
		<SearchPanel search={props.search} />
		<SearchResult results={props.results} isLoading={props.isLoading} />
	</main>
);

export { MasterPage };

```

Con _destructuring_:

```

import React from 'react';

import { SearchPanel } from './MasterPage/SearchPanel.js';
import { SearchResult } from './MasterPage/SearchResults.js';

import { root } from './masterPage.scss';

const MasterPage = ({ search, results, isLoading }) => (
	<main className={root}>
		<SearchPanel search={search} />
		<SearchResult results={results} isLoading={isLoading} />
	</main>
);

export { MasterPage };

```

## Conclusiones

_Destructuring_ es una nueva característica muy potente del lenguaje que nos permite escribir código más legible, aunque como todo no hay que abusar de ella. Esto han sido pequeños ejemplos, pero seguramente si lo que vas a destructurar solamente tiene una propiedad, la mejor opción no sea usarlo. _Destructuring_ coge mucha fuerza sobre todo cuando se usan varias propiedades de un mismo objeto que se pueden destructurar, ya que al hacer el desglose de las propiedades todo suele quedar más limpio y claro.