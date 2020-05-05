using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceArena.UI
{
    public class MyArgs : EventArgs
    {
        public string msg;

        public MyArgs(string m)
        {
            msg = m;
        }
    }

    public class Button : UIControl, Drawable
    {
        public string text;

        private readonly RectangleShape _shape = new RectangleShape();

        public Button(Vector2f position, Vector2f size)
        {
            this.position = position;
            this.size = size;

            _shape.Size = size;
            _shape.FillColor = new Color(50, 50, 120);
        }

        public void SetColor(Color color)
        {
            _shape.FillColor = color;
        }

        public override void Update(RenderWindow wnd)
        {
            if (parent != null)
            {
                _shape.Position = parent.position + position;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(wnd);
                IntRect rect = new IntRect((Vector2i)_shape.Position, (Vector2i)_shape.Size);

                if (rect.Contains(mousePos.X, mousePos.Y))
                {
                    onClick(this, new EventArgs());
                }
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(_shape);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shape,states);
        }
    }
}