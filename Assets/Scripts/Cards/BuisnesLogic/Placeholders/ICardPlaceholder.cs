namespace CardSystem
{
    public interface ICardPlaceholder
    {
        bool isEmpty { get; }

        ICard card { get; }


        public bool TryPlaceCard(ICard card);

        public void ReturnCardBack(ICard card);

        public void RemoveCard(ICard card);
    }
}
