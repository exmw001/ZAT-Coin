using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonProducts : MonoBehaviour
{
    public static WonProducts instance;

    public List<WonProductData> wonProducts;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddWonProduct(int _level, int productIdx, string productName)
    {
        WonProductData _WonProducts = new WonProductData();
        _WonProducts._Level = _level;
        _WonProducts._productIndex = productIdx;
        _WonProducts.ProductName = productName;
        wonProducts.Add(_WonProducts);
    }
}
