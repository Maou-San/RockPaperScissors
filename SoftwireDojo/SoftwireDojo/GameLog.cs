using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissorsPro;

namespace RockPaperAzure
{
    internal class GameLog : IGameLog, IEnumerable<string>
    {
        private List<string> logBook = new List<string>();

        public void AppendLine(string line)
        {
            logBook.Add(line);
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < logBook.Count; i++)
            {
                yield return logBook[i];
            }
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
