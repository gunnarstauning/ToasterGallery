using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundGetter : MonoBehaviour
{
    public GameObject[] backgrounds;

    public void Start()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        foreach(GameObject back in backgrounds)
        {
            back.GetComponent<Image>().sprite = AppManager.instance.background.sprite;           
        }
    }
}
