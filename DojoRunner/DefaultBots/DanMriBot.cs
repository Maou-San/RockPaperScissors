namespace RockPaperAzure
{
    using RockPaperScissorsPro;
    using System;
    using System.Collections.Generic;

    public class DanMriBot : IRockPaperScissorsBot
    {
        private readonly List<Move> opponentMoves = new List<Move>();
        private readonly Random random = new Random();
        private int moveCount;
        private int m = 1;
        private int opponentDynamiteCount = 100;
        private int myDynamiteCount = 100;
        private int draws;

        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            moveCount++;
            draws = you.LastMove == opponent.LastMove ? draws + 1 : 0;

            if (opponent.LastMove != null)
            {
                opponentMoves.Add(opponent.LastMove);
            }

            if (opponent.LastMove == Moves.Dynamite)
            {
                opponentDynamiteCount--;
                if (opponentMoves.Count >= 2)
                {
                    if (opponentMoves[opponentMoves.Count - 2] == Moves.Dynamite && opponent.HasDynamite)
                    {
                        return Moves.WaterBalloon;
                    }
                }
            }

            /*if (draws >= 2)
            {
                if (you.HasDynamite)
                {
                    return Moves.Dynamite;
                }
            }*/

            if (you.HasDynamite && moveCount % 10 == m)
            {
                //m = (random.Next(1, 100) % 10);
                myDynamiteCount--;
                return Moves.Dynamite;
            }

            return GetRandomMove();
        }

        private Move GetRandomMove()
        {
            switch (random.Next(1, 100) % 3)
            {
                case 0:
                    return Moves.Rock;
                case 1:
                    return Moves.Paper;
            }
            return Moves.Scissors;
        }
    }
}