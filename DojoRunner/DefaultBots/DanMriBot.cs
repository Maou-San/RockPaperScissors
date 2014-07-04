namespace RockPaperAzure
{
    using RockPaperScissorsPro;
    using System.Collections.Generic;

    public class DanMriBot : IRockPaperScissorsBot
    {
        private int moveCount;
        private List<Move> opponentMoves;
        private int opponentDynamiteCount = 100;

        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (opponent.LastMove != null)
            {
                opponentMoves.Add(opponent.LastMove);
            }

            if (opponent.LastMove == Moves.Dynamite)
            {
                opponentDynamiteCount--;
                if (opponentMoves[opponentMoves.Count - 2] == Moves.Dynamite) {}
            }

            if (you.HasDynamite && moveCount % 10 == 0)
            {
                return Moves.Dynamite;
            }
            moveCount++;
            return Moves.Scissors;
        }
    }
}