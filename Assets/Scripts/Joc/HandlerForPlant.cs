using UnityEngine;
using System.Collections;

public class HandlerForPlant : MonoBehaviour {

    public AudioClip seedLift;
    public AudioClip seedCancel;
    public AudioClip plantGrow;
    private GameObject tempPlant;
    private GameObject selectedPlant;
    private Card selectedCard;
    private int row = -1;
    private int col = -1;

    void Update() {
        HandleMouseMoveForPlant();
        HandleMouseDownForPlant();
    }

    void HandleMouseDownForPlant() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D collider = Physics2D.OverlapPoint(Utility.GetMouseWorldPos());

            if (collider != null) {
                CancelSelectdCard();
                if (collider.gameObject.tag == "Card") {
                    collider.gameObject.SendMessage("OnSelect");
                    AudioManager.GetInstance().PlaySound(seedLift);
                }             
            } else if (selectedPlant) {
                if (row != -1) {
                    selectedPlant.transform.position = StageMap.GetPlantPos(row, col);
                    selectedPlant.GetComponent<PlantGrow>().grow(row, col);
                    AudioManager.GetInstance().PlaySound(plantGrow);
                    selectedPlant = null;

                    Destroy(tempPlant);
                    tempPlant = null;

                    selectedCard.Select();
                    selectedCard = null;
                } else {
                    CancelSelectdCard();
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            CancelSelectdCard();
        }
    }

    void HandleMouseMoveForPlant() {
        if (selectedPlant) {
            Vector3 pos = Utility.GetMouseWorldPos();
            Vector3 plantPos = pos;
            plantPos.y -= 0.3f;
            selectedPlant.transform.position = plantPos;

            if (StageMap.IsPointInMap(pos)) {
                StageMap.GetRowAndCol(pos, out row, out col);
                if (tempPlant.GetComponent<PlantGrow>().canGrowInMap(row, col)) {
                    tempPlant.transform.position = StageMap.GetPlantPos(row, col);
                    tempPlant.GetComponent<SpriteDisplay>().SetOrderByRow(row);
                } else {
                    col = row = -1;
                    tempPlant.transform.position = new Vector3(1000, 1000, 0);
                }
            } else {
                col = row = -1;
                tempPlant.transform.position = new Vector3(1000, 1000, 0);
            }
        }
    }

    void CancelSelectdCard() {
        if (selectedCard) {
            Destroy(tempPlant);
            Destroy(selectedPlant);
            selectedPlant = tempPlant = null;

            selectedCard.state &= ~Card.State.SELECTED;
            selectedCard = null;
            AudioManager.GetInstance().PlaySound(seedCancel);
        }
    }


    public void SetSelectedCard(Card card) {
        card.state = Card.State.SELECTED;
        selectedCard = card;

        tempPlant = Instantiate(card.plant);
        tempPlant.GetComponent<SpriteDisplay>().SetAlpha(0.6f);
        tempPlant.transform.position = new Vector3(1000, 1000, 0);

        selectedPlant = Instantiate(card.plant);
        selectedPlant.GetComponent<SpriteDisplay>().SetOrder(15000);
        selectedPlant.transform.position = new Vector3(1000, 1000, 0);      
    }
}
