using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongPress : MonoBehaviour
{
    private Vector3 newpos;
    private Sprite targetImg;
    public GameObject target;
    public GameObject infoBack;

    private int spriteName;

    private float startTime;
    // Start is called before the first frame update
    private void Awake()
    {
        infoBack = GameObject.Find("DetailBack");
        target = GameObject.Find("showDetail");
    }
    void Start()
    {
        infoBack = GameObject.Find("DetailBack");
        target = GameObject.Find("showDetail");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name != "1")
        {
            spriteName = System.Convert.ToInt32(this.gameObject.GetComponent<SpriteRenderer>().sprite.name);

            //放圖片
            targetImg = Resources.Load<Sprite>("Sprites/Detail" + spriteName);
            target.gameObject.GetComponent<Image>().sprite = targetImg;

            target.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            infoBack.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
    void OnMouseUp()
    {
        target.gameObject.transform.localScale = new Vector3(0, 0, 0);
        infoBack.gameObject.transform.localScale = new Vector3(0, 0, 0);

    }

}
