namespace Storm;

using Components;
using Logging;
using Utils;

public class GameObject {
    public string name = "";
    public Transform transform;
    private readonly List<Component> components = new();
    public string[] tags;

    public int updateIndex = 0;
    public int renderIndex = 0;
    public bool visible = true;

    public GameObject(string name, Transform transform, string[] tags = null!, bool visible = true)
    {
        this.name = name;
        AddComponent(transform);
        this.transform = transform;
        this.tags = tags ?? Array.Empty<string>();

        this.visible = visible;

        Game.gameObjectsUpdate.Add(this);
        Game.gameObjectsDraw.Add(this);
        Log.Info($"GameObject \" {name} \" was registered!");
    }

    public virtual void OnUpdate(double deltaTime) {}
    public virtual void OnDraw(Graphics graphics) {}
    public void UpdateComponents(double deltaTime)
    {
        foreach (Component comp in components)
            if (comp.isActive) try { comp.OnUpdate(deltaTime); } catch {}
    }

    public void AddComponent(Component component)
    {
        component.BindObject(this);
        components.Add(component);
    }
    public void RemoveComponent(Component component)
    {
        component.BindObject(null!);
        components.Remove(component);
    }

    public override string ToString()
    {
        return $"GameObject : \" {name} \"";
    }

    public void LookAt(Vector2 target)
    {
        float dx = target.x - MathF.Cos(MathHelper.DegToRad(transform.rotation));
        float dy = target.y - MathF.Sin(MathHelper.DegToRad(transform.rotation));
        float angleRad = MathF.Atan2(dy, dx);

        transform.rotation = MathHelper.RadToDeg(angleRad) - 90;
    }
}