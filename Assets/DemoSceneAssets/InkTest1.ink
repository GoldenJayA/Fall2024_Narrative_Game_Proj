-> test1

=== main ===
What is your quest?
    + [to seek the holy grail]
        But are you worthy?
        -> main
    * to cross the bridge
        Oh, how disappointing.
        ->main
    * to get home safely
        Then turn back.
        ->main
    * ->
    The bridge keeper will hear nothing more from you.
 
 -> END
 
=== test1 ===
# speaker = Bystander
 You can't be too careful these days.
    *[Who are you?]
        -> who
    *[You saw what happened?]
        -> what
    *[Why did you come here today?]
        -> why
    *-> END
    
 ===who===
 # speaker = Bystander
 I'm just a bystander, the most dangerous kind of person in these situations.
    * [I mean your name.]
        Oh that. Mary should do. But I might as well be Margret or Maybel. You'll get the same story from any of us.
            ** [I have a different question.]
                -> test1
    * [I have a different question.]
        -> test1
        
   
    
 
 ===what===
 # speaker = Bystander
 Yes, it was dreadful. But not as dreadful as the truth.
  * [Tell me more.]
        He was just some beggar. He was raving in a corner. Clawing at the pavement. The bloody fingerprints were like the clawmarks of a caged animal.
            ** [And then?]
                And then he was dead. I heard him weep, then gasp out a name. "Marley" I think it was.
                **** [I have another question.]
                                -> test1
  * [What truth?]
        All of us here could have stopped it. And none of us lifted a finger.
            ** [So you admit you're guilty!]
                Guilty only of apathy. If that's enough to lock me up, then I'm afraid you'll need to arrest most of the townsfolk.
                **** [I have another question.]
                                -> test1
                
            ** [I'm sure you would have done something.]
                How have you gotten this far with that kind of trust in humanity?
                **** [I have another question.]
                                -> test1
    * [I have another question.]
        -> test1
 
 ===why===
 # speaker = Bystander
 Oh, I don't know. Just seemed like something to do.
    *[Being vague isn't helping you prove your innocence.]
        Oh I'm not trying to prove anything like that, quite the contratry
            ** [Are you admitting to the crime?]
                Oh no, not the crime. But perhaps a crime, a crime against humanity, a moral failing of our society.
                    *** [What are you talking about?]
                        The indifference of respectability. Minding ones own business even when a poor sod is dying in the street.
                            **** [I have another question.]
                                -> test1
 
 -> END