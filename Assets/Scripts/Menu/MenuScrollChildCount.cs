using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScrollChildCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.childCount);
        UpdateSize();
    }

    public void UpdateSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, transform.childCount * 80);
    }
}
