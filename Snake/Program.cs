using System;
using System.Threading;
using System.Media;

namespace Snake
{
    class Program
    {
        private static int totPoints;
        private static DeathReason deathReason;


        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';

        static void Loop()
        {
            // Initialisera spelet
            const int frameRate = 5;
            GameWorld world = new GameWorld(50, 40);
            ConsoleRenderer renderer = new ConsoleRenderer(world);


            //Skapar en spelare och lägger den i listan med spelobjekt.
            Player player = new Player(new Position(25, 39));
            player.SetSprite('v');
            world.AddToList(player);


            //Skapar massa väggar (stolar).
            #region skapa väggar
            world.CreateWall(3, 0, 4);
            world.CreateWall(3, 0, 6);
            world.CreateWall(3, 0, 8);
            world.CreateWall(3, 0, 10);
            world.CreateWall(3, 0, 12);
            world.CreateWall(3, 0, 14);
            world.CreateWall(3, 0, 16);
            world.CreateWall(3, 0, 18);
            world.CreateWall(3, 0, 24);
            world.CreateWall(3, 0, 26);
            world.CreateWall(3, 0, 28);
            world.CreateWall(3, 0, 30);
            world.CreateWall(3, 0, 32);
            world.CreateWall(3, 0, 34);
            world.CreateWall(3, 0, 36);
            world.CreateWall(3, 0, 38);

            world.CreateWall(4, 18, 4);
            world.CreateWall(4, 18, 6);
            world.CreateWall(4, 18, 8);
            world.CreateWall(4, 18, 10);
            world.CreateWall(4, 18, 12);
            world.CreateWall(4, 18, 14);
            world.CreateWall(4, 18, 16);
            world.CreateWall(4, 18, 18);
            world.CreateWall(4, 18, 24);
            world.CreateWall(4, 18, 26);
            world.CreateWall(4, 18, 28);
            world.CreateWall(4, 18, 30);
            world.CreateWall(4, 18, 32);
            world.CreateWall(4, 18, 34);
            world.CreateWall(4, 18, 34);
            world.CreateWall(4, 18, 36);
            world.CreateWall(4, 18, 38);

            world.CreateWall(3, 38, 4);
            world.CreateWall(3, 38, 6);
            world.CreateWall(3, 38, 8);
            world.CreateWall(3, 38, 10);
            world.CreateWall(3, 38, 12);
            world.CreateWall(3, 38, 14);
            world.CreateWall(3, 38, 16);
            world.CreateWall(3, 38, 18);
            world.CreateWall(3, 38, 24);
            world.CreateWall(3, 38, 26);
            world.CreateWall(3, 38, 28);
            world.CreateWall(3, 38, 30);
            world.CreateWall(3, 38, 32);
            world.CreateWall(3, 38, 34);
            world.CreateWall(3, 38, 34);
            world.CreateWall(3, 38, 36);
            world.CreateWall(3, 38, 38);
            #endregion


            //Skapar en mat.
            world.CreateFood();


            // Huvudloopen
            bool running = true;

            while (running)
            {
                // Kom ihåg vad klockan var i början
                DateTime before = DateTime.Now;

                // Hantera knapptryckningar från användaren
                char key = ReadKeyIfExists();
                switch (key)
                {
                    case 'Q':
                        running = false;
                        break;
                    case 'W':
                        player.SetDirection(Direction.Up);
                        break;
                    case 'S':
                        player.SetDirection(Direction.Down);
                        break;
                    case 'A':
                        player.SetDirection(Direction.Left);
                        break;
                    case 'D':
                        player.SetDirection(Direction.Right);
                        break;
                }
                
                //Om running är false har man tryckt på q. Dödsorsak fastställs, skärmen rensas på all grafik och man hoppar ut ur denna huvudloop.
                if (!running)
                {
                    deathReason = DeathReason.giveUp;
                    Console.Clear();
                    break;
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                world.Update();
                renderer.Render();

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }

                //Om spelet är slut så avsluta looen.
                if (world.GameOver)
                {
                    running = false;
                }

                //Få tag i poängen och dödsorsak.
                totPoints = world.Points;
                deathReason = world.deathReason;
            }
        }


        static void Main(string[] args)
        {
            Intro();
            Loop();
            GameOver();
            Console.ReadKey();
        }

