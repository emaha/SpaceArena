using SFML.Graphics;
using SpaceArena.UI;
using System.Collections.Generic;
using SFML.System;

namespace SpaceArena.Managers
{
    public static class UiManager
    {
        public static List<UIControl> controls = new List<UIControl>();

        public static void Add(UIControl control)
        {
            controls.Add(control);
        }

        public static void Update(RenderWindow wnd)
        {
            foreach (var control in controls)
            {
                control.Update(wnd);
            }
        }

        public static void Draw(RenderTarget target)
        {
            foreach (var control in controls)
            {
                control.Draw(target);
            }
        }
    }
}