namespace Storm;

using System.Drawing;

public abstract partial class Game {
    public static Color screenColor = Color.Black;
    public static int FPS = 30;

    public static List<GameObject> gameObjectsUpdate = new ObjectList(ObjectList.SortType.update);
    public static List<GameObject> gameObjectsDraw = new ObjectList(ObjectList.SortType.render);

    public static string GetRootFolderPath()
    {
        return AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
    }

}


