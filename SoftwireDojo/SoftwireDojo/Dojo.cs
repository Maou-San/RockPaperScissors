using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Bot;
using RockPaperScissorsPro;
using RockPaperAzure;
using System.IO;

namespace SoftwireDojo
{
    public class Dojo
    {
        private Player[] players;
        private Game[,] games;
        private int numberOfPlayers;
        private GameResult[,] results;
        private int columnWidth;
        private GameRules rules = GameRules.Default;

        public Dojo(params Player[] players)
        {
            Construct(players);
        }

        public Dojo(GameRules rules, params Player[] players)
        {
            Construct(players);
            this.rules = rules;
        }

        private void Construct(Player[] players)
        {
            this.players = players;
            numberOfPlayers = players.Count();
            results = new GameResult[numberOfPlayers, numberOfPlayers];
            // Columnwidth needs to be at least 7 to accommodate scores such as 1000-999
            columnWidth = Math.Max(GetMaxLengthOfTeamName(), 7);
        }

        public void Run()
        {
            games = new Game[numberOfPlayers,numberOfPlayers];

            // Player i plays against player j only if i < j.
            for (int i = 0; i < numberOfPlayers; i++)
            {
                for (int j = i + 1; j < numberOfPlayers; j++)
                {
                    PlayGame(i, j);
                }
            }

            OutputTable();
            OutputDigest();
            Console.Write("Press any key to exit...");
            Console.Read();
        }

        private void PlayGame(int i, int j)
        {
            games[i, j] = new Game(players[i], players[j], GameRules.Default);
            results[i, j] = games[i, j].Run();
            if (results[i, j].winner == players[i])
            {
                players[i].WinGame();
                players[j].LoseGame();
            }
            else if (results[i, j].winner == players[j])
            {
                players[j].WinGame();
                players[i].LoseGame();
            }
            else
            {
                players[j].DrawGame();
                players[i].DrawGame();
            }
            WriteLog(players[i], players[j]);
            WriteLog(players[j], players[i]);
        }

        private void OutputDigest()
        {
            string nameAlign = "{0," + columnWidth + "}";
            IEnumerable<Player> league = players.OrderByDescending(p => p.Draws);
            league = league.OrderByDescending(p => p.Wins);
            foreach (Player player in league) 
            {
                Console.WriteLine(nameAlign + "\tWins: {1}\tDraws:{2}\tLosses:{3}", player.TeamName, player.Wins, player.Draws, player.Losses);
            }
        }

        private void OutputTable()
        {
            StringBuilder table = new StringBuilder();
            string linebreak = "\n" + new string('-', (columnWidth + 1) * (numberOfPlayers + 1));

            string rightAligned = "{0," + columnWidth + "}|";
            string leftAligned = "{0," + (-columnWidth) + "}|";

            // Write header.
            table.AppendFormat(rightAligned, "vs");
            for (int j = 0; j < numberOfPlayers; j++)
            {
                table.AppendFormat(leftAligned, players[j].TeamName);
            }

            table.AppendLine(linebreak);

            int leftScore = -(int)Math.Ceiling((double)(columnWidth) / 2);
            int rightScore = (int)Math.Floor((double)(columnWidth) / 2);
            string scoreAlign = "{0," + leftScore + "}{1," + rightScore + "}|";

            for (int i = 0; i < numberOfPlayers; i++)
            {
                // Upper half of row.
                table.AppendFormat(rightAligned, players[i].TeamName);
                for (int j = 0; j < i; j++)
                {
                    table.AppendFormat(rightAligned, results[j, i].Score1);
                }
                table.AppendFormat(rightAligned, "");
                for (int j = i + 1; j < numberOfPlayers; j++)
                {
                    table.AppendFormat(rightAligned, results[i, j].Score2);
                }
                table.AppendLine();

                // Bottom half of row.
                table.AppendFormat(rightAligned, "");
                for (int j = 0; j < i; j++)
                {
                    table.AppendFormat(leftAligned, results[j, i].Score2);
                }
                table.AppendFormat(rightAligned, "");
                for (int j = i + 1; j < numberOfPlayers; j++)
                {
                    table.AppendFormat(leftAligned, results[i, j].Score1);
                }

                table.AppendLine(linebreak);
            }

            Console.WriteLine(table);
        }

        private int GetMaxLengthOfTeamName()
        {
            int length = 0;
            foreach (Player player in players)
            {
                length = Math.Max(player.TeamName.Length, length);
            }
            return length;
        }

        static private void WriteLog(Player player, Player opponent)
        {
            string myDir = @".\Log\";
            string fileName = string.Format("{0} (vs {1}).txt", player.TeamName, opponent.TeamName);
            string filePath = string.Format("{0}{1}", myDir, fileName);            

            if (!Directory.Exists(myDir))
            {
                Directory.CreateDirectory(myDir);
            }
            File.WriteAllLines(filePath, (GameLog)player.Log);
        }
    }
}
