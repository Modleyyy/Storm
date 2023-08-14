# `InputHandler` API reference documentation

## Info

**Full Name**: `Storm.Components.InputHandler`

**Extends**: `Storm.Components.Component`

**Description**: A `Component` that... well... handles input. Has a methods to help getting keyboard and mouse input.

**Tutorials**:
- [Getting User Input](https://github.com/Modleyyy/Storm/wiki/Getting-User-Input)

## Enums
### `MouseButton`
**Full Name**: `Storm.Components.InputHandler.MouseButton`

**Values**:

0. `Left`
1. `Right`

## Methods

### `IsKeyPressed(key)`
**Full Name**: `Storm.Components.InputHandler.IsKeyPressed(System.Windows.Forms.Keys)`

**Returns**: `bool`

**Description**: Returns `true` if `key` is held down, `false` otherwise.

**Parameters**:
- `key`: Key from the `System.Windows.Forms.Keys` enum (of type `Keys`).

### `IsKeyJustPressed(key)`
**Full Name**: `Storm.Components.InputHandler.IsKeyJustPressed(System.Windows.Forms.Keys)`

**Returns**: `bool`

**Description**: Returns `true` if `key` has just been pressed this frame, `false` otherwise.

**Parameters**:
- `key`: Key from the `System.Windows.Forms.Keys` enum (of type `Keys`).

### `IsKeyJustReleased(key)`
**Full Name**: `Storm.Components.InputHandler.IsKeyJustReleased(System.Windows.Forms.Keys)`

**Returns**: `bool`

**Description**: Returns `true` if `key` has just been released this frame, `false` otherwise.

**Parameters**:
- `key`: Key from the `System.Windows.Forms.Keys` enum (of type `Keys`).

### `IsMousePressed(button)`
**Full Name**: `Storm.Components.InputHandler.IsMousePressed(Storm.Components.InputHandler.MouseButton)`

**Returns**: `bool`

**Description**: Returns `true` if `button` is held down, `false` otherwise.

**Parameters**:
- `button`: Key from the `Storm.Components.InputHandler.MouseButton` enum (of type `MouseButton`).

### `IsMouseJustPressed(button)`
**Full Name**: `Storm.Components.InputHandler.IsMousePressed(Storm.Components.InputHandler.MouseButton)`

**Returns**: `bool`

**Description**: Returns `true` if `button` has just been pressed this frame, `false` otherwise.

**Parameters**:
- `button`: Key from the `Storm.Components.InputHandler.MouseButton` enum (of type `MouseButton`).

### `IsMouseJustReleased(button)`
**Full Name**: `Storm.Components.InputHandler.IsMousePressed(Storm.Components.InputHandler.MouseButton)`

**Returns**: `bool`

**Description**: Returns `true` if `button` has just been released this frame, `false` otherwise.

**Parameters**:
- `button`: Key from the `Storm.Components.InputHandler.MouseButton` enum (of type `MouseButton`).

### `GetRawInput(key)`
**Full Name**: `Storm.Components.InputHandler.GetRawInput(System.Windows.Forms.Keys)`

**Returns**: `byte`. Returns only 0 or 1, so a `bit` datatype would be great here, but C# doesn't have a built-in one, so we go with `byte`.

**Description**: Returns 1 if `button` is pressed, 0 otherwise.

**Parameters**:
- `key`: Key from the `System.Windows.Forms.Keys` enum (of type `Keys`).

### `GetAxis(input1, input2)`
**Full Name**: `Storm.Components.InputHandler.GetAxis(System.Windows.Forms.Keys, System.Windows.Forms.Keys)`

**Returns**: `sbyte`. Returns only -1, 0 or 1, and since `sbyte` is the smallest data type that has all three, we use it.

**Description**: Returns 1 if `input1` is being pressed, -1 if `input2` is being pressed, and 0 if neither are pressed.

**Parameters**:
- `input1`: First input. If this one is pressed and the other isn't, returns 1 (of type `Keys`).
- `input2`: Second input. If this one is pressed and the other isn't, returns -1 (of type `Keys`).

### `GetVector(xInput1, xInput2, yInput1, yInput2)`
**Full Name**: `Storm.Components.InputHandler.GetVector(System.Windows.Forms.Keys, System.Windows.Forms.Keys, System.Windows.Forms.Keys, System.Windows.Forms.Keys)`

**Returns**: `Vector2`

**Description**: Returns a vector with it's x component equal to `InputHandler.GetAxis(xInput1, xInput2)`, and the same for it's y component.

**Parameters**:
- `xInput1`: First x input. If this one is pressed and the other isn't, x is equal to 1 (of type `Keys`).
- `xInput2`: Second x input. If this one is pressed and the other isn't, x is equal to -1 (of type `Keys`).
- `yInput1`: First y input. If this one is pressed and the other isn't, y is equal to 1 (of type `Keys`).
- `yInput2`: Second y input. If this one is pressed and the other isn't, y is equal to -1 (of type `Keys`).

