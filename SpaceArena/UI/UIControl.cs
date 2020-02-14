using SFML.Graphics;
using SFML.System;
using System;

namespace SpaceArena.UI
{
    public abstract class UIControl
    {
        public Vector2f position;
        public Vector2f size;

        public EventHandler onClick;
        public EventHandler onMouseMove;

        public UIControl parent;

        public void SetParent(UIControl control)
        {
            parent = control;
        }

        public virtual void Update(RenderWindow wnd)
        {
        }

        public virtual void Draw(RenderTarget target)
        {
        }
    }
}