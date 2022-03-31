using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace Snake
{
    public class GameWorld
    {
        public int Width, Height;
        public int Points { get; private set; }
        public bool GameOver = false;
        public int timeLeft = 15; //Den tid man har på sig att ta nästa matbit.
        private int timeCount = 5; //Så många gåner som Update verkar hinna anropas på en sekund.

        public DeathReason deathReason;

        //Snakes föregående positioner för att veta vart svansen ska börja någonstans.
        public int oldPosX { get; set; }
        public int oldPosY { get; set; }

        public GameWorld(int width, int height)
        {
            Width = width;
            Height = height;
        }


        /// <summary>
        /// Lista som lagrar alla objekt i spelet.
        /// </summary>
        public List<GameObject> objects = new List<GameObject>();


        /// <summary>
        /// Lista som lagrar alla svansar och deras ppositioner.
        /// </summary>
        public List<Tail> tail = new List<Tail>();


        /// <summary>
        /// Lägger till ett nytt objekt i listan för GameObject.
        /// </summary>
        /// <param name="yourObject">Det objekt man vill lägga till i listan.</param>
        public void AddToList(GameObject yourObject)
        {
            objects.Add(yourObject);
        }


        /// <summary>
        /// Räknar ner tiden.
        /// </summary>
        private void Timer()
        {
            //Varje gång metoden anropas minskar timeCount med 1.
            timeCount -= 1;
            //När timeCount når 0 minskar tiden som är kvar med 1 och timeCount återställs till sitt utgångsvärde så att nästa sekund blir lika lång.
            if(timeCount <= 0)
            {
                timeLeft -= 1;
                timeCount = 5;
            }
            //När timeLeft är mindre än 0 är spelet slut och dödsorsak fastställs.
            if(timeLeft < 0)
            {
                GameOver = true;
                deathReason = DeathReason.starving;
            }
        }


        /// <summary>
        /// Loopar igenom hela listan med GameObject och anropar
        /// Update-metoden för alla objekt som finns i listan.
        /// </summary>
        public void Update()
        {
            Timer();
            
            foreach (var item in objects)
            {
                if (item is Player)
                {
                    oldPosX = item.Position.X;
                    oldPosY = item.Position.Y;
                }
                item.Update();
            }
            CheckIfSnakeEatFood();
            CheckIfSnakeCollideWall();
            UpdateTailPos();
            CheckIfSnakeCollideTail();
        }


        /// <summary>
        /// Kollar om Snake kolliderar med svansen.
        /// </summary>
        private void CheckIfSnakeCollideTail()
        {
            //Loopar igenom hela listan med alla objekt i spelet.
            foreach (var item in objects)
            {
                //Om ett objekt är utav klassen Spelare så...
                if (item is Player)
                {
                    //Loopa igenom listan med svansar.
                    for (int i = 0; i < tail.Count; i++)
                    {
                        //Om spelarens position är lika med svansens är selet slut och dödsorsak fastställs.
                        if (item.Position.X == tail[i].X &&
                        item.Position.Y == tail[i].Y)
                        {
                            GameOver = true;
                            deathReason = DeathReason.collideTail;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Skapar mat.
        /// </summary>
        public void CreateFood()
        {
            //Skapar två slumpmässiga punkter innanför consolens fönster.
            Random random = new Random();
            int randomPosX = random.Next(1, Width - 1);
            int randomPosY = random.Next(1, Height - 1);

            //Loopar igenom hela listan med objekt för att se om den hittar några väggar.
            foreach (var item in objects)
            {
                //Här är jag lite osäker på vad jag gjort. Tanken var att om item is Wall så ska den gå igenom alla Wall-objekt och kolla upp dett position.
                //Är positionerna lika så ska det skapas nya värden för X och Y ända tills det får fram två värden som inte matchar med någon Wall.
                //Men jag är osäker på om det är det som den här andra foreach-loopen gör.
                //Jag har å andra sidan inte råkat ut för att den skapas i något objekt än så länge när jag spelat.
                //Men den kanske gör så att den går igenom alla objekt och inte bara Wall för att se så den inte skapas på samma position som
                //något annat objekt heller.
                if (item is Wall)
                {
                    foreach (var item2 in objects)
                    {
                        if (item2.Position.X == randomPosX &&
                        item2.Position.Y == randomPosY)
                        {
                            randomPosX = random.Next(1, Width - 1);
                            randomPosY = random.Next(1, Height - 1);
                        }
                    }
                }
            }

            //Skapa en ny mat som läggs till i listan.
            Food food = new Food(new Position(randomPosX, randomPosY));
            AddToList(food);
        }


        /// <summary>
        /// Skapar en ny vägg (stol).
        /// </summary>
        /// <param name="howMany">Det antal stolar man vill skapa bredvid varandra.</param>
        /// <param name="startX">X-värdet där första stolen börjar.</param>
        /// <param name="Y">Den rad som stolen kommer skrivas ut på.</param>
        public void CreateWall(int howMany, int startX, int Y)
        {
            int counter = 0;

            //Loopar loopen en gång för varje stol.
            for (int i = 0; i < howMany; i++)
            {
                //Loopar 4 gånger då varje stol består av 4 tecken.
                for (int j = 0; j < 4; j++)
                {
                    //Skriver ut olika tecken beroende på vart i loopen man är.
                    switch (j)
                    {
                        //0 och 3 ger samma tecken.
                        case 0:
                        case 3:
                            //Varje del av "väggen" läggs till i listan med objekt så att spelaren kan krocka med dem.
                            Wall wall = new Wall(new Position(startX + counter, Y));
                            wall.SetSprite('|');
                            AddToList(wall);
                            break;
                        //1 och 2 ger samma tecken.
                        case 1:
                        case 2:
                            Wall wall2 = new Wall(new Position(startX + counter, Y));
                            wall2.SetSprite('_');
                            AddToList(wall2);
                            break;
                    }
                    counter += 1;
                }
            }
        }


        /// <summary>
        /// Uppdaterar svansarnas positioner.
        /// </summary>
        private void UpdateTailPos()
        {
            //Om det bara finns en svans så ska den få spelarens precis tidigares position.
            if (tail.Count == 1)
            {
                tail[0].X = oldPosX;
                tail[0].Y = oldPosY;
            }
            //Om det finns mer än en svans...
            else if (tail.Count > 0)
            {
                //Ta värdet på den svansdel som är längst bak och tilldela den
                //värdet för svansdelen som är precis framför i listan.
                for (int i = tail.Count - 1; i > 0; i--)
                {
                    tail[i].X = tail[i - 1].X;
                    tail[i].Y = tail[i - 1].Y;
                }
                //Svansdelen som är längst fram ska alltid få spelarens precis tidigares position.
                tail[0].X = oldPosX;
                tail[0].Y = oldPosY;
            }
        }


        /// <summary>
        /// Kollar om Snake är på samma ruta som maten.
        /// </summary>
        private void CheckIfSnakeEatFood()
        {
            //Går igenom hela listan med alla objekt i spelet.
            foreach (var item in objects)
            {
                //Om den hittar någon spelare så...
                if (item is Player)
                {
                    //Gå igenom hela listan igen och leta efter mat.
                    for (int i = 0; i < objects.Count; i++)
                    {
                        //Om den hittar någon mat så...
                        if (objects[i] is Food)
                        {
                            //Kolla om spelarens position och matens position är samma.
                            if (item.Position.X == objects[i].Position.X &&
                        item.Position.Y == objects[i].Position.Y)
                            {
                                //I så fall spela upp ett ljud, ge spelaren 1 poäng, ta bort maten från listan
                                //skapa en ny mat med hjälp av den metoden, lägg till en svans och återställ all tid till ursprungstiden.
                                PlayKillSound();
                                Points += 1;
                                objects.RemoveAt(i);
                                CreateFood();
                                tail.Add(new Tail());
                                timeLeft = 15;
                                timeCount = 5;

                                //Gå sedan ur alla looparna.
                                //Annars blev det något väldigt konstigt fel.
                                goto LoopEnd;
                            }
                        }
                    }
                }
            }
        LoopEnd:;
        }


        /// <summary>
        /// Spelar up ett ljud.
        /// </summary>
        private void PlayKillSound()
        {
            //Skapa ett slumpat tal.
            Random random = new Random();
            int nr = random.Next(16);

            var sound = new SoundPlayer();

            //Spela upp ljuddet som motsvarar det slumpade talet.
            switch (nr)
            {
                case 0:
                    sound.SoundLocation = @"kill_1.wav";
                    sound.Play();
                    break;
                case 1:
                    sound.SoundLocation = @"kill_2.wav";
                    sound.Play();
                    break;
                case 2:
                    sound.SoundLocation = @"kill_3.wav";
                    sound.Play();
                    break;
                case 3:
                    sound.SoundLocation = @"kill_4.wav";
                    sound.Play();
                    break;
                case 4:
                    sound.SoundLocation = @"kill_5.wav";
                    sound.Play();
                    break;
                case 5:
                    sound.SoundLocation = @"kill_6.wav";
                    sound.Play();
                    break;
                case 6:
                    sound.SoundLocation = @"kill_7.wav";
                    sound.Play();
                    break;
                case 7:
                    sound.SoundLocation = @"kill_8.wav";
                    sound.Play();
                    break;
                case 8:
                    sound.SoundLocation = @"kill_9.wav";
                    sound.Play();
                    break;
                case 9:
                    sound.SoundLocation = @"kill_10.wav";
                    sound.Play();
                    break;
                case 10:
                    sound.SoundLocation = @"kill_11.wav";
                    sound.Play();
                    break;
                case 11:
                    sound.SoundLocation = @"kill_12.wav";
                    sound.Play();
                    break;
                case 12:
                    sound.SoundLocation = @"kill_13.wav";
                    sound.Play();
                    break;
                case 13:
                    sound.SoundLocation = @"kill_14.wav";
                    sound.Play();
                    break;
                case 14:
                    sound.SoundLocation = @"kill_15.wav";
                    sound.Play();
                    break;
                case 15:
                    sound.SoundLocation = @"kill_16.wav";
                    sound.Play();
                    break;
            }
        }


        /// <summary>
        /// Kollar om Snake är på samma ruta som en vägg (stol).
        /// </summary>
        private void CheckIfSnakeCollideWall()
        {
            //Går igenom listan med alla objekt som finns i spelet.
            foreach (var item in objects)
            {
                //Om den hittar någon spelare så...
                if (item is Player)
                {
                    //Gå igenom listan igen och se om den hittar någon vägg (stol).
                    for (int i = 0; i < objects.Count; i++)
                    {
                        //Om den gör det så...
                        if (objects[i] is Wall)
                        {
                            //Kolla om spelaren och stolens position är samma.
                            if (item.Position.X == objects[i].Position.X &&
                        item.Position.Y == objects[i].Position.Y)
                            {
                                //I så fall Game Over och dödsorsak fastställs.
                                GameOver = true;
                                deathReason = DeathReason.collideWall;
                            }
                        }
                    }
                }
            }
        }
    }
}
