using System;


namespace CardGameWar.Objects
{
    public class Card
    {
        public string DisplayName { get; set; }
        public Suit Suit { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
