using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class BetterMenu : MonoBehaviour
{
    public string[] localMap;
    // Start is called before the first frame update
    void Start()
    {
        // C:/Users/enzod/AppData/LocalLow/EdStudio/BulletRythm/Beatmaps\Thaehan - Ohayou ! (Taeyangs Expert)
        localMap = Directory.GetDirectories(Application.persistentDataPath + "/Beatmaps");
        
        for (int i = 0; i < localMap.Length; i++){
            localMap[i] = localMap[i].Split('\\')[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
