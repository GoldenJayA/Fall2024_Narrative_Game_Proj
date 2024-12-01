VAR NPCName = "Riley"
VAR rileyConnection = 0
VAR talkedRiley = false

EXTERNAL quitDialogue()

{ talkedRiley == false: -> RileyStart | -> RileyDefault}

=== RileyStart ===
Oh hey! Alex said you might stob by. I'm Riley. Let me guess, you're a whiskey drinker.
    * [Wow, you're absolutely right!]
        -> RileyWhiskey
    * [Vodka is more my speed]
        -> RileyVodka
    * [Um, you always assume people's tastes?]
        ->RileyTastes

- ->RileyDefault

=== RileyWhiskey ===
~rileyConnection ++
I can just picture you curled up by a fire reading Anna Kerenina, or something. Sounds pretty cozy to me. All you need is this to enhance your vibe.

    *[<Take the proferred drink and sip>]
    -> RileyDrink
        
    *[<Hesitate>]
        Oh, I'm so sorry. Complete stranger hands you a drink at a party, what was I thinking? <She reaches back and takes a sip of your drink>
        **[Bad experience in the past?]
            ~rileyConnection ++
            Not me, but a friend of mine. I love being around people like this, but her experience reminds me that it gets really uncomfy for some folks.
            ***[<Take a sip, reassured>]
            ->RileyDrink
        **[<Take a sip, reassured>]
            ->RileyDrink
    *[You like Tolstoy?]
        My granda would read me scenes from Anna Karenina when I was in high school. I tried reading it myself at summer camp one year, but oof, 900 pages?
        **[What's your favorite part?]
            I know it's cliche, but the scene at the train station at the very end. So tragic, and so gorgeous.
            *** [Continue]
                    But try the drink! Tell me what you think.
                    [<sip the drink>]
                    ->RileyDrink
        **[Not much of a reader?]
            I do it when I have to, but I'd much prefer to listen. Audiobooks, podcasts, gossip. But try the drink! Tell me what you think.
                    [<sip the drink>]
                    ->RileyDrink

- -> RileyDefault

===RileyDrink ===
<The drink that passes your lips blossoms with the flavors of a crisp fall day. It is way too good to be served at this kind of party>
    * [You're quite the mixologist.]
         Haha, not really, my granda just really loved whiskey, so he taught me to make all of his favorites. I wouldn't know where to start if I had to start with vodka or rum.
    * [<immediately take another sip>]
        ~rileyConnection ++
        Tacit compliment much appreciated, but this is best enjoyed slowly. Plus, pro-tip, holding a cup at these kinds of parties gives you something to do with your hands.
        
- -> RileyDefault

=== RileyVodka ===
A fine choice, comrade, how else are we going to keep our balls warm on these Siberian nights? Oh wow, I'm so sorry, I don't know where that came from.
    * [<Respond in kind> Indeed, comrade]
    * [Are you bartending?]
        Ha, I wish. You know, I think it should be a rule that all college parties should have a designated bartender.
        ***[Sounds unnecessary]
        ***[Sounds too formal]
        ---- Not at all! <talks about the joys of having someone taking care of you>
        
- -> RileyDefault        
    
=== RileyTastes ===
<Apologizes and talks about something else>
*[Continue]

- ->RileyDefault

=== RileyDefault ===
~ talkedRiley = true
{~ Riley aknowledges you briefly with a glance, but continues speaking with someone next to her.|Riley smiles and excuses herself to refill her drink.}
    + [Leave]
    {quitDialogue()} 
    -> RileyDefault
    
===function quitDialogue===
    ~return