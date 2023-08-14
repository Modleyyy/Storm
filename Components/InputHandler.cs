using System.Numerics;

namespace Storm.Components;

public class InputHandler : Component
{
    public new bool isActive = true;
    private readonly Dictionary<Keys, bool> inputs;
    private Dictionary<Keys, bool> prevInputs;
    private bool mouseLeft = false;
    private bool mouseRight = false;
    private bool prevMouseLeft = false;
    private bool prevMouseRight = false;
    private readonly Vector2 localMousePos = new();
    
    #nullable disable
    private static Form mainForm = null;
    #nullable enable

    public InputHandler()
    {
        mainForm ??= Application.OpenForms[0];

        if (mainForm is not null)
        {   
            var keys = Enum.GetValues<Keys>();

            inputs = new(keys.Length);
            prevInputs = new(keys.Length);

            Parallel.For(0, Enum.GetValues(typeof(Keys)).Length, i => {
                Keys k = keys[i];
                inputs[k] = false;
                prevInputs[k] = false;
            });

            mainForm.KeyDown += KeyDown;
            mainForm.KeyUp += KeyUp;
            mainForm.MouseDown += MouseDown;
            mainForm.MouseUp += MouseUp;
            mainForm.MouseMove += MouseMove;
        }
        else
        {
            throw new NullReferenceException("There is no open form");
        }
    }

    public override void OnUpdate(double deltaTime)
    {
        prevInputs = new(inputs);
        prevMouseLeft = mouseLeft;
        prevMouseRight = mouseRight;
    }

    #region Events
    private void KeyDown(object? sender, KeyEventArgs e)
    {
        prevInputs[e.KeyCode] = inputs[e.KeyCode];
        inputs[e.KeyCode] = true;
    }

    private void KeyUp(object? sender, KeyEventArgs e)
    {
        prevInputs[e.KeyCode] = inputs[e.KeyCode];
        inputs[e.KeyCode] = false;
    }

    private void MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            mouseLeft = true;
        if (e.Button == MouseButtons.Right)
            mouseRight = true;
    }

    private void MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            mouseLeft = false;
        if (e.Button == MouseButtons.Right)
            mouseRight = false;
    }

    private void MouseMove(object? sender, MouseEventArgs e)
    {
        localMousePos.x = e.Location.X;
        localMousePos.y = e.Location.Y;
    }
    #endregion


    public bool IsKeyPressed(Keys key)
    {
        return inputs[key];
    }

    public bool IsKeyJustPressed(Keys key)
    {
        return inputs[key] && !prevInputs[key];
    }

    public bool IsKeyJustReleased(Keys key)
    {
        return !inputs[key] && prevInputs[key];
    }


    public bool IsMousePressed(MouseButton button) {
        if (button == MouseButton.Left) return mouseLeft;
        if (button == MouseButton.Right) return mouseRight;
        return false;
    }

    public bool IsMouseJustPressed(MouseButton button)
    {
        if (button == MouseButton.Left) return mouseLeft && !prevMouseLeft;
        if (button == MouseButton.Right) return mouseRight && !prevMouseRight;
        return false;
    }

    public bool IsMouseJustReleased(MouseButton button)
    {
        if (button == MouseButton.Left) return !mouseLeft && prevMouseLeft;
        if (button == MouseButton.Right) return !mouseRight && prevMouseRight;
        return false;
    }


    public byte GetRawInput(Keys key)
    {
        return inputs[key] ? (byte)1 : (byte)0;
    }

    public sbyte GetAxis(Keys input1, Keys input2)
    {
        sbyte a1 = (sbyte)GetRawInput(input1);
        sbyte a2 = (sbyte)GetRawInput(input2);
        return (sbyte)(a1 - a2);
    }

    public Vector2 GetVector(Keys xInput1, Keys xInput2, Keys yInput1, Keys yInput2)
    {
        return new Vector2(GetAxis(xInput1, xInput2), GetAxis(yInput1, yInput2));
    }


    public Vector2 GetWindowMousePos()
    {
        return localMousePos;
    }

    public Vector2 GetDesktopMousePos()
    {
        return (Vector2)Control.MousePosition;
    }

    public Vector2 GetClientDesktopMousePos()
    {
        return (Vector2)mainForm.PointToClient(Control.MousePosition);
    }


    public enum MouseButton
    {
        Left,
        Right
    }
}