using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

public class MenuBeatmapList : MonoBehaviour
{
    public GameObject item;
    public GameObject itemLocal;
    public GameObject itemOnline;
    public Transform SpawnItem;

    public MenuScrollChildCount menuScrollChildCount;

    public Text TextInfo;

    public string[] dir;
    [SerializeField] string[] dirOnline;


    // Start is called before the first frame update
    void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Beatmaps/");
        GetListMapsLocal();   
    }

    public void GetListMapsLocal()
    {
        try
        {
            foreach (Transform child in SpawnItem)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        catch (System.Exception)
        {

            throw;
        }
        
    
        dir = Directory.GetDirectories(Application.persistentDataPath + "/Beatmaps");

        Debug.Log(dir.Length + " maps found on local");
        Debug.Log(Application.persistentDataPath);

        Instantiate(itemLocal, SpawnItem);

        for (int i = 0; i < dir.Length; i++)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                dir[i] = dir[i].Split('/').Last();
            }
            else
            {
                dir[i] = dir[i].Split('\\')[1];
            }

            item.name = dir[i] + "_";
            Instantiate(item, SpawnItem);
        }

        menuScrollChildCount.UpdateSize();

        StartCoroutine(GetListMapsOnline());
    }

    IEnumerator GetListMapsOnline()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL.url + "beatmap.php");
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.LogWarning(www.error);
            TextInfo.text = www.error;
        }
        else
        {
            TextInfo.text = "";

            dirOnline = www.downloadHandler.text.Split('\n');

            Instantiate(itemOnline, SpawnItem);

            for (int i = 0; i < dirOnline.Length-1; i++)
            {
                bool result = false;
                for (int j = 0; j < dir.Length; j++) {
                    if(dirOnline[i] == dir[j]) {
                        result = true;
                        break;
                    }
                }

                if (!result) {
                    item.name = dirOnline[i] + "_dl";
                    Instantiate(item, SpawnItem);
                }
                
            }

            Debug.Log(dirOnline.Length-1 + " maps found on internet");

            menuScrollChildCount.UpdateSize();
        }
    }

}
