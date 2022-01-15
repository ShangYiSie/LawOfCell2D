using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [Header("放關卡的button陣列")]
    public GameObject [] LevelBtn;

    [Header("放定位主角的Image陣列")]
    public GameObject [] CharPosImg;

    [Header("放關卡的Image陣列")]
    public Image [] LevelImg;  

    [Header("放主角的圖")]
    public Image Character;
    [Header("放背景圖")]
    /*public Image BackGroundImg;
    public GameObject BackGround;*/
    public GameObject AllBackGround;
    [Header("放自組卡牌的按鈕")]
    public GameObject CardButton;
    [Header("放大腦")]
    public Image Brain;
    Animator BrainAni;
    /*public GameObject AllBtn;
    public GameObject levelImg;
    public GameObject CharacterPosImg;*/
    public float speed = 0.005f;//移動速度
    public float firstSpeed;//主角移動的速度
    private int j = 0;//紀錄update的次數

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if(Level.level == 0)
            {Level.Pos = Character.transform.position;}
        else
            {Character.transform.position = Level.Pos;}
        if (Level.level < 4)
            {firstSpeed = Vector3.Distance(Character.transform.position, CharPosImg[Level.level].transform.position) * speed;}
        
        int i = 0;
        while(i < LevelBtn.Length)//將三個按鈕都關掉
        {
            LevelBtn[i].SetActive(false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Level.level < 4)
        {
            Character.transform.position = Vector3.Lerp(Character.transform.position,CharPosImg[Level.level].transform.position,speed);
            speed = calculateNewSpeed();
        }
        if(speed > 1000 || speed == 0)
        {
            LevelBtn[Level.level].SetActive(true);
            Level.Pos = Character.transform.position;
        }
        if(Level.level == 4 && j  != 450)
        {
            MapMove();
            j++;
        }
        if(Level.level > 0 || Level.firstTutor == false)
        {
            CardButton.SetActive(true);
        }
        if(Level.level >= 3)
        {
            BrainAni = Brain.GetComponent<Animator>();
            BrainAni.SetBool("isBrain", true);
            if(Level.level == 4)
            {
                int i = 0;
                while (i < LevelBtn.Length)
                {
                    LevelBtn[i].SetActive(false);
                    i++;
                }
            }
        }
         //Debug.Log(Level.level.ToString());
    }
    void MapMove()
    {
        /*BackGroundImg.transform.Translate(0, -2f, 0);
        Character.transform.Translate(0,-2f,0);
        BackGround.transform.Translate(0, -2f, 0);*/
        AllBackGround.transform.Translate(0, -2f, 0);
    }
    float calculateNewSpeed()
    {
        float tmp = Vector3.Distance(Character.transform.position,CharPosImg[Level.level].transform.position);
        if(tmp == 0 || tmp > 1000)
        {
            tmp = 0;
            return tmp;
        }
        else
            return(firstSpeed/tmp);
    }

    /*public void level1_buttonclick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Game");
        Level.level = 1;
    }
    public void level2_buttonclick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Game");
        Level.level = 2;
    }
    public void level3_buttonclick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Game");
        //Level.level = 3;
    }*/

    #region 地圖按鈕onclick
    public void level_buttonclick()//按鈕控制寫成同一個function
    {
        Time.timeScale = 1;
        if (Level.firstTutor)
        {
            Application.LoadLevel("Tutor");
        }
        else
        {
            Application.LoadLevel("Game");
        }
        //改由勝負判斷那邊控制
        //Level.level++;
    }
    #endregion
}
