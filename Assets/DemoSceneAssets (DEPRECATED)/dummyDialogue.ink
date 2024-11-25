// dummy dialogue for testing building connections with NPCs

VAR kimConnection = 0
VAR NPCName = "Kim"
VAR talkedKim = false
//VAR exitConvo = false
EXTERNAL quitDialogue()

{ talkedKim == false: ->KimStart | ->KimDefault}

        
=== KimStart ===

Kim says something about the party.
*[You agree.] 
    ->KimAgree
*[You comment about something else.]
    ->KimSomething
*[You disagree.]
    ->KimDisagree


- -> KimDefault

=== KimAgree ===
~kimConnection = kimConnection +1
Kim asks who you are.
*[Be earnest.]
    Kim shakes your hand.
    ** [Continue]
*[Be funny.]
    ~kimConnection = kimConnection +1
    Kim laughs.
    **[Ask Kim how she's enjoying the party.]
         Kim says she's excited to be here.
        *** [Continue]
    **[Ask Kim about herself.]
        ~kimConnection = kimConnection +1
        Kim tells you something that makes you like her more.
        ***[Continue]
*[Be evasive.]
    Kim's not too sure what to make of that.
    **[Continue]
- ->KimDefault

=== KimSomething ===
Kim's never thought about it that way.
    *[Introduce yourself.]
        Kim shakes your hand.
        ***[Continue]
    * [Continue expounding.]
- -> KimDefault
        
=== KimDisagree ===
Kim doesn't seem to hear you and continues talking to someone next to her.
    *[Continue]
- ->KimDefault
        
==== KimDefault ===
~ talkedKim = true
{~ Kim aknowledges you briefly with a glance, but continues speaking with someone next to her.|Kim smiles and excuses herself to refill her drink.}
    + [Leave]
    {quitDialogue()} 
    //~ exitConvo = !exitConvo
    -> KimExit
    
=== KimExit ===
->KimDefault

==== KimDone ===
-> DONE

===function quitDialogue===
    ~return

