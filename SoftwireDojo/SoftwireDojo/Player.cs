using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    public class Player : IPlayer
    {
        public string TeamName { get; set; }
        public IRockPaperScissorsBot TeamBot { get; set; }
        public Move LastMove { get; set; }
        public IGameLog Log { get; set; }
        public int NumberOfDecisions { get; set; }
        public int Points { get; set; }
        public int DynamiteRemaining { get; set; }        
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        // TotalTimeDeciding not yet implemented.
        public TimeSpan TotalTimeDeciding { get; set; }

        public Player(string teamName, IRockPaperScissorsBot bot)
        {
            TeamName = teamName;
            TeamBot = bot;
        }        

        public bool HasDynamite
        {
            get
            {
                if (DynamiteRemaining == 0)
                {
                    return false;
                }
                return true;
            }            
        }

        public void EnsureMoveLegal(ref Move move)
        {
            if (move == Moves.Dynamite && !HasDynamite)
            {
                move = null;
            }
        }

        public void UpdateDynamiteCounter(Move move)
        {
            if (move == Moves.Dynamite)
            {
                DynamiteRemaining--;
            }
        }

        public void WinGame()
        {
            Wins += 1;
        }

        public void DrawGame()
        {
            Draws += 1;
        }

        public void LoseGame()
        {
            Losses += 1;
        }
    }
}
