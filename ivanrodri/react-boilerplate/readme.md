# Nuevo react boilerplate

Los miembros de **NoCountryForGeeks** hemos estado trabajando en un [proyecto base de **React**](https://github.com/NoCountryForGeeks/react-boilerplate) para que otros desarrolladores puedan aprovecharlo y usarlo para sus proyectos sin necesidad de configurarlo, de esta manera, evitamos perder días montando la estructura inicial de nuestro proyecto para hacerlo funcionar medianamente bien,esto nos ahorrará días de proyecto lo que permitirá que nuestros clientes se ahorren un pico.

En este proyecto base, nos hemos enfocado mucho en la experiencia de desarrollo, tener un buen entorno configurado, ágil y donde sentirnos cómodos nos ayudará a ser mas productivos.

## Aplicación

Actualmente las librerías que usa el proyecto, están en las últimas versiones para sacar todo el partido a las nuevas _features_ que éstas ofrecen. El proyecto tiene una configuración de **react** + **redux** + **redux-saga** donde podremos usar un **store centralizado** para manejar nuestros datos y generar flujos de nuestra aplicación con la potencia que nos ofrece **redux-saga** y los generadores. Para los reducers, como no somos muy partidarios de los **switch**, hemos usado **redux-act** para tener unos reducers mas simples y legibles. El proyecto base también cuenta con **i18n** que nos permite tener **multilenguaje** en la aplicación con un **lazy loading** sin necesidad de configurar nada. También tenemos un componente **ErrorBoundary** para capturar todas las excepciones que tengamos en el **render**, de esta manera podemos manejar los errores a nuestro gusto. Por último y no por eso menos importante es la parte de estilos, para la cual hemos configurado **css-modules** para poder tener estilos con _scope_ por componente y así poder tratar los estilos como un objeto más de JavaScript.

La aplicación lleva un **ServiceWorker** para poder trabajar de modo **offline** de esta manera, si perdemos la conexión, todos los ficheros y _assets_ de la aplicación estarán disponibles para su uso.

## Preprocesador

Para poder hacer funcionar muchas de las _features_ nombradas anteriormente, nos hemos apoyado en **Webpack** como **preprocesador**. Para ello hemos usado **Webpack 4** que ofrece múltiples avances respecto a la versión anterior, con **Webpack 4** el tamaño del bundle disminuye considerablemente (entorno al 70 - 80%) y las builds necesitan menos tiempo de bundle. En esta configuración hemos añadido todo tipo de **loaders** y **plugins**, para minificar imagenes (Sin perder calidad), minificas css, generar sprites, etc. 

Una de las cosas mas importantes que hemos añadido para conseguir un performance de carga de la aplicación más rápido es la minificacion de los ficheros, permitiéndonos rebajar hasta un **75%** el tamaño del **js** de salida. La minificación se hace a **.gz** y a **.br** (La que mas ganancia genera), estos ficheros seran usados directamente por los navegadores que lo soporten por lo que no es necesario hacer nada mas.

## Entorno de desarrollo

Para agilizar un poco el desarrollo y evitar cometer los típicos errores tontos cuando desarrollamos en JavaScript (usar variables no declaradas, escribir mal las variables, etc), hemos añadido un linter, que nos va a permitir identificar errores sintácticos directamente en el IDE, esto nos ayudará a agilizar un poco el proceso. Unificar el estilo de código entre desarrolladores, nos puede ayudar a entender mejor el código en algunas ocasiones, para eso, hemos añadido **prettier** que va a ser un formateador de código, una vez que guardemos, formateará el código de la manera que hayamos definido nuestras reglas, de esta manera todos tendremos el mismo estilo de código.

Una de las cosas mas importantes a la hora de desarrollar es tener un entorno de desarrollo **perfectamente configurado**, unas veces puede ser por falta de conocimiento de extensiones o por pereza que no tenemos bien configurado el IDE, por ello, hemos creado una [**imagen docker**](https://hub.docker.com/r/nocountryforgeeks/vscode-js/) con **VS Code** totalmente configurado y listo para trabajar.

El proyecto tiene configurado **HMR** (Hot Module Replacement) que nos permitirá reflejar los cambios en caliente sin perder el estado de la aplicación y con una rapidez inmediata.

## Docker

## Overview

Es una primera versión en la que continuamos trabajando para mejorarla. El objetivo es dar la máxima flexibilidad e ir añadiendo nuevas funcionalidades a medida que se vaya avanzando. Tenemos marcado un **Road Map** de mejora y actualización:

- Creación de un provider de temas
- Creación de un CLI
    - Generar el proyecto a través del CLI
    - Opción de crear un proyecto dinámicamente según configuración requerida
    - Extraer la configuración webpack como un paquete npm y permitir que sea extensible
- Diferentes opciones de build
    - GitHub
    - Gitlab
    - VSTS
- Service worker
    - Permitir usar la aplicación sin conexión.
    - Cachear las todas las llamadas
- Notificaciones
    - Firebase
    - Azure
    - Amazon
- Uso de servicios nativos (GPS, Webcam, Micrófono)
- Plash screen
- Soporte para PWA
- SSR

<h2 align="center">Contributors</h2>

<table>
  <tbody>
    <tr>
      <td align="center">
        <img width="150" height="150"
        src="https://avatars2.githubusercontent.com/u/5735315?s=460&v=4">
        </br>
        <a href="https://github.com/franmolmedo">Francisco Manuel Olmedo</a>
      </td>
      <td align="center">
        <img width="150" height="150"
        src="https://avatars0.githubusercontent.com/u/22966198?s=460&v=4">
        </br>
        <a href="https://github.com/IvanRodriCalleja">Iván Rodríguez</a>
      </td>
    </tr>
  <tbody>
</table>

Puedes contribuir a este proyecto mediante la publicación de [issues](https://github.com/NoCountryForGeeks/react-boilerplate/issues) para problemas detectados o sugerencias para añadir al **boilerplate**, también estaremos encantados de recibir una [pull request](https://github.com/NoCountryForGeeks/react-boilerplate/pulls) para aportar nuevas funcionalidades y solución de posibles bugs. 

<h4 align="center">Esperamos vuestro feedback!!</h4>

