using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialDrag : MonoBehaviour
{
    Animator PlayerAni;
    Animator EnemyAni;

    AnimatorStateInfo PlayerState;
    AnimatorStateInfo EnemyState;
    CircleCollider2D PlayerCollider;
    CircleCollider2D ChangeCardCollider;
    GameObject hoverimg;
    GameObject Player;
    GameObject Card;

    GameObject MPBarObject;

    GameObject EnemyObject;

    GameObject childobject;
    GameObject Playerchild;
    public bool trigger;
    private Vector3 newPosition;
    private Vector3 noMove;

    private Vector3 oldpos;

    private Collider2D temp;

    Material TriggerMaterial;
    Material NormalMaterial;
    int temprandom;

    int CurrentCardID;

    int hoverid;


    public int TargetCardID;

    int[] walk = { 3, 4, 7, -3, -4, -7 };

    #region  卡牌參數暫存
    float tempatk;
    float tempdfs;
    float tempCardConsume;
    float tempAddMp;
    bool tempCardbuff;

    bool tempCarddebuff;
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(EnemyAtkAction());
        hoverimg = GameObject.Find("hoverimg");
        TriggerMaterial = Resources.Load("OnTrigger", typeof(Material)) as Material;
        NormalMaterial = this.GetComponent<SpriteRenderer>().material;
        MPBarObject = GameObject.Find("MPBar");
        Player = GameObject.Find("Player");
        PlayerAni = Player.GetComponent<Animator>();

        Card = GameObject.Find("13");
        EnemyObject = GameObject.FindWithTag("Enemy");
        EnemyAni = EnemyObject.GetComponent<Animator>();

        oldpos = gameObject.transform.position;
        noMove = gameObject.transform.position;
        CurrentCardID = Convert.ToInt32(gameObject.tag);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerState = PlayerAni.GetCurrentAnimatorStateInfo(0);
        EnemyState = EnemyAni.GetCurrentAnimatorStateInfo(0);

    }
    void OnMouseUp()
    {
        gameObject.transform.localScale = new Vector3(100.6273f, 100.6273f, 0f);

        if (trigger == true)
        {
            childobject.SetActive(false);
            Playerchild.SetActive(true);
            PlayerCollider = this.gameObject.GetComponent<CircleCollider2D>();
            ChangeCardCollider = temp.gameObject.GetComponent<CircleCollider2D>();
            PlayerCollider.radius = .21f;
            ChangeCardCollider.radius = .69f;
            Enemy.EnemyCD--;
            newPosition = newPosition + new Vector3(0, 0, -0.01f);
            //gameObject.transform.position = newPosition;
            noMove = gameObject.transform.position;
            this.gameObject.GetComponent<SpriteRenderer>().material = NormalMaterial;
            temp.gameObject.GetComponent<SpriteRenderer>().material = NormalMaterial;

            #region 傳遞卡牌參數給Player腳本
            tempatk = temp.gameObject.GetComponent<CardParameter>().atk;
            tempdfs = temp.gameObject.GetComponent<CardParameter>().dfs;
            tempCardConsume = temp.gameObject.GetComponent<CardParameter>().CardConsume;
            tempAddMp = temp.gameObject.GetComponent<CardParameter>().AddMP;
            tempCardbuff = temp.gameObject.GetComponent<CardParameter>().Buff;
            tempCarddebuff = temp.gameObject.GetComponent<CardParameter>().DeBuff;
            Player.GetComponent<Player>().GetParameter(tempatk, tempdfs, tempCardConsume, tempAddMp, tempCardbuff, tempCarddebuff);
            #endregion

            #region 換置Sprite及Drag腳本
            temp.gameObject.GetComponent<SpriteRenderer>().sprite = Card.GetComponent<Tutorial>().CardsImg[0];
            Destroy(temp.GetComponent<CardParameter>());
            temp.gameObject.AddComponent<TutorialDrag>();
            this.GetComponent<Animation>().Play();

            temprandom = 1;
            this.GetComponent<SpriteRenderer>().sprite = Card.GetComponent<Tutorial>().CardsImg[temprandom];
            this.gameObject.AddComponent<CardParameter>();
            this.GetComponent<CardParameter>().DefineParameter(temprandom);
            this.transform.position = oldpos;
            this.gameObject.AddComponent<LongPress>();
            Destroy(this.GetComponent<TutorialDrag>());
            #endregion

            Player.gameObject.GetComponent<Player>().PlayerAction();
            MPBarObject.gameObject.GetComponent<MPUIContorl>().StartCoroutine(MPBarObject.gameObject.GetComponent<MPUIContorl>().UIControl());
            
        }
        else
        {
            gameObject.transform.position = noMove;
        }
    }
    void OnMouseDrag()
    {
        gameObject.transform.localScale = new Vector3(115f, 115f, 0f);
        Vector3 mos;
        mos = Input.mousePosition;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mos.x, mos.y, 9.99f));
        // Debug.Log(mos.x);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "longpress" && PlayerState.IsName("Player_idle") && EnemyState.IsTag("idle") && Enemy.EnemyCD != 0)
        {
            TargetCardID = Convert.ToInt32(other.tag);

            for (int i = 0; i < 6; i++)
            {
                if (CurrentCardID + walk[i] == TargetCardID)
                {
                    childobject = other.transform.GetChild(0).gameObject;
                    Playerchild = this.transform.GetChild(0).gameObject;
                    temp = other;
                    trigger = true;
                    hoverid = Convert.ToInt32(other.gameObject.GetComponent<SpriteRenderer>().sprite.name);
                    other.gameObject.GetComponent<SpriteRenderer>().material = TriggerMaterial;
                    hoverimg.GetComponent<HoverInfo>().showhover(other.transform.position.x, other.transform.position.y, hoverid);
                    break;
                }
            }
        }
        //newPosition = other.gameObject.transform.position;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "longpress")
        {
            hoverimg.GetComponent<HoverInfo>().hidehover();
            other.gameObject.GetComponent<SpriteRenderer>().material = NormalMaterial;
            trigger = false;
        }
    }

    // IEnumerator EnemyAtkAction()
    // void EnemyAtkAction()
    // {
    //     // Debug.Log("Atk0");
    //     // yield return new WaitForSeconds(0.1f);
    //     // if (Enemy.EnemyCD == 0 && PlayerState.IsName("Player_idle") && EnemyState.IsTag("idle"))
    //     {
    //         // float tempenemyatk = Enemy.Enemyatk;
    //         // EnemyObject.GetComponent<Enemy>().EnemyAttack(tempenemyatk);
    //         // Enemy.EnemyCD = UnityEngine.Random.Range(2, 4);
    //         // Debug.Log("AtkAni");

    //     }
    //     if (Enemy.EnemyCD == 0)
    //     {
    //         // while (PlayerState.IsName("Player_idle") && EnemyState.IsTag("idle"))
    //         {
    //             // Debug.Log("AtkAni");
    //             // yield return new WaitForSeconds(2.8f);
    //             Debug.Log("AtkAni2");
    //             float tempenemyatk = Enemy.Enemyatk;
    //             EnemyObject.GetComponent<Enemy>().EnemyAttack(tempenemyatk);
    //             Enemy.EnemyCD = UnityEngine.Random.Range(2, 4);
    //         }
    //     }

    // }
}
