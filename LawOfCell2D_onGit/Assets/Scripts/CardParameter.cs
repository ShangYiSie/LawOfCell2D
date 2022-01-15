using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardParameter : MonoBehaviour
{

    [Header("卡牌攻擊數值")]
    public float atk;
    [Header("卡牌護盾數值")]
    public float dfs;
    [Header("卡牌消耗魔力數值")]
    public float CardConsume;

    [Header("卡牌增加魔力值")]

    public float AddMP;

    [Header("卡牌是否為buff卡")]

    public bool Buff;

    [Header("卡牌是否為debuff卡")]

    public bool DeBuff;

    public void DefineParameter(int CardID)
    {
        switch (CardID)
        {
            case 1://蛋白質護盾
                {
                    atk = 0;
                    dfs = 4;
                    CardConsume = 1;
                    AddMP = 0;
                    Buff = false;
                    DeBuff = false;
                }
                break;
            case 2://細胞巨拳
                {
                    atk = 6;
                    dfs = 0;
                    CardConsume = 0;
                    AddMP = 0;
                    Buff = false;
                    DeBuff = false;
                }
                break;
            case 3://能量激活
                {
                    atk = 0;
                    dfs = 0;
                    CardConsume = 0;
                    AddMP = 2;
                    Buff = false;
                    DeBuff = false;
                }
                break;
            case 4://強化蛋白
                {
                    atk = 0;
                    dfs = 0;
                    CardConsume = 1;
                    AddMP = 0;
                    Buff = true;
                    DeBuff = false;
                }
                break;
            case 5://代謝干擾
                {
                    atk = 0;
                    dfs = 0;
                    CardConsume = 1;
                    AddMP = 1;
                    Buff = false;
                    DeBuff = true;
                }
                break;
            case 6://細胞龍
                {
                    atk = 12;
                    dfs = 0;
                    CardConsume = 2;
                    AddMP = 0;
                    Buff = false;
                    DeBuff = false;
                }
                break;
            default:

                break;
        }

    }


}
