using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    static public bool isDrag = true;
    public GameObject HurtFX;
    public Animator Ani;
    Animator EnemyAni;
    AnimatorStateInfo aniInfo;

    public GameObject shield;

    public GameObject shieldimg;

    static public bool isshield = false;

    GameObject Enemyobject;//要用程式碼找GameObject
    //public float PlayerHP;
    float SumPlayerHP;

    public float PlayerAtk;
    public static float PlayerMP = 0;

    public float PlayerDfs;

    [Header("玩家血條UI")]
    public Image PlayerHPUI;

    [Header("玩家血量文字")]
    public Text PlayerHPText;

    [Header("玩家護盾文字")]
    public Text PlayerDfsText;

    public static bool isbuff = false;

    public float buff = 1f;

    int counter = 1;//每三回合回一點MP

    bool reducehp = false;

    bool debuff = false;

    float timer;

    bool isAddMP;
    bool onshield;

    static public bool isboss = false;

    void Start()
    {
        HurtFX.SetActive(false);
        Enemyobject = GameObject.FindWithTag("Enemy");
        if (Enemyobject.name == "boss")
        {
            isboss = true;
        }
        else
        {
            isboss = false;
        }
        EnemyAni = Enemyobject.GetComponent<Animator>();
        PlayerMP = 3;
        SumPlayerHP = 30;
        PlayerHPText.text = Level.PlayerHP + "/" + SumPlayerHP;
        PlayerDfsText.text = "+" + PlayerDfs;
        PlayerHPUI.fillAmount = Level.PlayerHP / SumPlayerHP;
    }

    void Update()
    {
        aniInfo = Ani.GetCurrentAnimatorStateInfo(0);

        PlayerHPText.text = Level.PlayerHP + "/" + SumPlayerHP;
        PlayerDfsText.text = "+" + PlayerDfs;
        #region 遞減血條
        if (reducehp)
        {
            PlayerHPUI.fillAmount = (PlayerHPUI.fillAmount -= 0.3f * Time.deltaTime);
            if (PlayerHPUI.fillAmount <= Level.PlayerHP / SumPlayerHP)
            {
                if (Level.PlayerHP == 0)
                {
                    Ani.SetTrigger("Dead");
                }
                reducehp = false;
            }

        }
        #endregion

        if (PlayerDfs > 0)
        {
            isshield = true;
            timer = Time.deltaTime;//一秒
            shield.SetActive(true);
            shieldimg.SetActive(true);
            if (shield.GetComponent<CanvasGroup>().alpha < 1)
            {
                shieldimg.GetComponent<CanvasGroup>().alpha += timer;
                shield.GetComponent<CanvasGroup>().alpha += timer;
            }

        }
        else if (PlayerDfs <= 0)
        {
            isshield = false;
            timer = Time.deltaTime * 2;//半秒
            if (shield.GetComponent<CanvasGroup>().alpha != 0)
            {
                shieldimg.GetComponent<CanvasGroup>().alpha -= timer;
                shield.GetComponent<CanvasGroup>().alpha -= timer;
            }
            // shieldimg.GetComponent<CanvasGroup>().alpha = 0;
            // shield.GetComponent<CanvasGroup>().alpha = 0;
            if (shield.GetComponent<CanvasGroup>().alpha == 0)
            {
                shield.SetActive(false);
                shieldimg.SetActive(false);
            }
        }

    }
    #region 取得卡牌參數
    public void GetParameter(float atk, float dfs, float CardConsume, float AddMp, bool Cardbuff, bool Carddebuff)
    {
        if (counter < 3)
        {
            counter++;
        }
        else if (counter == 3 && PlayerMP < 3)
        {
            PlayerMP++;
            counter = 0;
        }
        // Debug.Log("卡牌消耗:" + CardConsume + " 玩家MP:" + PlayerMP);
        if (CardConsume <= PlayerMP)//MP足夠可以使用卡牌
        {
            // Debug.Log("GetParameter");
            // Debug.Log("ATK:" + atk + "  dfs:" + dfs + "  cardconsume:" + CardConsume + "  AddMp:" + AddMp + "  cardbuff:" + Cardbuff + "  carddebuff:" + Carddebuff);
            if (dfs != 0) { onshield = true; Ani.SetBool("Shield", true); }
            else onshield = false;
            PlayerMP -= CardConsume;//扣掉使用的MP


            if (AddMp != 0) isAddMP = true;
            else isAddMP = false;


            PlayerMP += AddMp;
            if (PlayerMP > 3)
            {
                PlayerMP = 3;
            }
            PlayerAtk = atk * buff;
            PlayerDfs += dfs * buff;
            if (buff == 1.5f) { buff = 1f; }
            if (Cardbuff)
            {
                buff = 1.5f;
                isbuff = true;
            }
            else
            {
                isbuff = false;
            }
            if (Carddebuff)
            {
                if (isboss)
                {
                    Boss.EnemyCD++;
                }
                else
                {
                    Enemy.EnemyCD++;
                }

            }
            debuff = Carddebuff;

        }
        else
        {
            PlayerAtk = 0;
            PlayerDfs += 0;
            if (buff == 1.5f) { buff = 1f; }
            isbuff = false;
        }
        //Debug.Log("Player魔力值:" + PlayerMP);

    }
    #endregion

    #region 玩家行動
    public void PlayerAction()
    {
        // Debug.Log("PlayerAction");

        if (PlayerAtk > 0 && PlayerAtk < 12)
        {
            // Debug.Log(isboss);
            // Debug.Log("Punch");
            EnemyAni.SetBool("Hurt", true);
            Ani.SetBool("Punch", true);
            if (isboss)
            {
                Enemyobject.gameObject.GetComponent<Boss>().Hurt(PlayerAtk);
            }
            else
                Enemyobject.gameObject.GetComponent<Enemy>().Hurt(PlayerAtk);
        }
        else if (PlayerAtk >= 12f)
        {
            // Debug.Log("Dragon");
            EnemyAni.SetBool("Hurt", true);
            Ani.SetBool("Dragon", true);
            if (isboss)
            {
                Enemyobject.gameObject.GetComponent<Boss>().Hurt(PlayerAtk);
            }
            else
                Enemyobject.gameObject.GetComponent<Enemy>().Hurt(PlayerAtk);
        }
        else if (debuff)
        {
            // Debug.Log("Debuff");
            Ani.SetBool("Debuff", true);
        }
        else if (isbuff || isAddMP)
        {
            // if (isbuff)
            //     Debug.Log("Energy");
            // else Debug.Log("AddMP");


            Ani.SetBool("Energy", true);
        }
    }
    #endregion

    public void setaniidle()
    {
        Ani.SetBool("Punch", false);
        Ani.SetBool("Dragon", false);
        Ani.SetBool("Shield", false);
        Ani.SetBool("Debuff", false);
        Ani.SetBool("QQ", false);
        Ani.SetBool("QQ_shield", false);
        Ani.SetBool("Energy", false);
    }


    #region 玩家受傷
    public void Hurt(float Enemyatk)
    {
        float tempEnemyatk = Enemyatk;
        EnemyAni.SetBool("Atk", true);
        //從護盾開始消耗
        Enemyatk -= PlayerDfs;
        //Debug.Log("敵人攻擊值=" + Enemyatk);
        if (Enemyatk >= 0)
        {
            //扣除玩家血量
            Level.PlayerHP -= Enemyatk;
            reducehp = true;
            // PlayerHPUI.fillAmount = (PlayerHP / SumPlayerHP);
            PlayerDfs = 0;
            if (Level.PlayerHP <= 0)
            {
                Level.PlayerHP = 0;
                StartCoroutine(PlayerDead());
            }
        }
        else
        {
            PlayerDfs -= tempEnemyatk;
        }
    }
    #endregion
    #region 玩家死亡
    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject.Find("EventSystem").gameObject.GetComponent<GameManager>().isWin = false;
        GameObject.Find("EventSystem").gameObject.GetComponent<GameManager>().GameOver();
    }
    #endregion

    public void EnemyAtkAction()
    {
        if (isboss)
        {
            if (Boss.EnemyCD == 0 && Boss.staTotalHP != 0)
            {

                {
                    float tempenemyatk = Boss.Enemyatk;
                    Enemyobject.GetComponent<Boss>().EnemyAttack(tempenemyatk);
                    Boss.EnemyCD = UnityEngine.Random.Range(2, 4);
                    // Enemy.EnemyCD = 1;
                }
            }
        }
        else
        {
            if (Enemy.EnemyCD == 0 && Enemy.staTotalHP != 0)
            {

                {
                    float tempenemyatk = Enemy.Enemyatk;
                    Enemyobject.GetComponent<Enemy>().EnemyAttack(tempenemyatk);
                    Enemy.EnemyCD = UnityEngine.Random.Range(2, 4);
                    // Enemy.EnemyCD = 1;
                }
            }
        }
    }

    public void HurtFXStart()
    {
        HurtFX.SetActive(true);
    }

    public void setDragf()
    {

        isDrag = false;
    }
    public void setDragt()
    {
        isDrag = true;
    }
    #region Sounds
    public GameObject DebuffSound = null;
    public GameObject EnegrySound = null;
    public GameObject PunchSound = null;
    public GameObject DragonSound = null;
    public GameObject ShieldSound = null;
    public GameObject HurtSound = null;
    GameObject Sound;
    public void DebuffSoundPlay()
    {
        DebuffSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (DebuffSound != null)
            Sound = Instantiate(DebuffSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 1);
    }
    public void EnegrySoundPlay()
    {
        EnegrySound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (EnegrySound != null)
            Sound = Instantiate(EnegrySound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 1);
    }
    public void PunchSoundPlay()
    {
        PunchSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (PunchSound != null)
            Sound = Instantiate(PunchSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 1);
    }
    public void DragonSoundPlay()
    {
        DragonSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (DragonSound != null)
            Sound = Instantiate(DragonSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 2);
    }
    public void ShieldSoundPlay()
    {
        ShieldSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (ShieldSound != null)
            Sound = Instantiate(ShieldSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 2);
    }
    public void HurtSoundSoundPlay()
    {
        HurtSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (HurtSound != null)
            Sound = Instantiate(HurtSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 2);
    }
    #endregion
}
