using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationText : MonoBehaviour
{
    public Text Notif;

    private void Start()
    {
        Notif.text = gameObject.name.Substring(0, gameObject.name.Length - 7);
        Destroy(gameObject, 2f);
    }





}
