// dummy dialoge for testing building connections with NPCs

VAR kimConnection = 0

->KimDialogue

=== KimDialogue ===
Kim says something about the party.
*   [You agree.] 
    ~kimConnection = kimConnection +1
     Kim asks who you are.
**         [Be earnest.]
            Kim shakes your hand.
**         [Be funny.]
           Kim laughs.
            ~kimConnection = kimConnection +1
***             [Ask Kim how she's enjoying the party.]
                Kim says she's excited to be here.
***             [Ask Kim about herself.]
                Kim tells you something that makes you like her more.
                ~kimConnection = kimConnection +1
****            [Continue]
**         [Be evasive.]
           Kim's not too sure what to make of that.
***             [Continue]
*   [You comment about something else.]
    Kim's never thought about it that way.
**         [Introduce yourself.]
           Kim shakes your hand.
**         [Continue expounding.]
*   [You disagree.]
    Kim doesn't seem to hear you and continues talking to someone next to them.
**      [Continue]
- Kim smiles and excuses herself to refll their drink.
    -> END
