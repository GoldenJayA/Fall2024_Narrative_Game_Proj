VAR NPCName = "Alex"
VAR alexConnection = 0
VAR introducedAlex = false
VAR interest = ""

EXTERNAL quitDialogue()

{ introducedAlex == false: -> AlexIntro | -> AlexDefault}

//Alex is going to serve as a tutorial for the game's mechanics

=== AlexIntro ===
You'll thank me for this later. I mean, what were you going to tell your family at Thanksgiving? "College is pretty boring, actually I've spent most of my time in my dorm room."
    *[Pretty much, yeah]
    *[I've been studying]
    *[They won't care]
    - You're letting the best years of your life roll past you. I won't let a friend of mine get left behind like that.
        **[So about this party...]
            Look, I know big social gatherings stress you out. But you gotta just dive in and mingle. Here, let's give it a try. I'll say something like, "hey, this is my friend Kai..."
                ***[< Placeholde 1 >] //flesh out later
                ***[< Placeholde 1 >]//flesh out later
                ***[< Placeholde 1 >]//flesh out later
                -- ~alexConnection ++
                -- < You're grateful for Alex trying to boost your confidence. The connection between you two in this moment is calming your mounting anxiety as you approach the party >
                    ****[Continue]
                        < Additional tutorial message >
                        ***** [Continue]
                        --- ->AlexInterests

-> END

=== AlexInterests ===
One of the things I like most about you is your passion. So someone else is bound to dig that, and better yet, even share it.
    *[Consider for a moment]
    < The next choice will determine your character's special interest. This will open up additional dialogue choices for other characters you meet at the party >
        **[< Interest A >]
           ~interest = "A"
        **[< Interest B >]
            ~interest = "B"
        **[< Interest C >]
            ~interest = "C"
        **[< Interest D >]
            ~interest = "D"
            -- You'd be surprised what people are into!
                ***[Continue]
                    Alright, we're here. If nothing else, just remember this: start lighthearted, a joke or some banter. Next, try to ask at least one question, people looove to talk about themselves.
                        ****[Jokes and questions, got it]
                        ****[Easier said than done]
                        --- Lastly, if ever you need to bail on a conversation say you have to "refill your drink".
                            *****[Continue]
                                -> AlexStart

=== AlexStart ===
~ introducedAlex = true
You'll do great! See you on the other side!
    *[Thanks for the vote of confidence]
    *[Other side? What do you mean?]
    - < Alex winks, and drags you inside > Gotta go see about a girl.
        **[Continue]
            -- < Alex leaves. Your anxiety starts mounting again. Try to find someone to talk to before it gets too high >
            ++[Leave]
            {quitDialogue()}
            ->AlexDefault

=== AlexDefault ===
Come back in a minute, kinda in the middle of something
+ [Leave]
{quitDialogue()}
->AlexDefault

//this function is empty because it tells Unity to do stuff
===function quitDialogue===
    ~return