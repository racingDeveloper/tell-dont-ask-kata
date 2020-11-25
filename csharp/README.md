# Tell don't ask

Una kata para su refactorización de código heredado, enfocado en la violación del principio tell don't ask y del modelo de dominio anémico.

## Instrucciones

Aquí encontrará una sencilla aplicación de flujo de pedidos. Es capaz de crear órdenes, hacer algunos cálculos (totales e impuestos), y administrarlos (aprobación / rechazo y envío).

El viejo equipo de desarrollo no encontró tiempo para construir un modelo de dominio propio, sino que prefirió usar un estilo procedimental, construyendo este modelo de dominio anémico. Afortunadamente, al menos se tomaron el tiempo para escribir pruebas de unidad para el código.

Su nuevo CTO, después de muchos errores causados ​​por esta aplicación, le pidió refactorizar este código para hacerlo más fácil de mantener y confiable.

## En qué centrarse

Como el título del kata dice, por supuesto, el decir no pide principio. Debería ser capaz de eliminar todos los setters que mueven el comportamiento en los objetos de dominio.

### disclaimer

Ésta es una kata sugerida por el grupo Software Craftsmanship de Barcelona y originariamente escrita para Java.
Me he permitido la osadia de traducir las instrucciones y transformarla a mi lenguaje natural C#

La kata original la encontrarás en https://github.com/gabrieletondi/tell-dont-ask-kata 

al no disponer de la clase BigDecimal he utilizado la libreria de terceros
dmath   https://github.com/deveel/deveel-math
