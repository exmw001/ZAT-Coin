using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    public string LevelName;
    public string _pName;
    public RawImage image;
    [SerializeField] Button button;
    [SerializeField] InputField Quantity;
    public Levels levels;
    private void Awake()
    {
        button.onClick.AddListener(AddProduct);
    }
    void Start()
    {
        //transform.GetChild(0).GetComponent<Text>().text = _pName;
    }

    void AddProduct()
    {
        if (image.texture != null)
            RealTimeDatabase.instance.AddProduct(LevelName, _pName, int.Parse(Quantity.text),levels);
    }

    /*public void GetImage(string URL)
    {
        StartCoroutine(DownloadImage(URL));
    }*/

    /*IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }*/
}
