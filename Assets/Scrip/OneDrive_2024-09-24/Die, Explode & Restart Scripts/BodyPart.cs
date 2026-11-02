using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Color start;
	private Color end;
	private float t = 0.0f;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		start = spriteRenderer.color; //ambil warna awal sprite
		end = new Color (start.r, start.g, start.b, 0.0f); //target akhir warnanya adalah membuat Alpha menjadi 0, yaitu transparan
		//efek bodypart mental
		GetComponent<Rigidbody2D>().AddForce(Vector3.right * (Random.Range (-50, 50))); //memberikan force/dorogan acak ke kanan
		GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(100, 400));//memberikan force/dorogan acak ke atas
	}
	void Update () {
		t += Time.deltaTime; //hitung lama kemunculan si bodypart
		GetComponent<Renderer>().material.color = Color.Lerp (start, end, t / 2); //LERP, linear interpolation dari warna awal ke warna akhir dgn kecepatan waktu hidup dibagi 2
		if (GetComponent<Renderer>().material.color.a <= 0.0) //kalo sudah transparan
			Destroy (gameObject); //destroy
	}
}
