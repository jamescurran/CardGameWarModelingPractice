#define ORIGINAL
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CardGameWar.Objects
{
    public class Game
    {

        public const int TurnLimt = 1000;
        private Player Player1;
        private Player Player2;
        public int TurnCount;

        // _out1 display just the game final results
        private readonly TextWriter _out1 =  Console.Out; // new NullTextWriter(); //   

        // _out2 displays verbose results of each hand.
        private readonly TextWriter _out2 = Console.Out; // new NullTextWriter(); //

        // _out3 display2 terse results of each hand.
        private readonly TextWriter _out3 = new NullTextWriter(); //  Console.Out; //

        public Game(string player1name, string player2name, int seed = 0)
        {
            Winner = Outcome.Playing;

            Player1 = new Player(player1name);
            Player2 = new Player(player2name);

            var cards = DeckCreator.CreateCards(seed); //Returns a shuffled set of cards

            var deck = Player1.Deal(cards); //Returns Player2's deck.  Player1 keeps his.
            Player2.Deck = deck;
        }

        public bool IsEndOfGame()
        {
            if (!Player2.Deck.Any())
            {
                _out1.WriteLine(Player2.Name + " is out of cards!  " + Player1.Name + " WINS!");
                _out1.WriteLine("TURNS: " + TurnCount.ToString());
                Winner = Outcome.Player1;
                return true;
            }
            else if(!Player1.Deck.Any())
            {
                _out1.WriteLine(Player1.Name + " is out of cards!  " + Player2.Name + " WINS!");
                _out1.WriteLine("TURNS: " + TurnCount.ToString());
                Winner = Outcome.Player2;
                return true;
            }
            else if(TurnCount > TurnLimt)
            {
                TestDeck(Player1, Player2);
                _out1.WriteLine("Infinite game!  Let's call the whole thing off.");
                Winner = Outcome.Draw;
                return true;
            }
            return false;
        }

        public void PlayTurn()
        {
            var pool = new Deck<Card>();

            var player1card = Player1.Deck.Pop();
            var player2card = Player2.Deck.Pop();

            if ((TurnCount & 1) == 1)
            {
            pool.Push(player1card);
            pool.Push(player2card);
            }
            else
            {
                pool.Push(player2card);
                pool.Push(player1card);
            }

            _out2.Write("{0} plays {1}, {2} plays {3} ", Player1.Name, player1card.DisplayName, Player2.Name, player2card.DisplayName);
            _out3.Write("(1:{0}, 2:{1} = ", player1card.DisplayName,  player2card.DisplayName);

            while (player1card.Value == player2card.Value)
            {
                _out2.WriteLine("WAR!");

                if(Player2.Deck.Count < 4)
                {
                    TestDeck(Player1, Player2, pool);
                    Player2.Deck.Clear();
                    return;
                }

                if (Player1.Deck.Count < 4)
                {
                    TestDeck(Player1, Player2, pool);
                    Player1.Deck.Clear();
                    return;
                }
                
                pool.Push(Player1.Deck.Pop());
                pool.Push(Player1.Deck.Pop());
                pool.Push(Player1.Deck.Pop());

                pool.Push(Player2.Deck.Pop());
                pool.Push(Player2.Deck.Pop());
                pool.Push(Player2.Deck.Pop());

                player1card = Player1.Deck.Pop();
                player2card = Player2.Deck.Pop();

                if ((TurnCount & 1) == 1)
                {
                pool.Push(player1card);
                pool.Push(player2card);
                }
                else
                {
                    pool.Push(player2card);
                    pool.Push(player1card);
                }

                _out2.Write("{0} plays {1}, {2} plays {3} ",    Player1.Name , player1card.DisplayName , Player2.Name , player2card.DisplayName);
                _out3.Write("W: 1:{0}, 2:{1} = ", player1card.DisplayName, player2card.DisplayName);
            }

            if (player1card.Value < player2card.Value)
            {
                Player2.Deck.Append(pool);
                _out2.WriteLine( "--- {0} takes the hand!", Player2);
                _out3.Write("2/{0});  ", Player2.Deck.Count);
            }
            else
            {
                Player1.Deck.Append(pool);
                _out2.WriteLine("--- {0} takes the hand over {1}", Player1, Player2);
                _out3.Write("1/{0});  ", Player1.Deck.Count);
            }

            TurnCount++;
        }
        public Outcome Winner { get; set; }

        // Make sure deck has not been corrupted.
        public static void TestDeck(Player one, Player two, IEnumerable<Card> pool = null)
        {
            pool = pool ?? Enumerable.Empty<Card>();
            var deck = one.Deck.Concat(two.Deck).Concat(pool).ToList();

            var suits = deck.GroupBy(c => c.Suit);
            Debug.Assert(suits.Count()==4);
            Debug.Assert(suits.All(s=>s.Count()==13));

            var ranks = deck.GroupBy(c => c.Value);
            Debug.Assert(ranks.Count() == 13);
            Debug.Assert(ranks.All(s => s.Count() == 4));

//            Console.WriteLine("Deck OK");
        }


    }


    public enum Outcome
    {
        Playing,
        Player1,
        Player2,
        Draw
    };
}
