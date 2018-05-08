using System;
using System.IO;
using System.Linq;

namespace CardGameWar.Objects
{
    public class Game
    {

        public const int TurnLimt = 5000;
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
            Player1 = new Player(player1name);
            Player2 = new Player(player2name);

            var cards = DeckCreator.CreateCards(seed); //Returns a shuffled set of cards

            var deck = Player1.Deal(cards); //Returns Player2's deck.  Player1 keeps his.
            Player2.Deck = deck;
        }

        public bool IsEndOfGame()
        {
            if(!Player1.Deck.Any())
            {
                _out1.WriteLine(Player1.Name + " is out of cards!  " + Player2.Name + " WINS!");
                _out1.WriteLine("TURNS: " + TurnCount.ToString());
                return true;
            }
            else if(!Player2.Deck.Any())
            {
                _out1.WriteLine(Player2.Name + " is out of cards!  " + Player1.Name + " WINS!");
                _out1.WriteLine("TURNS: " + TurnCount.ToString());
                return true;
            }
            else if(TurnCount > TurnLimt)
            {
                _out1.WriteLine("Infinite game!  Let's call the whole thing off.");
                return true;
            }
            return false;
        }

        public void PlayTurn()
        {
            var pool = new Deck<Card>();

            var player1card = Player1.Deck.Pop();
            var player2card = Player2.Deck.Pop();

            pool.Push(player1card);
            pool.Push(player2card);

            _out2.Write("{0} plays {1}, {2} plays {3} ", Player1.Name, player1card.DisplayName, Player2.Name, player2card.DisplayName);
            _out3.Write("(1:{0}, 2:{1} = ", player1card.DisplayName,  player2card.DisplayName);

            while (player1card.Value == player2card.Value)
            {
                _out2.WriteLine("WAR!");
                if (Player1.Deck.Count < 4)
                {
                    Player1.Deck.Clear();
                    return;
                }
                if(Player2.Deck.Count < 4)
                {
                    Player2.Deck.Clear();
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

                pool.Push(player1card);
                pool.Push(player2card);

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
    }
}
