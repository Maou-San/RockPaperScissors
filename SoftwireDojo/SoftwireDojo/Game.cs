using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;
using RockPaperAzure;
using System.Threading;

namespace SoftwireDojo
{
    internal class Game
    {
        private int consecutiveDraws = 0;
        private Player player1, player2;
        private GameRules rules;
        private int round;
        private Move move1;
        private Move move2;
        
        public Game(Player player1, Player player2, GameRules rules)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.rules = rules;
        }

        public GameResult Run()
        {
            SetupGame();            

            for (round = 1; (round <= rules.MaximumGames) && !(HasAnyoneWon()); round++)
            {
                PlayRound();
            }

            Player winnerOfGame = DetermineWinner();           

            return new GameResult() { Player1 = player1, Player2 = player2, winner = winnerOfGame, Score1 = player1.Points, Score2 = player2.Points };
        }

        public void SetupGame()
        {            
            player1.DynamiteRemaining = rules.StartingDynamite;
            player2.DynamiteRemaining = rules.StartingDynamite;
            player1.NumberOfDecisions = 0;
            player2.NumberOfDecisions = 0;
            player1.LastMove = null;
            player2.LastMove = null;
            player1.Points = 0;
            player2.Points = 0;
            player1.Log = new GameLog();
            player2.Log = new GameLog();
            AppendToBothLogs(string.Format("{0} vs {1} at time {2}", player1.TeamName, player2.TeamName, DateTime.Now));
        }

        public void PlayRound()
        {
            move1 = player1.TeamBot.MakeMove(player1, player2, rules);
            move2 = player2.TeamBot.MakeMove(player2, player1, rules);            

            player1.EnsureMoveLegal(ref move1);
            player2.EnsureMoveLegal(ref move2);

            player1.UpdateDynamiteCounter(move1);
            player2.UpdateDynamiteCounter(move2);

            Player winnerOfRound = WhoWins(move1, move2);
            if (winnerOfRound == null)
            {
                consecutiveDraws++;
            }
            else
            {
                winnerOfRound.Points += 1 + consecutiveDraws;
                consecutiveDraws = 0;
            }

            AppendToBothLogs(string.Format("Round {0}:", round));
            AppendMovesToBothLogs();
            UpdatePlayerStatistics();
        }

        public Player DetermineWinner()
        {
            if (player1.Points > player2.Points)
            {
                return player1;
            }
            else if (player1.Points < player2.Points)
            {
                return player2;
            }
            else
            {
                return null;
            }
        }

        private bool HasAnyoneWon()
        {
            if ((player1.Points >= rules.PointsToWin) || (player2.Points >= rules.PointsToWin))
            {
                return true;
            }
            return false;
        }

        // Returns the winning player given their moves -- returns null if draw.
        public Player WhoWins(Move move1, Move move2)
        {
            // Convert moves to moveString, e.g. Rock vs Scissors becomes "RvS"
            string playerOneMoveString = string.Format("{0}v{1}", ConvertMoveToChar(move1), ConvertMoveToChar(move2));
            string playerTwoMoveString = string.Format("{1}v{0}", ConvertMoveToChar(move1), ConvertMoveToChar(move2));
            string[] drawMoves = new string[] { "RvR", "SvS", "PvP", "NvN" , "DvD" , "WvW" };
            string[] leftWinMoves = new string[] { "RvS", "SvP", "PvR",
                                                    "DvR", "DvP", "DvS", "WvD",
                                                    "RvW", "PvW", "SvW",
                                                    "RvN", "SvN", "PvN", "DvN", "WvN" };            

            if (drawMoves.Contains(playerOneMoveString))
            {
                return null;
            }
            else if (leftWinMoves.Contains(playerOneMoveString))
            {
                return player1;
            }
            else if (leftWinMoves.Contains(playerTwoMoveString))
            {
                return player2;
            }
            else
            {
                throw new Exception(string.Format("Invalid moveString: {0}",playerOneMoveString));
            }            
        }

        private void AppendToBothLogs(string input)
        {
            player1.Log.AppendLine(input);
            player2.Log.AppendLine(input);
        }

        private void UpdatePlayerStatistics()
        {
            player1.NumberOfDecisions++;
            player2.NumberOfDecisions++;
            player1.LastMove = move1;
            player2.LastMove = move2;
        }

        private void AppendMovesToBothLogs()
        {
            player1.Log.AppendLine(string.Format("\t{0} vs {1}", FormatMove(move1), FormatMove(move2)));
            player1.Log.AppendLine(string.Format("\t\t{0} - {1}", player1.Points, player2.Points));
            player2.Log.AppendLine(string.Format("\t{0} vs {1}", FormatMove(move2), FormatMove(move1)));
            player2.Log.AppendLine(string.Format("\t\t{0} - {1}", player2.Points, player1.Points));
        }

        static public string ConvertMoveToChar(Move move)
        {
            if (move == Moves.Dynamite)
                return "D";
            if (move == Moves.WaterBalloon)
                return "W";
            if (move == Moves.Rock)
                return "R";
            if (move == Moves.Scissors)
                return "S";
            if (move == Moves.Paper)
                return "P";
            return "N";
        }

        static private string FormatMove(Move move)
        {
            if (move == null)
            {
                return "Invalid Move";
            }
            else
            {
                return move.ToString();
            }
        }
    }
}
