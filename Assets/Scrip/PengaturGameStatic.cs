using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengaturGameStatic : MonoBehaviour
{
    static Text skorTeks;
    static int skorSkrg=0;
    // Start is called before the first frame update
    void Start(){
        skorTeks = GameObject.Find("UISkor").GetComponent<Text>(); //cari si gameobject UISkor
        skorTeks.text="Score: "+skorSkrg.ToString(); //set skor awal mulai game
    }

    public static void TambahSkor(int skornya){
        skorSkrg+=skornya; //tambahkan skor
        skorTeks.text="Score: "+skorSkrg.ToString(); //update textnya
    }
}
