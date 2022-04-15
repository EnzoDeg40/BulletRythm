using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelMapselect;
    public GameObject panelUserinfo;
    public GameObject panelScores;

    [Header("DockerBtn")]
    public Button mapselect;
    public Button userinfo;
    public Button scores;


    //[Header("Other")]

    void changePanel(string panelName) {
        switch (panelName) {
            case "mapselect":
                panelMapselect.SetActive(true);
                panelUserinfo.SetActive(false);
                panelScores.SetActive(false);
                break;

            case "userinfo":
                panelMapselect.SetActive(false);
                panelUserinfo.SetActive(true);
                panelScores.SetActive(false);
                break;

            case "scores":
                panelMapselect.SetActive(false);
                panelUserinfo.SetActive(false);
                panelScores.SetActive(true);
                break;

            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapselect.onClick.AddListener(() => changePanel("mapselect"));
        userinfo.onClick.AddListener(() => changePanel("userinfo"));
        scores.onClick.AddListener(() => changePanel("scores"));

        // Evite les problèmes au cas ou deux vues serait activés
        changePanel("mapselect");
    }
}
