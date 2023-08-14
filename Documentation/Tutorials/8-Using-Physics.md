# How to use Storm.Physics
> By the end of this tutorial, you'll learn to create a physics world, add bodies to it and move them using their velocity, making them static, and even drawing them for debug.

> <span style="color:yellow">DISCLAIMER:</span> `Storm.Physics` is a really simple physics engine and it might not be powerful enough to suit your game. If so, you could still make your own implementation of a physics engine, use an existing one like BEPU or Box2DCS, or even build on top of `Storm.Physics` since the entire source code of the framework is in your project folder.

Table of contents:
- [The Physics World](#the-physics-world)
- [Body types](#body-types)
- [Debug Drawing](#debug-drawing)

## The Physics World
To use Storm's built-in Physics engine, `Storm.Physics`, you'll need to use a class that solves all physics bodies, moving them and handling their collisions accordingly. This class, is the `PhysicsWorld` class.

You'll need to instanciate a world in the beginning of your game, generally in `Game.OnLoad()`. You can also pass in a `Vector2` as a parameter to be the gravity of the world, it will be added to the velocity of each body that has been added to the physics world.

You should then have a way to globalize the world, maybe through a static singleton, or something like that. In this tutorial, we'll pass it as an argument to the constructor of the `GameObject`s that need it, as it is what I did when doing test for the engine. Here's a pretty big example:
```csharp
namespace PhysicsTutorial;

#nullable disable
public class PhysicsTests : Game
{
    PhysicsWorld world;
    Player player;

    /* ... */

    public override void OnLoad()
    {
        /* ... */

        world = new PhysicsWorld(new Vector2(0f, 5f)); // Create a new physics world with a gravity of 0 , 5

        player = new Player(world)

        /* ... */
    }

    public override void OnUpdate(double deltaTime)
    {
        /* ... */

        world.Update(deltaTime); // Don't forget to update the physics world!

        /* ... */
    }

    /* ... */
}

public class Player : GameObject
{
    /* ... */

    InputHandler input;
    CircleBody body;

    public Player(PhysicsWorld world) : base(/* ... */)
    {
        /* ... */

        body = new CircleBody(30); // Create a new CircleBody with a radius of 30
        AddComponent(body); // Add the CircleBody component
        world.AddBody(body); // Add the body to the physics world

        /* ... */
    }

    public override void OnUpdate(double deltaTime)
    {
        /* ... */

        // Apply velocity to the physics body
        body.velocity.x = input.GetAxis(Keys.Right, Keys.Left) * 250; // Move left and right
        if (body.IsOnFloor() && input.IsKeyJustPressed(Keys.Space)) body.velocity.y = -180; // Jump if space is just pressed and if is on floor

        /* ... */
    }

    /* ... */
}
```


## Body types
In `Storm.Physics`, there's 2 distinct body types: `RectangleBody` and `CircleBody` that both extend `PhysicsBody`, which means they both have an offset vector (don't use as it's not working, I'll have to fix it later), a center vector and a velocity vector. They also have `IsOnFloor`/`Ceiling`/`Wall` methods (not really optimized but they work).

`CircleBody` has an extra radius property and the other has a half size vector. When creating one, you must call `GameObject.AddComponent(Storm.Components.Component)` and `PhysicsWorld.AddBody(Storm.Physics.PhysicsBody)` passing the body in both methods. Example:
```csharp
AddComponent(body); // Add the PhysicsBody component
world.AddBody(body); // Add the body to the physics world
```
Once you did that, first make sure the PhysicsWorld is updated (`world.Update(deltaTime)` in your OnUpdate of your game class), and you'll be able to use physics!

Oh, I almost forgot! You can also set the `isStatic` flag of the bodies to `true` to make them... well *static*, which means they won't be affected to physics at all and can't move nor be pushed by anything, but other bodies can still collide with it! You can toggle it on and off at any time you'd like, even in the update loop if you want to.
```csharp
// In the OnUpdate method of your GameObject

if (input.IsKeyJustPressed(Keys.Space))
    // Toggle the isStatic flag of the body when space is just pressed.
    body.isStatic = !body.isStatic;
```

## Debug Drawing
Gettin' into a problem you don't understand in your game that's physics related? You can easily use draw your physics bodies using `PhysicsBody.Draw(Graphics, Color, DrawMode)`, here's an example:
```csharp
// In the OnDraw method of your GameObject

// Draw the body on top of the sprite in a white colored border
body.Draw(graphics, Color.White, DrawMode.Border); // You can also make the drawing filled using DrawMode.Fill
```
In case you're wondering, `DrawMode` is actually `Storm.Physics.PhysicsBody.DrawMode`, which is an enum with two values: `Border` and `Fill`. We can use `DrawMode` directly because inside of Usings.cs (in your project folder) we're doing:
```csharp
global using DrawMode = Storm.Physics.PhysicsBody.DrawMode;
```
If you removed Usings.cs, please remember to use `PhysicsBody.DrawMode` instead.

