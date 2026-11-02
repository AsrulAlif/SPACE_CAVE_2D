using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	//nabrak apapun, lgsg destroy
	void OnCollisionEnter2D(Collision2D target){
		Destroy (gameObject);
	}
}
