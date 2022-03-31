﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    interface IMovable
    {
        //Visual Studios autocorrect till min kod som syns nedan.
        Direction GetDirection();
        void SetDirection(Direction value);


        //Först skrev jag följande:
        //public Direction Direction { get; set; }  <--- Skrev såhär först när jag hade en .Net Core applikation.
        //Fick då följande felmeddelande då jag nu har lagt över all kod till en .Net Framework applikation istället:
        //The modifier 'public' is not valid for this item in C# 7.3. Please use language version '8.0' och greater.
        //Anledningen till att jag ändrade om är för att jag skulle kunna spela upp ljud, det gick inte i Core.
    }
}
