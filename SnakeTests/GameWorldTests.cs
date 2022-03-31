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
    public class GameWorldTests
    {
        /// <summary>
        /// Testar så maten läggs till i listan när man anropar metoden.
        /// </summary>
        [TestMethod()]
        public void CreateFoodTest()
        {
            GameWorld worldTest = new GameWorld(50, 40);
            Assert.AreEqual(0, worldTest.objects.Count);
            worldTest.CreateFood();
            Assert.AreEqual(1, worldTest.objects.Count);
        }
    }
}