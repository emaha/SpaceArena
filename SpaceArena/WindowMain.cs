using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceOnLine
{
	public class WindowMain
	{
		RenderWindow wnd;
		Button b1 = new Button(new Vector2f(10, 10), new Vector2f(50, 20));
		
		public WindowMain(RenderWindow w)
		{
			wnd = w;
			b1.text = "Bnt1";
			b1.OnClick += Button1Click;
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
