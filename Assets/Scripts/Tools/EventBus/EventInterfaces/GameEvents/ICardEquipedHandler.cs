using CardSystem;

namespace EventBusSystem 
{
    public interface ICardEquipedHandler : IGlobalSubscriber
    {
        public void CardEquiped(ICard card, ICard previousCard);
    }

}