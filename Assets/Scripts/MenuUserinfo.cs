using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;


public class MenuUserinfo : MonoBehaviour
{
    public Text Username;
    public Text Result;

    // Start is called before the first frame update
    void Start()
    {
        // Récupéré le nom d'utilisateur 
        string username = PlayerPrefs.GetString("username");
        if(username == ""){
            Debug.LogWarning("Username is not set");
            return;
        }

        // Affiche le pseudo 
        Username.text = username;

        // Définit l'url requête
        string url = URL.url + "info.php?query=playerscore&username=" + username;

        // Récupère les informations du joueur
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

                    string resultdata = "Beatmap name\nScore - Combo - Miss\nDate of submit\n\n";

                    JSONNode root = JSONNode.Parse(data);
                    foreach (JSONNode n in root){
                        string mapname = n["mapname"];
                        int score = n["score"];
                        int combo = n["combo"];
                        int miss = n["miss"];
                        string dateofsubbmit = n["dateofsubbmit"];
                        resultdata += $"{mapname}\n{score} - {combo} - {miss}\n{dateofsubbmit}\n\n";   
                    }
                    Result.text = resultdata;


                    // [2, { ""mapname":"Thaehan - Bwa ! (Deif)","username":"EnzoDeg40","dateofsubbmit":"2020-09-22","score":"5359050","combo":"458","hit":"541","miss":"2"}]
                    //jsonData[0]
                    //Result.text = jsonData[0];

                    break;
            }

        }
    }
}
