using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board board;
    public GameObject[] Baskets;

    /*    public List<Sprite> Level_1;
        public List<Sprite> Level_2;
        public List<Sprite> Level_3;
        public List<Sprite> Level_4;
        public List<Sprite> Level_5;
        public List<Sprite> Level_6;
        public List<Sprite> Level_7;
        public List<Sprite> Level_8;
        public List<Sprite> Products;
        public List<Texture2D> ProductsTexture;*/
    public List<UserProductLocal> _userProductLocal;

    public Texture NextTexture;
    int Rand;
    public List<int> listRandomBaskets = new List<int>();
    public List<int> index_random_Basket;
    public static GameManager instance;
    int scene_index;

    public List<Sprite> _sprites;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        scene_index = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        AudioManager.instance.Stop();

        if (scene_index > 0)
        {
            int n = Random.Range(0, 2);
            if (n == 0)
                AudioManager.instance.Play("BG Music 1");
            else
                AudioManager.instance.Play("BG Music 2");
        }

        switch (board)
        {
            case Board.Board1:
                Board_1();
                break;
            case Board.Board2:
                Board_2();
                break;
            case Board.Board3:
                Board_3();
                break;
            case Board.Board4:
                Board_4();
                break;
            default:
                break;
        }
    }

    #region Board 1
    void Board_1()
    {
        //Add 2 products from Level_1
        List<int> vs = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            while (vs.Contains(Rand))
            {
                Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            }
            vs.Add(Rand);
            UserProductLocal userProduct = new UserProductLocal
            {
                productName = LiveData.data.userData.Levels[0]._pName[Rand]._pName,
                Price = LiveData.data.userData.Levels[0]._pName[Rand].Price,
                ProductLevel = 0
            };
            _userProductLocal.Add(userProduct);

            //ProductsTexture.Add(LiveData.data.DataList[0]._productimage[vs[i]]);
        }

        //Add 2 products from Level_2
        List<int> _vs = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
            while (_vs.Contains(Rand))
            {
                Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
            }
            _vs.Add(Rand);
            UserProductLocal userProduct1 = new UserProductLocal();
            userProduct1.productName = LiveData.data.userData.Levels[1]._pName[Rand]._pName;
            userProduct1.Price = LiveData.data.userData.Levels[1]._pName[Rand].Price;
            userProduct1.ProductLevel = 1;
            _userProductLocal.Add(userProduct1);
        }

        //Add 1 products from Level_3
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        UserProductLocal userProduct2 = new UserProductLocal();
        userProduct2.productName = LiveData.data.userData.Levels[2]._pName[Rand]._pName;
        userProduct2.Price = LiveData.data.userData.Levels[2]._pName[Rand].Price;
        userProduct2.ProductLevel = 2;
        _userProductLocal.Add(userProduct2);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        UserProductLocal userProduct3 = new UserProductLocal();
        userProduct3.productName = LiveData.data.userData.Levels[3]._pName[Rand]._pName;
        userProduct3.Price = LiveData.data.userData.Levels[3]._pName[Rand].Price;
        userProduct3.ProductLevel = 3;
        _userProductLocal.Add(userProduct3);

        //Add 1 products to load next Board
        UserProductLocal userProduct4 = new UserProductLocal();
        userProduct4.productName = "Next";
        userProduct4.ProductLevel = -1;
        _userProductLocal.Add(userProduct4);
        //Add 1 products to load next Board
        UserProductLocal userProduct5 = new UserProductLocal();
        userProduct5.productName = "Next";
        userProduct5.ProductLevel = -1;
        _userProductLocal.Add(userProduct5);

        //Baskets fill with random items from products list 
        //Board_1_Assign();
        Board_Assign_new();
    }
    /*void Board_1_Assign()
    {
        listRandomBaskets = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, _userProductLocal.Count);

            while (listRandomBaskets.Contains(Rand))
            {
                Rand = Random.Range(0, _userProductLocal.Count);
            }

            listRandomBaskets[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            //Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[list[i]];
            //Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[list[i]].name;
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _userProductLocal[listRandomBaskets[i]].productName;
            for (int j = 0; j < LiveData.data.DataList[_userProductLocal[listRandomBaskets[i]].ProductLevel]._productName.Count; j++)
            {
                if (LiveData.data.DataList[_userProductLocal[listRandomBaskets[i]].ProductLevel]._productName.Contains(_userProductLocal[listRandomBaskets[i]].productName))
                {
                    Baskets[i].transform.GetChild(0).GetComponent<RawImage>().texture = LiveData.data.DataList[_userProductLocal[listRandomBaskets[i]].ProductLevel]._productimage[j];
                    break;
                }
            }

            if (listRandomBaskets[i] == 0 || listRandomBaskets[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (listRandomBaskets[i] == 2 || listRandomBaskets[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (listRandomBaskets[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (listRandomBaskets[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (listRandomBaskets[i] == 6 || listRandomBaskets[i] == 7)
            {
                Baskets[i].transform.GetChild(0).tag = "Egg";
            }
        }
    }*/
    #endregion

    #region Board 2
    void Board_2()
    {
        //Add 2 products from Level_1
        List<int> vs = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            while (vs.Contains(Rand))
            {
                Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            }
            vs.Add(Rand);
            UserProductLocal userProduct = new UserProductLocal();
            userProduct.productName = LiveData.data.userData.Levels[0]._pName[Rand]._pName;
            userProduct.Price = LiveData.data.userData.Levels[0]._pName[Rand].Price;
            userProduct.ProductLevel = 0;
            _userProductLocal.Add(userProduct);
            //ProductsTexture.Add(LiveData.data.DataList[0]._productimage[vs[i]]);
        }

        //Add 1 products from Level_2
        Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
        UserProductLocal userProduct1 = new UserProductLocal();
        userProduct1.productName = LiveData.data.userData.Levels[1]._pName[Rand]._pName;
        userProduct1.Price = LiveData.data.userData.Levels[1]._pName[Rand].Price;
        userProduct1.ProductLevel = 1;
        _userProductLocal.Add(userProduct1);

        //Add 1 products from Level_3
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        UserProductLocal userProduct2 = new UserProductLocal();
        userProduct2.productName = LiveData.data.userData.Levels[2]._pName[Rand]._pName;
        userProduct2.Price = LiveData.data.userData.Levels[2]._pName[Rand].Price;
        userProduct2.ProductLevel = 2;
        _userProductLocal.Add(userProduct2);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        UserProductLocal userProduct3 = new UserProductLocal();
        userProduct3.productName = LiveData.data.userData.Levels[3]._pName[Rand]._pName;
        userProduct3.Price = LiveData.data.userData.Levels[3]._pName[Rand].Price;
        userProduct3.ProductLevel = 3;
        _userProductLocal.Add(userProduct3);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[4]._pName.Count);
        UserProductLocal userProduct4 = new UserProductLocal();
        userProduct4.productName = LiveData.data.userData.Levels[4]._pName[Rand]._pName;
        userProduct4.Price = LiveData.data.userData.Levels[4]._pName[Rand].Price;
        userProduct4.ProductLevel = 4;
        _userProductLocal.Add(userProduct4);

        //Add 1 products to load next Board
        UserProductLocal userProduct5 = new UserProductLocal();
        userProduct5.productName = "Next";
        userProduct5.ProductLevel = -1;
        _userProductLocal.Add(userProduct5);

        //Add 1 products to load next Board
        UserProductLocal userProduct6 = new UserProductLocal();
        userProduct6.productName = "Next";
        userProduct6.ProductLevel = -1;
        _userProductLocal.Add(userProduct6);

        //Baskets fill with random items from products list 
        //Board_2_Assign();
        Board_Assign_new();

    }
    /* void Board_2_Assign()
     {
         listRandomBaskets = new List<int>(new int[Baskets.Length]);
         for (int j = 1; j < Baskets.Length; j++)
         {
             Rand = Random.Range(0, Products.Count);

             while (listRandomBaskets.Contains(Rand))
             {
                 Rand = Random.Range(0, Products.Count);
             }

             listRandomBaskets[j] = Rand;
         }

         for (int i = 0; i < Baskets.Length; i++)
         {
             Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[listRandomBaskets[i]];
             Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[listRandomBaskets[i]].name;

             if (listRandomBaskets[i] == 0 || listRandomBaskets[i] == 1)
             {
                 Baskets[i].transform.GetChild(0).tag = "Level 1";
             }
             else if (listRandomBaskets[i] == 2)
             {
                 Baskets[i].transform.GetChild(0).tag = "Level 2";
             }
             else if (listRandomBaskets[i] == 3)
             {
                 Baskets[i].transform.GetChild(0).tag = "Level 3";
             }
             else if (listRandomBaskets[i] == 4)
             {
                 Baskets[i].transform.GetChild(0).tag = "Level 4";
             }
             else if (listRandomBaskets[i] == 5)
             {
                 Baskets[i].transform.GetChild(0).tag = "Level 4";
             }
             else if (listRandomBaskets[i] == 6 || listRandomBaskets[i] == 7)
             {
                 Baskets[i].transform.GetChild(0).tag = "Egg";
             }
         }
     }*/
    #endregion

    #region Board 3
    void Board_3()
    {
        //Add 1 products from Level_1
        Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
        UserProductLocal userProduct = new UserProductLocal();
        userProduct.productName = LiveData.data.userData.Levels[0]._pName[Rand]._pName;
        userProduct.Price = LiveData.data.userData.Levels[0]._pName[Rand].Price;
        userProduct.ProductLevel = 0;
        _userProductLocal.Add(userProduct);

        //Add 1 products from Level_2
        Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
        UserProductLocal userProduct1 = new UserProductLocal();
        userProduct1.productName = LiveData.data.userData.Levels[1]._pName[Rand]._pName;
        userProduct1.Price = LiveData.data.userData.Levels[1]._pName[Rand].Price;
        userProduct1.ProductLevel = 1;
        _userProductLocal.Add(userProduct1);

        //Add 1 products from Level_3
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        UserProductLocal userProduct2 = new UserProductLocal();
        userProduct2.productName = LiveData.data.userData.Levels[2]._pName[Rand]._pName;
        userProduct2.Price = LiveData.data.userData.Levels[2]._pName[Rand].Price;
        userProduct2.ProductLevel = 2;
        _userProductLocal.Add(userProduct2);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        UserProductLocal userProduct3 = new UserProductLocal();
        userProduct3.productName = LiveData.data.userData.Levels[3]._pName[Rand]._pName;
        userProduct3.Price = LiveData.data.userData.Levels[3]._pName[Rand].Price;
        userProduct3.ProductLevel = 3;
        _userProductLocal.Add(userProduct3);

        //Add 1 products from Level_5
        Rand = Random.Range(0, LiveData.data.userData.Levels[4]._pName.Count);
        UserProductLocal userProduct4 = new UserProductLocal();
        userProduct4.productName = LiveData.data.userData.Levels[4]._pName[Rand]._pName;
        userProduct4.Price = LiveData.data.userData.Levels[4]._pName[Rand].Price;
        userProduct4.ProductLevel = 4;
        _userProductLocal.Add(userProduct4);

        //Add 1 products from Level_6
        Rand = Random.Range(0, LiveData.data.userData.Levels[5]._pName.Count);
        UserProductLocal userProduct5 = new UserProductLocal();
        userProduct5.productName = LiveData.data.userData.Levels[5]._pName[Rand]._pName;
        userProduct5.Price = LiveData.data.userData.Levels[5]._pName[Rand].Price;
        userProduct5.ProductLevel = 5;
        _userProductLocal.Add(userProduct5);

        //Add 1 products from Level_5
        Rand = Random.Range(0, LiveData.data.userData.Levels[6]._pName.Count);
        UserProductLocal userProduct6 = new UserProductLocal();
        userProduct6.productName = LiveData.data.userData.Levels[6]._pName[Rand]._pName;
        userProduct6.Price = LiveData.data.userData.Levels[6]._pName[Rand].Price;
        userProduct6.ProductLevel = 6;
        _userProductLocal.Add(userProduct6);

        //Add 1 products to load next Board
        UserProductLocal userProduct7 = new UserProductLocal();
        userProduct7.productName = "Next";
        userProduct7.ProductLevel = -1;
        _userProductLocal.Add(userProduct7);

        //Baskets fill with random items from products list 
        //Board_3_Assign(); 
        Board_Assign_new();

    }
    /*void Board_3_Assign()
    {
        listRandomBaskets = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (listRandomBaskets.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            listRandomBaskets[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[listRandomBaskets[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[listRandomBaskets[i]].name;

            if (listRandomBaskets[i] == 0)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (listRandomBaskets[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (listRandomBaskets[i] == 2)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (listRandomBaskets[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (listRandomBaskets[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 5";
            }
            else if (listRandomBaskets[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 6";
            }
            else if (listRandomBaskets[i] == 6)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 7";
            }
            else if (listRandomBaskets[i] == 7)
            {
                Baskets[i].transform.GetChild(0).tag = "Egg";
            }
        }
    }*/
    #endregion

    #region Board 4
    void Board_4()
    {
        //Add 1 products from Level_1
        Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
        UserProductLocal userProduct = new UserProductLocal();
        userProduct.productName = LiveData.data.userData.Levels[0]._pName[Rand]._pName;
        userProduct.Price = LiveData.data.userData.Levels[0]._pName[Rand].Price;
        userProduct.ProductLevel = 0;
        _userProductLocal.Add(userProduct);

        //Add 1 products from Level_2
        Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
        UserProductLocal userProduct1 = new UserProductLocal();
        userProduct1.productName = LiveData.data.userData.Levels[1]._pName[Rand]._pName;
        userProduct1.Price = LiveData.data.userData.Levels[1]._pName[Rand].Price;
        userProduct1.ProductLevel = 1;
        _userProductLocal.Add(userProduct1);

        //Add 1 products from Level_3
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        UserProductLocal userProduct2 = new UserProductLocal();
        userProduct2.productName = LiveData.data.userData.Levels[2]._pName[Rand]._pName;
        userProduct2.Price = LiveData.data.userData.Levels[2]._pName[Rand].Price;
        userProduct2.ProductLevel = 2;
        _userProductLocal.Add(userProduct2);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        UserProductLocal userProduct3 = new UserProductLocal();
        userProduct3.productName = LiveData.data.userData.Levels[3]._pName[Rand]._pName;
        userProduct3.Price = LiveData.data.userData.Levels[3]._pName[Rand].Price;
        userProduct3.ProductLevel = 3;
        _userProductLocal.Add(userProduct3);

        //Add 1 products from Level_5
        Rand = Random.Range(0, LiveData.data.userData.Levels[4]._pName.Count);
        UserProductLocal userProduct4 = new UserProductLocal();
        userProduct4.productName = LiveData.data.userData.Levels[4]._pName[Rand]._pName;
        userProduct4.Price = LiveData.data.userData.Levels[4]._pName[Rand].Price;
        userProduct4.ProductLevel = 4;
        _userProductLocal.Add(userProduct4);

        //Add 1 products from Level_6
        Rand = Random.Range(0, LiveData.data.userData.Levels[5]._pName.Count);
        UserProductLocal userProduct5 = new UserProductLocal();
        userProduct5.productName = LiveData.data.userData.Levels[5]._pName[Rand]._pName;
        userProduct5.Price = LiveData.data.userData.Levels[5]._pName[Rand].Price;
        userProduct5.ProductLevel = 5;
        _userProductLocal.Add(userProduct5);

        //Add 1 products from Level_7
        Rand = Random.Range(0, LiveData.data.userData.Levels[6]._pName.Count);
        UserProductLocal userProduct6 = new UserProductLocal();
        userProduct6.productName = LiveData.data.userData.Levels[6]._pName[Rand]._pName;
        userProduct6.Price = LiveData.data.userData.Levels[6]._pName[Rand].Price;
        userProduct6.ProductLevel = 6;
        _userProductLocal.Add(userProduct6);

        //Add 1 products from Level_8
        Rand = Random.Range(0, LiveData.data.userData.Levels[7]._pName.Count);
        UserProductLocal userProduct7 = new UserProductLocal();
        userProduct7.productName = LiveData.data.userData.Levels[7]._pName[Rand]._pName;
        userProduct7.Price = LiveData.data.userData.Levels[7]._pName[Rand].Price;
        userProduct7.ProductLevel = 7;
        _userProductLocal.Add(userProduct7);


        //Baskets fill with random items from products list 
        //Board_4_Assign();
        Board_Assign_new();

    }
    /*void Board_4_Assign()
    {
        listRandomBaskets = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (listRandomBaskets.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            listRandomBaskets[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[listRandomBaskets[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[listRandomBaskets[i]].name;

            if (listRandomBaskets[i] == 0)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (listRandomBaskets[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (listRandomBaskets[i] == 2)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (listRandomBaskets[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (listRandomBaskets[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 5";
            }
            else if (listRandomBaskets[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 6";
            }
            else if (listRandomBaskets[i] == 6)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 7";
            }
            else if (listRandomBaskets[i] == 7)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 8";
            }
        }
    }*/
    #endregion

    void Board_Assign_new()
    {
        #region create random number for baskets to show products
        for (int j = 0; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, _userProductLocal.Count);

            while (index_random_Basket.Contains(Rand))
            {
                Rand = Random.Range(0, _userProductLocal.Count);
            }

            index_random_Basket.Add(Rand);
        }
        #endregion
        for (int i = 0; i < Baskets.Length; i++)
        {
            int Basket_idx = index_random_Basket[i];
            Basket basket = Baskets[Basket_idx].GetComponent<Basket>();
            int _level = _userProductLocal[i].ProductLevel;
            if (_level >= 0)//LiveData.data.DataList[_level]._productName.Contains(_userProductLocal[i].productName))
            {
                for (int j = 0; j < LiveData.data.DataList[_level]._Tex2D.Count; j++)
                {
                    if (LiveData.data.DataList[_level]._Tex2D[j]._productName == _userProductLocal[i].productName + ".png")
                    {
                        Baskets[Basket_idx].name = _level.ToString();
                        basket._productName = _userProductLocal[i].productName;
                        basket.productLevel = _level;
                        basket.productIndex = j;
                        basket.productPrice = _userProductLocal[i].Price;
                        basket.productNameText.text = _userProductLocal[i].productName.ToString();
                        basket.productImage.texture = LiveData.data.DataList[_level]._Tex2D[j]._productimage;
                        int temp = _level + 1;
                        string _tag = "Level" + temp;
                        Baskets[Basket_idx].transform.GetChild(0).tag = _tag;
                        break;
                    }
                }
            }
            else
            {
                Baskets[Basket_idx].transform.GetChild(0).tag = "Egg";
                basket._productName = _userProductLocal[i].productName;
                basket.productLevel = _level;
                basket.productNameText.text = _userProductLocal[i].productName;
                basket.productImage.texture = NextTexture;
            }
        }
    }
}

public enum Board
{
    Board1,
    Board2,
    Board3,
    Board4
}
[System.Serializable]
public class UserProductLocal
{
    public string productName;
    public float Price;
    public int ProductLevel;
}