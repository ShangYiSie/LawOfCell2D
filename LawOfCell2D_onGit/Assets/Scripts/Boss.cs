using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject BossHurtFX;
    public GameObject BossHurtFX2;
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

    bool changetype = true;

    bool dead = false;

    bool addhp = false;
    // Start is called before the first frame update

    void Start()
    {
        BossHurtFX.SetActive(false);
        BossHurtFX2.SetActive(false);
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
                if (dead)
                {
                    EnemyAni.SetTrigger("dead");
                }
                reducehp = false;
            }

        }
        #endregion


        #region 遞增血條
        if (addhp)
        {
            EnemyHPUI.fillAmount = (EnemyHPUI.fillAmount += 0.6f * Time.deltaTime);
            if (EnemyHPUI.fillAmount == TotalHP / SumHP)
            {
                addhp = false;
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
            // Debug.Log("BossHurt");
            if (changetype)
            {
                EnemyCD = Random.Range(2, 4);
                Enemyatk = 0;
                TotalHP = 0;
                changetype = false;
                EnemyAni.SetTrigger("ToType2");

            }
            else
            {

                TotalHP = 0;
                dead = true;
                StartCoroutine(EnemyDead());
            }
        }
        // EnemyHPUI.fillAmount = TotalHP / SumHP;
        //Debug.Log("HP=" + TotalHP);
    }
    #endregion
    #region 怪物死亡
    IEnumerator EnemyDead()
    {

        yield return new WaitForSeconds(5f);

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
            Debug.Log("QQShield");
            PlayerAni.SetBool("QQ_shield", true);
        }
        else
        {
            Debug.Log("QQ");
            PlayerAni.SetBool("QQ", true);
        }

    }

    public void BossHurtFXStart()
    {
        BossHurtFX.SetActive(true);
    }

    public void BossHurtFX2Start()
    {
        BossHurtFX2.SetActive(true);
    }

    public void ToType2end()
    {
        TotalHP = 15;
        SumHP = TotalHP;
        addhp = true;
        EnemyAni.SetTrigger("ToType2End");
        Enemyatk = Random.Range(5, 11);
    }
}
