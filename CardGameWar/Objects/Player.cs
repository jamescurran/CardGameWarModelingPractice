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
            var owncards = new Deck<Card>();
            var opponentscards = new Deck<Card>();

            bool forMe = false;
            foreach (var card in cards)
            {
                if (forMe)   //Card etiquette says the player who is NOT the dealer gets first card 
                    owncards.Push(card);
                else
                    opponentscards.Push(card);
                forMe = !forMe;
                }

            Deck = owncards;
            return opponentscards;
        }

        public override string ToString()
        {
            return String.Format("{0} :{1} cards", Name, Deck.Count);
        }
    }
}
