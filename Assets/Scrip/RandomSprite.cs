using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour {

	public Sprite[] sprites;
	public string resourceName;
	public int currentSprite = -1;

	// Use this for initialization
	void Start () {
		if (resourceName != "") {
				sprites = Resources.LoadAll<Sprite> (resourceName); //mengambil sprite dari folder Resources, sesuai nama
			if(currentSprite == -1)
				currentSprite = Random.Range(0, sprites.Length); //kalo currentSprite diisi -1 artinya random
			else if(currentSprite > sprites.Length) //kalo diisi angka, berarti memunculkan sprite urutan angka tsb
				currentSprite = sprites.Length - 1;

			GetComponent<SpriteRenderer>().sprite = sprites[currentSprite]; //mengisi spritenya
		}
	}
}
