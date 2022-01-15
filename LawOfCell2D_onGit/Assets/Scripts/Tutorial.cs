using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    #region Creat1
    // //Vector2[] pos = new Vector2[];
    // public GameObject[] Instantiate_Position; //物件的生成點。
    // //public GameObject superGameObject;//要被放置在哪個物件底下

    // //public GameObject Box; //要生成的物件。
    // GameObject parent;
    // Vector3 _tmp;

    #endregion
    public GameObject longpresspos;
    GameObject childGOBJ;
    CircleCollider2D PlayerCollider;
    public Sprite[] CardsImg;

    int cardid;
    [Header("要是哪張卡片")]
    public int CardNum;


    void Start() //一開始就執行生成物件。
    {
        Creat2();
    }

    void Creat2()
    {
        cardid = CardNum;

        if (cardid == 0)
        {
            PlayerCollider = gameObject.GetComponent<CircleCollider2D>();
            PlayerCollider.radius = .69f;
            gameObject.AddComponent<TutorialDrag>();//新增Drag腳本
            childGOBJ = Instantiate(longpresspos);
            childGOBJ.transform.position = transform.position;
            childGOBJ.transform.parent = transform;
            childGOBJ.SetActive(false);
        }
        else
        {
            gameObject.AddComponent<CardParameter>();
            gameObject.GetComponent<CardParameter>().DefineParameter(cardid);//給卡牌參數
            childGOBJ = Instantiate(longpresspos);
            childGOBJ.transform.position = transform.position;
            childGOBJ.transform.parent = transform;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = CardsImg[cardid];

    }

}
