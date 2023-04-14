using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Firebase.Database;

public class LiveData : MonoBehaviour
{
    public static LiveData data;
    public string userID;
    public List<Data> DataList;
    public UserData userData = new UserData();

    DatabaseReference reference;
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
    private void Start()
    {
        
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
    public void subtractCoin()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        userData.Coins--;
        reference.Child("Users").Child(userID).Child("Coins").SetValueAsync(userData.Coins);
        ReadData();
    }
    void ReadData()
    {
        StartCoroutine(GetValue());//(string Coin) =>
        /*{
            LiveData.data.userData.Coins = Int16.Parse(Coin);
            Debug.Log(LiveData.data.userData.Coins);
            //ReadData();
        }));*/
    }
    IEnumerator GetValue(/*Action<string> oncallback*/)
    {
        var value = reference.Child("Users").Child(LiveData.data.userID).Child("Coins").GetValueAsync();
        yield return new WaitUntil(predicate: () => value.IsCompleted);

        if (value != null)
        {
            DataSnapshot snapshot = value.Result;
            data.userData.Coins = Int16.Parse(snapshot.Value.ToString());
            //oncallback.Invoke(snapshot.Value.ToString());
        }
    }
}

[Serializable]
public class UserData
{
    public int Coins;
    public int Earnings;
    public List<Levels> Levels = new List<Levels>();
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
    public List<Tex2D> _Tex2D;
}
[Serializable]
public class Tex2D
{
    public string _productName;
    public Texture2D _productimage;
}
