# Storm, a C# game framework.

![Storm](Assets/banner.png)

## Huh? What's that?
"I made a game framework all by my own! To use it? ***Just download 4269 NuGet packages!*** It'll just take ***3 hours*** to install the dependencies, and there you go!" No. We don't think this way.

Storm is a free and open-source framework you can build games with in C# with no extra packages or anything, *just a pure Windows Forms game framework!*

## So, how do I use it?
Good question, *it's simple!* First, make sure you've got .NET 6+ installed or your machine. Then, just clone the StormSampleProject repository and update Git submodules, and there you go! A brand new project for you to use!
```
git clone https://github.com/Modleyyy/StormSampleProject.git
git submodule update --init
```

You'll get a simple project with a little Player.cs GameObject, a GameData.json file, an icon, a Usings.cs to put global usings into, a Storm folder with all of the code of the framework, and finally a sample MyGame.cs file to get you truly started!

You could also make a project from scratch but uh... preferably... don't do that? Or something? Are you okay?? Why would you even do that!?!?!? If you still want to do that (weirdo), [here's a tutorial on that](https://github.com/Modleyyy/Storm/wiki/GetStarted/#create-a-project-from-scratch)!

Talking about GameObjects, Storm uses a simple GameObject -> Component system. You create GameObject by inheritting the GameObject class, give 'em some code, give 'em some Components, and instanciate 'em!
``` csharp
namespace MyGame;

// Create a GameObject
public class Player : GameObject
{
    Sprite2D sprite;
    InputHandler input;

    //                                     " name ",              (position,    scale   ),             {  tags  }, visible
    public Player(Vector2 position) : base("Player", new Transform(position, Vector2.One), new string[]{"Player"}, true)
    {
        // TODO: Initialize some stuff you might need here, like initializing your
        // components. eg:

        // Create a Component.
        sprite = new("icon.ico", centered: true); // We can just use the icon for the sprite.
        AddComponent(sprite);

        input = new();
        AddComponent(input);
    }

    public override void OnUpdate(double deltaTime)
    {
        // TODO: Update stuff the player might need, like their position for example. eg:
        transform.position += input.GetVector(Keys.Right,Keys.Left , Keys.Down,Keys.Up).Normalized() * 5;
    }

    public override void OnDraw(Graphics graphics)
    {
        // TODO: Draw stuff. For example the actual sprite or a rectangle representing the
        // player's bounding box, ect... or even drawing a placeholder circle if you don't
        // have a sprite! eg:
        sprite.Draw(graphics);
    }
}
```


## Oh, this seems cool! What kind of other cool features does it have?
Well, here's a short list:

- Sprite animation: Through sprite sheet, or seperate images, as you wish.
- Input: A InputHandler component, it has both mouse and keyboard input!
- Audio: A pretty simple AudioPlayer component, supports only Wav for now. Ogg and MP3 will come later. ***~~(maybe)~~***
- Pixel Shaders: Wow, shaders! They're really bad and not optimized, but hey, shaders!
- Tweening: You can tween stuff with custom easings that you can make from https://cubic-bezier.com/, and if that ain't enough, there's all of the easings from https://easings.net/ with all of their In, Out, InOut and OutIn counterparts *built-into the framework!*
- Physics: A *really* simple physics engine is built-into the framework, it supports basic Linear Velocity movement and Circle and AABB collision detection and response. The bodies can even be static!

That's a really short list, and that's because there's a lot more to come like particles and other stuff I forgot about!

## Meh, this thing is kinda weird... I'll do it that way if that was just me.
Good thing you said that, 'cause Storm is *completely open-source!* Something isn't working as you'd like it to, or maybe you'd like to remake it from scratch? Just modify the Storm code and maybe fork it or even make a pull request! We'd love to see what you don't like about it! ***~~(this sounds weird)~~***
