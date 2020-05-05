using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SpaceOnLine;

namespace SpaceArena.UI
{
    public class GmWindow : UIControl, IWindow
    {
        private string Title;
        private readonly List<UIControl> _controls = new List<UIControl>();
        private readonly RectangleShape _bodyShape = new RectangleShape();
        private readonly RectangleShape _titleShape = new RectangleShape();
        private readonly Button _closeButton;
        private readonly Text _titleText = new Text();

        public bool Closed { get; private set;}

        public GmWindow(Vector2f pos, Vector2f size)
        {
            _titleText.Font = AssetManager.GetFont("Terminus");
            _titleText.DisplayedString = "Window Title";
            _titleText.CharacterSize = 18;

            _titleShape.FillColor = new Color(200, 100, 200);
            _titleShape.OutlineColor = new Color(230, 150, 230);
            _titleShape.Size = new Vector2f(size.X, 40);

            _closeButton = new Button(new Vector2f(5, 5), new Vector2f(30, 30));
            _closeButton.SetColor(new Color(230, 20, 20));
            _closeButton.onClick += new System.EventHandler((s, e) =>
            {
                this.Close();
            });
            
            Add(_closeButton);

            position = _bodyShape.Position = pos;
            this.size = _bodyShape.Size = size;
            _bodyShape.FillColor = new Color(128, 128, 128, 100);
        }

        public void Add(UIControl control)
        {
            control.SetParent(this);
            _controls.Add(control);
        }

        public override void Update(RenderWindow wnd)
        {
            _bodyShape.Position = position;
            _bodyShape.Size = size;
            _titleShape.Position = position;

            _titleText.Position = position + new Vector2f(50, 10);

            foreach (var control in _controls)
            {
                control.Update(wnd);
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(_bodyShape);
            target.Draw(_titleShape);
            target.Draw(_titleText);
            foreach (var control in _controls)
            {
                control.Draw(target);
            }
        }

        public void Close()
        {
            Closed = true;
        }
    }
}