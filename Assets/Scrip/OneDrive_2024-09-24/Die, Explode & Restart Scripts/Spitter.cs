using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    //public float attackDelay = 3f; //nilai awal delay, 3 detik
	public Projectile projectile;
	private Animator animator;
	public AudioClip attackSound; 
    void OnShoot(){ //dipanggil pada saat animasi monster mulutnya terbuka
		if (projectile) {
			Projectile clone = Instantiate (projectile, transform.position, Quaternion.identity) as Projectile; //bikin objek projectile dari prefab
		}
		if (attackSound)//kalo file suaranya ga kosong
			AudioSource.PlayClipAtPoint (attackSound, transform.position);//play pada posisi attack
        //animator.SetInteger ("AnimState", 0); //setelah selesai beranimasi, balikkan ke animasi state 0
	}

	/*
    void Start () {	
		animator = GetComponent<Animator> ();
		if (attackDelay > 0) {
			StartCoroutine(OnAttack()); //jalanin coroutine sedari awal mulai
		}
	}
	

	IEnumerator OnAttack(){
		yield return new WaitForSeconds(attackDelay); //delay time
		animator.SetInteger ("AnimState", 1); //animasikan ke kondisi attack
		StartCoroutine (OnAttack ()); //rekursif, balik lagi terus. hanya stop kalo monsternya destroy
	}
	*/
}
