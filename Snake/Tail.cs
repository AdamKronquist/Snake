using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Tail : IRenderable
    {
        public int X { get; set; }
        public int Y { get; set; }


        //Sätter spelarens "bild".
        private char sprite = 'o';

        public char GetSprite()
        {
            return sprite;
        }


        /// <summary>
        /// Den symbol som ska synas när man väljer att skriva ut objektet.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return sprite.ToString();
        }

    }
}
