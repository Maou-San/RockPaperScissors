namespace DojoRunner
{
    using RockPaperAzure;
    using SoftwireDojo;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var cyclePlayer = new Player("CycleBot", new CycleBot());
            var randomPlayer = new Player("RandomBot", new RandomBot());
            var bigbangPlayer = new Player("BigbangBot", new BigbangBot());
            var mwrPlayer = new Player("MWRBot", new MasterBot());
            var shcPlayer = new Player("SHCBot", new MyBot());
            var domPlayer = new Player("DomriBot", new DanMriBot());

            var newDojo = new Dojo(cyclePlayer, randomPlayer, bigbangPlayer, mwrPlayer, shcPlayer, domPlayer);
            newDojo.Run();
        }
    }
}