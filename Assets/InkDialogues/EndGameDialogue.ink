VAR NPCName= ""
EXTERNAL quitDialogue()

~NPCName = ""
< Your ears are ringing and your heart beats in your chest. Your anxiety has gotten the best of you and it's making you leave the party early. You won't get the chance to see Rowan. >

+[-End game-]
    {quitDialogue()}
    ->Loop


===Loop===
TestLoop Thingie
+[-Loop-]
    ->Loop

- ->END

===function quitDialogue===
    ~return