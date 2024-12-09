VAR NPCName = "Alex"
VAR alexConnection = 0
VAR introducedAlex = false
VAR RowanStatus = ""
VAR RowanPronoun = ""

INCLUDE GlobalVariables.INK
EXTERNAL quitDialogue()

{ introducedAlex == false: -> AlexIntro | -> AlexDefault}

//Alex is going to serve as a tutorial for the game's mechanics

=== AlexIntro ===
~NPCName = "Alex"
You'll thank me for this later. It's been two months since you came back from medical leave, and every Friday night your just in the dorm. I mean, what were you going to tell your family at Thanksgiving? 
    *[I like staying home]
    *[I've been studying]
    *[It's hard, being back]
        Listen, I get it. I figured it was important to give you some time to get readjusted, but come on, we're like three weeks away from finals.
            **[I know, it's just...]
    - You're letting the best years of your life roll past you. I won't let a friend of mine get left behind like that.
    *[-Continue-]
        Plus, Rowan will be coming later!
        **[Wait, really?] -> RowanSetUp
= Rowan
~ NPCName = "Alex"
Yeah, {RowanPronoun} told me {RowanPronoun} got back early from France and would stop by the party tonight around midnight. I'm sure {RowanPronoun} would be thrilled to see you!
    *[So about this party...]-> party
    
= party
Look, I know big social gatherings stress you out. But you gotta just dive in and mingle. Here, let's give it a try. I'll say something like, "hey, this is my friend Kai..."
                // regardless of choice, this bumps Alex's connection score, giving the player at least one tether as they enter the party
    *[Great to be here!] //flesh out later
    *[What's good, fam?]//flesh out later
    *[Can we not right now?]//flesh out later
    - ~alexConnection ++
    - < You're grateful for Alex trying to boost your confidence. The connection between you two in this moment is calming your mounting anxiety as you approach the party. >
            **[-Continue-]-> tutorial
            
= tutorial 
~ NPCName = "Set up"
< You've just formed a connection with Alex. You can form connections with other partygoers by interacting with them and making dialogue choices they would approve of. >
* [-Talk to people, got it-]
- < Connections with characters will appear as a line linking you two together. >
* [-Lines between characters, ok-]
- < When you interact with a character you have a connection with, your anxiety will go down. >
* [-Connections lower anxiety, roger-]
- < The more you interact with them however, the less your connection to them will lower your anxiety. >
* [-Diminishing returns, I see-]
- < If your anxiety gets too high, you won't be able to stay at the party and will miss seeing Rowan. >
* [-Don't want that-]
- < It's now 9 pm. Alex said Rowan would be here at midnight. You just need to keep your anxiety in check until then. >
* [-Make it to 12 pm, got it-]
- < Walk around the party with ASWD. When you are near a character, press SPACE to talk with them. >
* [-ASWD and SPACE, easy-]
- ->AlexInterests

=== RowanSetUp ===
~NPCName = "Set up"
< You've been having a hard time reconnecting with people after your leave, especially because Rowan has been studying abroad. Getting the chance to see Rowan again makes your heart leap, because...>
*[You have a crush]
    ~ RowanStatus = "crush"
   < You were working your way to admitting feelings for Rowan, but then you had to leave Smithfield College, where both of you are students. <>
*[You miss your oldest friend]
    ~ RowanStatus = "friend"
    < Rowan was your go to person at Smithfield College, where you both study. <>
-  Rowan uses...>
*[he/him pronouns]
    ~ RowanPronoun = "he"
*[she/her pronouns]
    ~ RowanPronoun = "she"
*[they/them pronouns]
     ~ RowanPronoun = "they"
- -> AlexIntro.Rowan

=== AlexInterests ===
~ NPCName = "Alex"
One of the things I like most about you is your passion. Someone else is bound to dig that too. Better yet, they might even like what you're into as well.
    *[-Consider for a moment-]
    ~ NPCName = "Set Up"
    < The next choice will determine your character's special interest. This will open up additional interactions with other characters you meet at the party >
        **[< Bookworm >]
           ~playerInterest = "books"
        **[< Movie buff >]
            ~playerInterest = "movies"
        **[< Theater kid >]
            ~playerInterest = "theater"
        -- -> interestsEnd
= interestsEnd
~ NPCName = "Alex"
Don't doubt yourself. You'd be surprised what people are into!
*[-Continue-]-> aboutToEnter

=== aboutToEnter ====
Alright, we're here. Deep breath, and just remember this: start lighthearted, a joke or some banter. Always, try to ask at least one question, people looove to talk about themselves.
    *[Jokes and questions, got it]
    *[Easier said than done]
- Lastly, if ever you need to bail on a conversation say you have to "refill your drink".
*[-Continue-]
-> AlexStart

=== AlexStart ===
~ introducedAlex = true
You'll do great! See you on the other side!
    *[Thanks for the vote of confidence]
    *[Other side? What do you mean?]
    - < Alex winks, and drags you inside > Gotta go see about a girl. < Alex disappears into the crowd. >
        **[-Continue-] ->anxiety

= anxiety
~ NPCName = ""
< Your anxiety starts mounting again. If it gets too high, you know you will have to leave. To lower your anxiety, find someone to connect with. You really want to stay until Rowan gets here!>
+[-Leave-]
{quitDialogue()}
->AlexDefault

=== AlexDefault ===
~NPCName = "Alex"
Come back in a minute, kinda in the middle of something
+ [-Leave-]
{quitDialogue()}
->AlexDefault

//this function is empty because it tells Unity to do stuff
===function quitDialogue===
    ~return