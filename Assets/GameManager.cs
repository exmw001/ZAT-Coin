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

    public Sprite Next;
    int Rand;
    public List<int> list = new List<int>();
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
        List<string> _pNames = new List<string>();
        for (int i = 0; i < 2; i++)
        {
            //Rand = Random.Range(0, Level_1.Count);
            Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            while (vs.Contains(Rand))
            {
                //Rand = Random.Range(0, Level_1.Count);
                Rand = Random.Range(0, LiveData.data.userData.Levels[0]._pName.Count);
            }

            vs.Add(Rand);
            _pNames.Add(LiveData.data.userData.Levels[0]._pName[Rand]._pName);
            for (int j = 0; j < LiveData.data.DataList[0]._productName.Count; j++)
            {
                if (LiveData.data.userData.Levels[0]._pName[Rand]._pName == LiveData.data.DataList[0]._productName[j])
                {
                    ProductsTexture.Add(LiveData.data.DataList[0]._productimage[j]);
                    break;
                }
            }
            //Products.Add(Level_1[Rand]);
        }

        //Add 2 products from Level_2
        List<int> _vs = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            //Rand = Random.Range(0, Level_2.Count);
            Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
            while (_vs.Contains(Rand))
            {
                Rand = Random.Range(0, LiveData.data.userData.Levels[1]._pName.Count);
                //Rand = Random.Range(0, Level_2.Count);
            }
            _vs.Add(Rand);
            Products.Add(Level_2[Rand]);
        }

        //Add 1 products from Level_3
        //Rand = Random.Range(0, Level_3.Count);
        Rand = Random.Range(0, LiveData.data.userData.Levels[2]._pName.Count);
        Products.Add(Level_3[Rand]);

        //Add 1 products from Level_4
        //Rand = Random.Range(0, Level_4.Count);
        Rand = Random.Range(0, LiveData.data.userData.Levels[3]._pName.Count);
        Products.Add(Level_4[Rand]);

        //Add 1 products to load next Board
        Products.Add(Next);
        //Add 1 products to load next Board
        Products.Add(Next);

        //Baskets fill with random items from products list 
        Board_1_Assign();
    }
    void Board_1_Assign()
    {
        list = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            list[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[list[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[list[i]].name;

            if (list[i] == 0 || list[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (list[i] == 2 || list[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (list[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (list[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (list[i] == 6 || list[i] == 7)
            {
                Baskets[i].transform.GetChild(0).tag = "Egg";
            }
        }
    }
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
        list = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            list[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[list[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[list[i]].name;

            if (list[i] == 0 || list[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (list[i] == 2)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (list[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (list[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (list[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (list[i] == 6 || list[i] == 7)
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
        list = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            list[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[list[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[list[i]].name;

            if (list[i] == 0)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (list[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (list[i] == 2)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (list[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (list[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 5";
            }
            else if (list[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 6";
            }
            else if (list[i] == 6)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 7";
            }
            else if (list[i] == 7)
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
        list = new List<int>(new int[Baskets.Length]);
        for (int j = 1; j < Baskets.Length; j++)
        {
            Rand = Random.Range(0, Products.Count);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, Products.Count);
            }

            list[j] = Rand;
        }

        for (int i = 0; i < Baskets.Length; i++)
        {
            Baskets[i].transform.GetChild(0).GetComponent<Image>().sprite = Products[list[i]];
            Baskets[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Products[list[i]].name;

            if (list[i] == 0)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 1";
            }
            else if (list[i] == 1)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 2";
            }
            else if (list[i] == 2)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 3";
            }
            else if (list[i] == 3)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 4";
            }
            else if (list[i] == 4)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 5";
            }
            else if (list[i] == 5)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 6";
            }
            else if (list[i] == 6)
            {
                Baskets[i].transform.GetChild(0).tag = "Level 7";
            }
            else if (list[i] == 7)
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
