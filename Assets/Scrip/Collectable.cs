using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	public AudioClip pickupSound;
	public int theScore = 10;
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if(pickupSound)
				AudioSource.PlayClipAtPoint(pickupSound, transform.position);
			PengaturGameStatic.TambahSkor(theScore); //panggil fungsi static
			//PengaturGameSingleton.instance.TambahSkor(10); //panggil fungsi singleton
			Destroy (gameObject);
		}
	}
}
