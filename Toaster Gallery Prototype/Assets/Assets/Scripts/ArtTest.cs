using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System;

public class ArtTest : MonoBehaviour
{
    List<GameObject> artObj = new List<GameObject>();
    Texture2D[] textList;

    [SerializeField] private GameObject ArtPlaceholder;
    

    //Art Selection
    public GameObject ArtSelection;
    [SerializeField] private GameObject SelectedArtPlaceholder;

    [SerializeField] private FileChanger fileChanger;
    public string path;  

    //[SerializeField] GameObject PlaceholderParent;
    GameObject canvas;
    string[] files;
    Texture2D file;
    string pathPreFix;
    //WWW fileWWW;

    // Use this for initialization
    void Start()
    {
        path = fileChanger.path;
        pathPreFix = @"file://";

        //Find and Initialize Everything
        files = System.IO.Directory.GetFiles(path, "*.jpg");
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        AddArtSlots();

        //Load images from the selected folder
        StartCoroutine(LoadImages());
    }
    void UpdateImagesAndPlaceholders()
    {
        Debug.Log(path);
        files = System.IO.Directory.GetFiles(path + @"/", "*.jpg");

        AddArtSlots();

        StartCoroutine(LoadImages());
    }

    //Add all of the necessary Art Placeholders based on how many files(images) there are
    void AddArtSlots()
    {
        for(int i = 0; i < files.Length; i++)
        {
            GameObject newArtPlaceholder = Instantiate(ArtPlaceholder, new Vector3(0, i * -1000, 0), Quaternion.identity, this.transform);
            artObj.Add(newArtPlaceholder);
        }
        foreach (GameObject art in artObj) 
        {
            Button button = art.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectArtPiece(artObj.IndexOf(art)); });
        }

    }
    //Load the specific image to the next art placeholder
    private IEnumerator LoadImages()
    {
        /*
         * IMPORTANT
         * 
         * IMAGES CAN ONLY BE .JPG RIGHT NOW
         * IMAGES MUST BE LABELED AS FOLLOWS: "ExampleImage01", "ExampleImage02", etc (ExampleImage can be whatever name you wish)
         * WHEN PULLING IMAGES INTO THIS APPLICATION: 
         *          THEY MUST BE ON THE C: DRIVE
         * 
         */
        textList = new Texture2D[files.Length];

        int dummy = 0;
        foreach (string tstring in files)
        {
            string pathTemp = pathPreFix + tstring;
            WWW www = new WWW(pathTemp);
            yield return www;
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);

            textList[dummy] = texTmp;

            Sprite sprite = Sprite.Create(texTmp, new Rect(0, 0, texTmp.width, texTmp.height), new Vector2(texTmp.width / 2, texTmp.height / 2));
            artObj[dummy].GetComponent<RectTransform>().sizeDelta = new Vector2(texTmp.width / 50, texTmp.height / 50);
            artObj[dummy].GetComponent<Image>().overrideSprite = sprite;
            dummy++;
        }

    }

    /*public void SetFolder()
    {
        filePathParent.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }*/
    public void ConfirmFolderChange()
    {
        path = fileChanger.path;
        GameObject[] oldArt = GameObject.FindGameObjectsWithTag("Images");


        for (int i = 0; i < artObj.Count; i++)
        {
            Destroy(artObj[i].gameObject);
        }
        artObj.Clear();

        //For editor use only** opnes file dialog instead
        //path = EditorUtility.OpenFolderPanel("Select Directory", "", "");
        UpdateImagesAndPlaceholders();
    }
    void SelectArtPiece(int selectedArt)
    {
        ArtSelection.SetActive(true);

        WWW www = new WWW(files[selectedArt]);

        Texture2D tmpTexture = new Texture2D(1, 1);
        byte[] tmpBytes = File.ReadAllBytes(files[selectedArt]);
        tmpTexture.LoadImage(tmpBytes);

        Texture2D texTmp = new Texture2D(tmpTexture.width, tmpTexture.height, TextureFormat.DXT1, false);
        www.LoadImageIntoTexture(texTmp);
        Debug.Log("Raw size: " + texTmp.width + " x " + texTmp.height);

        float divider = 20f;
        if (texTmp.width > this.GetComponent<RectTransform>().rect.width)
        {
            divider = (tmpTexture.width / this.GetComponent<RectTransform>().rect.width) * 20f;
        }

        Sprite sprite = Sprite.Create(texTmp, new Rect(0, 0, texTmp.width, texTmp.height), new Vector2(texTmp.width / 2, texTmp.height / 2));
        SelectedArtPlaceholder.GetComponent<RectTransform>().sizeDelta = new Vector2(texTmp.width / divider, texTmp.height / divider);
        SelectedArtPlaceholder.GetComponent<Image>().overrideSprite = sprite;
    }
    public void CloseArtSelection()
    {
        ArtSelection.SetActive(false);
    }
}
