using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    //public Board board;
    public Sprite[] BackgroundBG;

    //public List<Sprite> _sprites;
    public GameObject prefab;
    public Transform parent;
    public Transform Wonparent;

    public Button WonProduct;

    public Text remamingcoins;
    private void Start()
    {
        WonProduct.onClick.AddListener(ProductsWon);
        ShowAllProducts();
        /*switch (board)
        {
            case Board.Board1:
                Board1();
                break;
            case Board.Board2:
                Board2();
                break;
            case Board.Board3:
                Board3();
                break;
            case Board.Board4:
                Board4();
                break;
            default:
                break;
        }*/
    }
    private void Update()
    {
        remamingcoins.text = LiveData.data.userData.Coins.ToString();
    }
    #region Products
    private void ShowAllProducts()
    {
        for (int i = 0; i < LiveData.data.DataList.Count; i++)//i is the product level
        {
            for (int j = 0; j < LiveData.data.DataList[i]._Tex2D.Count; j++) //j is the product that contain product texture and name
            {
                var v = Instantiate(prefab, parent);
                v.GetComponent<Image>().sprite = BackgroundBG[i];
                v.transform.GetChild(0).GetComponent<RawImage>().texture = LiveData.data.DataList[i]._Tex2D[j]._productimage;
                v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LiveData.data.DataList[i]._Tex2D[j]._productName;
            }
        }

        /*for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, parent);
            v.GetComponent<Image>().sprite = BackgroundBG[0];
            v.transform.GetChild(0).GetComponent<Image>().sprite = _sprites[i];
            v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _sprites[i].name;
        }*/
    }
    /*

    public void Board2()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, parent);
            v.GetComponent<Image>().sprite = BackgroundBG[1];
            v.transform.GetChild(0).GetComponent<Image>().sprite = _sprites[i];
            v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _sprites[i].name;
        }
    }

    public void Board3()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, parent);
            v.GetComponent<Image>().sprite = BackgroundBG[2];
            v.transform.GetChild(0).GetComponent<Image>().sprite = _sprites[i];
            v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _sprites[i].name;
        }
    }

    public void Board4()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, parent);
            v.GetComponent<Image>().sprite = BackgroundBG[3];
            v.transform.GetChild(0).GetComponent<Image>().sprite = _sprites[i];
            v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _sprites[i].name;
        }
    }*/
    #endregion

    #region Won

    int length;
    List<WonProductData> products;
    void ProductsWon()
    {
        ClearView();
        products = WonProducts.instance.wonProducts;
        length = products.Count;
        WonBoard();

        /*switch (board)
        {
            case Board.Board1:
                break;
            case Board.Board2:
                WonBoard2();
                break;
            case Board.Board3:
                WonBoard3();
                break;
            case Board.Board4:
                WonBoard4();
                break;
            default:
                break;
        }*/
    }

    public void WonBoard()
    {
        for (int i = 0; i < length; i++)
        {
            var v = Instantiate(prefab, Wonparent);
            v.GetComponent<Image>().sprite = BackgroundBG[products[i]._Level];//Background Of the won Product 

            v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = products[i].ProductName;
            v.transform.GetChild(0).GetComponent<RawImage>().texture = LiveData.data.DataList[products[i]._Level]._Tex2D[products[i]._productIndex]._productimage;


            /*for (int j = 0; j < LiveData.data.DataList[products[i]._Level]._Tex2D.Count; j++)
            {
                if (LiveData.data.DataList[products[i]._Level]._Tex2D[j]._productName == products[i].ProductName + ".png")
                {
                    v.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = products[i].ProductName;
                    v.transform.GetChild(0).GetComponent<RawImage>().texture = LiveData.data.DataList[products[i]._Level]._Tex2D[j]._productimage;
                    break;
                }
            }*/
        }
    }

    /*public void WonBoard2()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, Wonparent);
            v.GetComponent<Image>().sprite = BackgroundBG[1];
        }
    }

    public void WonBoard3()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, Wonparent);
            v.GetComponent<Image>().sprite = BackgroundBG[2];
        }
    }

    public void WonBoard4()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            var v = Instantiate(prefab, Wonparent);
            v.GetComponent<Image>().sprite = BackgroundBG[3];
        }
    }*/

    void ClearView()
    {
        for (int i = 0; i < Wonparent.childCount; i++)
        {
            Destroy(Wonparent.GetChild(i).gameObject);
        }
    }

    public void ClearList()
    {
        ClearView();
        WonProducts.instance.wonProducts.Clear();
    }

    #endregion
}