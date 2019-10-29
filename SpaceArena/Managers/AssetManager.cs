using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceOnLine
{
    class AssetManager
    {
        private const string TEX_RESOURCE_PATH = "Data/Textures/";
        private const string FONT_RESOURCE_PATH = "Data/Fonts/";

        public static Dictionary<string, Texture> textures;
        public static Dictionary<string, Font> fonts;

        static AssetManager()
        {
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
        }

        public static void LoadTextures()
        {
            // TODO: сделать загрузку списка спрайтов из файла
            try {
                Image im = new Image(TEX_RESOURCE_PATH + "ships0.png");
                im.CreateMaskFromColor(Color.Black);
                Texture tex = new Texture(im);
                textures.Add("ship", tex);

                im = new Image(TEX_RESOURCE_PATH + "station.png");
                im.CreateMaskFromColor(Color.Black);
                tex = new Texture(im);
                textures.Add("station", tex);

                im = new Image(TEX_RESOURCE_PATH + "asteroid.png");
                im.CreateMaskFromColor(Color.Black);
                tex = new Texture(im, new IntRect(0,0,200, 200));
                textures.Add("asteroid", tex);

            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public static void LoadFonts()
        {
            Font font = new Font(FONT_RESOURCE_PATH + "Terminus.ttf");
            fonts.Add("Terminus", font);
        }

        public static Texture GetTexture(string key)
        {
            return textures[key];
        }

        public static Font GetFont(string key)
        {
            return fonts[key];
        }

        public static void Destroy()
        {
            textures.Clear();
            fonts.Clear();
        }

    }
}
