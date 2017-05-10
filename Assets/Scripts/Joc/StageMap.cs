using UnityEngine;
using System.Collections;

public class StageMap {
    public const int ROW_MAX = 5;
    public const int COL_MAX = 9;

    public const float GRID_TOP = 2.3f;
    public const float GRID_LEFT = -2.0f;
    public const float GRID_BOTTOM = -2.6f;
    public const float GRID_RIGHT = 5.2f;
    public const float GRID_WIDTH = 0.8f;
    public const float GRID_HEIGHT = 1f;

    static public Vector3 GetPlantPos(int row, int col) {
        return new Vector3(
                    GRID_LEFT + 0.5f + col * GRID_WIDTH,
                    GRID_TOP - 0.7f - row * GRID_HEIGHT, 0);
    }

    static public Vector3 GetZombiePos(int row) {
        float offset = Random.Range(1.0f, 2.0f);
        return new Vector3(GRID_RIGHT + offset, GRID_TOP - 0.7f - row * GRID_HEIGHT, 0);
    }

    static public bool IsPointInMap(Vector3 point) {
        return point.x <= GRID_RIGHT && point.x >= GRID_LEFT &&
                point.y <= GRID_TOP && point.y >= GRID_BOTTOM;
    }

    static public void GetRowAndCol(Vector3 point, out int row, out int col) {
        col = Mathf.FloorToInt((point.x - GRID_LEFT) / GRID_WIDTH);
        row = Mathf.FloorToInt((GRID_TOP - point.y) / GRID_HEIGHT);
    }
}
