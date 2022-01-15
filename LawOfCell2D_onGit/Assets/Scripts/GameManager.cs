using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("遊戲設定UI物件")]
    public GameObject PauseUI;

    public GameObject SwitchLan;
    public GameObject PauseUI_CN;
    public GameObject Detail;

    public GameObject CardImg;
    public Sprite[] CardsImgs;

    // 判斷遊戲勝利還是失敗
    public bool isWin;
    [Header("遊戲結束UI物件")]
    public GameObject GameOverUI;

    [Header("遊戲勝利失敗背景圖")]
    public GameObject GameOverUIBack;

    [Header("遊戲勝利失敗Image")]
    public Image GameOverUIImage;

    [Header("遊戲勝利背景圖")]
    public Sprite WinBackSprite;

    [Header("遊戲失敗背景圖")]
    public Sprite LoseBackSprite;

    [Header("遊戲勝利錢")]
    public GameObject WinTextMoney;

    [Header("遊戲失敗分數")]
    public GameObject LoseTextLevel;

    [Header("遊戲勝利圖")]
    public Sprite WinSprite;

    [Header("遊戲失敗圖")]
    public Sprite LoseSprite;

    [Header("遊戲勝利UI")]
    public GameObject WinUI;

    [Header("遊戲失敗UI")]
    public GameObject LoseUI;

    [Header("新手教學UI")]
    public GameObject Tutor;

    [Header("音效slider")]
    public GameObject volumeSlider;

    [Header("音樂slider")]
    public GameObject BGMSlider;

    [Header("音樂播放器")]
    public GameObject BackgroundSound;

    [Header("放要播放的音樂")]
    public AudioClip[] clip;


    //目前播放哪一首
    int musicPlay = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q")) { Application.LoadLevel("Game"); }
        if (!BackgroundSound.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            //指向不同首歌
            if (musicPlay == clip.Length - 1)
                musicPlay = 0;
            else
                musicPlay++;
            //換首歌
            BackgroundSound.gameObject.GetComponent<AudioSource>().clip = clip[musicPlay];
            BackgroundSound.gameObject.GetComponent<AudioSource>().Play();
        }
        BackgroundSound.gameObject.GetComponent<AudioSource>().volume = Level.BGM;

    }

    #region 場景跳轉
    public void ToMap()//跳轉至Map
    {
        Time.timeScale = 1;//功能才正常
        Application.LoadLevel("Map");
    }

    public void ToMenu()//跳轉至Menu
    {
        Time.timeScale = 1;//功能才正常
        Application.LoadLevel("Menu");
    }

    public void ToTutor()//跳轉至Tutor
    {
        Time.timeScale = 1;//功能才正常
        Application.LoadLevel("Tutor");
    }
    #endregion

    #region 暫停
    public void Pause()
    {
        //Time.timeScale=0; 遊戲整體時間暫停
        Time.timeScale = 0;
        SwitchLan.SetActive(false);
        if (Level.Lan_CN)
        {
            PauseUI_CN.SetActive(true);
            //設定初始音量
            volumeSlider = GameObject.Find("Slider");
            BGMSlider = GameObject.Find("BGMSlider");
            volumeSlider.gameObject.GetComponent<Slider>().value = Level.volume;
            BGMSlider.gameObject.GetComponent<Slider>().value = Level.BGM;
        }
        else
        {
            PauseUI.SetActive(true);
            //設定初始音量
            volumeSlider = GameObject.Find("Slider");
            BGMSlider = GameObject.Find("BGMSlider");
            volumeSlider.gameObject.GetComponent<Slider>().value = Level.volume;
            BGMSlider.gameObject.GetComponent<Slider>().value = Level.BGM;
        }

    }
    public void UnPause()
    {
        //Time.timeScale=1; 遊戲整體時間恢復
        Time.timeScale = 1;
        if (Level.Lan_CN)
        {
            PauseUI_CN.SetActive(false);
        }
        else
        {
            PauseUI.SetActive(false);
        }

    }
    #endregion 暫停

    public void SwitchLanUI()
    {
        SwitchLan.SetActive(true);
        if (Level.Lan_CN)
        {
            PauseUI_CN.SetActive(false);
        }
        else
        {
            PauseUI.SetActive(false);
        }
    }
    public void SwitchToEN()
    {
        Level.Lan_CN = false;
        GamePause();
    }
    public void SwitchToCN()
    {
        Level.Lan_CN = true;
        GamePause();
    }

    #region Menu暫停
    public void GamePause()
    {

        //Time.timeScale=0; 遊戲整體時間暫停
        Time.timeScale = 0;
        Detail.SetActive(false);
        SwitchLan.SetActive(false);
        if (Level.Lan_CN)
        {
            PauseUI_CN.SetActive(true);
            //設定初始音量
            volumeSlider = GameObject.Find("Slider");
            BGMSlider = GameObject.Find("BGMSlider");
            volumeSlider.gameObject.GetComponent<Slider>().value = Level.volume;
            BGMSlider.gameObject.GetComponent<Slider>().value = Level.BGM;
        }
        else
        {
            PauseUI.SetActive(true);
            //設定初始音量
            volumeSlider = GameObject.Find("Slider");
            BGMSlider = GameObject.Find("BGMSlider");
            volumeSlider.gameObject.GetComponent<Slider>().value = Level.volume;
            BGMSlider.gameObject.GetComponent<Slider>().value = Level.BGM;
        }
    }
    public void GameUnPause()
    {

        //Time.timeScale=1; 遊戲整體時間恢復
        Time.timeScale = 1;
        Detail.SetActive(true);
        if (Level.Lan_CN)
        {
            PauseUI_CN.SetActive(false);
        }
        else
        {
            PauseUI.SetActive(false);
        }
    }
    #endregion Menu暫停

    #region GameOver
    int[] originalPlayerCardID = { 2, 4, 1, 0, 0, 0 };
    int[] originalPlayerCardNumbers = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };//預設牌組
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Detail.SetActive(false);
        //時間暫停
        Time.timeScale = 0;

        if (isWin)
        {
            WinUI.SetActive(true);
            if (Level.level < 3)
            {
                CardImg.GetComponent<Image>().sprite = CardsImgs[Level.level];
            }
            else
            {
                CardImg.SetActive(false);
            }
            for (int i = 3; i < 6; i++)
            {
                if (Level.PlayerCardID[i] == 0)
                {
                    Level.PlayerCardID[i] = Level.GetCardID[Level.level];
                    break;
                }
            }

            GameOverUIBack.gameObject.GetComponent<Image>().sprite = WinBackSprite;
            GameOverUIImage.sprite = WinSprite;
            Level.level++;
        }
        else
        {
            LoseUI.SetActive(true);
            LoseTextLevel.gameObject.GetComponent<Text>().text = Level.level.ToString();
            GameOverUIBack.gameObject.GetComponent<Image>().sprite = LoseBackSprite;
            GameOverUIImage.sprite = LoseSprite;
            Level.level = 0;
            Level.PlayerHP = 30;
            Level.PlayerCardID = originalPlayerCardID;
            Level.PlayerCardS = originalPlayerCardNumbers;

        }


    }
    #endregion

    public void ToTutorOnClick()//Map問號被打開
    {
        if (!Level.firstTutor)
            Tutor.SetActive(true);
    }
    public void NoBtnOnclick()//不要進去新手教學
    {
        Tutor.SetActive(false);
    }

    public void ToEdtiCard()
    {
        Time.timeScale = 1;//功能才正常
        Application.LoadLevel("CardEditor");
    }
}
