using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengaturGameSingleton : MonoBehaviour
{
     //bikin singleton
    public static PengaturGameSingleton instance = null;
    public Text skorTeks;
    int skorSkrg=0;
    
    //method awake diperlukan utk singleton tsb
    void Awake(){
        if(instance == null) instance =  this; //kalo blm ada instancenya, assign ke instance class ini
        else if(instance != this) Destroy(gameObject); //kalo sudah ada, destroy
    }

    void Start(){        
        skorTeks.text="Score: "+skorSkrg.ToString(); //set skor awal mulai game
    }

    public void TambahSkor(int skornya){
        skorSkrg+=skornya; //tambahkan skor
        skorTeks.text="Score: "+skorSkrg.ToString(); //update textnya
    }
}
