using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class downloadMap : MonoBehaviour
{
    Text Txt_PourcentDownload;
    Button Btn_Download;

    string url = "https://edstudio.fr/api/bulletrythm/download.php?user=EnzoDeg40&password=salut&map=Thaehan%20-%20Ohayou%20!%20(Taeyangs%20Expert)";

    void List(){
        
    }

    // Start is called before the first frame update
    void Start()
    {

        //Txt_PourcentDownload = GameObject.Find("Txt_PourcentDownload").GetComponent<Text>();

        //Btn_Download = GameObject.Find("Btn_Download").GetComponent<Button>();
        //Btn_Download.onClick.AddListener(() => Download());
    
    }

    void Download(){
        //StartCoroutine(DownloadFile(url));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
