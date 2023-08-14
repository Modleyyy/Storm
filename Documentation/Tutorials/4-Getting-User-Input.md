# Getting User Input to move our player around
> By the end of this tutorial, you'll have your player move when pressing the arrow keys or have him follow your mouse smoothely. If you want a complete list of all `InputHandler` methods and what they do, [here you go](#all-inputhandler-methods)!

## Adding the InputHandler component
We won't waste any time, let's just get into it. First, let's continue from last time and add a `InputHandler` `Component` in your `Player`:
```csharp
public Player : GameObject
{
    Sprite2D sprite;
    InputHandler input; // Declare a new field for the InputHandler

    public Player() : base("Player", new Transform())
    {
        sprite = new Sprite2D("icon.ico", null  , true    , false ); 
        AddComponent(sprite);

        input = new InputHandler(); // Add the InputHandler
        AddComponent(input);
    }

    public override void OnDraw(Graphics graphics)
    {
        sprite.Draw(graphics);
    }
}
```
And that's all we need! Now let's start moving the player.

## Moving the player with the arrow keys
So we will first move the player with the arrow keys, we will use the mouse later.

In the `OnUpdate(double)` method of the player, we will use a method from the `InputHandler` class to generate a vector that represents the direction that the player will move in, and that is `InputHandler.GetVector(xInput1, xInput2, yInput1, yInput2)`. `xInput1` and `2` represent the 2 keys on the x axis, and the others on the y axis. Basically, we can just do this: `input.GetVector(Keys.Right, Keys.Left, Keys.Down, Keys.Up)` which will return the vector... blablabla.

So! We will add this result to the position of the player in the update loop:
```csharp
public Player : GameObject
{
    Sprite2D sprite;
    InputHandler input; // Declare a new field for the InputHandler

    public Player() : base("Player", new Transform())
    {
        sprite = new Sprite2D("icon.ico", null  , true    , false ); 
        AddComponent(sprite);

        input = new InputHandler(); // Add the InputHandler
        AddComponent(input);
    }

    public override void OnUpdate(double deltaTime)
    {
        transform.position += input.GetVector(Keys.Right, Keys.Left, Keys.Down, Keys.Up) * 5; // 5 is the speed (5 pixels per frame)
    }

    public override void OnDraw(Graphics graphics)
    {
        sprite.Draw(graphics);
    }
}
```

And yes! Our player moves when we press the arrow keys. You might have noticed that the player is moving more quickly sideways, that can be easily fixed by normalizing the vector. Let me just show you:
```csharp
//                                                                                  This
transform.position += input.GetVector(Keys.Right, Keys.Left, Keys.Down, Keys.Up).Normalized() * 5;
```
This will fix everything :) So now, onto the mouse movement!

## Moving the player with the mouse
Ok, that is actually much easier! All we have to do is use one of 3 methods to get the mouse position, but we only 2 of the three methods listed here, and they are: `GetWindowMousePos` and `GetClientDesktopMouse`. Now these names might sound scary, but I didn't have any idea of how to name them.

So, what's the difference? Well it's easy! Here it is:
- `GetWindowMousePos`: Returns the mouse position on the window, if the mouse leaves it, it stays in place until the mouse enters the window once again (returns `Vector2`).
- `GetClientDesktopMouse`: Return the global mouse position on your screen, but it is modified so that it translates to the window coords (returns `Vector2`).

We will use the second one, but the first one works good too. So, now we just need to set the position to the mouse position!
```csharp
transform.position = GetClientDesktopMouse();
```
And that is all! We can even make it *smoooooth* by multiplying the mouse position minus the player's position by a small number, say 0.02, and add it to the position:
```csharp
transform.position += (GetClientDesktopMouse() - transform.position) * 0.02f;
```
And it's now much cooler! The player is smoothely following the mouse rather than directly on it! And that's all :D Thanks for following this tutorial! Next time, we'll animate a sprite! I don't know why I made it so hard to do that, but it works :/ See you then!
