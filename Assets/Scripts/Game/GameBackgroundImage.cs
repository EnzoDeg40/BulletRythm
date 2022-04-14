using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameBackgroundImage : MonoBehaviour
{
    public GameObject BackgroundImagePlane;
    public Texture2D texture;
    public MeshRenderer ah;
    

    void Start()
    {
        string backgroundImagePatch = Application.persistentDataPath + "/Beatmaps/" + Menu.mapSelected + "/bg.png";

        Debug.Log("LOADING BACKGROUND " + backgroundImagePatch);

        byte[] bytes = File.ReadAllBytes(backgroundImagePatch);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        ah.material.mainTexture = texture;

        if (ah.material.mainTexture != null)
        {
            BackgroundImagePlane.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("IMPOSSIBLE TO LOAD BACKGROUND " + backgroundImagePatch);
        }

        

        
        //backgroundImage.transform.localScale = new Vector3(Screen.width, Screen.height, 1);
    }
}
