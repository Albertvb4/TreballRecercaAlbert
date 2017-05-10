using UnityEngine;
using System.Collections;

public class GameModel {

    public GameObject[,] map;
    public ArrayList[] zombieList;
    public ArrayList[] bulletList;
    public int sun;
    public bool inDay;

    private GameModel() {
        Clear();
    }

    public void Clear() {
        map = new GameObject[StageMap.ROW_MAX, StageMap.COL_MAX];
        zombieList = new ArrayList[StageMap.ROW_MAX];
        bulletList = new ArrayList[StageMap.ROW_MAX];
        for (int i = 0; i < StageMap.ROW_MAX; ++i) {
            zombieList[i] = new ArrayList();
            bulletList[i] = new ArrayList();
        }
    }

    private static GameModel instance;
    public static GameModel GetInstance() {
        if (instance == null)
            instance = new GameModel();
        return instance;
    }
}
