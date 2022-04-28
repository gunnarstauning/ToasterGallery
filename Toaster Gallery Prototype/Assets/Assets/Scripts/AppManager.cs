using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AppManager : MonoBehaviour
{
    [SerializeField] private Text paneInput;
    private string numPanes;

    public static AppManager instance;

    public Image background;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ConfirmPanes(Image imgSource)
    {
        numPanes = paneInput.text;
        background.sprite = imgSource.sprite;

        Debug.Log(background.sprite.name);

        SceneManager.LoadScene(numPanes + "Wall");
        
    }
}
