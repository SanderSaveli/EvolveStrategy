using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardPalette", menuName = "Cards/New CardPalette")]
    public class TypeToColorConverter : ScriptableObject
    {
        [Header("Mouth")]
        [SerializeField] private Sprite MouthIcon;
        [SerializeField] private Color MouthColor;

        [Header("InternalStructure")]
        [SerializeField] private Sprite InternalStructureIcon;
        [SerializeField] private Color InternalStructureColor;

        [Header("Head")]
        [SerializeField] private Sprite HeadIcon;
        [SerializeField] private Color HeadColor;

        [Header("Cover")]
        [SerializeField] private Sprite CoverIcon;
        [SerializeField] private Color CoverColor;

        [Header("Limbs")]
        [SerializeField] private Sprite LimbsIcon;
        [SerializeField] private Color LimbsColor;

        public Sprite GetTypeSprite(CardType type)
        {
            switch (type)
            {
                case (CardType.Head):
                    return HeadIcon;
                case (CardType.Limbs):
                    return LimbsIcon;
                case (CardType.Mouth):
                    return MouthIcon;
                case (CardType.Cover):
                    return CoverIcon;
                case (CardType.InternalStructure):
                    return InternalStructureIcon;
            }
            return null;
        }

        public Color GetTypeColor(CardType type)
        {
            switch (type)
            {
                case (CardType.Head):
                    return HeadColor;
                case (CardType.Limbs):
                    return LimbsColor;
                case (CardType.Mouth):
                    return MouthColor;
                case (CardType.Cover):
                    return CoverColor;
                case (CardType.InternalStructure):
                    return InternalStructureColor;
            }
            return Color.white;
        }
    }
}

