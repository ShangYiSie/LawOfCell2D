using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{

    public GameObject Detail;
    public GameObject Tutorial;
    public GameObject TutorBack;
    public GameObject ClickToNext;
    
    public GameObject DNA;
    public GameObject Setting;
    public GameObject EnemyBlood;
    public GameObject CDandAttack;
    public GameObject PlayerBlood;
    public GameObject Energy;
    public GameObject IntroText;
    public GameObject IntroContent;
    public GameObject LongPressObj;
    public Image longPressGes;
    public Image PunchSprite;
    public GameObject MoveBtn;
    public Image MoveBtnGes;
    public GameObject MainCharObj;
    public Image MainChar;

    public GameObject CharacterCard;
    public GameObject FunctionCard;

    public Sprite longPressSprite;
    public Sprite moveCharacterSprite;
    public Sprite goalSprite;
    public Sprite click1;
    public Sprite click2;
    bool tutorLongPress;
    bool tutorMove;

    int step;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //Detail.SetActive(false);
        CharacterCard.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        FunctionCard.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Tutorial.SetActive(true);
        openDNA();
        step = 0;
        Level.isTutorial = true;
    }


    public void clickNext()
    {
        step++;
        switch (step)
        {
            case 1:
                openSetting();
                break;
            case 2:
                openEnemyBlood();
                break;
            case 3:
                openCDandAttack();
                break;
            case 4:
                openPlayerBlood();
                break;
            case 5:
                openEnergy();
                break;
            case 6:
                openIntroText();
                break;
            case 7:
                longPress();
                break;
            case 8:
                beforeMove();
                break;
            case 9:
                TryMove();
                break;
            case 10:
                StartToGame();
                break;
            case 11:
                GoToGame();
                break;
        }
    }

    void openDNA()
    {
        DNA.SetActive(true);
    }
    void openSetting()
    {
        DNA.SetActive(false);
        Setting.SetActive(true);
    }
    void openEnemyBlood()
    {
        Setting.SetActive(false);
        EnemyBlood.SetActive(true);
    }
    void openCDandAttack()
    {
        EnemyBlood.SetActive(false);
        CDandAttack.SetActive(true);
    }
    void openPlayerBlood()
    {
        CDandAttack.SetActive(false);
        PlayerBlood.SetActive(true);
    }
    void openEnergy()
    {
        PlayerBlood.SetActive(false);
        Energy.SetActive(true);
    }
    void openIntroText()
    {
        //開啟longPress 之前的說明
        Energy.SetActive(false);
        IntroText.SetActive(true);
        LongPressObj.SetActive(true);

        tutorLongPress = true;
        StartCoroutine(longPressAnim());
    }
    void longPress()
    {
        //關閉longPress的說明 並讓玩家實際操作
        tutorLongPress = false;
        Detail.SetActive(true);

        Tutorial.SetActive(false);
        IntroText.SetActive(false);
        LongPressObj.SetActive(false);

        IntroContent.gameObject.GetComponent<Image>().sprite = moveCharacterSprite;
        IntroContent.gameObject.GetComponent<Image>().SetNativeSize();

    }
    void beforeMove()
    {
        Detail.SetActive(false);

        Tutorial.SetActive(true);
        IntroText.SetActive(true);
        MoveBtn.SetActive(true);

        tutorMove = true;
        StartCoroutine(MoveBtnAnim());
    }
    void TryMove()
    {
        //關閉Move的說明 並讓玩家實際操作
        tutorMove = false;
        Detail.SetActive(true);

        Tutorial.SetActive(false);
        IntroText.SetActive(false);
        MoveBtn.SetActive(false);

        IntroContent.gameObject.GetComponent<Image>().sprite = goalSprite;
        IntroContent.gameObject.GetComponent<Image>().SetNativeSize();

        CharacterCard.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        FunctionCard.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    void StartToGame()
    {
        //讓遊戲時間暫停
        Time.timeScale = 0;
        //顯示出發文字
        Detail.SetActive(false);

        Tutorial.SetActive(true);
        IntroText.SetActive(true);
        
    }

    void GoToGame()
    {
        //讓遊戲時間恢復
        Time.timeScale = 1;
        Level.isTutorial = false;
        //轉跳去Game Scene 或 Map Scene
        if (Level.firstTutor)
        {
            Level.firstTutor = false;
            Application.LoadLevel("Game");
        }
        else
        {
            Application.LoadLevel("Map");
        }
    }

    //卡牌長按示範動畫
    IEnumerator longPressAnim()
    {
        float timer = 0;

        while (tutorLongPress)
        {
            while (longPressGes.transform.localScale.x >= 0.9f)
            {
                timer += Time.deltaTime;
                longPressGes.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(0.001f);
            longPressGes.sprite = click2;

            yield return new WaitForSeconds(0.3f);
            longPressGes.sprite = click1;
            timer = 0;
            while (longPressGes.transform.localScale.x <= 1)
            {
                timer += Time.deltaTime;
                longPressGes.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(0.001f);
            
        }
        
    }

    //卡牌移動示範動畫
    IEnumerator MoveBtnAnim()
    {
        float timer = 0;
        //移動速度
        int moveSpeed = 100;
        Vector3 tmp = MoveBtn.transform.position;

        while (tutorMove)
        {
            //歸零
            MoveBtnGes.transform.localScale = new Vector3(1, 1, 1);
            MainChar.transform.localScale = new Vector3(1, 1, 1);
            MainCharObj.SetActive(true);

            //開始
            while (MoveBtnGes.transform.localScale.x >= 0.9f)
            {
                timer += Time.deltaTime;
                MoveBtnGes.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(0.001f);

            timer = 0;
            MoveBtnGes.sprite = click2;

            MainChar.transform.localScale = new Vector3(1.1f, 1.1f, 1);

            
            //Move
            while (MoveBtn.transform.position.x != PunchSprite.transform.position.x)
            {
                MoveBtn.transform.position = Vector3.MoveTowards(MoveBtn.transform.position, PunchSprite.transform.position, moveSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.001f);
            }

            yield return new WaitForSeconds(1f);

            MoveBtnGes.sprite = click1;
            while (MoveBtnGes.transform.localScale.x <= 1f)
            {
                timer += Time.deltaTime;
                MoveBtnGes.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.1f;
                yield return null;
            }
            //Move Back
            MainCharObj.SetActive(false);
            while (MoveBtn.transform.position.x != tmp.x)
            {
                MoveBtn.transform.position = Vector3.MoveTowards(MoveBtn.transform.position, tmp, moveSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(0.3f);

        }
    }

}
