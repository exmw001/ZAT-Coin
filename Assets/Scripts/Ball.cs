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
            else if ((collision.CompareTag("Level1") || collision.CompareTag("Level2") || collision.CompareTag("Level3") || collision.CompareTag("Level4") || collision.CompareTag("Level5") || collision.CompareTag("Level6") || collision.CompareTag("Level7") || collision.CompareTag("Level8")) && trigger)
            {
                trigger = false;
                TableSoundCheck();

                Basket basket = collision.transform.parent.gameObject.GetComponent<Basket>();
                Panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Ganhou 1 " + basket._productName;
                prize.texture = LiveData.data.DataList[basket.productLevel]._Tex2D[basket.productIndex]._productimage;

                WonProducts.instance.AddWonProduct(basket.productLevel, basket.productIndex, basket._productName);
                RealTimeDatabase.instance.PushWonProduct(basket._productName, basket.productPrice);
                StartCoroutine(Won());
                LiveData.data.SubtractProduct(basket._productName);
                soundName = collision.tag;
            }
        }
    }

    IEnumerator Won()
    {
        yield return new WaitForSeconds(1f);
        Panel.SetActive(true);
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
