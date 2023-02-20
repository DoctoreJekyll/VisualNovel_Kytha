{Name("Name00")}
Failed.
Mas y mas
y Mas pruebas

Vamos a probar los fondos a ver si cuela
{CallSetBg("Test")}
Igual con suerte sale a la primera
o no
{CallSetBg("Hojas")}
Pero vamos a confiar que tampoco pasa na

{Enter("Mage")}{Name("Mage")}{SetPositionTest("Mage", -500)}
Lo ocurrido aquí es clasificado y no podrá ser expuesto a ningún organismo ni ser vivo fuera de éste establecimiento aquí y ahora.
aeaeaeaeaeae {MoveCharacter("Mage", 1, 500)}
Cambiando pos del mago
talta
taltapos
pos

Pruebita-chan de opciones
->My_Choices
== My_Choices ==
* [Good] -> Good
* [Bad] -> Bad
* [What] -> Repeat

== Good ==
Estoy establecimiento
{Scene("eva tests je")}
-> END

== Bad ==
No estoy establecimiento
-> Cointinue00

== Repeat ==
Repite establecimiento
-> My_Choices


jaja
testing positions o no 
Me quiero morir



== Cointinue00 ==
{Enter("Mage")}{Name("Mage")}
Lo ocurrido aquí es clasificado y no podrá ser expuesto a ningún organismo ni ser vivo fuera de éste establecimiento aquí y ahora.
aeaeaeaeaeae
{Chapter("Chapter0")}
jaja
aaaaaaaeeeeeaeaeeeee
Si 

{Name("Name01")}{Enter("Mage2")}{Name("Mage2")}
El mundo está a punto de acabarse y solo tú eres el posible salvador, todo depende de tí.
La vida tal y como la conocemos podría estallar en cualquier momento.
{Exit("Mage")}
La maga se acaba de ir, una lástima, pero si se va es bueno, que se vaya siginifica que:
Todo esto está funcionando, y eso siempre es bueno, creo.
{Scene("test")}

-> END




EXTERNAL Name(charName)
=== function Name(charname) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return charname

EXTERNAL Scene(sceneN)
=== function Scene(SceneN) ===
~ return SceneN

EXTERNAL Enter(pjName)
=== function Enter(pjName) ===
~ return pjName

EXTERNAL Exit(pjName)
=== function Exit(pjName) ===
~ return pjName

EXTERNAL Chapter(chapter)
=== function Chapter(chapter) ===
~ return chapter

EXTERNAL CallSetBg(layer)
=== function CallSetBg(layer) ===
~ return layer

EXTERNAL SetPositionTest(pjName, amount)
=== function SetPositionTest(pjName, amount) ===
~ return pjName
~ return amount

EXTERNAL MoveCharacter(namePj, locationX, speed)
=== function MoveCharacter(namePj, locationX, speed) ===
~ return namePj
~ return locationX
~ return speed


