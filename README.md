# SnoopyTheAdventureInUniqlo
## Table of Content
* [Introduction](Introduction)
* [Gameplay](Gameplay)
* [Download/Setup](Download/Setup)
* [Implementation](Implementation)
* [Reference](Reference)

## Introduction
This game is created using Unity C#. The aim of the game is to control Snoopy by following the arrow signs so that he will do the right pose and let the gate pass thorugh him safely.

## Gameplay
![image](https://user-images.githubusercontent.com/9387781/68892906-ea9eab80-071b-11ea-91b9-a39be4abb5c9.png)
![image](https://user-images.githubusercontent.com/9387781/68905062-cea90300-0737-11ea-9ed3-00e78c37de2e.png)
1. Read the Arrow Signs on the ground
2. Draw on the screen the direction of the Arrow Signs
3. Snoopy will perform the correct pose
4. The gate will pass thorugh Snoopy safely

## Download/Setup
[Download APK](https://drive.google.com/open?id=1OlmOLiPiV_8hWz28bV9pCWCmoL3Cf5tO)

To Run the Project: 
1. Clone this git repository
2. Open Unity
3. Select Open Project
4. Select the project folder `~/SnoopyTheAdventureInUniqlo`
5. Click Open

## Implementation
### Game Logic
`GameMode.cs`

### Gesture Recognition
`SwipeHandler.cs`

### Skinned Animation
`SnoopyAnimationController.cs`

### Scene Transition
`SceneManager.cs`

### SFX & BGM
``
PlaySoundThenLoad
audio.Play();

### High Score
`ScoreHandler.cs`
The highscore is only stored in game instant, not in external file

## Reference
1. Menu BGM taken from: `https://www.youtube.com/watch?v=x6zypc_LhnM`
2. Level BGM taken from: `https://www.youtube.com/watch?v=YFFKuOBOixM`
