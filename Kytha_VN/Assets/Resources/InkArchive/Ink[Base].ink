

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