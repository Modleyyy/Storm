namespace Storm.Signals;

using Storm.Logging;

public sealed class Signal<TDelegate> where TDelegate : Delegate
{
    private readonly List<TDelegate> connectedCallbacks;
    public Signal()
    {
        connectedCallbacks = new();
    }

    public void Connect(TDelegate callback)
    {
        if (IsConnectedTo(callback))
        {
            Log.Error("Callback: \" {callback.Method.Name} \" is already connected to the signal.");
            return;
        }

        connectedCallbacks.Add(callback);
    }

    public void Disconnect(TDelegate callback)
    {
        if (!IsConnectedTo(callback))
        {
            Log.Error("Callback: \" {callback.Method.Name} \" isn't connected to the signal.");
            return;
        }

        connectedCallbacks.Remove(callback);
    }

    public void Emit(params object?[]? arguments)
    {
        ReadOnlySpan<TDelegate> callbacks = connectedCallbacks.ToArray().AsSpan();

        foreach (TDelegate callback in callbacks)
        {
            callback.DynamicInvoke(arguments);
        }
    }

    public bool IsConnectedTo(TDelegate callback)
    {
        return connectedCallbacks.Contains(callback);
    }
}

public sealed class Signal
{
    private readonly List<Delegate> connectedCallbacks;
    public Signal()
    {
        connectedCallbacks = new();
    }

    public void Connect(Delegate callback)
    {
        if (IsConnectedTo(callback))
        {
            Log.Error("Callback: \" {callback.Method.Name} \" is already connected to the signal.");
            return;
        }

        connectedCallbacks.Add(callback);
    }

    public void Disconnect(Delegate callback)
    {
        if (!IsConnectedTo(callback))
        {
            Log.Error("Callback: \" {callback.Method.Name} \" isn't connected to the signal.");
            return;
        }

        connectedCallbacks.Remove(callback);
    }

    public void Emit(params object?[]? arguments)
    {
        ReadOnlySpan<Delegate> callbacks = connectedCallbacks.ToArray().AsSpan();

        foreach (Delegate callback in callbacks)
        {
            callback.DynamicInvoke(arguments);
        }
    }

    public bool IsConnectedTo(Delegate callback)
    {
        return connectedCallbacks.Contains(callback);
    }
}
