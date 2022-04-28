using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class FileChanger : MonoBehaviour
{
    //Change this to change pictures folder
    public string path = @"C:/LuminaryImages/";

    [SerializeField] private Text filePathText;
    [SerializeField] private GameObject menuObjects;
    [SerializeField] private Button confirmButton;

    [SerializeField] private List<ArtTest> artHolders;

    private bool toggleMenu = false;

    public void SetFolder()
    {
        if (toggleMenu)
        {
            menuObjects.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            toggleMenu = false;
        }

        else
        {
            toggleMenu = true;
            menuObjects.SetActive(true);
            confirmButton.gameObject.SetActive(true);
        }
    }
    public void ConfirmFolderChange()
    {
        //filePathParent.SetActive(false);
        //confirmButton.gameObject.SetActive(false);

        path = filePathText.text.ToString();

        foreach(ArtTest artCode in artHolders)
        {
            artCode.ConfirmFolderChange();
        }
    }
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("PanelNumSelector");
    }
}
