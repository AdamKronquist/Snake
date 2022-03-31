using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Food : GameObject, IRenderable
    {
        /// <summary>
        /// Konstruktorn som ärver positionen från GameObject.
        /// </summary>
        public Food(Position position) : base(position) { }


        //Sätter matens "bild".
        private char sprite = '\x263A';

        
        public char GetSprite()
        {
            return sprite;
        }

        public override void Update() { }


        /// <summary>
        /// Den symbol som ska synas när man väljer att skriva ut objektet.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return GetSprite().ToString();
        }
    }
}
