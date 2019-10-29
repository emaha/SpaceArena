using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceOnLine
{
	public class myArgs : EventArgs
	{
		public string msg;
		public myArgs(string m)
		{
			msg = m;
		}
	}
	
	
	public class Button : UIControl
	{
		public string text;
		
		RectangleShape sh = new RectangleShape();
		
		
		public Button(Vector2f pos, Vector2f size)
		{
			this.pos = sh.Position = pos;
			this.size = sh.Size = size;
			sh.FillColor = Color.Blue;
		}
		
		public void Update(RenderWindow wnd)
		{
			if(Mouse.IsButtonPressed(Mouse.Button.Left))
			{
				Vector2i mousePos = Mouse.GetPosition(wnd);
				Console.WriteLine(mousePos.X + " " + mousePos.Y);
				IntRect rect = new IntRect((Vector2i)pos,(Vector2i)size);
				
				if(rect.Contains(mousePos.X, mousePos.Y))
				{
					OnClick(this, new EventArgs());
				}
			}
			
		}
		
		public void Draw(RenderTarget target)
		{
			target.Draw(sh);
		}
	}
}
