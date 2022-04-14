using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 300;

    public float yPlayerPosition = -4.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /*
        if(Application.platform == RuntimePlatform.Android)
        {
            speed = 150;
        }*/
    }

    void Update()
    {
        Vector3 screenTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouve = (screenTouch - transform.position).normalized;
        rb.velocity = new Vector2(mouve.x * speed, yPlayerPosition);
    }
}
