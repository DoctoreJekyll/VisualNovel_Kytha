

El traqueteo del carruaje me despertó. La carretera había dejado de ser lisa y pavimentada; habíamos vuelto a los caminos. Apreté con algo de nerviosismo el sobre que guardaba en mi regazo.
Mi vida daba un vuelvo de nuevo. Todos sabíamos que eso podía ocurrir en cualquier momento, pero nunca se estaba realmente preparado. Un traslado.
{CallSetBg(0)}
Dejaba todo lo que conocía para empezar de nuevo en otra ciudad, con otras personas, y otro ritmo de vida. Sabía que era una gran oportunidad, pero no podía evitar sentir cómo me invadían los nervios.
Aunque aquel sobre era la versión oficial de mi traslado, lo cierto era que tenía una misión más importante que cumplir. Al fin y al cabo, el propio Ministro se había subido a este carruaje horas antes para hablar directamente conmigo. Era la primera vez que había visto a un Ministro en persona. 
Sólo pude asentir mientras escuchaba toda la información nueva. Mi traslado no era por azar o por falta de recursos en la ciudad, lo que querían era un topo. El Ministerio de Justicia tenía sospechas sobre la malversación de fondos en Kytha, pero sin pruebas no podían hacer nada, y encargárselo a alguno de sus funcionarios sólo iba a provocar el caos. Por eso mismo, mandaban a alguien que ya estuviera en la Guardia.

* [Era molesto]
Sabía que no tenía que haberme metido en aquel lío. Había estado toda mi vida adulta en un pequeño pueblo, pero ayudé a destapar varios casos de corrupción. No sabía que desencadenaría algo tan grande como esto. ¡El propio Ministerio me lo había pedido! ¿Cómo de peligroso podía ser?
{Chapter("Choice1")}

* [Había que hacer justicia]
Tengo experiencia en casos de corrupción, he investigado y destapado varios, dentro y fuera de la Guardia. Aunque sólo fuera un pequeño pueblo... Era lo que había que hacer. Este es mi trabajo. No importa de quién se trate, lo encontraré y lo llevaré ante la justicia. 
{Chapter("Choice2")}


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

EXTERNAL CallSetBg(arrayPos)
=== function CallSetBg(arrayPos) ===
~ return arrayPos

EXTERNAL SetPositionTest(pjName, amount)
=== function SetPositionTest(pjName, amount) ===
~ return pjName
~ return amount

EXTERNAL MoveCharacter(namePj, locationX, speed)
=== function MoveCharacter(namePj, locationX, speed) ===
~ return namePj
~ return locationX
~ return speed
