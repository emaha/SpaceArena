using SFML.Graphics;
using System;
using System.Collections.Generic;
using SFML.System;
using SpaceArena.GameObjects;

namespace SpaceOnLine
{
    public static class LevelManager
    {
        public static List<SpaceObject> objects = new List<SpaceObject>();

        public static void Update()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update();

                if (!objects[i].isAlive)
                {
                    objects.RemoveAt(i--);
                }
            }
        }

        public static void Draw(RenderTarget target)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(target);
            }
        }

        public static void AddObject(SpaceObject obj)
        {
            objects.Add(obj);
        }
    }
}