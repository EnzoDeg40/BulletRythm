using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class MenuNetwork : MonoBehaviour
{
    public MenuScrollChildCount scroll;
    public Text playerlist;
    public GameObject SpawnPlayerList;
    public GameObject PlayerItem;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://edstudio.fr/api/bulletrythm/playerslist.php"));
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    

                    string data = webRequest.downloadHandler.text;
                    // JsonUtility.FromJson<PlayerList>(data);
                    string numberUser = data.Split(';').Length.ToString();

                    playerlist.text = playerlist.text + " (" + numberUser + ")";

                    for (int i = 0; i < data.Split(';').Length-1; i++) {
                        PlayerItem.name = data.Split(';')[i];
                        Instantiate(PlayerItem, SpawnPlayerList.transform);
                    }

                    scroll.UpdateSize();

                    break;
            }
        }
    }
}
