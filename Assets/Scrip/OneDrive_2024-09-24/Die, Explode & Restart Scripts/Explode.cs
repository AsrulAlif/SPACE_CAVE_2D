using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public BodyPart bodyPart;
	public int totalParts;
    void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Deadly") { //kalo tabrakan onTrigger sama deadly
			OnExplode();
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Deadly") { //kalo tabrakan onCollision sama deadly
			OnExplode();
		}		
	}

	public void OnExplode(){
		Destroy (gameObject); //destroy player
		
		var t = transform; //ambil posisi player

		for (int i = 0; i < totalParts; i++) { //loop sebanyak totalparts yg diisi
			BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart; //buat objek dari prefab pada posisi dan rotasi pemain			
		}
		
		GameObject go = new GameObject ("ClickToContinue"); //bikin gameobject bernama ClickToContinue
		go.AddComponent<ClickToContinue> (); //kasih script ClickToContinue
		go.AddComponent<DisplayRestartText> (); //kasih script DisplayRestartText
		
	}
}
