using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    Menu menu;

    [Header("UI")]
    public Text TitleAuthor;
    public Text CreatorDifficultyTime;
    private Button item;


    [Header("Image")]
    public GameObject bgImage;
    private Texture2D texture;

    void Start()
    {
        string mapname = gameObject.name.Split('_')[0];
        string titleAuthor = gameObject.name.Split('(')[0];
        string creatorDifficultyTime = gameObject.name.Split('(')[1].Split(')')[0];
        string lastString = gameObject.name.Split('_')[1];

        menu = GameObject.Find("MenuSystem").GetComponent<Menu>();
        item = GetComponent<Button>();

        if (lastString == "dl(Clone)") 
        {
            item.onClick.AddListener(() => menu.Download(mapname));
        }
        else
        {
            item.onClick.AddListener(() => menu.Launch(mapname));
            loadImageBackground();
        }

        TitleAuthor.text = titleAuthor;
        CreatorDifficultyTime.text = creatorDifficultyTime;

    }

    void loadImageBackground()
    {
        // Définit le nom du fichier
        string mapname = gameObject.name.Split('_')[0];

        // Définit le chemin de l'image de fond
        string backgroundImagePatch = Application.persistentDataPath + "/Beatmaps/" + mapname + "/bg.png";
        
        // Lit le fichier
        byte[] bytes = File.ReadAllBytes(backgroundImagePatch);

        // Applique la texture
        texture = new Texture2D(1,1);
        texture.LoadImage(bytes);
        texture.name = mapname + "_background";
        bgImage.GetComponent<RawImage>().texture = texture;
    }
}
