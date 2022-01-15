using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverInfo : MonoBehaviour
{
    public GameObject hoverImg;

    public GameObject GameObjectpos;
    public Sprite[] InfoImg;
    Vector3 pos;
    Vector3 hidepos;
    private void Start()
    {
        hidepos = GameObjectpos.transform.position;
    }
    public void showhover(float x, float y, int imgid)
    {
        pos = GameObjectpos.transform.position;
        pos.x = x;
        pos.y = y + 120f;
        GameObjectpos.transform.position = pos;
        if (Player.isbuff && imgid == 3) imgid = 6 + 2;
        if (Player.isbuff && imgid == 7) imgid = 7 + 2;
        if (Player.isbuff && imgid == 2) imgid = 8 + 2;
        hoverImg.GetComponent<SpriteRenderer>().sprite = InfoImg[imgid - 2];
        //Debug.Log("id=" + imgid);
    }
    public void hidehover()
    {
        GameObjectpos.transform.position = hidepos;
    }
}
