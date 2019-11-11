using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiesielecLogika;

namespace WisielecGUI
{
    public static class DrawMethods
    {
        public static void Draw(this GraWisielec graWisielec, SpriteBatch spriteBatch,SpriteFont font)
        {
            spriteBatch.DrawString(font, "Twoje haslo jest o kategorii " + graWisielec.GetSlowo().GetKategoria(), new Vector2(0, 0), Color.White);
        }
    }
}
