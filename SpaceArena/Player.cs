using SFML.Graphics;
using System;
using SFML.System;
using SpaceArena.GameObjects;

namespace SpaceOnLine
{
    public static class Player
    {
        public static Ship ship;

        static Player()
        {
            ship = new Ship();
        }

        public static void Update()
        {
            ship.Update();
        }

        public static void Draw(RenderTarget target)
        {
            ship.Draw(target);
        }
    }
}