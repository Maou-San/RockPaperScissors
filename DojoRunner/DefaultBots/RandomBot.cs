using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class RandomBot : IRockPaperScissorsBot
    {
        // Random sample implementation
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            you.Log.AppendLine("Choose Random Move!");
            return Moves.GetRandomMove();
        }
    }
}
