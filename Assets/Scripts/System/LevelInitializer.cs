using BattleSystem;
using UISystem;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    private SceneLoader loader;
    private GameLoadData data;

    private void Awake()
    {
        loader = SceneLoader.instance;
        data = loader.gameLoadData;
        CreateTilemap();
        new ServiceRegistrator().RegistrateAllServices();
    }

    private void CreateTilemap()
    {
        Instantiate(GetTilemapForLevel(data.levelNumber));
    }

    private GameObject GetTilemapForLevel(int level)
    {
        string path = "Tilemaps/level" + level.ToString();
        return (GameObject)Resources.Load(path);
    }
}
