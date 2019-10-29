using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SpaceOnLine
{
    class Station : SpaceObject
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
