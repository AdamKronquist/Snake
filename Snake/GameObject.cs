using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public abstract class GameObject
    {
        public Position Position;

        public GameObject(Position position)
        {
            Position = position;
        }

        public abstract void Update();
    }
}
