VAR NPCName = "??"
VAR taylorConnection = 0
VAR talkedTaylor = false
VAR pongScore = 0

INCLUDE GlobalVariables.ink

EXTERNAL quitDialogue()

{ talkedTaylor == false: -> TaylorStart | -> TaylorDefault}

=== TaylorStart ===
~NPCName = "??"
< You hear the people around the pong table chanting, "Taylor! Taylor!" A tall guy in an Eagles' letterman jacket finishes his drink with a prodigious gulp and slams his solo cup down triumphantly. He beckons to you. > 
    *[-Continue-] ->startChoices

= startChoices
~NPCName = "Taylor"
“Yo! You look like you’ve got game. Wanna play some pong?”
    *[Sure thing!]-> TaylorPongStart 
    *[I don't have a partner] -> TaylorPongStart           
    *[No thanks, I'm terrible]
        Suit yourself, I guess.
        **[-Leave-] -> TaylorDefault
            ~quitDialogue()
        

=== TaylorPongStart ===
~NPCName = "Taylor"
You're playing with me. I'm Taylor by the way.
*[-Introduce yourself-]
    Oh yeah, Alex's friend. Glad you could make it. Here, newcomers go first. -> smallTalk

= smallTalk
*[-Try to make the shot-]-> PongGame

= firstShot
{PongGame: Taylor squints at the other end of the table, lining up his shot.}
*[Eagles fan?] -> Eagles
*[Alex mentioned me?] -> Alex

= Eagles
    You know Robert De Niro's character in Silver Linings Playbook?
    *{playerInterest == "movies"}[*Movie buff* That bad?] 
    *[Never seen it.]
    - ->TaylorInterrupt

= Alex
    Just that he was excited you were back. You go abroad last semester?
    *[Yeah, something like that] -> abroad
    *[Not exactly] ->leave

= abroad
Nice. Always wanted to go abroad, but my mom... anyway, where'd you go?
*[-Lie- Barthelona]
    Always wondered why people say it like that when they come back.
*[-Lie- Mozambique]
    Oh wow, definitely could not tell you where that is on a map. What was it like?
    **[Hot]
    **[Not too different from here]
    **[Food was spicy]
*[I actually had to take medical leave]
    {
    - not TaylorInterrupt:->TaylorInterrupt
    - else: ->TaylorInterrupt1
    }
- **[Um...]->TaylorInterrupt

= leave    
You take medical leave or something?
*[-Lie- No, internship]
    Good for you. What did you do?
    **[Paperwork and files]
    **[Answered the phone]
    **[Meetings, lots of meetings]
    -- ~taylorConnection-- 
        -> TaylorInterrupt
*[How did you guess?]
    Thought about doing it myself last semester. But here we are.
    **[You ok?]
    **[Why didn't you?]
- -> TaylorInterrupt


=== TaylorPong1 ===
< Taylor turns back to the game and effortlessly lands his ball in one of the cups across the table > Nothing but net. You're up.

*[-Try to copy his shot-]-> PongGame

= secondShot
< Taylor's drinking from his solo cup. >
*[How's Henderson's class?] -> class
*{not TaylorPongStart.Alex} [Alex mentioned me?] 
    ->TaylorPongStart.Alex

= class
Bro, not you too! It's kicking my ass, ok? And I don't want to think about it until after the Birds play on Sunday. Is that too much to ask for?
    *[-Apologize-] ->TaylorInterrupt1
    *{TaylorPongStart.Eagles} [*Movie buff* Silver Linings Playbook?] -> movie
    
    
= movie
Ha. Kinda weird, but mom's alot like De Niro in that movie.
    *[An obsessive fan?]
    *[OCD and superstitious?]
- Something like that. But she's also a crazy successful private equity banker, so I guess nothing at all like him too. Biggest Eagles fan I know though.
    *[She helps with problem sets?]
        She did... I mean, yeah, she does.
        **[She ok?] -> TaylorInterrupt1
    *[Pressure to follow her?]
        Nah, I've always wanted to get a business degree.
        **[-Continue-] -> TaylorInterrupt1

=== TaylorPong2 ===
Head in the game. We've got this in the bag. We get a re-rack, what do you think?
    *[Stoplight]
    *[Triangle]
    - ->lastShot

= lastShot
Good choice.< To the other team> You heard my friend.
    * [< Take your shot >]-> PongGame

= dating
< Taylor is taking aim for the last shot of the game >
    * [You two dating?]
        She definitely wants to be.
        ** [And you don't?]
            I have a lot on my plate right now. Things at home... nevermind
        ** [Oof, savage]
            Look, I don't need you throwing shade. I'm trying not to lead her on, but you saw how she was.
        -- ***[-End the pong game-] 
         -> TaylorPongEnd

=== TaylorPongEnd ===
{pongScore >= 3: < You won. > Told you we'd crush it. Knew you had it in you. Imma go grab a refill.}
{pongScore < 3: < You lost. > We could have played better. Good effort. Imma get another drink.}

*[-Continue-]
    ->TaylorDefault

=== TaylorInterrupt ===
~ NPCName = "??"
< Before Taylor can respond, another guy walks over and dabs him up >
*[-Continue-]
- Bro, what's good! You finish that problem set for Henderson yet?
    *[-Continue-]
- ~NPCName = "Taylor"
Not now Axel, let me make this shot.
*[-Continue-]
- ~NPCName = "Axel"
The first problem took me and Jessie two hours, but I think we crushed it. Think we could get your mom to look it over when you call her this weekend?
*[-Continue-]
- ~NPCName = "Taylor"
Dude, it's Friday night, what are you doing bringing up that shit? Go have a beer, or stay here and watch us crush these fools at pong. But stop talking about fucking homework.
*[-Stay quiet-]
*[Taylor's gotta focus, Axel]
    ~taylorConnection++
-~ NPCName = "Axel"
Alright you cocky son of a bitch. I'll grab a drink and brb.
*[-Continue-]
    ~NPCName = "Taylor"
    ->TaylorPong1


=== TaylorInterrupt1 ===
< Taylor gets interrupted by a small knot of people that shoulder their way around the pong table. One of them, a girl with curly brown hair holds out a nearly empty solo cup to Taylor. >
    ~ NPCName = "??"
    *[-Continue-] ->Gabriela

= Gabriela
Come on, Taylor, take a shot, it's my birthday!
* [-Continue-]
    ~ NPCName = "Taylor"
    Cheers Gabriela! Happy birthday!
    ** [Happy birthday, Gabriela!]
        ~ NPCName = "Gabriela"
        < She smiles briefly at you, and turns her attention back to Taylor. >
            ***[-Continue-] -> mom
    ** [-Stay quiet-] -> mom

= mom
~ NPCName = "Gabriela"
Your mom still coming up this weekend? Would love to see her. Wanna grab dinner on Sunday, the three of us?
    *[-Stay quiet-]
        ~ NPCName = "Taylor"
    *[It's Taylor's shot, Gabriela]
        ~ NPCName = "Gabriela"
        < Gabriela ignores you. >
        ** [-Contine-]
            ~ NPCName = "Taylor"
 - She can't make it this weekend.
    *[-Contine-]
        ~NPCName = "Gabriela"
- Aw, that's too bad. You usually call her on Sundays, right? What if I popped in to say hi to her?
    *[-Stay quiet-] ->TaylorUneasy
    *[Gabriela, not now] -> disaproves

= disaproves
~NPCName = "Gabriela"
~taylorConnection++
< She gives you a withering look. Taylor looks relieved to have her attention off him for a bit. >
*[-Continue-] -> leaves
    
= TaylorUneasy
~NPCName = "Taylor"
~taylorConnection--
<Taylor looks uneasy> Um, yeah, sure Gabriela.
    *[-Continue-]->leaves


= leaves    
~NPCName = "Gabriela"
Anyways, come out with us later, ok? Not every night your girl turns 21. Gotta go. Text me, yeah? < She leaves with her friends. >
    *[-Continue-]-> TaylorPong2
    ~NPCName = "Taylor"
    

=== PongGame ===
{ ~ < Your ball goes straight into a cup. Nice! > ->increaseScore | < Your ball bounces off the table and into the crowd. Better luck next time. > ->turnBack }
= increaseScore
~ pongScore++
-> turnBack

= turnBack
    * {TaylorPongStart.smallTalk} [-Turn back to Taylor-]-> TaylorPongStart.firstShot
    * {TaylorPong1} [-Turn back to Taylor-]-> TaylorPong1.secondShot
    * {TaylorPong2} [-Turn back to Taylor-]-> TaylorPong2.dating
    

=== TaylorDefault ===
~ talkedTaylor = true
{ 
    -TURNS_SINCE(->TaylorDefault) <= 0: < Taylor nods to you and lifts his solo cup. Before he can say anything, another partygoer cuts you off and acosts him. >
    -TURNS_SINCE(->TaylorDefault) >= 0: {~< Taylor looks up from scrolling on his phone and raises his chin in greeting. You look behind you see it was meant for someone else. > | < Taylor nods to you and lifts his solo cup. Before he can say anything, another partygoer cuts you off and acosts him. >}
}
    + [-Leave-]
    {quitDialogue()} 
    -> TaylorDefault


===function quitDialogue===
   ~return