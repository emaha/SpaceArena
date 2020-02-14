using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SpaceArena.UI
{
    public class GmWindow : UIControl
    {
        private string Title;
        private readonly List<UIControl> _controls = new List<UIControl>();
        private readonly RectangleShape _shape = new RectangleShape();

        public GmWindow(Vector2f pos, Vector2f size)
        {
            position = _shape.Position = pos;
            this.size = _shape.Size = size;
            _shape.FillColor = new Color(128, 128, 128, 100);
        }

        public void Add(UIControl control)
        {
            control.SetParent(this);
            _controls.Add(control);
        }

        public override void Update(RenderWindow wnd)
        {
            _shape.Position = position;
            _shape.Size = size;

            foreach (var control in _controls)
            {
                control.Update(wnd);
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(_shape);
            foreach (var control in _controls)
            {
                control.Draw(target);
            }
        }
    }
}