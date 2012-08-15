using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class BigbangBot : IRockPaperScissorsBot
    {
        // BigBang sample implementation
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (you.NumberOfDecisions < 5)
            {
                return Moves.Dynamite;
            }
            else
            {
                return Moves.GetRandomMove();
            }
        }
    }
}
