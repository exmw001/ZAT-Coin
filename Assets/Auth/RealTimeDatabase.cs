using Firebase.Database;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Application;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Firebase.Extensions;

public class RealTimeDatabase : MonoBehaviour
{

    [SerializeField] List<String> _levelName;

    public static RealTimeDatabase instance;
    public int ProductAddCounter;
    DatabaseReference reference;

    [Header(".....Data from Firebase.....")]
    public List<LevelData> levels;
    public GameObject ProductPrefab;
    public GameObject Canvas;
    int levelCounter;
    string _userID;

    [SerializeField] Button LoadData;

    [Header(".....User Data.....")]
    public List<LevelData> _UserLevels;
    public GameObject loading;

    //[Header("Levels List")]
    //public List<LevelsString> Levels;

    UnityEvent showProductEvent;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (instance != this || instance == null)
            instance = this;

        LoadData.onClick.AddListener(LoadLevelWithBtnClick);

        levelCounter = 0;
    }

    #region read Coins
    public void ReadData()
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
            LiveData.data.userData.Coins = Int16.Parse(snapshot.Value.ToString());
            //oncallback.Invoke(snapshot.Value.ToString());
        }
    }
    #endregion
    public void RetrieveUserData(string UID, string userName)
    {
        loading.SetActive(true);
        FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(UID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string data = snapshot.GetRawJsonValue();
                LiveData.data.userData = JsonUtility.FromJson<UserData>(data);

                DataSnapshot newSnap = task.Result.Child("Levels");


                foreach (var item in newSnap.Children)
                {
                    string LevelsSting = item.GetRawJsonValue();
                    Levels _levels = JsonUtility.FromJson<Levels>(LevelsSting);
                    Debug.Log(LevelsSting);
                    //Levels _levels = new Levels();
                    _levels = JsonUtility.FromJson<Levels>(item.GetRawJsonValue());
                    //_levels.LevelName = 
                    foreach (var _item in item.Children)
                    {
                        ProductData productData = new ProductData();
                        productData = JsonUtility.FromJson<ProductData>(_item.GetRawJsonValue());
                        _levels._pName.Add(productData);
                    }
                    LiveData.data.userData.Levels.Add(_levels);
                }
                UnityEvent @event = new UnityEvent();
                @event.AddListener(LoadNewScene);
                LoadServerData(@event);
                // Do something with snapshot...
            }
        });
    }
    void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }
    #region Creating User Basic Data
    public void CreateUser(string id, string UID)
    {
        loading.SetActive(true);
        _userID = UID;

        LiveData.data.userID = _userID;
        LiveData.data.userData.Coins = 2;
        LiveData.data.userData.Earnings = 0;
        LiveData.data.userData.Token = id;
        string json = JsonUtility.ToJson(LiveData.data.userData);
        reference.Child("Users").Child(_userID).SetRawJsonValueAsync(json);
        if (showProductEvent == null)
            showProductEvent = new UnityEvent();
        showProductEvent.AddListener(ShowProducts);
        LoadServerData(showProductEvent);
        //_ali ReadALLData();
    }
    #endregion

    #region load Levels data on start... after login 
    public void LoadServerData(UnityEvent @event)
    {
        int counter = 0;
        StartCoroutine(Load((string name) =>
        {
            LoadProduct_Sprites(counter, @event);
            counter += 1;
            //Invoke("ShowProducts", 5f);
        }));
    }
    IEnumerator Load(Action<string> oncallback)
    {
        foreach (var item in LiveData.data.DataList)
        {
            var value = reference.Child(item.LevelName).GetValueAsync();
            yield return new WaitUntil(predicate: () => value.IsCompleted);

            if (value != null)
            {
                DataSnapshot snapshot = value.Result;
                foreach (var childSnapShot in snapshot.Children)
                {
                    item._productName.Add(childSnapShot.Value.ToString());
                }
                oncallback.Invoke(snapshot.Value.ToString());
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void LoadProduct_Sprites(int counter, UnityEvent @event)
    {
        Debug.Log("Counter " + counter);
        var _p = LiveData.data.DataList[counter];
        for (int i = 0; i < _p._productName.Count; i++)
        {
            LoadImage.instance.LoopFunction_GetSprite(_p.LevelName, _p._productName[i], _p, @event);
        }
    }
    void LoadLevelWithBtnClick()
    {
        //Add At least 2 Products
        if (ProductAddCounter < 2) return;
        ProductAddCounter = 0;

        levelCounter++;
        if (levelCounter >= levels.Count)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            ShowProducts();
        }
    }
    void ShowProducts()
    {
        loading.SetActive(false);
        Debug.Log("Counter " + levelCounter);
        if (Canvas.transform.childCount > 0)
        {
            foreach (Transform child in Canvas.transform)
            {
                Destroy(child.gameObject);
            }
        }

        var _p = LiveData.data.DataList[levelCounter];

        Levels _levels = new Levels();
        LiveData.data.userData.Levels.Add(_levels);

        for (int i = 0; i < _p._productName.Count; i++)
        {
            var _product = Instantiate(ProductPrefab, Canvas.transform);
            _product.GetComponent<Product>().name = _p._productName[i];
            _product.GetComponent<Product>()._pName = _p._productName[i];
            _product.GetComponent<Product>().LevelName = _p.LevelName;

            _product.GetComponent<Product>().levels = _levels;
            if (_p._Tex2D[i]._productimage != null)
                _product.GetComponent<Product>().image.texture = _p._Tex2D[i]._productimage;

            //LoadImage.instance.LoopFunction_GetImage(_p.LevelName, _p._productName[i], _product.GetComponent<Product>().image);
        }
    }
    #endregion
    #region Load data one by one...
    void ReadALLData()
    {
        StartCoroutine(GetName((string name) =>
        {
            //ReadData();
        }));

        //StartCoroutine(GetValue((string name) =>
        //{
        //    coins = Int16.Parse(name);
        //    text.text = coins+"";
        //    Debug.Log(coins);
        //    ReadData();
        //}));
    }

    IEnumerator GetName(Action<string> oncallback)
    {
        int _dataNameCounter = 0;

        foreach (var item in levels)
        {
            var value = reference.Child(item.LevelName).GetValueAsync();
            yield return new WaitUntil(predicate: () => value.IsCompleted);

            if (value != null)
            {
                DataSnapshot snapshot = value.Result;
                foreach (var childSnapShot in snapshot.Children)
                {
                    item._productName.Add(childSnapShot.Value.ToString());
                }
                Debug.Log(snapshot.Value.ToString());
                oncallback.Invoke(snapshot.Value.ToString());
                _dataNameCounter++;
                yield return new WaitForEndOfFrame();
            }
        }

        while (_dataNameCounter < levels.Count)
        {
            yield return null;
        }
        LoadProduct(0);
    }

    #endregion

    #region Read User Data ================not used
    void ReadUserData()
    {
        StartCoroutine(GetUserData((string name) =>
        {
            //ReadData();
        }));
        //StartCoroutine(GetValue((string name) =>
        //{
        //    coins = Int16.Parse(name);
        //    text.text = coins+"";
        //    Debug.Log(coins);
        //    ReadData();
        //}));
    }

    IEnumerator GetUserData(Action<string> oncallback)
    {
        foreach (var item in LiveData.data.DataList)
        {
            var value = reference.Child(item.LevelName).GetValueAsync();
            yield return new WaitUntil(predicate: () => value.IsCompleted);

            if (value != null)
            {
                DataSnapshot snapshot = value.Result;
                foreach (var childSnapShot in snapshot.Children)
                {
                    item._productName.Add(childSnapShot.Value.ToString());
                }
                oncallback.Invoke(snapshot.Value.ToString());
                yield return new WaitForEndOfFrame();

                #region Commented
                //for (int i = 0; i < item.ImagesName.Count; i++)
                //{
                //    var _product = Instantiate(ProductPrefab, Canvas.transform);
                //    _product.GetComponent<Product>().name = item.ImagesName[i];
                //    _product.GetComponent<Product>()._pName = item.ImagesName[i];
                //    _product.GetComponent<Product>().LevelName = item.LevelName;

                //    LoadImage.instance.LoopFunction(item.LevelName, item.ImagesName[i], _product.GetComponent<Product>().raw);
                //}
                #endregion
            }
        }

    }

    #endregion

    #region Load selected Level Data...
    public void LevelCounter()
    {
        levelCounter++;
        if (levelCounter > levels.Count)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            LoadProduct(levelCounter);
        }
    }

    void LoadProduct(int counter)
    {
        Debug.Log("Counter " + counter);
        if (Canvas.transform.childCount > 0)
        {
            foreach (Transform child in Canvas.transform)
            {
                Destroy(child.gameObject);
            }
        }

        var _p = LiveData.data.DataList[counter];
        for (int i = 0; i < _p._productName.Count; i++)
        {
            var _product = Instantiate(ProductPrefab, Canvas.transform);
            _product.GetComponent<Product>().name = _p._productName[i];
            _product.GetComponent<Product>()._pName = _p._productName[i];
            _product.GetComponent<Product>().LevelName = _p.LevelName;

            LoadImage.instance.LoopFunction_GetImage(_p.LevelName, _p._productName[i], _product.GetComponent<Product>().image);
        }
    }
    #endregion

    public void _UserData()
    {
        for (int i = 0; i < _UserLevels.Count; i++)
        {
            LoadUserProducts(i);
        }
    }
    void LoadUserProducts(int counter)
    {
        var _p = _UserLevels[counter];
        for (int i = 0; i < _p._productName.Count; i++)
        {
            Debug.Log("Counter: " + counter);
            var _product = Instantiate(ProductPrefab, Canvas.transform);
            _product.GetComponent<Product>().name = _p._productName[i];
            _product.GetComponent<Product>()._pName = _p._productName[i];
            _product.GetComponent<Product>().LevelName = _p.LevelName;

            LoadImage.instance.LoopFunction_GetImage(_p.LevelName, _p._productName[i], _product.GetComponent<Product>().image);
        }
    }

    public void AddProduct(string LevelName, string ProductName, int Quantity, float Price, Levels _levels)
    {
        ProductData productData = new ProductData();
        productData._pName = ProductName;
        productData.Quantity = Quantity;
        productData.Price = Price;
        string json = JsonUtility.ToJson(productData);
        reference.Child("Users").Child(_userID).Child("Levels").Child(LevelName).Child(ProductName).SetRawJsonValueAsync(json);
        //Add user Data Local
        if (!LiveData.data.userData.Levels[0]._pName.Contains(productData))
        {
            _levels._pName.Add(productData);
        }
        else if (LiveData.data.userData.Levels[0]._pName.Contains(productData))
        {
            for (int i = 0; i < _levels._pName.Count; i++)
            {
                if (_levels._pName[i] == productData)
                {
                    _levels._pName[i]._pName = productData._pName;
                    _levels._pName[i].Quantity = productData.Quantity;
                    _levels._pName[i].Price = productData.Price;
                    break;
                }
            }
        }
        //reference.Child("Users").Child(_userID).Child(LevelName).SetRawJsonValueAsync(json);
    }
    #region Push Won Product and update earning
    public void PushWonProduct(string pName, float price)
    {
        reference.Child("Users").Child(LiveData.data.userID).Child("Earnings").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                LiveData.data.userData.Earnings = Int16.Parse(snapshot.Value.ToString());
                LiveData.data.userData.Earnings += price;
                //update earning 
                reference.Child("Users").Child(LiveData.data.userID).Child("Earnings").SetValueAsync(LiveData.data.userData.Earnings);
                //post won product
                ProductWonEarning entry = new ProductWonEarning(pName, price);
                Dictionary<string, System.Object> entryValues = entry.ToDictionary();
                reference.Child("Users").Child(LiveData.data.userID).Child("WonProducts").Push().SetValueAsync(entryValues);
                //subtract product that won

            }
        });
    }

    #endregion

    #region Commented
    //public void SaveData()
    //{
    //    coins--;
    //    reference.Child(name).SetValueAsync(coins);
    //    ReadData();
    //}

    //IEnumerator GetValue(Action<string> oncallback)
    //{
    //    var value = reference.Child(name).GetValueAsync();
    //    yield return new WaitUntil(predicate: () => value.IsCompleted);

    //    if(value != null)
    //    {
    //        DataSnapshot snapshot = value.Result;
    //        oncallback.Invoke(snapshot.Value.ToString());
    //    }
    //}
    #endregion
}


