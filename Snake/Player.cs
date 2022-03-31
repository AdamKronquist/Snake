using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Player : GameObject, IRenderable, IMovable
    {
        /// <summary>
        /// Konstruktorn som ärver positionen från GameObject.
        /// </summary>
        /// <param name="position"></param>
        public Player(Position position) : base(position) { }

        private Direction direction;

        public Direction GetDirection()
        {
            return direction;
        }

        public void SetDirection(Direction value)
        {
            direction = value;
        }

        private char sprite;

        //Sätter spelarens "bild".
        public char GetSprite()
        {
            return sprite;
        }

        //Sätter spelarens "bild".
        public void SetSprite(char value)
        {
            sprite = value;
        }


        /// <summary>
        /// Den symbol som ska synas när man väljer att skriva ut objektet.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return GetSprite().ToString();
        }


        /// <summary>
        /// Hanterar vad som ska hända om spelaren hamnar utanför skärmen.
        /// </summary>
        public void OutsideScreen()
        {
            //Går man utanför på vänster sida flyttas man längst ut på höger sida.
            if (Position.X < 0)
            {
                Position.X = 49;
            }
            //Går man utanför på höger sida flyttas man längst ut på vänster sida.
            else if (Position.X > 49)
            {
                Position.X = 0;
            }
            //Går man utanför längst upp flyttas man längst ner.
            else if (Position.Y < 0)
            {
                Position.Y = 39;
            }
            //Går man utanför längst ner flyttas man längst upp.
            else if (Position.Y > 39)
            {
                Position.Y = 0;
            }
        }


        /// <summary>
        /// Uppdaterar spelarens position beroende på vilken riktning den har.
        /// Ändrar även spelarens bild beroende på riktningen.
        /// </summary>
        public override void Update()
        {
            //Går spelaren mot vänster minskar X-värdet med 1.
            if (GetDirection() == Direction.Left)
            {
                Position.X -= 1;
                SetSprite('>');
            }
            //Går spelaren mot höger ökar X-värdet med 1.
            else if (GetDirection() == Direction.Right)
            {
                Position.X += 1;
                SetSprite('<');
            }
            //Går spelaren uppåt minskar Y-värdet med 1.
            else if (GetDirection() == Direction.Up)
            {
                Position.Y -= 1;
                SetSprite('v');
            }
            //Går spelaren nedåt ökar Y-värdet med 1.
            else if (GetDirection() == Direction.Down)
            {
                Position.Y += 1;
                SetSprite('^');
            }

            //Kollar sedan spelarens position ifall den har hamnat utanför skärmen.
            OutsideScreen();
        }
    }
}
