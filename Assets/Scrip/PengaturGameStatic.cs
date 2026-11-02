using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengaturGameStatic : MonoBehaviour
{
    static Text skorTeks;
    static int skorSkrg = 0;

    void Start()
    {
        GameObject skorObj = GameObject.Find("UISkor");
        if (skorObj != null)
        {
            skorTeks = skorObj.GetComponent<Text>();
            skorTeks.text = "Score: " + skorSkrg.ToString();
        }
    }

    public static void TambahSkor(int skornya)
    {
        skorSkrg += skornya;
        if (skorTeks != null)
        {
            skorTeks.text = "Score: " + skorSkrg.ToString();
        }
    }

    // Fungsi untuk mereset skor kembali ke 0 saat player mati
    public static void ResetSkor()
    {
        skorSkrg = 0;
    }
}