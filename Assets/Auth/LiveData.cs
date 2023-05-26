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
    public void subtractCoin()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        userData.Coins--;
        reference.Child("Users").Child(userID).Child("Coins").SetValueAsync(userData.Coins);
        ReadData();
    }
    public void SubtractProduct(string productName)
    { 

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
    #region Push Won Product and update earning
    string pName;
    float price;
    public void PushWonProduct(string pName, float price)
    {
        this.pName = pName;
        this.price = price;
        StartCoroutine(PushWonProductCo());
    }
    IEnumerator PushWonProductCo(/*Action<string> oncallback*/)
    {
        var value = FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(userID).Child("Earnings").GetValueAsync();
        yield return new WaitUntil(predicate: () => value.IsCompleted);

        if (value != null)
        {
            DataSnapshot snapshot = value.Result;
            float _tempE = float.Parse(snapshot.Value.ToString());
            userData.Earnings += _tempE;

            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(userID).Child("Earnings").SetValueAsync(userData.Earnings);
            //post won product
            ProductWonEarning entry = new ProductWonEarning(pName, price);
            Dictionary<string, System.Object> entryValues = entry.ToDictionary();
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(userID).Child("WonProducts").Push().SetValueAsync(entryValues);
            //subtract product that won
        }
    }

    #endregion
}

[Serializable]
public class UserData
{
    public int Coins;
    public float Earnings;
    public List<Levels> Levels = new List<Levels>();
    public string Token;
}
[Serializable]
public class Levels
{
    public List<ProductData> _pName = new List<ProductData>();
}
[Serializable]
public class ProductWonEarning
{
    private string pName;
    public float price = 0;

    public ProductWonEarning(string pName, float price)
    {
        this.pName = pName;
        this.price = price;
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["product"] = pName;
        result["price"] = price;

        return result;
    }
}
[Serializable]
public class ProductData
{
    public string _pName;
    public int Quantity;
    public float Price;
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
[Serializable]
public class WonProductData
{
    public int _Level;
    public int _productIndex;
    public float price;
    public String ProductName;
}
[System.Serializable]
public class LevelData
{
    public String LevelName;
    public List<string> _productName;
}
