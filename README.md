# MMP_Project
MMP SS21 Gruppenarbeit


14.06.21 - Alex
Pushed into new branch: Code Base.

 Major Changes:
 Reworked and debugged my MVP movement system. Build on the KinematicObject.cs of a tutorial (extensively studied and commented). 
 Coded a PlayerController.cs that inherits from it.
 Currently there are two PlayerController scripts, one with and one without the use of JumpStates, both work. Jet there remains an illusive
 inconsistency which causes the jump path to get overly compressed, otherwise it's very floaty. Can't fix that till i understand it...
  
 Minor Changes: (what i remember)
 - deleted all floor tiles, recreated them as a tile map.
 - adjusted "pixel per unit" for the floor graphics to fit into the squares.
 - adjusted DeathZone to only respawn player.
 - placed a few more tiles to expand test world.

 Note 1 : Be aware: The player is now Kinematic, not Dynamic. As such, the FallingBlock will not react to us hitting it. That remains to be coded manually.
    Clarification: It would react, if we could hit it. But my collision detection is preemptive not reactive, the player stops just shy of hitting it.

 Note 2 : Both PlayerController.cs are not commented. I hope to finish that tomorrow morning. My main goal was to get it somewhat running.
