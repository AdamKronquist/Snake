using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// Olika saker man kan dö av.
    /// </summary>
    public enum DeathReason
    { collideWall, collideTail, starving, giveUp }
}
