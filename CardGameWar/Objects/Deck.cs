using System;
using System.Collections.Generic;


namespace CardGameWar.Objects
{
#if true
    public class Deck<T> : Queue<T>
    {

        public Deck()
        {
            
        }

        public Deck(IEnumerable<T> initial) : base(initial)
        {
                
        }

        public T Pop()
        {
            return this.Dequeue();
        }

        public void Push(T card)
        {
            this.Enqueue(card);
        }

        public void Append(IEnumerable<T> newCards)
        {
            foreach (var card in newCards)
            {
                this.Enqueue(card);
            }
        }
    }
#else
    public class Deck<T> : List<T>
    {

        public Deck()
        {

        }

        public Deck(IEnumerable<T> initial) : base(initial)
        {

        }
        public T Pop()
        {
            var card = this.First();
            this.RemoveAt(0);
            return card;
        }

        public void Push(T card)
        {
            this.Insert(0, card);
        }

        public void Append(IEnumerable<T> newCards)
        {
            foreach (var card in newCards)
            {
                this.Insert(this.Count, card);
            }
        }
    }
#endif
}
