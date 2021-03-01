# Snake game 
  Recreation repository of the famous "Snake game", using C # as a base. This repository contains 4 projects:
- **Snake.Logic**: The main logic of the game is the main features.
- **Snake.Console**: main console for development and testing assistance.
- **Snake.Logic.Graphics**: Extension of the main logic that adds graphics to the game.
- **Snake.App**: Game extension for Android.

# News from Versions 
# **1.0** Logic Create
Creating the main logic is a test.
- **Features**
	- The apple adds size to the snake's body.
	- The apple consistent with game speed.
	- Wrote solid objects that "kill" a snake.
	- The snake's body "kills" itself.
# **1.0.1** Bug fixes
Bug fixes for version ** 1.0 **

- **Bug Fix:**
	- Fixed bug that the snake's body did not follow it.
	- Fixed bug that the apple did not add size to the snake's body.
	- Fixed bug that when adding size to the snake's body it stopped following.
# **1.2.1** Creating Graphics
Beginning of the construction of the graphic part of the game.
- **Features**
	- The graphic system already generates a background image corresponding to the following:
![Image generated by the systems in the root folder of the application console for name 'Background.jpeg'](https://raw.githubusercontent.com/JuanDouglas/Snake-APP/master/Images/first_background_result.jpeg)
The image is already generated at the resolution requested in **UI (Platform: platform, int: Width, int Height)**
  - Apples now don't slow down the game. 
  -  Standard game update time is **750 milliseconds.**

# **1.3.2** Rework for graphic project.
Started working for rework graphic project with objective for construct the **Android App.**

# **1.5.4** Working for Graphic Game
- **Bug Fix:**
	- Fixed bug that all objects were null.
	- Fixed bug that graphic objects could not be treated as game objects.
- **Features**
	- In-game objects are automatically converted to graphic objects.
	- Graphic objects now drawn are saved in OutPut.jpeg
	![Image generated by the systems in the root folder of the application console for name 'OutPut.jpeg'](https://raw.githubusercontent.com/JuanDouglas/Snake-APP/master/Images/first_draw_result.jpeg)