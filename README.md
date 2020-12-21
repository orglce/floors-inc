# Project Title

Name: Luka Dragar  

Student Number: D20124481 

Class Group: TU858  

# Description of the project

The game consists of three main stages and is played in first person with the usual controls. The player is progressing through the stages until reaching the end. 1. We start in a randomly generated maze and our goal is to find a button that will teleport us to our next stage.
2. After the teleport we end up in a room with four paths. Each path seems to lead back to the main room??? But if we go back into the path we came from 
we end up in the final stage.
3. The final stage seems to be an empty room. As we start walking towards the exit music starts playing. As we climb onto the platform we can observe some visuals reacting to the music. 

The game was partly inspired by the concepts present in The Stanley Parable. Particularly the confusing teleporting second stage and the final platfrom with the music. 

The part that inspired the ending: https://youtu.be/EEvR4gqqrsI

# Instructions for use

Everything should work out of the box. The "completion" of the game realies on the exploration of the user.

# How it works

The game is played in the first person using CharacterController component in Unity. [1]

#### First stage

Random maze generation is used to dynamically create the first stage. The algorithm used is frequently called recursive backtracker [2]. It was implemented using stack. The maze consists of walls and emissive lights that help to see the maze better during navigation. When we reach the button we initiate the teleport with the help of a Raycast that is shot towards the button and checks if we clicked the button. [3]

#### Second stage

This stage was prebuilt using ProBuilder addon in Unity. It uses four different planes that act as a trigger and teleport the player to the identical hallway on the other side of the floor and keep the illusion of continous walking. The teleportation itself is very simple. We save the relative position to the plane and teleport the player to the same exact relative position but next to the other plane.

#### Third stage

Consists of an empty floor and a platform from which we can observe the music visualizations. The platform fence is generated dynamically using a script. 
When we are walking towards the platform music volume slowly starts going up until finally reaching 100% when we get on the platform. AudioAnalyzer script was copied from the 'Game Engines Examples 2020' project on Github.

The two visualizations consist of stars resizing and rotating the the spectrum of the music and a few randomly generated clusters of points that are traversed by a cube with a TailRenderer that also gets resized to the spectrum of the music.

# References

[1] https://www.youtube.com/watch?v=_QajrabyTJc&t=1066s
[2] https://www.youtube.com/watch?v=Y37-gB83HKE&t=458s&ab_channel=javidx9
[3] https://www.youtube.com/watch?v=_yf5vzZ2sYE

'Game Engines Examples 2020' project on Github
https://github.com/skooter500/GE1-2020-2021/tree/master/Game%20Engines%20Examples%202020

# What I am most proud of in the assignment

Probably the implementation of the maze. The algorithm itself is not that dificult but the generation of the walls and emissive lights took me some time.

# Demo

[![YouTube](https://youtu.be/V6rPB2iACLo)
