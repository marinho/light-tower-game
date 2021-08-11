### JaKayne

* Player movement is not very good with little movements. I would suggest to implement an acceleration value, so it takes about a second to get to full speed.

### Cammin

* When walking with the analog stick, the animation plays at the same speed. not an issue, but there's an opportunity for polish by matching the animation speed to the player speed.
* I noticed some tile seams on my 1020x1080 monitor. Though, I had to go out of my way to see this, so not a huge problem. Might be solvable with a sprite atlas, and assigning all of the tile's sprite assets to the atlas.
* The character felt jittery when moving. I think this is happening because the camera is updating its position in Update instead of FixedUpdate, which is something I've personally seen happen to me before. (assuming the character updates in FixedUpdate)
* I noticed that the player animates a little weird when walking on the bridge. but it's always tricky to try and make this perfect, haha
* Perhaps the game can force me to talk to someone to progress so that I know I can talk to people?
* I was hoping I could talk to people after I have already talked to them. sometimes I forget what they were talking about, like figuring out how to get the man's bird
* I hit the maximum cap of how many music boxes I can have active at a time, but I didn't realize that and I thought one of the music boxes wasn't working. maybe provide some feedback like a sound effect or UI twitching to indicate I hit the cap
* The music doesn't loop seamlessly. I suppose it just takes some tuning.
* The ladies still looked unhappy even though they were united, so I was confused if their quest was fulfilled.
* When talking to people for dialogue, perhaps there could be something extra visual to indicate that they have something new to say. I wasn't aware I finished the man's bird quest until I tried one extra time (oops, haha)
* I noticed that there were some orange rectangles at grandma's house. They might have been platforms that were accidentally left visible?
* After winning the game, i walked down and turned off a music box, which disabled my movement for a few seconds, bug?
* Ah, i pressed escape and i realized that i can see everyone's quest in a list. Perhaps some kind of way for the game to tell me this, but not a problem.

edit: There was one other thing that occurred to me that i forgot about; when i jumped up against the invisible wall next to the tower, i stuck to it. I think that giving the player a frictionliess physics material can help with sliding on the wall instead

