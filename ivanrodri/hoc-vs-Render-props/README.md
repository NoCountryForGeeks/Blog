En **React** para reutilizar o compartir lógica entre componentes podemos usar patrones como puede ser **HOC (High Order Component)** o **Render props**.

# ¿Que es un HOC?

Un **HOC (High Order Component)** o traducido **Componente de Order Superior** es un patrón que no es parte del **API** de **React**, si no que llega por parte de la **programación funcional**. Este patrón consiste en una **función** que recibe una o mas **funciones** como argumento y devuelven una nueva **funcion** o **componente**, también existen los **HOC** de **reducers**, en definitiva un **HOC** es una **función** que retorna otra **función**.


Este patrón lo usan librerías como **Redux** con el **connect()** o **Apollo** con **graphql()**

# ¿Que es Render Props?

Es un patrón que nos permite compartir código teniendo un **componente** el cual recibe una  **función render** vía **props** la cual será el **render** que ejecutará el **componente**.

Este patrón lo usan librerías como **react-router** o **react-motion**.

# Desventajas de usar HOC

- No nos protegen de colisiones entre nombres de **props**, de esta manera no sabemos que **HOC** nos da ese valor
- Los HOC nos restringen la composición ya que va a ser una composición estática
- Los HOC se evalúan en tiempo de compilación.

⚠️ **El siguiente ejemplo podría resolverse de una manera distinta y mas común. !Sólo de trata de un ejemplo!**

# HOC example

En este caso vamos a realizar un ejemplo de un input en el cual, al escribir nos mostrará el mensaje en pantalla.

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

    updateValue({ target: {value} }) {
        this.setState({ message: event.target.value })
    }
    
    render() {
        const { message } = this.state;
        return(
            <div>
                <input type='text' value={ message } onChange={ event => this.updateValue(event) } />
                <div>
                    <span>{ message }</span>
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
    
        updateValue({ target: {value} }) {
            this.setState({ message: event.target.value })
        }

        render() {
            const { message } = this.state;
            return(
                <div>
                    <input type='text' value={ message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ message } />
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
    
        updateValue({ target: {value} }) {
            this.setState({ message: event.target.value })
        }

        render() {
            const { message } = this.state;
            return(
                <div>
                    <input type='text' value={ message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ message } />
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
    
        updateValue({ target: {value} }) {
            this.setState({ message: event.target.value })
        }

        render() {
            const { message } = this.state;
            return(
                <div>
                    <input type='text' value={ message } onChange={ event => this.updateValue(event) } />
                    <div>
                        <Message message={ message } />
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
        const { message, custom } = this.props;
        return <span>{ custom } { message }</span>
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

    updateValue({ target: {value} }) {
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

    updateValue({ target: {value} }) {
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

Usando **Render Props** evitamos la coisión de **props** que teniamos usando los **HOC** y nuestras **composiciones** ahora son **dinamicas**, todo sucede en el **render** por lo cual podremos aprovechar el ciclo de vida de **React** y el flujo natural de **prop** y **state**.


# Conclusión

Con el uso de este **patrón** se puede reemplazar cualquier **HOC** por un componente con **Render props** pero no todos los componentes con **Render props** se pueden hacer con un **HOC**.


![Conclusion tweet](./images/tweet.jpg)






