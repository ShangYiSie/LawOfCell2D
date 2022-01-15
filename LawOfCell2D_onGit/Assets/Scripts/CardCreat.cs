using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreat : MonoBehaviour
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

    public static int CardSum;
    int temp;
    bool PlayerCreat = true;//紀錄是否還能產生主角卡
    int[] cardid = new int[33];//第一張為主角卡


    void Start() //一開始就執行生成物件。

    {
        // CardSum = CardsImg.Length;
        CardSum = Level.PlayerCardS.Length;
        Creat2();

    }


    #region Creat1
    // void Creat1()
    // {

    //     for (int i = 0; i < 22; i++)
    //     {
    //         GameObject obj = (GameObject)Resources.Load("Image");
    //         obj = Instantiate(obj, Instantiate_Position[i].transform.position, Instantiate_Position[i].transform.rotation);
    //         _tmp = obj.transform.position;
    //         _tmp.z = -1f;
    //         obj.transform.position = _tmp;
    //         parent = GameObject.Find((i + 1).ToString());//寻找父物体
    //         obj.transform.SetParent(parent.transform);

    //         //Debug.Log("i=" + i + Instantiate_Position[i].transform.position);
    //         //Debug.Log("i=" + i + Instantiate_Position[i].transform.position + Instantiate_Position[i].transform.rotation);
    //         //obj.transform.parent = parent.transform;//指定父物体
    //         //Box.transform.parent = superGameObject.transform;
    //     }

    // }
    #endregion

    void Creat2()
    {
        cardid[0] = 0;//主角卡

        // for (int i = 1; i < 22; i++)
        // {

        //     // if (PlayerCreat)//如果主角卡尚未生成
        //     // {
        //     //     temp = Random.Range(0, CardsImg.Length);
        //     // }
        //     // else
        //     // {
        //     //     temp = Random.Range(1, CardsImg.Length);
        //     // }
        //     // if (temp == 0 && PlayerCreat)//第一次生成腳色卡
        //     // {
        //     //     cardid[i] = temp;
        //     //     PlayerCreat = !PlayerCreat;

        //     // }
        //     // else if (temp != 0)
        //     // {
        //     //     cardid[i] = temp;
        //     // }

        //     cardid[i] = Random.Range(1, CardSum);

        // }

        #region 生成卡牌
        // //生成卡片的時候不再用亂數 固定每張卡牌的數量
        // for (int i = 1; i < 9; i++)
        // {
        //     //生成8張蛋白質護盾(+4dfs)
        //     cardid[i] = 1;
        //     //生成8張細胞巨拳(+6atk)
        //     cardid[i + 8] = 2;
        // }
        // for (int i = 17; i < 21; i++)
        // {
        //     //生成4張能量激活(+2AddMP)
        //     cardid[i] = 3;
        //     //生成4張細胞龍(+12atk,+1CardConsume)
        //     cardid[i + 4] = 6;
        // }
        // for (int i = 25; i < 28; i++)
        // {
        //     //生成3張強化蛋白(+1CardConsume,Buff=true)
        //     cardid[i] = 4;
        //     //生成3張代謝干擾(+1CardConsume,DeBuff=true)
        //     cardid[i + 3] = 5;
        // }
        #endregion
        #region 載入卡牌

        for (int i = 1; i < 33; i++)
        {
            // Debug.Log(i + "=" + Level.PlayerCardS[i - 1]);
            cardid[i] = Level.PlayerCardS[i - 1];
        }

        #endregion

        //洗牌
        cardid = GetRandomArray(cardid);
        // for (int i = 0; i < 22; i++)
        // {
        //     Debug.Log(i + "=" + cardid[i]);
        // }
        int j = 0;
        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
        {

            if (child.transform == transform)
            {
                continue;
            }

            if (cardid[j] == 0)
            {
                PlayerCollider = child.gameObject.GetComponent<CircleCollider2D>();
                PlayerCollider.radius = .69f;
                child.gameObject.AddComponent<Drag>();//新增Drag腳本
                childGOBJ = Instantiate(longpresspos);
                childGOBJ.transform.position = child.transform.position;
                childGOBJ.transform.parent = child.transform;
                childGOBJ.SetActive(false);
            }
            else
            {
                child.gameObject.AddComponent<LongPress>();
                child.gameObject.AddComponent<CardParameter>();
                child.gameObject.GetComponent<CardParameter>().DefineParameter(cardid[j]);//給卡牌參數
                childGOBJ = Instantiate(longpresspos);
                childGOBJ.transform.position = child.transform.position;
                childGOBJ.transform.parent = child.transform;
            }

            child.sprite = CardsImg[cardid[j]];
            j++;

            /*
            if (PlayerCreat)//如果主角卡尚未生成
            {
                temp = Random.Range(0, 3);
            }
            else
            {
                temp = Random.Range(1, 3);
            }
            if (temp == 0 && PlayerCreat)//第一次生成腳色卡
            {
                PlayerCreat = !PlayerCreat;
                child.sprite = CardsImg[temp];
                child.gameObject.AddComponent<Drag>();//新增Drag腳本
            }
            else if (temp != 0)
            {
                child.sprite = CardsImg[temp];
            }
            */

        }
    }

    int[] GetRandomArray(int[] num)// 打亂陣列
    {
        //從1開始洗 先把後面的卡片洗亂
        for (int i = 1; i < num.Length; i++)
        {
            int temp = num[i];
            //洗牌的亂數也不可以有0 所以range從1開始
            int randomIndex = Random.Range(1, num.Length);
            num[i] = num[randomIndex];
            num[randomIndex] = temp;
        }
        //再洗一次 這一次從前面的22張互洗 這樣主角牌才不會永遠都生在同一個位置
        for (int i = 0; i < 22; i++)
        {
            int tmp = num[i];
            int rand = Random.Range(0, 22);
            num[i] = num[rand];
            num[rand] = tmp;
        }


        return num;
    }


}
