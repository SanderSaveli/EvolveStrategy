using UnityEngine;

namespace CardSystem
{
    public interface ICard
    {
        RectTransform rectTransform { get; }
        bool IsEquiped { get; }

        CardData cardData { get; }
    }

}

