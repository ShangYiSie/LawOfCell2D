using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardEditorInfo : MonoBehaviour
{

    public GameObject ScrollRect;
    public Sprite[] Cardimages;//卡牌icon
    public Sprite[] Cardinfo;

    public GameObject[] Buttons;

    GameObject childGOBJ;
    public GameObject infoPrefab;
    static int CardCount = 0;
    public Image[] Card;//32張卡牌

    static int[] CardNumberArray = new int[32];//紀錄卡牌

    static int[] btnevents = { 0, 0, 0, 0, 0, 0, 0 };
    static int[] cardsums = { 0, 0, 0, 0, 0, 0 };
    static int tempcardid;

    static public Sprite[] staCardimages;//卡牌icon
    static public Sprite[] staCardinfo;
    static public Image[] staCard;//32張卡牌
    // static public GameObject[] staButtons;

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < 32; i++)
        {
            // Level.PlayerCardS[i] = 6;
            CardNumberArray[i] = Level.PlayerCardS[i] - 1;//0~5
            if (CardNumberArray[i] == -1)
            {
                CardNumberArray[i] = 6;
            }
            // Debug.Log("i=" + i + "" + CardNumberArray[i]);
        }
        creatScrollBar();
        CreatEditCard();
        staCard = Card;
        staCardimages = Cardimages;
        staCardinfo = Cardinfo;
        // staButtons = Buttons;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 32; i++)
        {
            // if (Level.PlayerCardS[i] == 6)
            if (CardNumberArray[i] == 6)
            {
                CardCount = i;
                break;
            }
        }
        for (int i = 0; i < counter; i++)
        {
            Buttons[i].GetComponentInChildren<Text>().text = "x" + cardsums[btnevents[i]];// Button[i]→cardsums[btnevents[i]]
        }

        if (Input.GetKey("w"))
        {
            // Debug.Log("test");
            for (int i = 0; i < 32; i++)
            {
                // if (Level.PlayerCardS[i] != 6)
                // {
                //     Debug.Log(i + "=" + Level.PlayerCardS[i]);
                // }
                if (CardNumberArray[i] != 6)
                {
                    // Debug.Log(i + "=" + CardNumberArray[i]);
                }
                else { break; }
            }
        }
    }
    void CreatEditCard()
    {
        for (int i = 0; i < 32; i++)
        {
            // Debug.Log(CardNumberArray[i] + 1);
            if (CardNumberArray[i] == 6)
            {
                Card[i].GetComponent<Image>().sprite = Cardimages[0];
            }
            else
            {
                Card[i].GetComponent<Image>().sprite = Cardimages[CardNumberArray[i] + 1];
            }

            if (CardNumberArray[i] != 6)
            {
                cardsums[CardNumberArray[i]]--;
            }
        }

    }

    int counter = 0;
    public void creatScrollBar()
    {
        Vector3 btnpos = Buttons[0].transform.position;

        for (int i = 0; i < 6; i++)
        {
            if (Level.PlayerCardID[i] != 0)
            {
                childGOBJ = Instantiate(infoPrefab);
                childGOBJ.transform.SetParent(Buttons[i].transform);
                // childGOBJ.transform.parent = Buttons[i].transform;
                childGOBJ.transform.position = Buttons[i].transform.position;
                childGOBJ.GetComponentInChildren<Image>().sprite = Cardinfo[Level.PlayerCardID[i] - 1];
                btnevents[i] = Level.PlayerCardID[i] - 1;//012345
                cardsums[btnevents[i]] = 12;//再加switch控制數量
                counter++;
            }
            else break;
        }
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(btnevents[i]);
        }

        for (int i = 0; i < counter; i++)
        {
            float ScrollRectY = counter * (233f) + ScrollRect.GetComponent<VerticalLayoutGroup>().spacing;
            ScrollRect.GetComponent<RectTransform>().sizeDelta = new Vector2(966, ScrollRectY);
            Buttons[i].transform.SetParent(ScrollRect.transform);
        }


    }


    #region 新增按鈕被點擊
    public void brn1Onclick()
    {
        if (cardsums[btnevents[0]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {

            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[0] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[0] + 1;
            CardNumberArray[CardCount] = btnevents[0];
            cardsums[btnevents[0]]--;
            // Buttons[0].GetComponentInChildren<Text>().text = "x" + cardsums[0];
            // Level.PlayerCardS[CardCount] = 1;
            CardCount++;
        }
    }
    public void brn2Onclick()
    {
        if (cardsums[btnevents[1]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {
            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[1] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[1] + 1;
            CardNumberArray[CardCount] = btnevents[1];
            // Card[CardCount].GetComponent<Image>().sprite = Cardimages[2];
            // Level.PlayerCardS[CardCount] = 2;
            // Debug.Log(CardCount);
            cardsums[btnevents[1]]--;
            // Buttons[1].GetComponentInChildren<Text>().text = "x" + cardsums[1];
            CardCount++;
        }

    }
    public void brn3Onclick()
    {
        if (cardsums[btnevents[2]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {
            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[2] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[2] + 1;
            CardNumberArray[CardCount] = btnevents[2];
            // Card[CardCount].GetComponent<Image>().sprite = Cardimages[3];
            // Level.PlayerCardS[CardCount] = 3;
            // Debug.Log(CardCount);
            cardsums[btnevents[2]]--;
            // Buttons[2].GetComponentInChildren<Text>().text = "x" + cardsums[2];
            CardCount++;
        }


    }
    public void brn4Onclick()
    {
        if (cardsums[btnevents[3]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {
            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[3] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[3] + 1;
            CardNumberArray[CardCount] = btnevents[3];
            // Level.PlayerCardS[CardCount] = 4;
            cardsums[btnevents[3]]--;
            // Buttons[3].GetComponentInChildren<Text>().text = "x" + cardsums[3];
            CardCount++;
        }


    }
    public void brn5Onclick()
    {
        if (cardsums[btnevents[4]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {
            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[4] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[4] + 1;
            CardNumberArray[CardCount] = btnevents[4];
            // Level.PlayerCardS[CardCount] = 5;
            cardsums[btnevents[4]]--;
            // Buttons[4].GetComponentInChildren<Text>().text = "x" + cardsums[4];
            CardCount++;
        }
    }
    public void brn6Onclick()
    {
        if (cardsums[btnevents[5]] > 0 && CardCount < 32 && CardNumberArray[CardCount] == 6)
        {
            Card[CardCount].GetComponent<Image>().sprite = Cardimages[btnevents[5] + 1];
            // Level.PlayerCardS[CardCount] = btnevents[5] + 1;
            CardNumberArray[CardCount] = btnevents[5];
            // Level.PlayerCardS[CardCount] = 4;
            cardsums[btnevents[5]]--;
            // Buttons[5].GetComponentInChildren<Text>().text = "x" + cardsums[5];
            CardCount++;
        }
    }
    #endregion


    static public void CardDelet(int cardspritename, int cardidnumber)
    {
        cardspritename -= 1;
        // Debug.Log("卡牌編號:" + cardspritename + " 卡牌位置編號:" + cardidnumber);
        staCard[cardidnumber].GetComponent<Image>().sprite = staCardimages[0];
        // Level.PlayerCardS[cardidnumber] = 6;//6為空
        CardNumberArray[cardidnumber] = 6;
        cardsums[cardspritename - 1]++;//cardspritename1~6 array0~5
        // Debug.Log(cardsums[cardspritename]);
        // cardsums[cardspritename]++;

    }
    public GameObject ErrorUI;
    public void EdtitToMap()
    {
        for (int i = 0; i < 32; i++)
        {
            if (CardNumberArray[i] == 6)
            {
                Level.PlayerCardS[i] = 0;//0為無卡牌
                ErrorUI.SetActive(true);
                break;
            }
            else
            {
                Level.PlayerCardS[i] = CardNumberArray[i] + 1;
                if (i == 31)
                {
                    Time.timeScale = 1;//功能才正常
                    Application.LoadLevel("Map");

                }
            }
        }

    }
    public void BtnErrorUIClose()
    {
        ErrorUI.SetActive(false);
    }
}