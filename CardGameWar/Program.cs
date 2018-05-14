using CardGameWar.Objects;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace CardGameWar
{
    class Program
    {

        static readonly ConcurrentDictionary<int, int> _countByTurns = new ConcurrentDictionary<int, int>();

        private const int GameCount = 100;
        static void Main()
        {
            int totalTurnCount = 0;
            int finiteGameCount = 0;
            string player1name = "Alice";
            string player2name = "Bob";
            int player1WinDifference = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < GameCount; i++)
            {
                //Create game

                Console.WriteLine("Starting game #{0}", i);
                Game game = new Game(player1name, player2name, 0);
                while (!game.IsEndOfGame())
                {
                    game.PlayTurn();
                }

                if (game.Winner == Outcome.Player1)
                {
                    player1WinDifference++;
                }
                else if (game.Winner == Outcome.Player2)
                {
                    player1WinDifference--;
                }

                if (game.Winner != Outcome.Draw)
                {
                    totalTurnCount += game.TurnCount;
                    finiteGameCount++;

                    var key = (game.TurnCount / 100) * 100;
                    _countByTurns.AddOrUpdate(key, k => 1, (k, v) => v + 1);

                }
            }
            sw.Stop();
            double avgTurn = totalTurnCount / (double)finiteGameCount;

            Console.WriteLine("{0} finite games with an average of {1} turns per game. Time:{2}", finiteGameCount,  Math.Round(avgTurn, 2), sw.Elapsed );

            foreach (var kvp in _countByTurns.OrderBy(kvp=>kvp.Key))
            {
                Console.WriteLine("{0:0000}-{1:0000}: {2}", kvp.Key, kvp.Key+99, kvp.Value);
            }
            Console.WriteLine(finiteGameCount + " finite games with an average of " + Math.Round(avgTurn, 2) + " turns per game.");
            if (player1WinDifference == 0)
            {
                Console.WriteLine("Both players won the same number of games!");
            }
            else if (player1WinDifference > 0)
            {
                Console.WriteLine(player1name + " won " + player1WinDifference.ToString() + " more games than " + player2name);
            }
            else
            {
                Console.WriteLine(player2name + " won " + (player1WinDifference * -1).ToString() + " more games than " + player1name);
            }


            Console.Read();
        }
    }
}
