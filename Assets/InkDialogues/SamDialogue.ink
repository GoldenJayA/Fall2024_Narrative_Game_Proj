VAR NPCName = "??"
VAR samConnection = 0
VAR introducedSam = false

INCLUDE GlobalVariables.INK

EXTERNAL quitDialogue()

{introducedSam == false: -> SamIntro | -> SamDefault}

=== SamIntro ===
~NPCName = "??"
You're not an econ major are you?
    *[No...]
    *[What if I was?]
- You know what's so BS about econ? A whole field of study based around the premise that people will act rationally. Me, I know people. We aren't rational.
    *[What are we then?]
        Scared shitless. I'm Sam by the way.
        ~NPCName = "Sam"
            **[I'm Kai] -> Alexsfriend
            **[Guess you're not an econ major?] ->SamEcon


= Alexsfriend
You're Alex's friend, right? I must commend you on your return to dear old Smithfield.
    *[Thank you?]
        That was genuinely meant, I swear. Happy to see someone else with the endurance to withstand the bullshit.
            **[Just needed time]
            **[Not going out helps] 
            -- ->brownnoser
    *[One day at a time] -> brownnoser
    *[Feels like nothing's changed]
        Quite a damning observation. How refreshing.
        **[You're cheery]-> cheery

= brownnoser
Ha. I'm sure. How else would we move survive in this world of effete brown-nosers?
    *[You're very cheery]-> cheery
    *[Ugh, tell me about it]
        ~samConnection ++
        ->comiserate
    
= cheery
I'm a fucking delight. And Econ is still bullshit.
*[You an econ major?]->SamEcon

= comiserate
I mean, the brown-nosers are harmless enough, if you stay out of their way. The posers on the otherhand, out of control.
    *[I know what you mean]
        ~samConnection++
    *[Anyone at this party?]

- Riley for one, the host... What do you think of her?
    *[Haven't met her]
        I'll let you form your own oppinion then. Just keep your guard up when you meet her.
    *[I hear she makes good drinks]
    *[Attention seeker]
        ~samConnection++
        Precisely. The worst is that everyone is absolutely charmed by her antics. Scared shitless, I'm telling you.

- ** [You study econ?]->SamEcon

=== SamEcon ===
Unfortunately, I do in fact have that pleasure.
    *[You don't seem thrilled]
        I try not to. But, I assure you, I have nothing but the most righteous of end goals. Take em down from the inside and all that.
    *[What made you choose?]
        Technically, I didn't. But dad pays for this outrageous tuition, so... here I am.
    - **[-Continue-]-> guess
    
= guess    
What about you, what are you studying? Wait, let me guess. Definitely liberal arts, no one back home is stressing about their future. Hmm... comp lit?
    *[You guessed it]
        ~samConnection++
        It's written all over your face. "I'm just here to read books and have stimulating discussions." You lucky bastard.
            **[-Continue-] -> semesterEnd
    *[Bio, actually]
        Oh don't tell me you're premed too. It's what my sister's doing, and she's won't shut up about it.
    *[Nope, sociology]
        Aha, a.k.a. anthropology of the European urban poor.
    *[Computer Science]
        I spoke too soon. You're clearly meant to be your parents' retirement plan.
    - **[-Continue-] -> semesterEnd
    
            
= semesterEnd
How's the end of the semester looking for you? Finals stressing you out?
*[A bit]
*[It's gonna be brutal]
*[Why do you care?]
- ->SamDrugs

-> END

=== SamDrugs ===
It's ok to act from a place of fear, worry, anxiety, whatever you want to call it. It's what actually makes the world go round. All those stuck up dipshits in the Econ department are deluding themselves.
    *[What are you saying?]
        They're all just symptoms you can treat. Say what you will, those premed losers on the otherhand are actually onto something.
        **[Sure, yeah...]-> contact

= contact
Stress, lack of focus, loosing sleep, they all have an easy fix. If you ever find yourself needing an easy fix, text me.
*[Like a massage?]
    Sure yeah. A massage in an unmarked plastic baggy.
*[Offering me drugs?]
    ~samConnection++
    Real subtle. But yes.
-<> Interested?
    *[Need your number first]
        < Sam takes your profered phone and saves a number as "The Econ Major." > 
    *[No thanks]
        Suit yourself. If you change your mind, just ask Riley you need help with Econ homework. She'll put you in touch.
-*[-Continue-] ->SamDefault


=== SamDefault ===
< Sam just raises and eyebrow. You're unsure if it's an acknowledgement or a dismissal. >
    +[-Leave-]
    {quitDialogue()} 
    -> SamDefault


=== function quitDialogue ===
    ~return