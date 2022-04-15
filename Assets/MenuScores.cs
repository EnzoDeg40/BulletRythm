using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class MenuScores : MonoBehaviour
{
    public Text Scores;

    // Start is called before the first frame update
    void Start()
    {
        // Définit l'url requet
        string url = URL.url + "info.php?query=globalranking";

        StartCoroutine(GetRequest(url));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
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

                    //var jsonData = JSON.Parse(data);

                    /*string resultdata = "";
                    foreach (var item in jsonData){
                        string currentdata = item[0].ToString(); 
                    }*/

                    string resultdata = ".\t\tUsername\t(Total scores)\n";
                    int rank = 0;

                    JSONNode root = JSONNode.Parse(data);
                    foreach (JSONNode n in root)
                    {
                        rank++;
                        string username = n["username"];
                        int total_score = n["total_score"];
                        resultdata += $"{rank} \t\t<b>{username}</b> \t({total_score})\n";
                    }
                    Scores.text = resultdata;


                    // [2, { ""mapname":"Thaehan - Bwa ! (Deif)","username":"EnzoDeg40","dateofsubbmit":"2020-09-22","score":"5359050","combo":"458","hit":"541","miss":"2"}]
                    //jsonData[0]
                    //Result.text = jsonData[0];

                    break;
            }

        }
    }
}
