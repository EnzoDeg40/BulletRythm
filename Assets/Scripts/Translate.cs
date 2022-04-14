using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    public string Language;

    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.French)
        {
            Language = "French";
        }
        else
        {
            Language = "English";
        }
    }


}
