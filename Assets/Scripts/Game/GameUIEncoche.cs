using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIEncoche : MonoBehaviour
{
    public bool notch;

    public GameObject BackgroundUp;
    public Text Pseudo;
    public Text Pourcent;

    void Start()
    {
        if (notch)
        {
            //BackgroundUp.gameObject.transform.position += new Vector3(0, -62.5f, 0);
            //Pseudo.transform.position += new Vector3(64, 0, 0);
            //Pourcent.transform.position += new Vector3(-64, 0, 0);
            //BackgroundUp.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -5, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
