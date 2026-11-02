using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickToContinue : MonoBehaviour {
	private bool loadLock; //supaya cm bisa diklik 1x saja
	
	void Update () {
		if (Input.GetMouseButtonDown (0) && !loadLock) //kalau mouse diklik dan nilai loadlock adalah not true
			LoadScene ();
	}

	void LoadScene(){
		loadLock = true; //jadikan true, supaya cm bisa 1x klik saja
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //loadscene yg skrg active
	}
}
