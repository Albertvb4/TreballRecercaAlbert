using UnityEngine;
using System.Collections;

public class PlantHealthy : MonoBehaviour {

    public int hp;

    protected Animator animator;
    protected GameModel model;
    protected PlantGrow grow;

    protected void Awake() {
        animator = GetComponentInChildren<Animator>();
        model = GameModel.GetInstance();
        grow = GetComponent<PlantGrow>();
	}

    public virtual void Damage(int val) {
        hp -= val;
        if (hp < 0) Die();
    }

    public virtual void Die() {
        model.map[grow.row, grow.col] = null;
        Destroy(gameObject);
    }
}
