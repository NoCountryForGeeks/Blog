
![Header](images/header.jpg)

# Templates String in Javascript

"Primero resuelve el problema. Entonces, escribe el código" John Johnson

En el lejano oeste de _Javascript_, uno de los ámbitos abiertos es como manejar las cadenas de texto. Aun hoy en día los navegadores son capaces de lidiar con las dobles comillas en los _strings_ mientras ya existen muchos _developers_ que no pueden con un fenómeno tal, que acaban cambiando de proyecto. Bromas aparte, ___ES6_ ha mejorado mucho el tratamiento en las _Templates con Strings___. Ahora ya podemos tener un código mucho más legible gracias a las nuevas características que nos ofrecen poniendo un poquito de empeño de nuestra parte.

Para ello en el post trataremos los siguientes puntos:

* String interpolation
* Expresiones embebidas
* Cadenas con Multilíneas
* Postprocesado de Templates

## String interpolation

Para las cadenas fijas, cuyo contenido no sea dinámico se seguirá utilizando las comillas simples para las cadenas; pero la novedad de ES6 es que para los _strings_ cuyo contenido queramos interpolar cualquier variable las podemos encerrar en comillas invertidas `. Para poner variables dentro del string tendremos que usar la sintaxis ```${variable}``` y con ello podemos conseguir estos resultados:

```javascript

const name = 'Pepe'
const greetings = `Buenos días ${name}`
console.log(greetings)

// Buenos días Pepe.

```

Podemos poner tantas variables como queramos dentro de nuestra cadena.

```javascript

const name = 'Pepe'
const date = new Date().toLocaleString()
const greetings = `Buenos días ${name}. Hoy ${date} queremos desearle que pase un buen día.`
console.log(greetings)

// Buenos días Pepe. Hoy 2/2/2018 12:48:04 queremos desearle que pase un buen día.

```

## Expresiones embebidas

Si vamos un paso más allá del apartado anterior, __ES6 no solo nos permite interpolar variables, sino que dentro de los corchetes del dolar podemos poner expresiones.__ Por ejemplo la suma de dos números, la concatenación de varios _strings_ o trabajar con _arrays_:

```javascript
const name = 'Pepe'
const yearBirth = 1992
const date = new Date().toLocaleString()
const greetings = `Buenos días ${name}. Tu edad es ${new Date().getFullYear() - yearBirth} años. Hoy ${date} queremos desearle que pase un buen día.`
console.log(greetings)

// Buenos días Pepe. Tu edad es 26 años. Hoy 2/2/2018 12:48:04 queremos desearle que pase un buen día.

```

Y lo que viene como anillo al dedo es su uso con la función ```join``` de un _array_:

```javascript
const name = 'Pepe'
const yearBirth = 1992
const date = new Date().toLocaleString()
const hobbies = ['chess', 'running', 'basket']
const greetings = `Buenos días ${name}. Tu edad es ${new Date().getFullYear() - yearBirth} años. Hoy ${date} queremos desearle que pase un buen día. Tus hobbies son: ${hobbies.join(', ')}`
console.log(greetings)

// Buenos días Pepe. Tu edad es 26 años. Hoy 2/2/2018 12:59:56 queremos desearle que pase un buen día. Tus hobbies son: chess, running, basket

```

Por supuesto que la potencia de estas templates permiten usar operadores como el ternario (``` a ? a : b```), ejecutar funciones en su interior o operadores lógicos que vienen muy bien para los valores _undefined_:

```javascript
const name = 'Pepe'
const greetings = `Buenos días ${name.toUpperCase()}.`
console.log(greetings)

// Buenos días PEPE.
```

```javascript

const yearBirth = undefined
const date = new Date().toLocaleString()
const ageStr = `Tu edad es ${yearBirth ? new Date().getFullYear() - yearBirth : 'desconocida'}.`
console.log(ageStr)

// Tu edad es desconocida

