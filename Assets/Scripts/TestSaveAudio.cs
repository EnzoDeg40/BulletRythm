using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class TestSaveAudio : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.persistentDataPath);

        WebClient client = new WebClient();

        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/test/ ");

        client.DownloadFile("http://192.168.1.40/edstudiologin/download.php", Application.persistentDataPath + "/test/filsde.txt");     
    }

}
