En **React** para reutilizar o compartir lógica entre componentes podemos usar patrones como puede ser **HOC (High Order Component)** o **Render props**.

# ¿Que es un HOC?

Un **HOC (High Order Component)** o traducido **Componente de Order Superior** es un patrón que nos es parte del **API** de **React**. Este patrón consiste en una **función** que recibe un **componente** y retorna un nuevo **componente** transformado.

Este patrón lo usan librerías como **Redux** con el **connect()** o **Apollo** con **graphql()**

# ¿Que es Render Props?

Este patrón consiste en un **componente** que recibe una **función render** la cual será el **render** que ejecutará el **componente**.

Este patrón lo usan librerías como **react-router** o **react-motion**.

# Desventajas de usar HOC

Los **HOC** sustituyen a los **mixins** que podemos pasarle a un componente **React** cuando lo hacemos con **React.ceateClass** pero con **ES6** creamos **componente** con **clases** y estas **clases** no nos permiten utilizar **mixins** por eso se adoptó este patrón.

- No nos protegen de colisiones entre nombres de **props**, de esta manera no sabemos que **HOC** nos da ese valor
- Los HOC nos restringe la composición ya que va a ser una composición estática

# HOC example

En esre caso vamos a realizar un ejemplo de un input en el cual, al escribir nos mostrará el mensaje en pantalla.

![Input example](./images/classComponent.gif)

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

class WriteMessage extends React.Component {
    constructor() {
        super();
        this.state =  { 
            message: '' 
        };
    }

    updateValue(event) {
        this.setState({ message: event.target.value })
    }
    
    render() {
        return(
            <div>
                <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                <div>
                    <span>{ this.state.message }</span>
                </div>
            </div>
        )
    }
}

ReactDOM.render(<WriteMessage/>, document.querySelector('#app'));
```

En este caso hemos creado un **componente** que nos muestra el texto escrito en el input en un **span**, es posible que esta funcionalidad queramos usarla en mas de un **componente** y queramos reutilizar el código para no repetir en todos lo mismo. Para compartir código, podemos usar un **HOC**.

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

const WriteMessage = Message => 
    class extends React.Component {
        constructor() {
            super();
            this.state =  { 
                message: '' 
            };
        }
    
        updateValue(event) {
            this.setState({ message: event.target.value })
        }

        render() {
            return(
                <div>
                    <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ this.state.message } />
                    </div>
                </div>
                
            )
        }
    }
    

class Message extends React.Component {
    render() {
        return <span>{ this.props.message }</span>
    }
}
    

const AppWithMessage = WriteMessage(Message);

ReactDOM.render(<AppWithMessage/>, document.querySelector('#app'));
```

En este caso hemos creado un **HOC** el cual va  a tener la funcionalidad de mantener el estado del input y se lo va a pasar al **componente** que le estamos pasando como parámetro mediante una **prop**. ¿Qué pasaría si quisieramos que uno de nuestros componentes que usa el **HOC** tuviese un mensaje distinto?

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

const WriteMessage = Message => 
    class extends React.Component {
        constructor() {
            super();
            this.state =  { 
                message: '' 
            };
        }
    
        updateValue(event) {
            this.setState({ message: event.target.value })
        }

        render() {
            return(
                <div>
                    <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ this.state.message } />
                    </div>
                </div>
                
            )
        }
    }  

const WithCustomMessage = Component => 
    class extends React.Component {
        constructor() {
            super();
            this.state = {
                message: 'My custom message:'
            }
        }
        render() {
            return <Component message={ this.state.message } />
        }
    }

class Message extends React.Component {
    render() {
        return <span>{ this.props.message }</span>
    }
}
    

const AppWithCustomMessage = WriteMessage(WithCustomMessage(Message);

ReactDOM.render(<AppWithCustomMessage/>, document.querySelector('#app'));
```

En este caso, hemos creado otro **HOC** que estará entre el **HOC** con la funcionalidad y el **componente** que muestra el mensaje. ¿Funciona correctamente?

![Props colision](./images/customMessage.gif)

En este caso nos encontramos con el problema de colisión de **props** el **HOC** **WithCustomMessage** esta machacando las **props** que esta pasando **WriteMessage**, para que esto funcione, tenemos que cambiar el nombre de una de las **props**.

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

const WriteMessage = Message => 
    class extends React.Component {
        constructor() {
            super();
            this.state =  { 
                message: '' 
            };
        }
    
        updateValue(event) {
            this.setState({ message: event.target.value })
        }

        render() {
            return(
                <div>
                    <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ this.state.message } />
                    </div>
                </div>
                
            )
        }
    }  

const WithCustomMessage = Component => 
    class extends React.Component {
        constructor() {
            super();
            this.state = {
                message: 'My custom message:'
            }
        }
        render() {
            return <Component {...this.props} custom={ this.state.message } />
        }
    }

class Message extends React.Component {
    render() {
        return <span>{ this.props.custom } { this.props.message }</span>
    }
}
    

const AppWithCustomMessage = WriteMessage(WithCustomMessage(Message));

ReactDOM.render(<AppWithCustomMessage/>, document.querySelector('#app'));
```

Tendríamos que hacerlo de esta manera para que la **composición** de **HOC** no tenga colisiones en las **props**. 

Antes hemos mencionado que la **composición** con **HOC** es una **composición** estática, esa **composición** estática surge en esta linea:

```javascript
const AppWithCustomMessage = WriteMessage(WithCustomMessage(Message));
```

# Render Props example

En este caso vamos a realizar el mismo ejemplo pero usando el **patrón Render props** que consiste en tener un **componente** con la funcionalidad compartida pasarle una función como render.

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

class WriteMessage extends React.Component {
    constructor() {
        super();
        this.state =  { 
            message: '' 
        };
    }

    updateValue(event) {
        this.setState({ message: event.target.value })
    }

    render() {
        return(
            <div>
                <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                <div>
                    { this.props.render(this.state) }
                </div>
            </div>
            
        )
    }
}

const Message = ({ message }) => 
    <span>{ message }</span>
 
    
ReactDOM.render(<WriteMessage render={ Message } />, document.querySelector('#app'));
```
Lo que estamos haciendo es renderizar un **WriteMessage** al cual le pasamos una **prop** **render** con la función que queremos que renderice, a la cual le le va a psar un parametro del cual hacemos el **destructuring** y cogemos el **message** para luego mostrarlo en el span. 


De esta manera tendríamos el input que muestra el texto escrito, ahora vamos a hacer que tenga el mensaje custom.

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

class WriteMessage extends React.Component {
    constructor() {
        super();
        this.state =  { 
            message: '' 
        };
    }

    updateValue(event) {
        this.setState({ message: event.target.value })
    }

    render() {
        return(
            <div>
                <input type='text' value={ this.state.message } onChange={ event => this.updateValue(event) } />
                <div>
                    { this.props.render(this.state) }
                </div>
            </div>
            
        )
    }
}

const WithCustomMessage = ({ message }) => 
    <span>My custom message: <Message message={ message } /></span>

const Message = ({ message }) => 
    <span>{ message }</span>
 
    
ReactDOM.render(
    <WriteMessage render={ WithCustomMessage } />
    , document.querySelector('#app'));
```

De esta manera tendíramos el mismo funcionamiento. 

Usando **Render Props** esvitamos la coisión de **props** que teniamos usando los **HOC** y nuestras **composiciones** ahora son **dinamicas**.


# Conclusión

![Conclusion tweet](./images/tweet.jpg)






