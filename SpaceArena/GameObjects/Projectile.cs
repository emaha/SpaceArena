using System;
using SFML.Graphics;
using SFML.System;
using SpaceOnLine;

namespace SpaceArena.GameObjects
{
    internal class Projectile : SpaceObject
    {
        private int liveTime = 120;

        //TODO:

        public Projectile(Vector2f pos, float rot)
        {
            sprite = new Sprite(AssetManager.GetTexture("asteroid"));

            Position = pos;
            Rotation = rot;
            Velocity = new Vector2f((float)Math.Cos(Rotation) * 10.0f, (float)Math.Sin(Rotation) * 10.0f);
            Size = new Vector2f(3, 3);
        }

        public override void Update()
        {
            base.Update();
            isAlive = liveTime-- > 0;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}