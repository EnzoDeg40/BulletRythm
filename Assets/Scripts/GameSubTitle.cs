using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubTitle : MonoBehaviour
{
    GameUI gameUI;

    [SerializeField] string[] subInput;
    [SerializeField] int[] subTime;
    [SerializeField] string[] subText;

    float time = -3000;
    int cursor = 0;
    bool fileReady = false;

    void Start()
    {
        string path = Application.persistentDataPath + "/Beatmaps/" + Menu.mapSelected + "/subtitle.brs";
        gameUI = GetComponent<GameUI>();

        if (!System.IO.File.Exists(path)) {
            return;
        }
        else{
            fileReady = true;
        }

        // Lit le fichier de sous titre
        subInput = System.IO.File.ReadAllText(path).Split('\n');

        // Définit la taille des arrays selon le nombre de ligne dans le input
        subTime = new int[subInput.Length];
        subText = new string[subInput.Length];

        // Sépare le subinput en prenant le caractère \t (tabulation) pour les arrays subtime et subtext
        for (int i = 0; i < subInput.Length; i++)
        {
            // Convertit le texte en format de milliseconds
            string tempSubTime = subInput[i].Split('\t')[0];

            //Debug.Log(tempSubTime);

            int minute = int.Parse(tempSubTime.Split(':')[0]);
            int second = int.Parse(tempSubTime.Split(':')[1].Split(',')[0]);
            int ms = int.Parse(tempSubTime.Split(',')[1]);

            subTime[i] = (minute * 60000) + (second * 1000) + ms;
            subText[i] = subInput[i].Split('\t')[1];
        }
    }

    void Update()
    {
        time += Time.deltaTime * 1000;

        if (fileReady) {
            if (time >= subTime[cursor] && cursor! > subTime.Length) {
                gameUI.UpdateSubtitle(subText[cursor]);
                cursor++;
            }
        }
    }
}
