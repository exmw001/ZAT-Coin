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
    [SerializeField] InputField Price;
    public Levels levels;
    private void Awake()
    {
        button.onClick.AddListener(AddProduct);
    }

    void AddProduct()
    {
        Debug.Log(Price.text);
        float _price = float.Parse(Price.text);
        int _quantity = int.Parse(Quantity.text);
        if (image.texture != null && _price > 0f && _quantity > 0)
        {
            RealTimeDatabase.instance.ProductAddCounter++;
            button.interactable = false;
            RealTimeDatabase.instance.AddProduct(LevelName, _pName, _quantity, _price, levels);
        }
    }
}