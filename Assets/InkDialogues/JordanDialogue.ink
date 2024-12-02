VAR NPCName = "Jordan"
VAR jordanConnection = 0
VAR talkedJordan = false

INCLUDE GlobalVariables.INK

EXTERNAL quitDialogue()

{ talkedJordan == false: -> JordanStart | -> JordanDefault}


=== JordanStart ===
~NPCName = "??"
< You see someone not talking to anyone sitting alone in the corner. You get closer and see they are cradling their phone in their lap and holding their solo cup between their teeth. They stare blankly into space. >
    *[Can I sit?] -> jordanIntroduce
    *[Are you ok?] ->jordanIntroduce
    +[-Leave-] ->JordanStart
        {quitDialogue()}
        
= jordanIntroduce
< They hurriedly put their phone away. > Oh, hi. Didn’t think anyone else would find this spot.
    *[-Introduce yourself-]
        You looking for some quiet too? Have a seat. I'm Jordan by the way.
        **[Yeah, kinda overwhelmed]->overwhelmed
        **[You ok?] -> peoplewatching

= overwhelmed
Totally get that. I usually come out here after an hour inside.
 * [You here often?]
    Oh, all the time. I live here, with Riley. The parties are always her idea.
    **[She's very hospitable]//if you've talked to Riley already
            -> JordanRiley.highschool
    **[Don't like parties?]
            I like them, but for different reasons than most people.
       ***[Why is that?] ->JordanQuotes

= peoplewatching
Oh totally. Just people watching. Great spot for it, don't you think?
*[Seen anything good?]
    It's been pretty tame so far. What about you? Hear anything quotable?
    **[Quotable?] -> JordanQuotes
*[Never understood people watching]
    It's the saving grace of parties for us introverts, I find.
    **[What do you watch for?] -> JordanPerformers
    **[Seen anything good?]
        It's been pretty tame so far. What about you? Hear anything quotable?
            ***[Quotable?] -> JordanQuotes
        

=== JordanPerformers ===
You ever do theater in high school?
    *{playerInterest == "theater"}[*Theater kid* Yeah, stage tech] //unlocked by start trait?
        ~jordanConnection++
        No way, me too! <>
        -> stagePresence
    *[No]
        Nevermind then. It's just a feeling you get looking at people.
        **[-Continue-]-> JordanDefault


= stagePresence
Teenagers very rarely have any kind of stage presence. Like they know they should be acting, but they're not actually acting.
    *[Makes sense]
    *[They're not that bad...]
- That's who I keep a close eye on at parties. The ones who still think this is a high school stage.
    * [Why them?]
    * [Haha, that's brilliant!]
        ~jordanConnection ++
- -> JordanQuotes 

=== JordanQuotes ===
I probably shouldn't be telling you this, but Riley and I run The Wall Flowers Have Ears.
    *[What's that?]
- It's our blog. Most of Overheard at Smithfield is actually posted by us first.
    *[Heard anything good so far tonight?]
- Plenty! < They fish for their phone. >
    *[-Continue-]
- We sort the stuff people say into different categories, you want "keep it in the bedroom", "drunk nonsense", or "potpourri"?
    *[Keep it in the bedroom]
        Middle of eating me out, he just stops and says, "looks like the eye of Sauron".
    *[Drunk nonsense]
        Come open me in, the door is gone.
    *[Potpourri]
        My to-do list for the 24th: Derrida, Saussure, Lil Nas X
    --**[That's hillarious!] 
                ~jordanConnection ++
                ->JordanRiley.alone
                
- -> END

=== JordanRiley ===
= highschool
She is, isn't she. I've known her since high school. She's always loved the spotlight.
*[You sound bitter]
    Me? No! I mean... maybe a little.
    **[How come?] -> JordanQuotes

= alone
I'm glad you think so. Lately it's been all me harvesting the quotes.

*[Riley too busy hosting?]
    It used to be something we could share. I put up with the parties, because we could create something together afterwards.
    **[Want help?] -> JordanRiley.help
*[Want help?] -> JordanRiley.help

= help
~jordanConnection++
Sure, why not? If you've overheard a worthy soundbite by the end of the night let me know. < They stand up. >
    *{not JordanPerformers} [What should I look for?]->JordanPerformers
    *[-Continue-] ->JordanDefault


=== JordanDefault ===
~ talkedJordan = true
{~ Gotta go refill my drink. Come find me later, yeah? | Hold on, I just heard something incredible, gotta go write it down before I forget.}
+ [Leave]
    {quitDialogue()} 
    -> JordanDefault


===function quitDialogue===
    ~return