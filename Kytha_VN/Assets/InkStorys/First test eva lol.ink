

{Name("Player")}
Salí del cuartel y caminé hasta la plaza de la ciudad. Estaba bastante transitada, muchas personas iban y venían. 

Me quedé mirando alrededor, pensando por dónde y cómo podría encontrar al licántropo. 

Entonces alguien tocó mi hombro y me giré. 

{Enter("Ethan")}{Name("Ethan")}
¡Hola! Me dijeron que buscase a alguien con pinta de estar perdido. ¿Eres tú?

lalallala

{Exit("Ethan")}{Name("Player")}

sxwdgejknvcf
Entonces vi un chaval con chaqueta acercarse.

{Enter("Vaenus")}{Name("Vaenus")}

Soy una especie de vampiro albino to guay
Lol

Po me voy

{Exit("Vaenus")}{Name("Player")}

Buena prueba lol

{Enter("Valena")}{Name("Valena")}

Esto es otra señora vampira o yokse
Yasss

{Exit("Valena")}{Name("Player")}

Hay que mirar cómo ponerlos más grandecitos

¿Podremos tener diferentes expresiones para cada uno? 
Chan chan que misterio


EXTERNAL Name(charName)
EXTERNAL Scene(sceneN)
EXTERNAL Enter(pjName)
EXTERNAL Exit(pjName)

=== function Name(charname) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return charname

=== function Scene(SceneN) ===
~ return SceneN

=== function Enter(pjName) ===
~ return pjName

=== function Exit(pjName) ===
~ return pjName