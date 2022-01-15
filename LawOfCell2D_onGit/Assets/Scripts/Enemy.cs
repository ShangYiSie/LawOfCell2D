using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public Animator EnemyAni;

    public GameObject Playerobj;
    Animator PlayerAni;
    [Header("設定怪物總血量")]
    public float TotalHP;
    static public float staTotalHP;
    float SumHP;

    [Header("敵人血條UI")]
    public Image EnemyHPUI;

    [Header("敵人血量文字")]
    public Text EnemyHPText;

    [Header("敵人CD時間")]
    public static int EnemyCD;

    [Header("敵人CD文字")]
    public Text EnemyCDText;

    [Header("敵人Atk值")]
    public static float Enemyatk;

    [Header("敵人Atk文字")]
    public Text EnemyatkText;
    bool reducehp = false;
    // Start is called before the first frame update

    void Start()
    {

        PlayerAni = Playerobj.GetComponent<Animator>();
        EnemyAni = gameObject.GetComponent<Animator>();
        EnemyCD = Random.Range(1, 4);
        // EnemyCD = 1;
        EnemyCDText.text = "CD " + EnemyCD;
        EnemyHPText.text = TotalHP + "/" + SumHP;
        Enemyatk = Random.Range(5, 11);
        // Enemyatk = 1;
        EnemyatkText.text = "" + Enemyatk;
        SumHP = TotalHP;
    }

    // Update is called once per frame
    void Update()
    {
        staTotalHP = TotalHP;
        EnemyCDText.text = "CD " + EnemyCD;
        EnemyHPText.text = TotalHP + "/" + SumHP;
        EnemyatkText.text = "" + Enemyatk;
        #region 遞減血條
        if (reducehp)
        {
            EnemyHPUI.fillAmount = (EnemyHPUI.fillAmount -= 0.3f * Time.deltaTime);
            if (EnemyHPUI.fillAmount <= TotalHP / SumHP)
            {
                if (TotalHP == 0)
                {
                    EnemyAni.SetTrigger("Dead");
                }
                reducehp = false;
            }

        }
        #endregion

    }

    #region 怪物受傷
    public void Hurt(float HurtHP)
    {
        //扣除怪物總血量
        TotalHP -= HurtHP;
        reducehp = true;
        if (TotalHP <= 0)
        {
            TotalHP = 0;

            StartCoroutine(EnemyDead());
        }
        // EnemyHPUI.fillAmount = TotalHP / SumHP;
        //Debug.Log("HP=" + TotalHP);
    }
    #endregion
    #region 怪物死亡
    IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(4f);

        if (Level.isTutorial)
        {
            GameObject.Find("EventSystem").gameObject.GetComponent<TutorialControl>().clickNext();
        }
        else
        {
            GameObject.Find("EventSystem").gameObject.GetComponent<GameManager>().isWin = true;
            GameObject.Find("EventSystem").gameObject.GetComponent<GameManager>().GameOver();
        }
    }
    #endregion

    #region 怪物攻擊
    public void EnemyAttack(float Enemyatk)
    {
        Playerobj.gameObject.GetComponent<Player>().Hurt(Enemyatk);
    }
    #endregion

    public void AniEnd()
    {
        EnemyAni.SetBool("Hurt", false);
        EnemyAni.SetBool("Atk", false);
    }

    public void PlayerHurt()
    {
        if (Player.isshield)
        {
            // Debug.Log("QQShield");
            PlayerAni.SetBool("QQ_shield", true);
        }
        else
        {
            // Debug.Log("QQ");
            PlayerAni.SetBool("QQ", true);
        }

    }
    #region Sounds
    public GameObject AtkSound = null;
    // public GameObject EnegrySound = null;
    // public GameObject PunchSound = null;
    // public GameObject DragonSound = null;
    GameObject Sound;
    public void AtkSoundPlay()
    {
        AtkSound.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        if (AtkSound != null)
            Sound = Instantiate(AtkSound, Vector2.zero, Quaternion.identity);
        Destroy(Sound, 1);
    }
    // public void EnegrySoundPlay()
    // {
    //     if (EnegrySound != null)
    //         Sound = Instantiate(EnegrySound, Vector2.zero, Quaternion.identity);
    //     Destroy(Sound, 1);
    // }
    // public void PunchSoundPlay()
    // {
    //     if (PunchSound != null)
    //         Sound = Instantiate(PunchSound, Vector2.zero, Quaternion.identity);
    //     Destroy(Sound, 1);
    // }
    // public void DragonSoundPlay()
    // {
    //     if (DragonSound != null)
    //         Sound = Instantiate(DragonSound, Vector2.zero, Quaternion.identity);
    //     Destroy(Sound, 2);
    // }
    #endregion


}
