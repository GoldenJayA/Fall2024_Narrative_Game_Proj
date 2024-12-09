VAR NPCName = "??"
EXTERNAL quitDialogue()

~NPCName = "Rowan"
< Turning to the door, you can't help a smile creeping across your face as you recognize who has just walked in. > It's so good to see you! Been way too long!

+[-End game-] 
    {quitDialogue()}
    ->Loop


===Loop===
TestLoop Thingie
+[-Loop-]
    ->Loop

        
===function quitDialogue===
    ~return