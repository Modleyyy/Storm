namespace Storm.Components;

public abstract class Component
{
    public GameObject boundObject = null!;
    public bool isActive = true;
    public void BindObject(GameObject boundTo)
    {
        if (boundObject is not null)
            boundObject.RemoveComponent(this);
        boundObject = boundTo;
    }

    public virtual void OnUpdate(double deltaTime) {}
}