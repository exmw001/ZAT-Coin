using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board board;
    public GameObject[] Baskets;

    public List<Sprite> Level_1;
    public List<Sprite> Level_2;
    public List<Sprite> Level_3;
    public List<Sprite> Level_4;
    public List<Sprite> Level_5;
    public List<Sprite> Level_6;
    public List<Sprite> Level_7;
    public List<Sprite> Level_8;

    public List<Sprite> Products;
    public List<Texture2D> ProductsTexture;
    public List<UserProductLocal> _userProductLocal;

    public Sprite Next;
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
            UserProductLocal userProduct = new UserProductLocal();
            userProduct.productName = LiveData.data.userData.Levels[0]._pName[Rand]._pName;
            userProduct.ProductLevel = 0;
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
            userProduct1.ProductLevel = 1;
            _userProductLocal.Add(userProduct1);
        }

        //Add 1 products from Level_3
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        UserProductLocal userProduct2 = new UserProductLocal();
        userProduct2.productName = LiveData.data.userData.Levels[2]._pName[Rand]._pName;
        userProduct2.ProductLevel = 2;
        _userProductLocal.Add(userProduct2);

        //Add 1 products from Level_4
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        UserProductLocal userProduct3 = new UserProductLocal();
        userProduct3.productName = LiveData.data.userData.Levels[3]._pName[Rand]._pName;
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
        /*ProductsTexture.Add(NextTexture);
        ProductsTexture.Add(NextTexture);*/

        //Baskets fill with random items from products list 
        //Board_1_Assign();
        Board_1_Assign_new();
    }
    void Board_1_Assign_new()
    {
        #region create random number for baskets to show products
        for (int j = 0; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, _userProductLocal.Count);

            while (index_random_Basket.Contains(Rand))
            {
                Debug.Log("Rand Basket" + Rand);
                Rand = Random.Range(0, _userProductLocal.Count);
            }

            index_random_Basket.Add(Rand);
        }
        #endregion
        for (int i = 0; i < Baskets.Length; i++)
        {
            Debug.Log(i + "products");
            int Basket_idx = index_random_Basket[i];
            int _level = _userProductLocal[i].ProductLevel;
            if (_level >= 0)//LiveData.data.DataList[_level]._productName.Contains(_userProductLocal[i].productName))
            {
                Debug.Log(_level + "level");
                for (int j = 0; j < LiveData.data.DataList[_level]._Tex2D.Count; j++)
                {
                    if (LiveData.data.DataList[_level]._Tex2D[j]._productName == _userProductLocal[i].productName + ".png")
                    {
                        Debug.Log("tag Level");
                        Baskets[Basket_idx].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _userProductLocal[i].productName.ToString();
                        Baskets[Basket_idx].transform.GetChild(0).GetComponent<RawImage>().texture = LiveData.data.DataList[_level]._Tex2D[j]._productimage;
                        int temp = _level + 1;
                        string _tag = "Level" + temp;
                        Baskets[Basket_idx].transform.GetChild(0).tag = _tag;
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("tag egg");
                Baskets[Basket_idx].transform.GetChild(0).tag = "Egg";
                Baskets[Basket_idx].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _userProductLocal[i].productName;
                Baskets[Basket_idx].transform.GetChild(0).GetComponent<RawImage>().texture = NextTexture;
            }
        }
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
        List<int> vs = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            Rand = Random.Range(0, Level_1.Count);
            while (vs.Contains(Rand))
            {
                Rand = Random.Range(0, Level_1.Count);
            }
            vs.Add(Rand);
            Products.Add(Level_1[Rand]);
        }

        Rand = Random.Range(0, Level_2.Count);
        Products.Add(Level_2[Rand]);

        Rand = Random.Range(0, Level_3.Count);
        Products.Add(Level_3[Rand]);

        Rand = Random.Range(0, Level_4.Count);
        Products.Add(Level_4[Rand]);

        Rand = Random.Range(0, Level_5.Count);
        Products.Add(Level_5[Rand]);

        Products.Add(Next);
        Products.Add(Next);

        Board_2_Assign();
    }
    void Board_2_Assign()
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
    }
    #endregion

    #region Board 3
    void Board_3()
    {
        Rand = Random.Range(0, Level_1.Count);
        Products.Add(Level_1[Rand]);

        Rand = Random.Range(0, Level_2.Count);
        Products.Add(Level_2[Rand]);

        Rand = Random.Range(0, Level_3.Count);
        Products.Add(Level_3[Rand]);

        Rand = Random.Range(0, Level_4.Count);
        Products.Add(Level_4[Rand]);

        Rand = Random.Range(0, Level_5.Count);
        Products.Add(Level_5[Rand]);

        Rand = Random.Range(0, Level_6.Count);
        Products.Add(Level_6[Rand]);

        Rand = Random.Range(0, Level_7.Count);
        Products.Add(Level_7[Rand]);

        Products.Add(Next);

        Board_3_Assign();
    }
    void Board_3_Assign()
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
    }
    #endregion

    #region Board 4
    void Board_4()
    {
        Rand = Random.Range(0, Level_1.Count);
        Products.Add(Level_1[Rand]);

        Rand = Random.Range(0, Level_2.Count);
        Products.Add(Level_2[Rand]);

        Rand = Random.Range(0, Level_3.Count);
        Products.Add(Level_3[Rand]);

        Rand = Random.Range(0, Level_4.Count);
        Products.Add(Level_4[Rand]);

        Rand = Random.Range(0, Level_5.Count);
        Products.Add(Level_5[Rand]);

        Rand = Random.Range(0, Level_6.Count);
        Products.Add(Level_6[Rand]);

        Rand = Random.Range(0, Level_7.Count);
        Products.Add(Level_7[Rand]);

        Rand = Random.Range(0, Level_8.Count);
        Products.Add(Level_8[Rand]);

        Board_4_Assign();
    }
    void Board_4_Assign()
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
    }
    #endregion

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
    public int ProductLevel;
}
