using UnityEngine;
using System.Collections;

public class PlantSpriteDisplay : MonoBehaviour, SpriteDisplay {

    public int orderOffset = 0;

    private SpriteRenderer shadow;
    private SpriteRenderer plant;

    void Awake() {
        if (transform.FindChild("shadow")) {
            shadow = transform.FindChild("shadow").GetComponent<SpriteRenderer>();
        }
        plant = transform.FindChild("plant").GetComponent<SpriteRenderer>();
    }

    public void SetAlpha(float a) {
        Color color = Color.white;
        color.a = a;
        if (shadow) {
            shadow.color = color;
        }
        plant.color = color;
    }

    public void SetOrder(int order) {
        plant.sortingOrder = order;
    }

    public void SetOrderByRow(int row) {
        plant.sortingOrder = 1000 * (row + 1) + orderOffset;
    }
}
