using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonProducts : MonoBehaviour
{
    public static WonProducts instance;

    public List<WonProductData> wonProductsName;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddProduct(int _level,string sprite)
    {
        WonProductData _WonProducts = new WonProductData();
        _WonProducts._Level = _level;
        _WonProducts.ProductName = sprite;
        wonProductsName.Add(_WonProducts);
    }
}
