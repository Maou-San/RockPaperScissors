using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperAzure;
using SoftwireDojo;

namespace DojoRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Player cyclePlayer = new Player("CycleBot", new CycleBot());
            Player randomPlayer = new Player("RandomBot", new RandomBot());
            Player bigbangPlayer = new Player("BigbangBot", new BigbangBot());
            Player mwrPlayer = new Player("MWRBot", new MasterBot());
            Player shcPlayer = new Player("SHCBot", new MyBot());

            Dojo newDojo = new Dojo(cyclePlayer, randomPlayer, bigbangPlayer, mwrPlayer, shcPlayer);
            newDojo.Run();
        }
    }
}
