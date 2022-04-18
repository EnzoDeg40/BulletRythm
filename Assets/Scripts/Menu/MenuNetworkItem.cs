using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNetworkItem : MonoBehaviour
{
    public Text Username;

    void Start()
    {
        Username.text = gameObject.name.Split('(')[0];
    }


}
