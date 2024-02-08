namespace Storm;

using System.Drawing.Drawing2D;
using System.Diagnostics;

public abstract partial class Game
{
    private Canvas window;
    private Thread gameLoop;
    private Stopwatch stopwatch;

    private bool isGameActive;


    private string _windowTitle;

    public int windowWidth { get; }
    public int windowHeight { get; }
    public string windowTitle { get { return _windowTitle; } set { window.Text = value; _windowTitle = value; } }

    public Game(
        int windowWidth = 800, int windowHeight = 600, string windowTitle = "Game", string iconPath = "icon.ico", int fps = 30)
    {
        window = new()
        {
            ClientSize = new(windowWidth, windowHeight),
            Text = windowTitle,
            Icon = new(iconPath),
        };
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;

        this._windowTitle = windowTitle;
        FPS = fps;

        window.Paint += Renderer;
        
        stopwatch = new();

        isGameActive = true;
        gameLoop = new Thread(GameLoop);

        window.FormClosed += (sender, e) => Exit();
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
        while (isGameActive)
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
                    obj.UpdateComponents(dt);
                }
                Thread.Sleep(1000/FPS);
            }
            catch
            {
                Exit();
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

    public void Exit()
    {
        isGameActive = false;
        Application.Exit();
    }
}
