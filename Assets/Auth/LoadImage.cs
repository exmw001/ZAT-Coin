using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using Firebase.Database;
using UnityEngine.Events;
public class LoadImage : MonoBehaviour
{
    public static LoadImage instance;
    FirebaseStorage storage;
    StorageReference storageReference;

    private void Start()
    {
        instance = this;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://coin-6388d.appspot.com");//gs://coins-3cbfc.appspot.com");
    }
    #region Load image as Sprite...
    public void LoopFunction_GetSprite(string LevelName, string ChildName, Data data, UnityEvent @event)
    {
        GetSprite(LevelName, ChildName + ".png", data,@event);
    }
    void GetSprite(string LevelName, string ChildName, Data data, UnityEvent @event)
    {
        Debug.Log(LevelName + "/" + ChildName);
        StorageReference image = storageReference.Child(LevelName + "/" + ChildName);
        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(_LoadSprite(Convert.ToString(task.Result), data, @event));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }
    IEnumerator _LoadSprite(string MediaUrl, Data data ,UnityEvent @event)
    {
        Debug.Log(MediaUrl);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
        yield return request.SendWebRequest(); //Wait for the request to complete

        if (request.isNetworkError || request.isHttpError)
        {
            data._productimage.Add(null);
            Debug.Log(request.error);
        }
        else
        {
            Texture2D myTexture2D = ((DownloadHandlerTexture)request.downloadHandler).texture;
            data._productimage.Add(myTexture2D);
            if (data._productName.Count == data._productimage.Count)
            {
                if (@event != null)
                {
                    @event.Invoke();
                    @event.RemoveAllListeners();
                }
            }
        }
    }
    #endregion

    #region Load image as raw image...
    public void LoopFunction_GetImage(string LevelName, string ChildName, RawImage raw)
    {
        GetImage(LevelName, ChildName + ".png", raw);
    }
    void GetImage(string LevelName, string ChildName, RawImage rawImage)
    {
        Debug.Log(LevelName + "/" + ChildName);
        StorageReference image = storageReference.Child(LevelName + "/" + ChildName);
        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(_LoadImage(Convert.ToString(task.Result), rawImage));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }
    IEnumerator _LoadImage(string MediaUrl, RawImage raw)
    {
        Debug.Log(MediaUrl);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
        yield return request.SendWebRequest(); //Wait for the request to complete

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            raw.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            raw.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            raw.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            raw.SetNativeSize();
        }
    }
    #endregion

}


