using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Level //紀錄目前進行的關卡數值
{
    public static float volume = 0.5f;
    public static float BGM = 0.5f;

    public static int level = 0;
    public static Vector3 Pos;
    public static float PlayerHP = 30;

    public static bool isTutorial = false;
    public static bool firstTutor = true;

    public static bool Lan_CN = true;//預設為中文介面

    public static int[] GetCardID = { 3, 5, 6 };

    public static int[] PlayerCardID = { 2, 4, 1, 0, 0, 0 };//紀錄玩家目前有什麼卡牌 new int[6]

    // public static int[] PlayerCardS = new int[32];//紀錄玩家卡牌牌組
    public static int[] PlayerCardS = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };//預設牌組 0~6

}
