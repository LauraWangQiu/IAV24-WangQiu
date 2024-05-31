# IAV - Proyecto Final

> [Yi (Laura) Wang Qiu](https://github.com/LauraWangQiu)

Este proyecto pertenece a la asignatura de Inteligencia Artificial para Videojuegos del Grado en Desarrollo de Videojuegos de la UCM.

## Objetivo

Los objetivos de este proyecto se precisan en el siguiente [link](https://narratech.com/es/docencia/prueba/).

## Contexto

`Jorge` es un camarero que trabaja con sus padres en el mismo restaurante. A `Jorge` le han dejado todo el salón para él solo durante un ajetreado domingo. ¿Será capaz de apañárselas?

Habrá una cola de clientes, unas mesas con dos asientos cada mesa, un espacio para sacar los platos, un espacio para sacar las bebidas, un baño con dos lavabos y una puerta de entrada/salida de los clientes.

## Características

### Característica A

`Jorge` podrá ser dirigido por un árbol de comportamientos como una IA o mediante el control del usuario. Para cambiar de estado, se pulsará el espacio `SPACE`.

### Característica B

Los clientes tendrán una lista de deseos: pedir comida (y dentro del menú, cualquier plato), pedir bebida (cualquiera), ir al baño o pedir la cuenta para irse. Depende de cada deseo, los clientes cambiarán de comportamiento. Estos mismos comportamientos se explican en el apartado de `Diseño de la solución`.

### Característica C

El baño está compuesto por dos lavabos, por lo que es posible que se genere una cola. Los clientes se dirigirán al baño y esperarán a que haya un lavabo libre para poder entrar.

## Punto de partida

La versión de Unity utilizada es **Unity 2022.3.5f1**.

Partimos de un proyecto en blanco con las siguientes características:

- **ProyectoFinal**: escena en la que se desarrollará el proyecto.
- **BehaviorBricks**: asset pack de Behavior Bricks para Unity.

Y partimos de los siguientes conocimientos:

- Para intercambiar el comportamiento principal de un agente, hace falta activar el que se quiere y desactivar el que se estaba ejecutando.
- No se puede cambiar el brick asset ni parar la ejecución de un comportamiento si está activado el componente.

## Diseño de la solución

Realizaremos diferentes árboles de comportamiento para `Jorge` y los clientes:

- `Jorge` tendrá un árbol de comportamiento que le permitirá atender a los clientes para asignarles una mesa, tomarles nota, llevarles los platos y cobrarles.

```mermaid
flowchart TD
    A(("↺")) --> B((?))
    B -->|"IsThereFood && (Food.TimeToCoolDown < X || (!IsThereOrder && !IsThereMoney))"| C[MoveSequence/MoveToFood,MoveToClient,GiveOrder]
    B -->|"IsThereOrder"| D[MoveSequence/MoveToClient,TakeOrder,MoveToKitchen,GiveOrder]
    B -->|"IsThereMoney"| E[MoveSequence/MoveToCheckPoint,TakeMoney]
```

La variable `X` será el tiempo de espera máximo para cada acción.

La idea es priorizar la entrega de comida, seguido de la toma de pedidos y la recogida de dinero.

Es muy importante que `Jorge` no se quede parado en ningún momento y teniendo en cuenta que las acciones tienen un tiempo de ejecución, se deberá tener en cuenta el tiempo de espera máximo para cada acción.

Los platos de comida tendrán un tiempo de enfriamiento que desafortunadamente no hay un feedback visual durante la ejecución, pero se puede observar en el inspector del Editor de Unity al seleccionar la comida. Una vez que alguno de estos platos baje de `X` tiempo, `Jorge` se verá obligado a recoger la comida y llevárselo al cliente.

En cuanto a la toma de pedidos, tras llevar la tanda a la cocina, se deberá esperar a que los platos estén listos por lo que durante ese tiempo, `Jorge` podrá realizar otras acciones.

Los clientes tendrán una lista de deseos y cada deseo tendrá un árbol de comportamiento que les permitirán pedir comida, coger bebida, ir al baño e ir a pagar la cuenta.

Cada vez que un cliente termine su deseo, se le asignará otro de forma aleatoria (`SelectRandomBehavior`). Esto ocurre siempre tras sentarse nuevamente a su mesa.

Los diagramas de los deseos que tendrán los clientes son:

- **Pedir comida**: si previamente se han sentado, podrán pedir un plato de comida.

```mermaid
flowchart TD
    Z[OrderFood] -->A
    A(("G")) --> B(("->"))
    B -->C[AddToOrderList]
    B -->D(("~?"))
    D -->E[Burger]
    D -->F[Doughnut]
    D -->G[Cupcake]
```

Una vez recibida la comida, se les sumará en la cuenta del cliente el precio de la bebida `owingMoney`.

Con el RandomSelector (~?), se seleccionará un plato de comida de forma aleatoria entre las propuestas.

- **Coger bebida**: si previamente se han sentado, podrán coger una bebida.

```mermaid
flowchart TD
    Z[TakeDrink] -->A
    A(("G")) --> B(("->"))
    B -->C(("~?"))
    C -->D[Soda]
    C -->E[Coke]
    C -->F[Lemonade]
    C -->G[...]
    B -->H[MoveToDrinkMachine]
    B -->I[GetInstancePosition]
    B -->J[Instantiate]
    B -->K(("?"))
    K -->|CheckCatch| L[TakeDrink]
    B -->M[MoveToTable]
```

Una vez cogida la bebida, se les sumará en la cuenta del cliente el precio de la bebida `owingMoney`.

Con el RandomSelector (~?), se seleccionará una bebida de forma aleatoria entre las propuestas. Luego, se moverá hacia la máquina de bebidas, cogerá la bebida, comprobará si la ha cogido y se dirigirá a la mesa.

- **Ir al baño**:

```mermaid
flowchart TD
    Z[GoToBathroom] -->A
    A(("G")) --> B(("->"))
    B -->C(("?"))
    C -->|CheckWaitPoint| D[MoveToWaitPoint]
    B -->E["RepeatUntilSuccess"]
    E -->F(("?"))
    F -->|CheckBathroom| G[MoveToBathroom]
    B -->H[WaitForSeconds]
    B -->I[DisassignBathroom]
    B -->J[MoveToTable]
```

Se mueve al punto de espera, espera a que haya un lavabo libre y se dirige al lavabo. Se esperará una cierta cantidad de tiempo en el baño y, una vez terminado, se dirigirá a la mesa.

- **Pedir la cuenta**:

```mermaid
flowchart TD
    Z[AskForBill] -->A
    A(("G")) --> B(("->"))
    B -->C(("?"))
    B -->D(("?"))
    B -->E[Destroy]
    C -->|CheckBill| F[MoveToCashier]
    D -->|CheckPaid| G[MoveToExit]
```

Se dirigirá a la caja, comprobará si ha pagado y una vez pagado se mueve a la salida y se destruye su instancia.

## Pruebas

| Pruebas | Links |
|:-:|:-:|
| Al pulsar el `SPACE`, se intentará controlar a `Jorge` clicando en la escena. Al pulsar nuevamente el `SPACE`, `Jorge` volverá a ser manejado por la IA | [Space](https://drive.google.com/file/d/1wrCsgnLa8WZ0ErL16Q4oGY1PC9p4TNbu/view?usp=sharing) |
| Comprobar que los clientes pidan comida y bebida aleatorios. | - [Pedir comida aleatoria](https://drive.google.com/file/d/1C75uezfVopQQaNjQAZjBoKp7lRQmHPFH/view?usp=sharing) <br> - [Escoger bebida aleatoria](https://drive.google.com/file/d/1wEDtBlnEJrz1btSi0mti8SedACCnd8u9/view?usp=sharing) |
| Asignar a varios clientes el deseo de ir al baño. Comprobar que se genera una cola de espera. | [Gestión de los lavabos](https://drive.google.com/file/d/1EWv_H4ov2E2bXxu_UXkmY3D6n1PkP_IO/view?usp=sharing) |
| Comprobar que los clientes paguen la cuenta y se vayan si previamente han comido o bebido. | - [Habiendo comido o bebido](https://drive.google.com/file/d/1uhP2gCptH9xFxv-qbfRP8FjjFekhG6T3/view?usp=sharing) <br> - [Sin comer o beber](https://drive.google.com/file/d/1O5HjVCLjjIAe3Ngn8WOhTeyufrzdmAfR/view?usp=sharing) |
| Comprobar que si hay comida sacada, la IA lo llevará al cliente indicado | [TakeOrder](https://drive.google.com/file/d/1HH1CxYdmweEUKNMzZSVatsUi3GMjbKbb/view?usp=sharing) |
| Comprobar que si hay más de un cliente para tomar nota, la IA pasará por todos | [GiveOrder](https://drive.google.com/file/d/1_A9ww-2uO10aW1HnXkXe7YtFC9xWRyow/view?usp=sharing) |
| Comprobar que el camarero se acerque a la caja si hay clientes que han pedido la cuenta | [TakeMoney](https://drive.google.com/file/d/13sy5MiUAR_ZaA_ZsMKani1cuBsufyOre/view?usp=sharing) |

## Producción

Observa la tabla de abajo para ver el estado y las fechas de realización de las mismas. Puedes visitar el proyecto de GitHub en el siguiente [link](https://github.com/users/LauraWangQiu/projects/2/).

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔️ | Diseño: Primer borrador | 15-05-2024 |
| ✔️ | Característica A | 31-05-2024 |
| ✔️ | Característica B | 30-05-2024 |
| ✔️ | Característica C | 30-05-2024 |

## Licencia

Yi (Laura) Wang Qiu, autora de la documentación, código y recursos de este trabajo, concedo permiso permanente a los profesores de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar este material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente nuestra autoría.

Una vez superada con éxito la asignatura se prevee publicar todo en abierto (la documentación con licencia Creative Commons Attribution 4.0 International (CC BY 4.0) y el código con licencia GNU Lesser General Public License 3.0).

## Referencias

Los recursos de terceros utilizados son de uso público.

- *AI for Games*, Ian Millington.
- [Liquid Snake](https://ceur-ws.org/Vol-3305/paper7.pdf)
- Behavior Bricks de PadaOne Games (empresa de base tecnológica de la UCM)
https://assetstore.unity.com/packages/tools/visual-scripting/behavior-bricks-74816
- Unity, Navegación y Búsqueda de caminos
https://docs.unity3d.com/es/2021.1/Manual/Navigation.html
- Unity 2018 Artificial Intelligence Cookbook, Second Edition (Repositorio)
https://github.com/PacktPublishing/Unity-2018-Artificial-Intelligence-Cookbook-Second-Edition 
- Unity Artificial Intelligence Programming, 5th Edition (Repositorio)
PacktPublishing/Unity-Artificial-Intelligence-Programming-Fifth-Edition: Unity Artificial Intelligence Programming – Fifth Edition, published by Packt (github.com)
- CoffeeShop Starter Pack: https://assetstore.unity.com/packages/3d/props/coffeeshop-starter-pack-160914
- Fast Food Restaurant Kit: https://assetstore.unity.com/packages/3d/environments/fast-food-restaurant-kit-239419
- Cutlery: https://assetstore.unity.com/packages/3d/props/food/cutlery-silverware-pbr-106932
- Customizable Kitchen Pack: https://assetstore.unity.com/packages/3d/props/interior/customizable-kitchen-pack-22269
- Food Pack | Free Demo: https://assetstore.unity.com/packages/3d/props/food/food-pack-free-demo-225294
- Paper panels: https://gamedeveloperstudio.itch.io/paper-panels
- Retro Pixel Ribbons, Banners and Frames 2: https://bdragon1727.itch.io/retro-pixel-ribbons-banners-and-frames-2
- Free 16x16 Pixel Food Icons: https://electrikjelli.itch.io/free-pixel-food-icons
- Lemonade 64x64: https://rone3190.itch.io/lemonade-64x64
- Fondo pantalla de inicio: https://pixabay.com/es/illustrations/burger-hamburguesa-comida-delicioso-2041192/
