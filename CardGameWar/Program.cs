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
        static void Main(string[] args)
        {
            int totalTurnCount = 0;
            int finiteGameCount = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < GameCount; i++)
            {
                //Create game

                Console.WriteLine("Starting game #{0}", i);
                Game game = new Game("Alice", "Bob", i+1);
                while (!game.IsEndOfGame())
                {
                    game.PlayTurn();
                }

                if (game.TurnCount < Game.TurnLimt)
                {
                    totalTurnCount += game.TurnCount;
                    finiteGameCount++;

                    var key = (game.TurnCount / 100) * 100;
                    _countByTurns.AddOrUpdate(key, k => 1, (k, v) => v + 1);

                }
            }
            sw.Stop();
            double avgTurn = (double)totalTurnCount / (double)finiteGameCount;

            Console.WriteLine("{0} finite games with an average of {1} turns per game. Time:{2}", finiteGameCount,  Math.Round(avgTurn, 2), sw.Elapsed );

            foreach (var kvp in _countByTurns.OrderBy(kvp=>kvp.Key))
            {
                Console.WriteLine("{0:0000}-{1:0000}: {2}", kvp.Key, kvp.Key+99, kvp.Value);
            }
            {
                
            }


            Console.Read();
        }
    }
}
