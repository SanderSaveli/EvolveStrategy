using UnityEngine;

[CreateAssetMenu(fileName = "RegionPalette", menuName = "Palettes/New Region Palette")]
public class RegionPalette : ScriptableObject
{
    public Shader walcking;
    public Shader swimming;
    public Shader climbing;

    public Color temperate;
    public Color hot;
    public Color cold;
    public Color poison;

    public Shader GetShader(MoveType moveType)
    {
        switch (moveType)
        {
            case (MoveType.Walcking):
                return walcking;
            case (MoveType.Swimming):
                return swimming;
            case (MoveType.Climbing):
                return climbing;
            default:
                return walcking;
        }
    }

    public Color GetColor(ClimateType moveType)
    {
        switch (moveType)
        {
            case (ClimateType.Temperate):
                return temperate;
            case (ClimateType.Cold):
                return cold;
            case (ClimateType.Hot):
                return hot;
            case (ClimateType.Poison):
                return poison;
            default:
                return temperate;
        }
    }
}
