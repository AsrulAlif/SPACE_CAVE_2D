using UnityEngine;
using System.Collections;

public class Tremor : MonoBehaviour {
	private Animator animator;
	public bool readyToAttack; //boolean utk cek tremor lagi mau gigit
    public bool entered; //boolean utk cek player melewati si tremor
	GameObject siPlayer; //utk referensi object player
    
	void Start () {
		animator = GetComponent<Animator> ();
	}	
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") { //kalo ditabrak sama player			
			animator.SetInteger ("AnimState", 1); //animasi gigit
            entered = true; //player melewati tremor is True			
			siPlayer = target.gameObject; //referensikan si Player, krn mau diakses di fungsi lain
		}
	}
	void OnTriggerExit2D(Collider2D target){ 
        entered = false; //player leave tremor
		animator.SetInteger ("AnimState", 0); //kembalikan ke animasi idle
	}

	void Attack(){ //dipasang pada frame gigit
		readyToAttack = true;
	}

    void Idle(){ //dipasang pada frame mulai idle
        readyToAttack = false;
    }

    void Update(){
        if(entered && readyToAttack){ //kalo player melewati dan si tremor lagi animasi gigit
            var explode = siPlayer.GetComponent<Explode>(); //cari script explode pada player
            explode.OnExplode(); //panggil fungsi onExlpode
        }
    }	
}
