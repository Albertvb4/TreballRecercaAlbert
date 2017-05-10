using UnityEngine;
using System.Collections;

public class MenuListener : MonoBehaviour {

    public AudioClip clickSound;

    public void OnStageButton(string sceneName) {
        Application.LoadLevel(sceneName);
    }

	public void OnQuitBtn() {
		Debug.Log ("onQuitBtn");
		Application.Quit ();
	}

    public void PlayClickSound() {
        AudioManager.GetInstance().PlaySound(clickSound);
    }
}
