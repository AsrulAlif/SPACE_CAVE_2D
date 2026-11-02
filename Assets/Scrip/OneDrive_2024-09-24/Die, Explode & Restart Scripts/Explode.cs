using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public BodyPart bodyPart;
    public int totalParts;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Deadly")
        {
            OnExplode();
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Deadly")
        {
            OnExplode();
        }
    }

    public void OnExplode()
    {
        // Reset skor saat player mati
        PengaturGameStatic.ResetSkor();

        var t = transform; // Ambil posisi player sebelum dihancurkan

        // Pastikan bodyPart tidak kosong agar tidak error
        if (bodyPart != null)
        {
            for (int i = 0; i < totalParts; i++)
            {
                BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
            }
        }
        else
        {
            Debug.LogWarning("BodyPart prefab belum di-assign di Inspector Player!");
        }

        // Hancurkan player setelah pecahan dibuat
        Destroy(gameObject);

        GameObject go = new GameObject("ClickToContinue");
        go.AddComponent<ClickToContinue>();
        go.AddComponent<DisplayRestartText>();
    }
}