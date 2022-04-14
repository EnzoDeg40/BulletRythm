using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    Menu menu;

    private Button item;

    public Text TitleAuthor;
    public Text CreatorDifficultyTime;

    void Start()
    {
        menu = GameObject.Find("MenuSystem").GetComponent<Menu>();
        item = GetComponent<Button>();

        if (gameObject.name.Split('_')[1] == "dl(Clone)") 
        {
            item.onClick.AddListener(() => menu.Download(gameObject.name.Split('_')[0]));
        }
        else
        {
            item.onClick.AddListener(() => menu.Launch(gameObject.name.Split('_')[0]));
        }

        TitleAuthor.text = gameObject.name.Split('(')[0];
        CreatorDifficultyTime.text = gameObject.name.Split('(')[1].Split(')')[0];
    }
}
