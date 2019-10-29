using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceOnLine
{
	public class Ship : SpaceObject
	{
        int ID { get; set; }
        public float Thrust { get; set; } //тяга
        public float Weight { get; set; } = 2.0f; //вес
        public EventHandler damegeTakenEvent;
        private float fireCD = 0.1f;
        private float fireCDremaining = 0.0f;

        public Ship()
		{
            sprite = new Sprite(AssetManager.GetTexture("ship"));
            sprite.TextureRect = new IntRect(50,0,70,105);
            sprite.Origin = new Vector2f(35, 52);
        }
		
		public override void Update()
		{
            Velocity += new Vector2f((float)Math.Cos(Rotation) * Thrust/100/Weight,
                    (float)Math.Sin(Rotation) * Thrust/100 /Weight);
            
            fireCDremaining -= 0.016f;

            base.Update();
        }
		
		public override void Draw(RenderTarget target)
		{
            target.Draw(sprite);
        }

        public void Fire()
        {
            if (fireCDremaining < 0)
            {
                LevelManager.AddObject(new Projectile(Position, Rotation));
                fireCDremaining = fireCD;
            }
        }
		
		public void TakeDamage(int amount)
		{
			damegeTakenEvent(this , new EventArgs());
		}
		
		
	}
}
