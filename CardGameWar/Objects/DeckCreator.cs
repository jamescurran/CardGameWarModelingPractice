using System;
using System.Collections.Generic;

namespace CardGameWar.Objects
{
    public static class DeckCreator
    { 
        public static Deck<Card> CreateCards(int seed = 0)
        {
            var  cards = new List<Card>(52);
            for(int i = 2; i <= 14; i++)
            {
                foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(new Card()
                    {
                        Suit = suit,
                        Value = i,
                        DisplayName = GetShortName(i, suit)
                    });
                }
            }
            return Shuffle(cards, seed);
        }

        private static Deck<Card> Shuffle(List<Card> cards, int seed = 0)
        {
            if (seed == 0)
                seed = DateTime.Now.Millisecond;
            //First, shuffle the existing cards using Fisher-Yates
            Random r = new Random(seed);
            for (int n = cards.Count - 1; n > 0; --n)
            {
                //Step 2: Randomly pick a card which has not been shuffled
                int k = r.Next(n + 1);

                //Step 3: Swap the selected item with the last "unstruck" letter in the collection
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return new Deck<Card>(cards);
            }


        private const string Ranks = "__23456789_JQKA";
        private const string Suits = "CDSH";

        private static string GetShortName(int value, Suit suit)
        {
#if true
            return (value == 10 ? "10" : Ranks[value].ToString()) + Suits[(int) suit].ToString();
#else
            string valueDisplay = "";
            if (value >= 2 && value <= 10)
            {
                valueDisplay = value.ToString();
            }
            else if (value == 11)
            {
                valueDisplay = "J";
            }
            else if (value == 12)
            {
                valueDisplay = "Q";
            }
            else if (value == 13)
            {
                valueDisplay = "K";
            }
            else if (value == 14)
            {
                valueDisplay = "A";
            }

            return valueDisplay + Enum.GetName(typeof(Suit), suit)[0];
#endif
        }
    }
}
