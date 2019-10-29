using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceOnLine
{
    public class Game
    {
        const int APP_WIDTH = 1600;
        const int APP_HEIGHT = 900;

        Random random = new Random();
        Vector2f centerScreen = new Vector2f(APP_WIDTH / 2.0f, APP_HEIGHT / 2.0f);

        Clock clock = new Clock();
        Time UPS = Time.FromSeconds(1/60.0f);
        Time accum = Time.Zero;

        float fps;

        RenderWindow wnd;
        public static Vector2f offset;

        Text fpsText, thrustText, posText;

        public Game()
        {
            LoadResources();
        }

        public void LoadResources() {
            AssetManager.LoadTextures();
            AssetManager.LoadFonts();

            try {
                #if DEBUG
                wnd = new RenderWindow(new VideoMode(APP_WIDTH, APP_HEIGHT), "Title!");
                #else
                wnd = new RenderWindow(new VideoMode(), "Title!", Styles.Fullscreen);
                #endif

                //wnd.SetFramerateLimit(100);
                wnd.SetVerticalSyncEnabled(true);
                wnd.Closed += OnClose;
                wnd.SetActive();

                fpsText = new Text {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 20)
                };

                thrustText = new Text {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 40)
                };

                posText = new Text {
                    FillColor = Color.Green,
                    Font = AssetManager.GetFont("Terminus"),
                    CharacterSize = 14,
                    Position = new Vector2f(20, 50)
                };


                Player.ship.Position = new Vector2f(600, 450);
                Player.ship.Size = new Vector2f(30, 30);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void Run()
        {
            bool isExit = false;
            InitLevel();

            Color windowColor = new Color(28, 28, 28);

            while (wnd.IsOpen && !isExit) {
                wnd.DispatchEvents();
                wnd.Clear(windowColor);

                while (accum >= UPS) {
                    accum -= UPS;
                    Update();
                }

                Draw();

                fps = 1.0f / (clock.ElapsedTime.AsSeconds());
                accum += clock.Restart();

                isExit = Keyboard.IsKeyPressed(Keyboard.Key.Escape);
            }
            wnd.Close();
        }

        public void Update() {
            HandleKeyboard();
            offset = Lerp(offset, Player.ship.Position - centerScreen, 0.05f);
            
            LevelManager.Update();
            Player.Update();
        }

        public void Draw() {
            thrustText.DisplayedString = "Thrust: " + Player.ship.Thrust;
            posText.DisplayedString = "POS: " + (int)Player.ship.Position.X + "," + (int)Player.ship.Position.Y;
            fpsText.DisplayedString = "FPS: " + (int) fps;
            
            LevelManager.Draw(wnd);
            wnd.Draw(fpsText);
            wnd.Draw(thrustText);
            wnd.Draw(posText);
            Player.Draw(wnd);

            wnd.Display();
        }

        void HandleKeyboard()
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) {
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

        Vector2f Lerp(Vector2f v0, Vector2f v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }

        void InitLevel()
        {
            //Ships
            for (int i = 0; i < 500; i++)
            {
                Ship s = new Ship {
                    Position = new Vector2f(random.Next(10000) - 5000, random.Next(10000) - 5000),
                    Velocity = new Vector2f((float) random.NextDouble() - 0.5f, (float) random.NextDouble() - 0.5f),
                    Size = new Vector2f(20, 20)
                };

                LevelManager.AddObject(s);
            }


            //Stations
            for (int i = 0; i < 10; i++)
            {
                SpaceObject station = new Station();
                station.Position = new Vector2f(random.Next(10000)-5000, random.Next(10000) - 5000);
                station.Size = new Vector2f(random.Next(200) + 10, random.Next(200) + 10);

                LevelManager.AddObject(station);
            }

            //Stations
            for (int i = 0; i < 1000; i++) {
                SpaceObject asteroid = new Asteroid();
                asteroid.Position = new Vector2f(random.Next(10000) - 5000, random.Next(10000) - 5000);
                asteroid.Size = new Vector2f(random.Next(200) + 10, random.Next(200) + 10);
                asteroid.Rotation = (float)random.NextDouble()*360f;
                LevelManager.AddObject(asteroid);
            }


        }

        void DrawLine(RenderTarget target)
        {
            Vertex[] line = new Vertex[2];
            line[0].Position = new Vector2f(20, 20);
            line[0].Color = Color.Red;
            line[1].Position = new Vector2f(50, 50);
            line[1].Color = Color.Green;

            target.Draw(line, PrimitiveType.Lines);
        }
        void DrawShape(RenderTarget target)
        {
            Shape sh = new RectangleShape(new Vector2f(10, 10));
            sh.Position = new Vector2f(10, 10);
            sh.FillColor = Color.Blue;
            //sh.
            target.Draw(sh);
        }
        void DrawTriangle(RenderTarget target)
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

