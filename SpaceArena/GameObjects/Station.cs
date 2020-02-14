using SFML.Graphics;
using SpaceOnLine;

namespace SpaceArena.GameObjects
{
    internal class Station : SpaceObject
    {
        public Station()
        {
            sprite = new Sprite(AssetManager.GetTexture("station"));
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