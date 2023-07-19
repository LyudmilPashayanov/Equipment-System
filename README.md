# Equipment system

## Vertigo technical assignment
The "Equipment system" project was a technical assignment created for Vertigo Games. Its aim is to assess my technical knowledge.

## Technical Description
The project was developed using C# on the Unity Engine 2022.3.4f1 LTS version. The app was tested and works in landscape mode on Windows and in the Unity Editor.

## Extra Plugins
For the animations in the project it was used the DoTween plugin and the Unity Input System to handle Input from different devices. 

## How to run?
You can run the app/game from:

- Inside the Unity Editor with version 2022.3.4f1.
  OR
- Build the game for Windows from the Unity Editor.
  OR
- Download it from the following link: https://drive.google.com/drive/folders/1bOesEzbW3ulZXOI5TiFp_Pf-mAVUj8k0

## About the game  
The Equipment System game, was done as a PC prototype, which eventually will be ported for VR. In the game you can interact with a number of items: Shoot a target with a gun; reload the gun with an ammo-clip; equip a hat on your head; Throw a rock; Pull Levers to play nice music and spawn stuff.
Try to get the maximum number of score(200) with the use of only a single ammo-clip! (it ain't easy)

## Cool technical stuff
- The game utilizes the "Unity Input System". All the functionality, which requires any input from the user, was done with the vision to easily add/change the Input Actions(buttons) in the Input System, so that it can also run in a VR environment, without that much of refactoring.
- MVC pattern used to handle Items game objects in the game.
- SOLID principles were taken consideration and used when applicable, so that the app can reuse its code as much as possible and allow maintainability and flexability.
- Factory Pattern to create Bullets and Pooling technique inside the factory in order to optimize memory.
- Singleton pattern for an Audio Manager, so that you can play sounds from anywhere in the code + Quality music inside.
- All the interactable items in the game can be thrown, depending on the direction the player is moving the currently used Input Device.
- Lever component in the game which works, just as any UI input component from Unity (UI Button for example). You can reuse the Lever anywhere in the game. Just subscribe to the lever OnPulledEvent and when the lever is pulled, it will Invoke() the event you have added!

## TO-DO (Known Flaws)
### Code

- Currently the MVC pattern is not strictly followed. The Controllers in my project, do inherit from MonoBehaviour and do handle some Unity logic. This doesn't allow for unit testing or reusability in Unreal for example. Eventually, if we want to have only the business logic in the controllers, we can remove their MonoBehaviour inheritance and construct them (with their needed references) in their specific ItemView class.
- Currently we don't have LevelManager for starting the needed level and spawning the Player in it. This could eventually happen when we have a system to save and load the Player progress.
- Currently we don't have any persistency in the game. Every time the game starts from the same place and all the "progress"(there isn't much progress anyway) is lost.
  
### In-game
- Dropping an item out of the map, when you go near the wall and drop the item behind the wall.
- Spawning too much items from the spawner (especially flashlight and turning it on) will eventually run the app out of memory.

## Early designs
Before I start implementing I always try to design upfront. Here are some of my early design sketches:

<img src="https://user-images.githubusercontent.com/41620452/254587208-8c7fa72d-4779-4baa-b05e-a2c6276711bb.jpg" alt="My Image" width="500"/>
<img src="https://user-images.githubusercontent.com/41620452/254587241-b963d531-3d21-4dc9-bed8-83e5016b375a.jpg" alt="My Image" width="500"/>
<img src="https://user-images.githubusercontent.com/41620452/254587187-b7a1e2c7-337c-4ee1-b3e6-e2d666f75edb.jpg" alt="My Image" width="500"/>
<img src="https://user-images.githubusercontent.com/41620452/254587219-c7fd9f0b-dd13-4707-b21b-94ce1dc90cdb.jpg" alt="My Image" width="500"/>
<img src="https://user-images.githubusercontent.com/41620452/254587230-9938035d-76ca-47b1-88ef-7e1d685b34db.jpg" alt="My Image" width="500"/>

## Questions
For further questions about the game, please refer to its codebase, where almost all functions are commented and explained. For more information, please contact me.
