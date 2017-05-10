using UnityEngine;
using System.Collections;

public class PlantGrow : MonoBehaviour {

    public GameObject soil;
    [HideInInspector]
    public int row, col;
    [HideInInspector]
    public int price;

    protected GameModel model;
    protected Transform shadow;
    protected PlantSpriteDisplay display;
    protected void Awake() {
        model = GameModel.GetInstance();
        shadow = transform.FindChild("shadow");
        display = GetComponent<PlantSpriteDisplay>();
	}


    public virtual bool canGrowInMap(int row, int col) {
        GameObject plant = model.map[row, col];
        if (!plant || plant.GetComponent<PumpkinGrow>()) {
            return true;
        } else {
            return false;
        }
    }

    public virtual void grow(int _row, int _col) {
        row = _row;
        col = _col;

        GameObject mapPlant = model.map[row, col];
        if (mapPlant && mapPlant.GetComponent<PumpkinGrow>()) {
            mapPlant.GetComponent<PumpkinGrow>().innerPlant = gameObject;
        } else {
            model.map[row, col] = gameObject;
        }

        display.SetOrderByRow(row);

        if (shadow) {
            shadow.gameObject.SetActive(true);
        }

        if (soil) {
            GameObject temp = Instantiate(soil);
            temp.transform.position = transform.position;
            Destroy(temp, 0.2f);
        }

        gameObject.SendMessage("AfterGrow");
    }

    public void Sell() {
        model.sun += (int)(price * 0.6);
        GetComponent<PlantHealthy>().Die();
    }
}
