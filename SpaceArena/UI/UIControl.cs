
using System;
using SFML.System;

namespace SpaceOnLine
{

	public abstract class UIControl
	{
		public Vector2f pos;
		public Vector2f size;

		public EventHandler OnClick;
		public EventHandler OnMouseMove;
	}
}
