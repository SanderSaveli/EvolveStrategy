using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadData
{
    public int levelNumber { get; private set; }
    public GameLoadData(int levelNumber) 
    {
        this.levelNumber = levelNumber;
    }
}
