namespace Storm;

using Logging;

using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Diagnostics;

public abstract partial class Game
{
    private Canvas window;
    private Thread gameLoop;
    private GameData data;
    private Stopwatch stopwatch;

    public Game()
    {
        string t = File.ReadAllText(Path.Combine(GetRootFolderPath(), "GameData.json"));
        this.data = JsonSerializer.Deserialize<GameData>(t, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        window = new()
        {
            ClientSize = new(data.Width, data.Height),
            Text = data.Title,
            Icon = new(data.IconPath),
        };
        FPS = data.FPS;

        window.Paint += Renderer;
        
        window.FormClosed += (sender, e) =>
        {
            Application.Exit();
        };

        stopwatch = new();

        gameLoop = new Thread(GameLoop);
    }

    public void Run()
    {
        window.Show();
        gameLoop.Start();
        stopwatch.Start();

        Application.Run(window);
    }

    private void GameLoop()
    {
        OnLoad();
        while (gameLoop.IsAlive)
        {
            try
            {
                window.Invalidate();
                double dt = stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();
                try { OnUpdate(dt); } catch {}
                foreach (GameObject obj in gameObjectsUpdate)
                {
                    try { obj.OnUpdate        (dt); } catch {}
                    try { obj.UpdateComponents(dt); } catch {}
                }
                Thread.Sleep(1000/FPS);
            }
            catch
            {
                Application.Exit();
                break;
            }
        }   
    }

    private void Renderer(object? sender, PaintEventArgs e)
    {
        Graphics graph = e.Graphics;
        graph.Clear(screenColor);
        graph.InterpolationMode = InterpolationMode.NearestNeighbor;

        for (int i = 0; i < gameObjectsDraw.Count; i++)
        {
            // The reason we do a try block when calling OnDraw is because stuff that is
            // drawn might be null when drawing which will result in a crash, so we do
            // this just in case it might happen
            if (gameObjectsDraw[i].visible) try { gameObjectsDraw[i]?.OnDraw(graph); } catch {}
        }

        // Same thing here
        try { OnDraw(graph); } catch {}
    }

    public abstract void OnLoad();
    public abstract void OnUpdate(double deltaTime);
    public abstract void OnDraw(Graphics graphics);
}
