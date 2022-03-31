using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        /// <summary>
        /// Kollar så spelarens position ändras när man anropas dess Update-metod.
        /// I detta fall ett steg åt vänster.
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Player playerTest = new Player(new Position(10, 20));
            playerTest.SetDirection(Direction.Left);
            playerTest.Update();
            Assert.AreEqual(9, playerTest.Position.X);
        }

        /// <summary>
        /// Kollar så spelarens position ändras till andra sidan skärmen om man kommer utanför.
        /// </summary>
        [TestMethod()]
        public void OutsideScreenTest()
        {
            Player playerTest = new Player(new Position(-1, 20));
            playerTest.OutsideScreen();
            Assert.AreEqual(49, playerTest.Position.X);
        }
    }
}