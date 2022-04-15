using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class MenuScores : MonoBehaviour
{
    public GameObject TableContent;
    public GameObject ItemPlayerRanking;

    // Start is called before the first frame update
    void Start()
    {
        // Définit l'url de la requet
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

                    // Récupère le contenu de la page
                    string data = webRequest.downloadHandler.text;

                    // Initie l'index
                    int index = 0;

                    // Pour chaque element du JSON
                    JSONNode root = JSONNode.Parse(data);
                    foreach (JSONNode n in root)
                    {
                        // Incrémente de 1 l'index
                        index++;

                        // Initie les variables
                        string username = n["username"];
                        int total_score = n["total_score"];

                        // Créé un item puis le place dans le tableau
                        GameObject child = GameObject.Instantiate(ItemPlayerRanking);
                        child.transform.parent = TableContent.transform;

                        // Modifie les paramètres de l'item
                        child.transform.Find("RankingText").GetComponent<Text>().text = index.ToString();
                        child.transform.Find("BackgroundItem").Find("UsernameText").GetComponent<Text>().text = username;
                        child.transform.Find("BackgroundItem").Find("TotalScoreText").GetComponent<Text>().text = total_score.ToString("#,#").Replace(',', ' ');

                        // Met en couleur le pseudo si c'est le joueur actuel
                        if(n["username"].ToString().ToLower() == "\"" + PlayerPrefs.GetString("username").ToLower() + "\""){
                            child.transform.Find("BackgroundItem").Find("UsernameText").GetComponent<Text>().color = Color.yellow; //new Color(255, 255, 88);
                        }
                    }

                    break;
            }

        }
    }
}