        /// <summary>
        /// Presenterar början av spelet.
        /// </summary>
        private static void Intro()
        {
            //Ändrar fönsterstorleken och buffersize till samma som för spelet.
            Console.SetWindowSize(50, 40);
            Console.SetBufferSize(50, 40);
            Console.CursorVisible = false; //Döljer Cursor som blinkar.
            PrintTitle(); //Skriver ut titeln.
            Console.Write("Tryck på valfri tangent för att börja attackera");
            PlaySound(); //Spela up ljud.
            Console.ReadKey();

            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
        }


        /// <summary>
        /// Design för titelskärmen.
        /// </summary>
        private static void PrintTitle()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                             XX");
            Console.WriteLine("                             XX          XXXX");
            Console.WriteLine("                             XX         XX XXX");
            Console.WriteLine("    XXXX                     XX  XXX   XX   XX");
            Console.WriteLine("  XXXXXX             XXXXX   XX  XX    XXXXXXX");
            Console.WriteLine("  XX       XX XXXX  XXXXXX   XXXXX     XXXX");
            Console.WriteLine("  XX       XXXXXXX      XXX  XXXXX     XXX   X");
            Console.WriteLine("  XXXXX    XXX  XX   XXXXXX  XXXXXX     XXXXXX");
            Console.WriteLine("   XXXXX   XXX  XXX XXX  XX   XX XXX      XX");
            Console.WriteLine("      XXX  XXX  XXX XXX XXX   XX  XXX");
            Console.WriteLine("       XX   XX  XXX  XXXXXX   XX");
            Console.WriteLine("  X   XXX   XX   XX  XXX");
            Console.WriteLine("   XXXXX    XX");
            Console.WriteLine("   XXX                    XXX  XXXXX       XXXX");
            Console.WriteLine("                         X  XX XX  XX         XX");
            Console.WriteLine("                        XX  XX XX  XX       XXXX");
            Console.WriteLine("                        XX  XX XX  XX      XX XX");
            Console.WriteLine("                        XX  X  XX  XX      XX XX");
            Console.WriteLine("                         XXX   XX  XX       XXXX");
            Console.WriteLine("  XXXXX     XX");
            Console.WriteLine("  XXXXXX    XX");
            Console.WriteLine("  XX   XX   XX");
            Console.WriteLine("  XX   XX   XX    XXXXX    XXXXXX     XXX");
            Console.WriteLine("  XX   XX   XX     XXXX    XXXXXXX   XXXXX");
            Console.WriteLine("  XXXXXX    XX        XX   XX   XX  XX   XX");
            Console.WriteLine("  XXXXX     XX      XXXX   XX   XX  XXXXXXX");
            Console.WriteLine("  XX        XX     XXXXX   XX   XX  XXXXXXX");
            Console.WriteLine("  XX        XX    XX  XX   XX   XX  XX");
            Console.WriteLine("  XX        XX    XX  XX   XX   XX   XXXXX");
            Console.WriteLine("  XX        XX     XXXXX   XX   XX    XXXXX");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }


        /// <summary>
        /// Lägger till ett bakgrundsljud.
        /// </summary>
        private static void PlaySound()
        {
            var sound = new SoundPlayer();
            sound.SoundLocation = @"snake_intro.wav";
            sound.Play();
        }


        /// <summary>
        /// Avslutningsskärm som berättar att spelet är slut.
        /// </summary>
        private static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            //Beroende på dödsorsak får man olika meddelanden.
            Console.WriteLine($"Du lyckades döda {totPoints} personer.");
            if (deathReason == DeathReason.collideTail)
            {
                Console.WriteLine($"Därefter råkade du bita dig själv i svansen.\n" +
                    $"Sakta sprids giftet i din egna kropp och du går\n" +
                    $"en plågsam död till mötes.");
            }
            else if (deathReason == DeathReason.collideWall)
            {
                Console.WriteLine($"I all kalabalik råkade du bita i en stol istället för en människa.\n" +
                    $"Stolen var så hårt så du spräckte käken och dog.");
            }
            else if (deathReason == DeathReason.starving)
            {
                Console.WriteLine($"Sen tog det för lång tid innan du hittade ny mat.\n" +
                    $"Du svalt ihjäl.");
            }
            else if (deathReason == DeathReason.giveUp)
            {
                Console.WriteLine($"Därefter tröttnade du på detta och gav upp.");
            }
        }
    }
}




