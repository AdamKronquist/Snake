using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Wall : GameObject, IRenderable
    {
        /// <summary>
        /// Konstruktorn som ärver positionen från GameObject.
        /// </summary>
        /// <param name="position"></param>
        public Wall(Position position) : base(position) { }

        private char sprite;

        public char GetSprite()
        {
            return sprite;
        }

        public void SetSprite(char value)
        {
            sprite = value;
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
