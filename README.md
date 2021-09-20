# Black Hole

## About the Game
Black Hole is a simple turn based board game where two players competitively place numbered tokens on the board until there is one empty cell left, this cell is called the black hole and the surrounding scores are counted, whoever has the lowest score after the score is counted wins the game.

### Contributors:
This version of Black Hole was created by Daniel Mayo.

Inspired by Wal Joris (https://nestorgames.com)

## Build Steps:
The project can currently be built for both Windows, WebGL and Android in the following ways:

* **Manual:** Via the Unity Engine Build Settings.
  * Open the project in Unity
  * Select `File->BuildSettings`
  * Switch to the desired build platform (Windows, WebGL or android)
  * Select `Build`
  * You will be prompted to select an output directory
  * Once the build has finished open your chosen folder to find your build

* **Automated**: `build_all.bat` will build a `PC`, `WebGL` and `Android` version of the project.
  * Double click on `build_all.bat`
  * The process will take some time, leave the console window open.
  * The following files will be produced:
    * PC Build: `builds/pc/BlackHole.exe`
    * WebGL Build: `builds/web/index.html`
    * Android Build: `builds/android/BlackHole.apk`

* **Automated**: Upload to GitHub Pages using `gh-pages-deploy.bat`
  * Double click on `gh-pages-deploy.bat`
  * The process will take some time, leave the console window open.
  * Open the GitHub Pages link to open the game online.

## Daily Builds:
Daily Builds of the project are created and stored on my (Daniel Mayo) computer.

# Credits:
#### Special Mentions
- Aaron Cox (Teacher)
- Benjamin Scott (Tester)
- Samnang Yorng (Tester)
- Wilson Khan (Music)
