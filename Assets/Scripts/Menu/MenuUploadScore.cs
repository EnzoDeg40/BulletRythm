using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuUploadScore : MonoBehaviour
{
    public static bool GameEnd;

    public GameObject endgame;
    public Button quitEndgame;
    public Button classment;

    public static int score;
    public static int combo;
    public static int miss;
    public static int hit;
    public static string mapname;

    public Text hideMapname;
    public Text hideStats;
    public Text hideScore;
    public Text hideLetter;
    public Text hideUpload;

    void Start()
    {
        quitEndgame.onClick.AddListener(() => closeEngGamePanel());
        classment.onClick.AddListener(() => Application.OpenURL("http://edstudio.fr/project/bulletrythm/beatmap.php?mapname=" + Menu.mapSelected));


        if (!GameEnd)
        {
            if (PlayerPrefs.GetString("username") == "Guest")
            {
                Debug.LogWarning("Score is not upload as Player Guest");
            }
            endgame.gameObject.SetActive(false);
            return;  
        }
        else
        {
            GameEnd = false;
            endgame.gameObject.SetActive(true);
        }


        if(score <= 0 || mapname == null)
        {
            Debug.LogError("Score or map name is null " + score + "" + mapname);
            return;
        }
        else
        {
            StartCoroutine(UploadScore());
        }

        hideLetter.text = " ";
        hideScore.text = score.ToString();
        hideStats.text = "Combo : " + combo + "\n Acc : " + (hit*100)/(hit+miss) + " \n Hit : " + hit + "\n Miss : " + miss;
        hideMapname.text = mapname.ToString();
    }

    void closeEngGamePanel()
    {
        endgame.gameObject.SetActive(false);
    }

    IEnumerator UploadScore()
    {
        string urlUpload = URL.url
            + "upload.php"
            + "?user=" + PlayerPrefs.GetString("username")
            + "&password=" + PlayerPrefs.GetString("password")
            + "&mapname=" + mapname
            + "&score=" + score
            + "&combo=" + combo
            + "&miss=" + miss
            + "&hit=" + hit;

        Debug.Log("UPLOAD SCORE " + urlUpload);

        UnityWebRequest www = UnityWebRequest.Get(urlUpload);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            hideUpload.text = "Impossible to upload score : " + www.error;
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text == "true")
            {
                hideUpload.text = "Score has been upload";
            }
            else if(www.downloadHandler.text == "true but not highscore")
            {
                hideUpload.text = "Score is has upload but is not the best";
            }
            else
            {
                hideUpload.text = www.downloadHandler.text;
            }

            //whene score is upload
            score = combo = miss = hit = 0;
            mapname = null;
        }
    }
}
