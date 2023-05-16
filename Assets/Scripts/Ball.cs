using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public Button ShopButton, WonButton;
    public GameObject Panel, NextPanel, PlayButton;
    public RawImage prize;
    public int scene_index;
    

    private void Awake()
    {
        ShopButton.interactable = WonButton.interactable = true;
        Time.timeScale = 1f;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    private void Start()
    {
        scene_index = SceneManager.GetActiveScene().buildIndex;
        scene_index++;
    }
    public bool check;
    bool move = true;

    private void Update()
    {
        if (move)
        {
            if (check)
                transform.DOLocalMoveX(520f, 2.5f, true).OnComplete(() => check = false);
            if (!check)
                transform.DOLocalMoveX(-520f, 2.5f, true).OnComplete(() => check = true);
        }

    }
    public void gamePlay()
    {
        //if(RealTimeDatabase.coins > 0)
        if (LiveData.data.userData.Coins > 0)
        {
            ShopButton.interactable = WonButton.interactable = false;
            move = false;
            GetComponent<Rigidbody2D>().gravityScale = 9.8f;
            Time.timeScale = 1.5f;
            PlayButton.SetActive(false);
            AudioManager.instance.Play("Play");
        }
    }

    bool trigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Egg"))
            {
                NextPanel.SetActive(true);
                StartCoroutine(LoadScene());
            }
            else if ((collision.tag == "Level1" || collision.tag == "Level2" || collision.tag == "Level3" || collision.tag == "Level4" || collision.tag == "Level5" || collision.tag == "Level6" || collision.tag == "Level7" || collision.tag == "Level8") && trigger)
            {
                trigger = false;
                TableSoundCheck();
                StartCoroutine(Won());
                Panel.transform.GetChild(2).GetComponent<Text>().text = "Ganhou 1 " + collision.transform.parent.GetChild(1).gameObject.GetComponent<TextMeshPro>().text;
                    //prize.transform.parent.GetChild(1).gameObject.GetComponent<TextMesh>().text;
                prize.texture = collision.GetComponent<Texture>();
                int lvl = int.Parse(prize.transform.parent.GetChild(1).name);
                WonProducts.instance.AddProduct(lvl, prize.transform.parent.GetChild(1).gameObject.GetComponent<TextMesh>().text);
                soundName = collision.tag;
            }

        }
    }

    IEnumerator Won()
    {
        yield return new WaitForSeconds(1f);
        Panel.SetActive(true);
        //RealTimeDatabase.instance.subtractCoin();
        LiveData.data.subtractCoin();
        if (LiveData.data.userData.Coins > 0)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(1);
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            Panel.SetActive(false);
            ShopButton.interactable = WonButton.interactable = true;
        }
    }

    bool won = true;
    void TableSoundCheck()
    {
        if (won)
        {
            AudioManager.instance.Stop();
            int n = scene_index - 1;
            switch (n)
            {
                case 0:
                    AudioManager.instance.Play("Table 1");
                    break;
                case 1:
                    AudioManager.instance.Play("Table 2");
                    break;
                case 2:
                    AudioManager.instance.Play("Table 3");
                    break;
                case 3:
                    AudioManager.instance.Play("Table 4");
                    break;
            }
            won = false;
        }
    }

    string soundName;

    public void PlaySoundOnComplete()
    {
        AudioManager.instance.Play(soundName);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Hurdle"))
        {
            AudioManager.instance.Play("Hitting");
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene_index);
    }
}
