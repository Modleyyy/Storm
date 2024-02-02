# How to Tween objects
> By the end of this tutorial, you'll learn how to tween positions, rotations, and pretty much everything else.

Table of contents:
- [Tween Explanation](#tween-explanation)
- [Using Tweens in Storm](#using-tweens-in-storm)

## Tween Explanation
The word "*Tween*" comes from "*BeTWEEN*", and is the concept of making a value change from a starting point to an end point smoothely over time. To do that, we generally use something called "*Easings*" which, as their names suggest, ease the tweening in different ways, it could make the tween slower at the start and end quickly. (we call this an "In" easing, as it eases in, and we call the other way around "Out", because it eases out)

In Storm, there is a wide library of built-in easings that you can access from the `Storm.Tweening.Easings` static class, in all of their 4 forms. (In, Out, In Out, and Out In) If this is still not enough, you can make your own easings using `Easings.CreateEasing(p1x, p1y, p2x, p2y)`. `p1x` and `p1y` represent the first control point's position (they must be between 0 and 1), and the same is for `p2x` and `p2y`. "How do you get these values", you say? Well it's easy! You can use a website that does that just for you, for example [cubic-bezier.com](https://cubic-bezier.com)!

## Using Tweens in Storm
Now that we know WHAT is a tween, we can easily make one! Start by making a GameObject that uses this tween. In the file, don't forget to add a `using Storm.Tweening` at the start of the file, or a `global using` in Usings.cs! We then add the Tween component to the object like any other component:
```csharp
namespace TweenTutorial;

using Storm.Tweening;

public class TweenExample : GameObject
{
    /* ... */

    Tween tween;

    public TweenExample() : base(/* ... */)
    {
        /* ... */

        // Create the Tween Component and add it as a component to the Circle
        tween = new Tween();
        AddComponent(tween);

        /* ... */
    }
}
```
Nice, now that we have the tween component on, we can start tweening! To do that, you gotta call the `Tween.TweenValue<T>` method. Here's the arguments:
- `startValue` and `endValue`: Start and end values of the tween (of type `T`).
- `duration`: Duration (in seconds) of the tween (of type `float`).
- `easingFunction`: Easing function from `Storm.Tweening.Easings` (of type `Func<float, float>`).
- `valueChanged`: Event method that is called each time the eased value is changed, this is where you should update your value (of type `Action<T>`).
- `onComplete`: Event method that is called once the tweening is completed (of type `Action`).
Ok so now that we know how everything works, let's try it out!
```csharp
transform.position = new Vector2(400, 0); // Set initial position to 400 , 0
//                        startValue        , endValue             , duration, easingFunction       ,
tween.TweenValue<Vector2>(transform.position, new Vector2(400, 300), 5f      , Easings.EaseOutBounce,
//  valueChanged
    (value) =>
    {
        transform.position = value;
    },
//  onComplete
    () =>
    {
        // Do stuff once the tweening is done
    }
);
```
And BOOM! That's it! Run the game and you'll see your GameObject fall down, and bounce on the ground like a ball once it hits position 400, 300 and slowly settles down, all of that in 5 seconds! Yeah, as simple as that!
