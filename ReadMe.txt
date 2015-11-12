This is a console application to compose and play drum songs! see la_poderosa_muerte.wav as an example of what can be done with this

To run this project you need to have VisualStudio installed on your machine.
Just open the .sln file

######################################################################
#########################  Readme Tarea 2  ###########################
######################################################################

Explicaré la tarea en 3 partes: El navegador de archivos, El lector de partituras y el editor de partituras.

1) Navegador de archivos: Se muestra la ruta del directorio actual en la parte de arriba, luego se muestran todos los archivos y carpetas que están contenidos en este directorio. El usuario puede escribir el nombre del archivo, el nombre de una carpeta o incluso puede hacer paste de su portapapeles en consola de una ruta específica. Cuando se elige un archivo se comienza automáticamente el paso 2: el lector de partituras y se crea el archivo wav en el directorio del XML con el mismo nombre (por supuesto con extención .wav)

2) Lector de partituras: Primero se determina la duración que tiene la canción y se crea un objeto Canales que contiene los canales izquierdo y derecho en una lista de int32 (para
sobrepasar el limite de int16 y luego normalizar). Estos canales empiezan vacíos. Luego se recorre el XML creando objetos Compas y Nota para cada compás y notas especí-
ficas. En el árbol de jerarquía de clases, Canción contiene Compas y Compas contiene Nota. Un atributo de Nota es la ruta donde está el sample relacionado. Una vez que se crea un objeto Cancion se recorren sus compases y sus notas y se agregan a los canales. Luego se normalizan los canales, se eliminan los samples vacíos que pueden haber al final de los canales y se guardan en un archivo .wav (nota: tuve problemas con el archivo sample del snare, s1.wav. Este archivo tiene 4 canales y lo tuve que convertir a stereo)

3) Editor de partituras: Cambié el color de la consola a blanco porque las partituras son blancas(dah?). Primero se pregunta el tempo de la canción, la cantidad de compases y los tiempos que va a tener cada compás. Según esto se crean los compases correspondientes. Se incluye una regla en la parte superior para mantener la ubicación de los tiempos y los compases. Para cada instrumento hay una línea para posicionar notas en la partitura. El movimiento se hace con las flechas (o flechas + shift para moverse rápido). Las notas se crean usando los números 1, 2, 3 según el numero de sample que se quiera usar. Se pueden borrar notas usando el punto (.). Además agregué dos funcionalidades que creo que son indispensables en un editor de partituras: Poder cambiar el tempo de la canción y por sobre todo poder escuchar lo que llevo hasta ahora, oh yeah! (Léase con voz de Véctor de Mi villano Favorito) Para esto se crea un XML temporal. Luego se crea un wav temporal a partir de este XML y se reproduce using System.Media. Finalmente se eliminan estos archivos temporales. Se sale del editor con ESC y se le pregunta al usuario el nombre de su creación. Se crea el XML y se le pregunta si quiere generar el archivo .wav. Finalmente se vuelve al menu principal.


######################################################################
#########################   Bonus Tarea 2  ###########################
######################################################################

1) This is my jam!: viendo el video me di cuenta que la parte interesante de batería empezaba en 4:49 y terminaba en 5:29 así que trabajé en ese intervalo. Para poder reescribir la batería necesitaba un mejor audio y la habilidad de poder reproducirlo en un tempo más lento. Por eso busqué la canción en formato midi. Escribí la canción en compases de 6/8 a un tempo de 100. El archivo midi utilizado está en el repositorio. Como la batería usada tiene muchos más tambores y platillos tuve que simplificar mucho los sonidos. A pesar de esto se logra apreciar la esencia de la canción usando tan solo 3 tom y 2 crash. Se parece mucho a la versión que tocaron Los Jaivas en Viña 2011 porque ahí usaron una batería con menos elementos y más parecida a los samples que dispongo.
Ver 9:16 - 9:55:
https://www.youtube.com/watch?v=WuZ2YCj7jhM

2) Needs more cowbell: la carpeta con los samples se llama cowbell y el id del instrumento es c, porque los archivos son c(n).wav.
Los samples los bajé de este sitio: http://www.flstudiomusic.com/2010/10/104-percussion-samples.html pero venían en audio mono a 32bits, así que los convertí a stereo y 16bits para no tener problemas de compatibilidad con otros samples. Se pueden leer y escribir partituras con este nuevo instrumento.

Nota: Tuve problemas de compatibilidad de TextEdit (Mac) con el Block de notas de Windows y no me reconocía los saltos de línea. Por eso subí una versión PDF del ReadMe.







