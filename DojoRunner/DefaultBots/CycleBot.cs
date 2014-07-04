namespace RockPaperAzure
{
    using RockPaperScissorsPro;

    public class CycleBot : IRockPaperScissorsBot
    {

        // Cycle sample implementation
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (you.LastMove == Moves.Rock)
            {
                return Moves.Paper;
            }

            if (you.LastMove == Moves.Paper)
            {
                return Moves.Scissors;
            }

            if (you.LastMove == Moves.Scissors)
            {
                if (you.HasDynamite)
                {
                    return Moves.Dynamite;
                }
                else
                {
                    return Moves.WaterBalloon;
                }
            }

            if (you.LastMove == Moves.Dynamite)
            {
                return Moves.WaterBalloon;
            }

            return Moves.Rock;
        }
    }
}