using System;
using SFML.Graphics;
using SFML.System;
using SpaceOnLine;

namespace SpaceArena.GameObjects
{
    public abstract class SpaceObject
    {
        public bool isAlive = true;

        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
        public float Rotation { get; set; }
        public Vector2f Size { get; set; }

        protected Sprite sprite;

        public virtual void Draw(RenderTarget target)
        {
        }

        public virtual void Update()
        {
            Position += Velocity;

            sprite.Position = Position - Game.offset;
            sprite.Rotation = Rotation * 180 / (float)Math.PI;
        }
    }
}