# Get Started with Storm
> By the end of this tutorial, you'll have a base understanding of Storm and a complete project up and running!

Table of contents:
- [Making a new project](#making-a-new-project)
- [Contents of the Sample Project](#contents-of-the-sample-project)
  1. ["Storm" folder](#storm-folder)
	2. ["GameData.json" file](#gamedatajson-file)
	3. ["icon.ico" icon](#iconico-icon)
	4. ["Usings.cs" file](#usingscs-file)
	5. ["MyGame.cs" file](#mygamecs-file)
	6. ["Player.cs" file](#playercs-file)
  7. ["Program.cs" file](#programcs-file)
- [Create a project from scratch](#create-a-project-from-scratch)

## Making a new project
*As easy as cream!* To create a new Storm project, you can just clone th- ***WAIT***! Do you have [.NET Framework 6.0+](https://dotnet.microsoft.com) installed on your machine? This is a ***C#*** project after all! If not, [install the .NET Framework 6.0 or a newer vesion](https://dotnet.microsoft.com/download). Anyways, \*hum hum\*, you can just clone the [StormSampleProject repository](https://github.com/Modleyyy/StormSampleProject) and update submodules, then boom!
```console
git clone https://github.com/Modleyyy/StormSampleProject.git
cd StormSampleProject
git submodule update --init
cd Storm
git checkout master
git pull
```
You'll get a simple project with all you need to start making games! [You could also create a Storm project from scratch if you want to](#create-a-project-from-scratch). (it's more time consuming but okay)


## Contents of the Sample Project
### "Storm" folder:
It's a folder containing all of Storm's source code and documentation! (yes, the one you're reading right now so you can even read it offline) You can modify it as you wish :)
### "GameData.json" file:
A JSON file containing data about the game. (thanks Captain Obvious!) Theres:
- "Width" and "Height: Width and height of the game window (integer).
- "Title": The game window title (string).
- "FPS": Number of frames per second (integer).
- "IconPath": Do I really need to explain (string)?
### "icon.ico" icon:
Storm's main logo as an icon for the game window, replace it by any icon you'd like. If you rename or move the icon file, make sure to modify "IconPath" in "GameData.json" as well.
### "Usings.cs" file:
Last random file before we get into the *spicy stuff*! Here you can put some *Global Usings* (introduced in C# 10) instead of doing `using {insert namespace};` on every file. You can delete this file if you want though, I won't judge ;)
### "MyGame.cs" file:
Finally, here we are! This is the main file as it contains the most important class in all of your game: Your game class! Itw inherits from `Storm.Game` and implements all 3 main methods: `Storm.Game.OnLoad()`, `Storm.Game.OnUpdate(System.Double)` and `Storm.Game.OnDraw(System.Drawing.Graphics)`! They each have comments explaining what each one do, so I don't need to explain that.
### "Player.cs" file:
A simple little player `GameObject` with simple arrow movement and the main icon as a placeholder sprite. It's for showcasing how `GameObject`s and `Component`s work.
### "Program.cs" file:
The main file of your program, like in any .NET project. It creates an instance of the `MyGame` class and calls it's `Run` method.


## Create a project from scratch
> Are you sure you want to do this? [Using the Sample Project is much simpler...](#making-a-new-project)
1. Create a Windows Forms project.
2. Delete the "Form1.cs" and "Form1.Designer.cs" files.
3. Add a ["GameData.json" file](#gamedatajson-file) and an [icon](#iconico-icon) (don't forget to add it's path to the JSON file).
4. Put this in the contents of your "\*.csproj" file:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <!-- Replace "6.0" by your current .NET version if it isn't 6.0 -->
    <TargetFramework>net6.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <NoWarn>CA1416</NoWarn>
  </PropertyGroup>

</Project>
```
5. [Download the Storm repository from Github](https://github.com/Modleyyy/Storm) and place the "Storm" folder in your project folder.
6. Create a game C# class inheriting from `Storm.Game` and extend all 3 main methods. It is recommended to add the line `#nullable disable` at the start of the file, so you don't get any annoying warnings saying "[var name] is required to be not null at the end of the constructor" or stuff like that.
7. In "Program.cs", create an instance of your game class and call it's `Run()` method.

And there you go! It took some time, but you got your Storm project up and running from scratch. You can start coding some stuff now :D
