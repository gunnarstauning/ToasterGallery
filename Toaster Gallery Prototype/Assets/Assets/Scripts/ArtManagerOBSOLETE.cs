using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ArtManagerOBSOLETE : MonoBehaviour
{
    /*[SerializeField] private Transform[] placeholders;

    public List<Texture2D> Art = new List<Texture2D>();

    private void FindImages()
    {
        string pathPrefix = @"file://";
        string pathImageAssets = @"C:\LuminaryImages\";
        string pathSmall = @"small\";
        string fileName = @"Luminary";
        string fileSuffix = @".jpg";

        for(int i = 0; i < 33; i++)
        {
            string indexSuffix = "";
            float logIdx = Mathf.Log10(i + 1);
            if (logIdx < 1.0)
                indexSuffix += "00";
            else if (logIdx < 2.0)
                indexSuffix += "0";
            indexSuffix += (i + 1);
            string fullFilename = pathPrefix + pathImageAssets + pathSmall + fileName + indexSuffix + fileSuffix;

            WWW www = new WWW(fullFilename);
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            //LoadImageIntoTexture compresses JPGs by DXT1 and PNGs by DXT5
            www.LoadImageIntoTexture(texTmp);
            Art.Add(texTmp);
        }
        LoadImages();
    }
    private void LoadImages()
    {
        for (int i = 0; i < Art.Count; i++)
        {
            Art[i].LoadImage() = placeholders[i].position;
        }
    }*/


    GameObject[] gameObj;
    Texture2D[] textList;

    string[] files;
    string pathPreFix;

    // Use this for initialization
    void Start()
    {
        //Change this to change pictures folder
        string path = @"C:\LuminaryImages\small\";

        pathPreFix = @"file://";

        files = System.IO.Directory.GetFiles(path, "*.jpg");

        StartCoroutine(LoadImages());
    }


    void Update()
    {

    }
    

    private IEnumerator sendTextToFile()
    {
        bool successful = true;

        WWWForm form = new WWWForm();
        form.AddField("name", "Joe");
        form.AddField("age", "32");
        form.AddField("score", "100");
        WWW www = new WWW("http://localhost:9000/fromunity.php", form);

        yield return www;
        if(www.error != null)
        {
            successful = false;
        }
        else
        {
            Debug.Log(www.text);
            successful = true;
        }

    }

    private IEnumerator LoadImages()
    {
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
            gameObj[dummy].GetComponent<Image>().overrideSprite = sprite;
            dummy++;
        }

    }
}
