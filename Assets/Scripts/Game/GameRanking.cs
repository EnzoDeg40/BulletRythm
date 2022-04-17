using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class GameRanking : MonoBehaviour
{
    [Header("UI")]
    public Text rankNumber;
    public Text username;
    public Text score;

    [Header("Varibles")]
    string[,] ranking;
    JSONNode root;


    // Start is called before the first frame update
    void Start()
    {
        string mapname = Menu.mapSelected;

        // Définit l'url de la requête
        string url = URL.url + "info.php?query=mapranking&mapname=" + mapname;

        Debug.Log(url);

        StartCoroutine(GetRequest(url));

        updateRanking(0);

        // Récupère le classement de la map

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRanking(int playerscore)
    {
        if(root == null){
            return;
        }

        // Pour chaque score
        for (int i = 0; i < root.Count; i++){
            if(ranking[i, 1] == ""){
                continue;
            }
            
            // Si le score du joueur est supérieur à celui du json
            if(playerscore > int.Parse(ranking[i,1])){
                // Vide les champs
                ranking[i, 0] = "";
                ranking[i, 1] = "";
            }
        }

        for (int i = root.Count - 1; i >= 0 ; i--){
            if(ranking[i, 0] != ""){
                username.text = ranking[i, 0];
                score.text = ranking[i, 1];
                rankNumber.text = (i+1).ToString();
                break;
            }
            Debug.Log(i + "---" + ranking[i, 0]);
        }

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

                    // Créé la structure JSON
                    root = JSONNode.Parse(data);
                    
                    // Resize the array
                    ranking = new string[root.Count+1, 2];

                    // Pour chaque element du JSON
                    foreach (JSONNode n in root)
                    {
                        ranking[index, 0] = n["username"];
                        ranking[index, 1] = n["score"];

                        // Incrémente de 1 l'index
                        index++;
                    }

                    // For each cont
                    /*for (int i = 0; i < root.Count; i++)
                    {
                        Debug.Log(ranking[i, 1] + " --- " + ranking[i, 0]);
                    }*/

                    break;
            }

        }
    }

}
