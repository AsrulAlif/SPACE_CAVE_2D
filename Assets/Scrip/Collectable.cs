using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    public AudioClip pickupSound;
    public int theScore = 10;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (pickupSound)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Diaktifkan kembali agar skor bertambah saat koin/item diambil
            PengaturGameStatic.TambahSkor(theScore);

            Destroy(gameObject);
        }
    }
}