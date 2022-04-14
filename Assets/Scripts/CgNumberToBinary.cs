using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CgNumberToBinary : MonoBehaviour
{
    public int number;
    public string binary;

    void Start()
    {
        binary = "";

        for (int p = 12; p > 0; p--)
        {
            if (number > Mathf.Pow(2, p))
            {
                binary = "1";
                number -= int.Parse(Mathf.Pow(2, p).ToString());
            }
            else
            {
                binary = "0";
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
