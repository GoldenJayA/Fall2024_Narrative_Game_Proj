VAR NPCName = "??"
EXTERNAL quitDialogue()

~NPCName = "??"
< Turning to the door, you can't help a smile creeping across your face as you recognize who has just walked in. >
    *[-Continue-]
    ~NPCName = "Rowan"
    - It's so good to see you! Been way too long!
    +[-End Game-] 
        {quitDialogue()}
        ->Loop


===Loop===
TestLoop Thingie
+[-Loop-]
    ->Loop

- ->END

===function quitDialogue===
    ~return