```

```javascript
const age = undefined
const ageStr = `Tu edad es ${age || 'desconocida'}.`
console.log(ageStr)

// Tu edad es desconocida
```

## Multilínea

Otra ventaja de usar las comillas investidas es que para usar multilínea solo hay que ponerla tal cual. Fácil y práctico.

```javascript
const name = 'Pepe'
const yearBirth = 1992
const date = new Date().toLocaleString()
const hobbies = ['chess', 'running', 'basket']
const greetings = `Buenos días ${name}. 
Tu edad es ${new Date().getFullYear() - yearBirth} años. 
Hoy ${date} queremos desearle que pase un buen día. 
Tus hobbies son: ${hobbies.join(', ')}`
console.log(greetings)

// Result:
Buenos días Pepe. 
Tu edad es 26 años. 
Hoy 2/2/2018 12:59:56 queremos desearle que pase un buen día. 
Tus hobbies son: chess, running, basket

```

## Postprocesado de templates

Esta es quizá la característica más desconocida de las nuevas. Podemos postprocesar un template en funciones. ¿Que quiere decir esto? Lo mejor es un ejemplo:

```javascript

const greetings = name => `Buenos días ${name}`
console.log(greetings`Pepe`)

// Buenos días Pepe.

```

Si os fijáis para poder procesar la función ```greetings``` no ha sido necesario usar paréntesis, sino que poniendo directamente el _template_ ha sido suficiente. Esto es interesante para poder generar html:

```javascript

function generateTemplate (strings, ...keys ) {
    return function(data) {
        let result = strings.slice();

        keys.forEach((key, i) => {
            result[i] = `${result[i]}${data[key]}`
        })

        return result.join( '' )
    }
};

var person = {
    name: 'Pepe',
    age: 26
};

var personTemplate = generateHtml`<div>
    <h1>${'name'}</h1>
    <p>Tu edad es ${'age'}</p>
</div>`

console.log(personTemplate(person));

// <div>
//    <h1>Pepe</h1>
//    <p>Tu edad es 26</p>
// </div>

```

El código tanto de ```person``` como de ```personTemplate``` es muy claro y solo con verlo intuyes lo que hace. La magia está en el generateTemplate. Esta función recibe el array ```strings``` dividido justo por las keys, y luego las propias keys, que operando con ellas conseguimos unirlo todo. Fijaos en como traduce todo el código ES6:

```javascript

'use strict';

var _templateObject = _taggedTemplateLiteral(['<div>\n    <h1>', '</h1>\n    <p>Tu edad es ', '</p>\n</div>'], ['<div>\n    <h1>', '</h1>\n    <p>Tu edad es ', '</p>\n</div>']);

function _taggedTemplateLiteral(strings, raw) { return Object.freeze(Object.defineProperties(strings, { raw: { value: Object.freeze(raw) } })); }

var generateHtml = function generateHtml(strings) {
    for (var _len = arguments.length, keys = Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
        keys[_key - 1] = arguments[_key];
    }

    return function (data) {
        var result = strings.slice();

        keys.map(function (key, i) {
            result[i] = '' + result[i] + data[key];
        });

        return result.join('');
    };
};

var person = {
    name: 'Pepe',
    age: 26
};

var personTemplate = generateHtml(_templateObject, 'name', 'age');

console.log(personTemplate(person));

```

En la traducción se crea el array ```_templateObject``` con los valores cortados justo por las keys. Luego tenemos accesibles las keys y lo juntamos todo para encajarlo y obtener nuestro div. 

## Conclusiones

El código con las nuevas características de __ES6 siempre queda más legible__ que anteriormente; pero esto siempre tiene un coste. En este caso, que __el código es más difícil de escribir al principio__ ya que nos tenemos que adaptar y aprender estas características, pero una vez las interioricemos y las usemos en el día a día seguro que conseguimos este código legible cada vez de manera más natural.
