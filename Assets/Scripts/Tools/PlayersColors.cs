using UnityEngine;

public class PlayersColors
{
    public Color GetColor(AcktorList player) 
    {
        switch (player) 
        { 
            case AcktorList.None:
                return Color.gray;

            case AcktorList.Player:
                return Color.yellow;

            case AcktorList.Red:
                return Color.red;

            case AcktorList.Blue:
                return Color.blue;

            case AcktorList.Green:
                return Color.green;

            default: 
                return Color.white;
        }
    }
}
