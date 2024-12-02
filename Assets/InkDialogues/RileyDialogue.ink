VAR NPCName = "Riley"
VAR rileyConnection = 0
VAR talkedRiley = false

INCLUDE GlobalVariables.INK

EXTERNAL quitDialogue()

{ talkedRiley == false: -> RileyStart | -> RileyDefault}

=== RileyStart ===
~NPCName = "Riley"
Oh hey! Alex said you might stob by. I'm Riley. Let me guess, you're a whiskey drinker.
    * [Wow, you're absolutely right!]-> RileyWhiskey
    * [Vodka is more my speed]-> RileyVodka
    * [You just assume people's tastes?]->RileyTastes

=== RileyWhiskey ===
I can just picture you curled up by a fire reading Anna Kerenina, or something. Sounds pretty cozy to me. All you need is this to enhance your vibe.

    *[-Take the drink and sip-]-> RileyDrink
    *[-Hesitate-] -> hesitate
    *{playerInterest == "books"} [*Bookworm* You like Tolstoy?] -> Tolstoy
    
= hesitate
Oh, I'm so sorry. Complete stranger hands you a drink at a party, what was I thinking? < She reaches back and takes a sip of your drink >
    *[Bad experience in the past?]
        ~rileyConnection ++
- Not me, but a friend of mine. I love being around people like this, but her experience reminds me that it can be really uncomfy for some folks.
    *[-Take a sip, reassured-] ->RileyDrink
    
= Tolstoy
~ rileyConnection ++
My granda would read me scenes from Anna Karenina when I was in high school. I tried reading it myself at summer camp one year, but oof, 900 pages?
    *[What's your favorite part?]
        I know it's cliche, but the scene at the train station at the very end. So tragic, and so gorgeous.
        ** [-Continue-]
            But try the drink! Tell me what you think.
            ***[-sip the drink-]->RileyDrink
    *[Not much of a reader?]
        I do it when I have to, but I'd much prefer to listen. Audiobooks, podcasts, gossip. But try the drink! Tell me what you think.
            **[-sip the drink-]->RileyDrink

===RileyDrink ===
< The drink that passes your lips blossoms with the flavors of a crisp fall day. It is way too good to be served at this kind of party. > 
    * [Quite the mixologist.]
         Haha, not really, my granda just really loved whiskey, so he taught me to make all of his favorites. I wouldn't know where to start if I had to start with vodka or rum.
    * [-Immediately take another sip-]
        ~rileyConnection ++
        Tacit compliment much appreciated, but this is best enjoyed slowly. Plus, pro-tip, holding a cup at these kinds of parties gives you something to do with your hands.
        
- **[-Continue-]-> RileyDefault

=== RileyVodka ===
< adopting a Russian accent > A fine choice, comrade. After all, how will we keep our balls warm on these Siberian nights? 
    *[-Continue]-> accent

=accent
< speaking normally > Oh wow, I'm so sorry, I don't know where that came from.

    * [-Respond in kind- na strovye, comrade]
        ~rileyConnection++
       < Russian again > And to your health, comrade. Regretably, Comrade Levin has made of with the last of the vodka, and so I offer you this paltry substitute from the Western pigs.
       **[-Take the drink and sip-]-> RileyDrink
       **{playerInterest == "books"}[*Bookworm* Levin? from Anna Karenina?]->RileyWhiskey.Tolstoy
         
    * [Are you bartending?]
        Ha, I wish. You know, I think all college parties should have a designated bartender. I'm serious, like it should be mandated by the university.
        **[Sounds pretty nice]
            ~rileyConnection++
            Right? Acts of service are one of my love languages, so making people nice drinks is one way I can show them I care.
        **[Sounds too formal]
        Not at all! A bartender makes people feel cared for. Feel heard. Also would make sure no one ever has to drink jungle juice.
            --***[-Continue-]-> RileyDefault        
    
=== RileyTastes ===
Yes, and it's such a fun game, especially when getting to know someone for the first time! It gets them curious, reals them in, and look, now we've started a conversation.
    *[Good point]
    *[Still kinda weird]
    - If I was wrong about the whiskey, let me try something else. Hmm... < she gives you a playful look up and down > 
        *[What do you see?]
        *[-stay quiet and squirm-]
        - You moved around alot as a kid, you're into some niche craft like basketweaving, and you're going to love this drink I made just for you.
        *[Wow, impressive]
        *[Not even close]
        *[-Take the drink and sip-]->RileyDrink

- ->RileyDefault

=== RileyDefault ===
~ talkedRiley = true
{~ < Riley gives you a friendly pat on the shoulder, but quickly turns to offer another one of her drinks to an aquaintance. > | < Riley winks and ruefully holds up her empty ice bucket as she walks away.>}
    + [-Leave-]
    {quitDialogue()} 
    -> RileyDefault
    
===function quitDialogue===
    ~return