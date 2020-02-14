using SFML.Graphics;
using SpaceOnLine;

namespace SpaceArena.GameObjects
{
    internal class Asteroid : SpaceObject
    {
        public Asteroid()
        {
            sprite = new Sprite(AssetManager.GetTexture("asteroid"));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}