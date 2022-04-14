using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConvertOsuMap : MonoBehaviour
{
    public Text textIntruction;

    string mapInput;

    [SerializeField] string[] mapSplitLine;

    public string mapOutput;

    int[] x;
    int[] time;
    int[] newCombo;

    // Start is called before the first frame update
    void Start()
    {
        textIntruction.text = "Put all map of osu in " + Application.persistentDataPath + "/osu. Then, press button convert.";

        Directory.CreateDirectory(Application.persistentDataPath + "/osu");

        string path = Application.persistentDataPath + "/osu/map.osu";
        mapInput = File.ReadAllText(path);
        print(mapInput);
        StartConvert();
    }

    public void StartConvert()
    {
        //mapInput = inputConvert.text;

        // Sépare les lignes
        mapSplitLine = mapInput.Split('\n');

        int lineNumber = mapSplitLine.Length;
        Debug.Log(lineNumber + " objects fonds");

        // Définit la taille des arrays selon le nombre de lignes
        time = new int[lineNumber];
        x = new int[lineNumber];
        newCombo = new int[lineNumber];

        // Pour chaque ligne du fichier
        for (int i = 0; i < lineNumber - 1; i++)
        {
            // To do : s'occuper des sliders
            /*try
            {
                mapSplitLine[i].Split('|');
                

            }
            catch (System.Exception)
            {
                string[] line = mapSplitLine[i].Split(',');

                time[i] = int.Parse(line[2]);
                x[i] =  float.Parse(line[0])/100;         
            }*/

            // Si la ligne est vide
            if (mapSplitLine[i] == "" || mapSplitLine[i] == null)
            {
                continue;
            }

            print(mapSplitLine[i]);

            // Sépare la ligne
            string[] line = mapSplitLine[i].Split(',');

            time[i] = int.Parse(line[2]);
            x[i] = int.Parse(line[0]);

            if(line[3] == "5" || line[3] == "6")
            {
                newCombo[i] = 1;
            }
            else
            {
                newCombo[i] = 0;
            }

            // 0 : point
            // 1 : new combo

        
            // to do : ad spike

            //debugText.text = "Phase 1 : " + i + "/" + lineNumber;
        }

        mapOutput = "";

        for (int i = 0; i < time.Length; i++)
        {
            mapOutput += time[i] + "," + newCombo[i] + "," + x[i] + ";\n";
        }

        File.WriteAllText(Application.persistentDataPath + "/osu/map.txt", mapOutput);
        print(mapOutput);
    }
}
