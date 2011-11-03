using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperAzure;

namespace SoftwireDojo
{
    internal class GameResult
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player winner { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }
}
