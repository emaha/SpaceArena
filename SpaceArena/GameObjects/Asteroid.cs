using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SpaceOnLine
{
    class Asteroid : SpaceObject
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
