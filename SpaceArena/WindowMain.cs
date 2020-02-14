using System;
using SFML.Graphics;
using SFML.System;
using SpaceArena.UI;

namespace SpaceArena
{
    public class WindowMain
    {
        private RenderWindow wnd;
        private Button b1 = new Button(new Vector2f(10, 10), new Vector2f(50, 20));

        public WindowMain(RenderWindow w)
        {
            wnd = w;
            b1.text = "Bnt1";
            b1.onClick += Button1Click;
        }

        public void Update()
        {
            b1.Update(wnd);
        }

        public void Draw(RenderTarget target)
        {
            b1.Draw(target);
        }

        public void Button1Click(Object sender, EventArgs e)
        {
            Console.WriteLine("B1 clicked");
        }
    }
}