using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceArena.Managers;
using SpaceArena.UI;

namespace SpaceOnLine
{
    public class Game
    {
        private const int AppWidth = 1600;
        private const int AppHeight = 900;

        private Random _random = new Random();
        private readonly Vector2f _screenCenter = new Vector2f(AppWidth / 2.0f, AppHeight / 2.0f);

        private readonly Clock _clock = new Clock();
        private readonly Time _ups = Time.FromSeconds(1 / 60.0f);
        private Time _accum = Time.Zero;

        private float _fps;

        private RenderWindow _wnd;
        public static Vector2f offset;

        private Text _fpsText, _thrustText, _posText;

        public Game()
        {
            LoadResources();
        }

        public void LoadResources()
        {
            AssetManager.LoadTextures();
            AssetManager.LoadFonts();

            try
            {
#if DEBUG
                _wnd = new RenderWindow(new VideoMode(AppWidth, AppHeight), "Title!");
#else
                wnd = new RenderWindow(new VideoMode(), "Title!", Styles.Fullscreen);
#endif

                //wnd.SetFramerateLimit(100);
                _wnd.SetVerticalSyncEnabled(true);
                _wnd.Closed += OnClose;
                _wnd.SetActive();

                _fpsText = new Text
                {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 20)
                };

                _thrustText = new Text
                {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 40)
                };

                _posText = new Text
                {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 50)
                };

                Player.ship.Position = new Vector2f(600, 450);
                Player.ship.Size = new Vector2f(30, 30);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Run()
        {
            bool isExit = false;
            InitLevel();

            Color windowColor = new Color(28, 28, 28);

            while (_wnd.IsOpen && !isExit)
            {
                _wnd.DispatchEvents();
                _wnd.Clear(windowColor);

                while (_accum >= _ups)
                {
                    _accum -= _ups;
                    Update();
                }

                Draw();

                _fps = 1.0f / (_clock.ElapsedTime.AsSeconds());
                _accum += _clock.Restart();

                isExit = Keyboard.IsKeyPressed(Keyboard.Key.Escape);
            }
            _wnd.Close();
        }

        public void Update()
        {
            HandleKeyboard();
            offset = Lerp(offset, Player.ship.Position - _screenCenter, 0.05f);

            LevelManager.Update();
            Player.Update();
            UiManager.Update(_wnd);
        }

        public void Draw()
        {
            _thrustText.DisplayedString = "Thrust: " + Player.ship.Thrust;
            _posText.DisplayedString = "POS: " + (int)Player.ship.Position.X + "," + (int)Player.ship.Position.Y;
            _fpsText.DisplayedString = "FPS: " + (int)_fps;

            LevelManager.Draw(_wnd);
            _wnd.Draw(_fpsText);
            _wnd.Draw(_thrustText);
            _wnd.Draw(_posText);
            //Player.Draw(wnd);

            UiManager.Draw(_wnd);

            _wnd.Display();
        }

        private void HandleKeyboard()
        {
            Player.ship.Thrust = 0.0f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
            {
                Player.ship.Weight = 1.0f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
            {
                Player.ship.Weight = 10f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Player.ship.Rotation -= 0.1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Player.ship.Rotation += 0.1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Player.ship.Thrust = 10f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                Player.ship.Thrust = -5f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
            {
                Player.ship.Velocity = Lerp(Player.ship.Velocity, new Vector2f(0, 0), 0.03f);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                Player.ship.Thrust = 100f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Player.ship.Fire();
            }
        }

        private Vector2f Lerp(Vector2f v0, Vector2f v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }

        private void InitLevel()
        {
            var window = new GmWindow(new Vector2f(100, 100), new Vector2f(300, 200));
            var button1 = new Button(new Vector2f(50, 50), new Vector2f(60, 40));
            window.Add(button1);
            UiManager.Add(window);

            ////Ships
            //for (int i = 0; i < 500; i++)
            //{
            //    Ship s = new Ship {
            //        Position = new Vector2f(random.Next(10000) - 5000, random.Next(10000) - 5000),
            //        Velocity = new Vector2f((float) random.NextDouble() - 0.5f, (float) random.NextDouble() - 0.5f),
            //        Size = new Vector2f(20, 20)
            //    };

            //    LevelManager.AddObject(s);
            //}

            ////Stations
            //for (int i = 0; i < 10; i++)
            //{
            //    SpaceObject station = new Station();
            //    station.Position = new Vector2f(random.Next(10000)-5000, random.Next(10000) - 5000);
            //    station.Size = new Vector2f(random.Next(200) + 10, random.Next(200) + 10);

            //    LevelManager.AddObject(station);
            //}

            ////Asteroids
            //for (int i = 0; i < 1000; i++) {
            //    SpaceObject asteroid = new Asteroid();
            //    asteroid.Position = new Vector2f(random.Next(10000) - 5000, random.Next(10000) - 5000);
            //    asteroid.Size = new Vector2f(random.Next(200) + 10, random.Next(200) + 10);
            //    asteroid.Rotation = (float)random.NextDouble()*360f;
            //    LevelManager.AddObject(asteroid);
            //}
        }

        private void DrawLine(RenderTarget target)
        {
            Vertex[] line = new Vertex[2];
            line[0].Position = new Vector2f(20, 20);
            line[0].Color = Color.Red;
            line[1].Position = new Vector2f(50, 50);
            line[1].Color = Color.Green;

            target.Draw(line, PrimitiveType.Lines);
        }

        private void DrawShape(RenderTarget target)
        {
            Shape sh = new RectangleShape(new Vector2f(10, 10));
            sh.Position = new Vector2f(10, 10);
            sh.FillColor = Color.Blue;
            target.Draw(sh);
        }

        private void DrawTriangle(RenderTarget target)
        {
            VertexArray triangle = new VertexArray(PrimitiveType.Triangles, 3);

            triangle[0] = new Vertex(new Vector2f(10, 10), Color.Red);
            triangle[1] = new Vertex(new Vector2f(100, 10), Color.Blue);
            triangle[2] = new Vertex(new Vector2f(100, 100), Color.Green);

            target.Draw(triangle);
        }

        private void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
    }
}