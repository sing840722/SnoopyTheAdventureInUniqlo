# SnoopyTheAdventureInUniqlo
## Table of Content
* [Introduction](#introduction)
* [Gameplay](#gameplay)
* [Download/Setup](#download-and-setup)
* [Implementation](#implementation)
* [Reference](#reference)

## Introduction
This game is created using Unity C#. The aim of the game is to control Snoopy by following the arrow signs so that he will do the right pose and let the gate pass thorugh him safely.

## Gameplay
![image](https://user-images.githubusercontent.com/9387781/68892906-ea9eab80-071b-11ea-91b9-a39be4abb5c9.png)
![image](https://user-images.githubusercontent.com/9387781/68905062-cea90300-0737-11ea-9ed3-00e78c37de2e.png)
1. Read the Arrow Signs on the ground
2. Draw on the screen the direction of the Arrow Signs
3. Snoopy will do the correct pose
4. The gate will pass thorugh Snoopy safely

Video of Playing the Game: **https://www.youtube.com/watch?v=dteUYIC9xf8**

## Download and Setup
[Download APK](https://drive.google.com/open?id=1OlmOLiPiV_8hWz28bV9pCWCmoL3Cf5tO)

To Run the Project: 
1. Clone this git repository ``https://github.com/sing840722/SnoopyTheAdventureInUniqlo.git``
2. Open Unity
3. Select Open Project
4. Select the project folder ``~/SnoopyTheAdventureInUniqlo``
5. Click Open

## Implementation
### Game Logic
`GameMode.cs`
* Spawn random printer automatically
* Spawn a set of random arrow sign
* Constantly add score when Snoopy is alive

### Gesture Recognition
`SwipeHandler.cs`
* Determine if the swipe a straight line by checking the Gradient between Initial Touch Position and End Position
* Determine which direction the swipe is by comparing the Inital Touch Position and End Position

### Skinned Animation
`SnoopyAnimationController.cs`
* The skinned animation is attached on the Character
* The script select which animation to play
* Then rotate the character

### Scene Transition
`SceneManager.cs`

Only One-way Scene Transition:
1. Menu -> Level
2. Level -> Gameover
3. Gameover -> Menu

A switch method based on the current scene and is called when a touch is made

### SFX & BGM
`Printer.cs`
* Using the Unity function StartCoroutine to Play a Sound Then Load level
* Load and Play audio

### High Score
`ScoreHandler.cs`
* Constantly update the UI Score
* Record the new high score if current score is higher then history
The highscore is only stored in game instant, not in external file.

## Reference
1. Menu BGM taken from: `https://www.youtube.com/watch?v=x6zypc_LhnM`
2. Level BGM taken from: `https://www.youtube.com/watch?v=YFFKuOBOixM`
