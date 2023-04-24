using CardSystem;

namespace EventBusSystem
{
    public interface ICardBoughtHandler : IGlobalSubscriber
    {
        public void CardBought(CardData card);
    }
}

