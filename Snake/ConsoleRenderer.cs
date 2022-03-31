using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class ConsoleRenderer
    {
        private GameWorld world;
        public ConsoleRenderer(GameWorld gameWorld)
        {
            //Tar bredd och höjd från GameWorld-klassen och tilldelar det till konsolfönstret.
            world = gameWorld;
            Console.SetWindowSize(world.Width, world.Height);
            Console.SetBufferSize(world.Width, world.Height);
        }

        /// <summary>
        /// Skriver ut spelarobjektet i en viss färg på dess position.
        /// </summary>
        private void PrintPlayer(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(gameObject.Position.X, gameObject.Position.Y);
                Console.Write(gameObject);
            }
        }


        /// <summary>
        /// Skriver ut matobjektet i en viss färg på dess position.
        /// </summary>
        /// <param name="gameObject"></param>
        private void PrintFood(GameObject gameObject)
        {
            if (gameObject is Food)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(gameObject.Position.X, gameObject.Position.Y);
                Console.Write(gameObject);
            }
        }


        /// <summary>
        /// Skriver ut väggobjektet i en viss färg på dess position.
        /// </summary>
        /// <param name="gameObject"></param>
        private void PrintWall(GameObject gameObject)
        {
            if (gameObject is Wall)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(gameObject.Position.X, gameObject.Position.Y);
                Console.Write(gameObject);
            }
        }


        /// <summary>
        /// Skriver ut svansen i en viss färg på dess position.
        /// </summary>
        private void PrintTail()
        {
            foreach (var tail in world.tail)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(tail.X, tail.Y);
                Console.Write(tail);
            }
        }


        /// <summary>
        /// Skriver ut tomma tecken på skärmen för att rensa den.
        /// </summary>
        public void RenderBlank()
        {
            foreach (var item in world.objects)
            {
                //Om objektet är Wall eller Food ska detta inte göras då dessa alltid är på samma position.
                if (item is IRenderable)
                {
                    if (item is Wall || item is Food)
                    {

                    }
                    else
                    {
                        Console.SetCursorPosition(item.Position.X, item.Position.Y);
                        Console.Write(' ');
                    }

                }
            }
            foreach (var item in world.tail)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }


        /// <summary>
        /// Skriver ut allt på skärmen.
        /// </summary>
        public void Render()
        {
            //Om spelet inte är slut så gör allt detta.
            if (!world.GameOver)
            {
                //Skriver ut poängen
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Antal döda = {world.Points}\t\tTid till svält = {world.timeLeft:00}"); // :00 efter timeLeft för att det altid ska visas två siffror. Annars stannar den bakre siffran kvar när talet är mindre än 10 då inte den rutan töms vid RenderBlank.

                //Loopar igenom alla objekt i listan med objekt.
                foreach (var gameObject in world.objects)
                {
                    //Hittar den något objekt som har interfacet IRenderable så skriv ut det på skärmen.
                    if (gameObject is IRenderable)
                    {
                        PrintPlayer(gameObject);
                        PrintWall(gameObject);
                        PrintTail();
                        PrintFood(gameObject);
                    }
                }
            }
            //Är spelet Game Over så rensa skärmen och sätt CursorPosition till 0,0.
            else
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
