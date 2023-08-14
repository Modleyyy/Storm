# Drawing Sprites on the Screen
> By the end of this tutorial, you'll learn to draw sprites on your game window, scale them, rotate them, and even flip them vertically and horizontally.

Table of contents:
- [Create a sprite](#create-a-sprite)
- [Actually Drawing the sprite](#actually-drawing-the-sprite)

## Create a sprite
So, now that we know what `GameObject`s and `Component`s are (from the [past tutorial](https://github.com/Modleyyy/Storm/wiki/GameObjects-And-Components)), let's actually use them! First, in your `Player` `GameObject` that we have made last time, create a `Sprite2D` `Component` just like how we learned last time.
```csharp
public Player : GameObject
{
    // Declare your component here
    Sprite2D sprite;

    public Player() : base("Player", new Transform())
    {
        sprite = new Sprite2D();
        AddComponent(sprite);
    }
}
```
But, hey! Something's wrong here! If you try to run this, your game will crash! What's the problem? Well you see, we gotta add some arguments to the constructor of the `Sprite2D`! And here they are (in order):
- `path`: A string describing the path to your image (from the root folder of your project) (of type `string`).
- `offset`: A vector that offsets the sprite when drawn (of type `Vector2`, defaults to `null`).
- `centered`: A boolean that means that if the sprite needs to be drawn centered or not (of type `bool`, defaults to `false`).
- `pooled`: A boolean that means that if the Bitmap (actual image) of the sprite should be taken or saved to the sprite pool or not. Basically, if you want this sprite to be reused by other stuff, just set it to `true` or `false` otherwise (of type `bool`, defaults to `true`)
All of these parameters are completely optional, except for the `path` which must be defined and link to an actual image file. You can also modify `offset` and `centered` at any time as they are defined as variables. Now, let's update our code!
```csharp
public Player : GameObject
{
    // Declare your component here
    Sprite2D sprite;

    public Player() : base("Player", new Transform())
    {
        //                    "  path  ", offset, centered, pooled
        sprite = new Sprite2D("icon.ico", null  , true    , false ); // For testing, we can use the icon of the game window
        AddComponent(sprite);
    }
}
```
"Nice! So let's run and... Huh?! Why is the window empty?! Where's my sprite?!" Well, that's because... We aren't even drawing the sprite!

## Actually Drawing the sprite
To do that, we will have to override a method from the `GameObject` class, and that is the `OnDraw(Graphics)`! It passes in a `System.Drawing.Graphics` object to draw stuff! We then call `Sprite2D.Draw(Graphics)` on our sprite passing in the passed `Graphics` object! (I said the same thing 50 times...) So here's now our final code:
```csharp
public Player : GameObject
{
    // Declare your component here
    Sprite2D sprite;

    public Player() : base("Player", new Transform())
    {
        //                    "  path  ", offset, centered, pooled
        sprite = new Sprite2D("icon.ico", null  , true    , false ); // For testing, we can use the icon of the game window
        AddComponent(sprite);
    }

    public override void OnDraw(Graphics graphics)
    {
        sprite.Draw(graphics);
    }
}
```
Yesss! We finally got it working! The player is correctly drawing!

## Flipping the sprite
Now before we finish this tutorial, I'm gonna show you how to flip the sprite vertically and horizontally. To do that, all we need to do is use the `flippedH` and `flippedV` properties of the sprite, or call the `FlipH()` and `FlipV()` methods that will toggle both properties. And that's pretty much it! Next we'll tackle keyboard and mouse input to move our little player around! See ya next time!