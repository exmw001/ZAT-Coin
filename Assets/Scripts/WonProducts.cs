using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonProducts : MonoBehaviour
{
    public static WonProducts instance;

    public List<Sprite> wonProducts;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddProduct(Sprite sprite)
    {
        wonProducts.Add(sprite);
    }
}
