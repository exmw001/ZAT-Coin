using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class LiveData : MonoBehaviour
{
    public static LiveData data;
    public string userID;
    public List<Data> DataList;
    public UserData Levels = new UserData();
    //public RawImage _Image;
    private void Awake()
    {
        #region Instance...
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    }
    /*private void Update()
    {
        if (DataList[0]._productimage.Count > 12 && !cals)
        {
            cals = true;
            _Image.texture = DataList[0]._productimage[11];
            _Image.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            _Image.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            _Image.SetNativeSize();
        }
    }*/
}

[Serializable]
public class UserData
{
    public int Coins;
    public int Earnings;
    public List<Levels> levelData = new List<Levels>();
    public string Token;
}
[Serializable]
public class Levels
{
    public String LevelName;
    public List<ProductData> _pName = new List<ProductData>();
}
[Serializable]
public class Data
{
    public String LevelName;
    public List<string> _productName;
    public List<Texture2D> _productimage;
}