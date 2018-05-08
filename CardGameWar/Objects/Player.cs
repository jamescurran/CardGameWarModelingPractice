using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameWar.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public Deck<Card> Deck { get; set; }

        public Player() { }

        public Player(string name)
        {
            Name = name;
        }

        public Deck<Card> Deal(Deck<Card> cards)
        {
            var player1cards = new Deck<Card>();
            var player2cards = new Deck<Card>();

            bool forOne = false;
            foreach (var card in cards)
            {
                if (forOne) //Card etiquette says the player who is NOT the dealer gets first card
                    player1cards.Push(card);
                else
                    player2cards.Push(card);
                forOne = !forOne;
                }

            Deck = player1cards;
            return player2cards;
        }

        public override string ToString()
        {
            return String.Format("{0} :{1} cards", Name, Deck.Count);
        }
    }
}
