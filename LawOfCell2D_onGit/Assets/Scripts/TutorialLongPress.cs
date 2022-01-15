using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLongPress : MonoBehaviour
{
    private Vector3 newpos;
    private Sprite targetImg;
    public GameObject target;
    public GameObject infoBack;
    public GameObject eventSystem;

    private int spriteName;

    private float startTime;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name != "1")
        {
            //放圖片
            targetImg = Resources.Load<Sprite>("Sprites/Detail3");
            target.gameObject.GetComponent<Image>().sprite = targetImg;

            target.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            infoBack.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
    void OnMouseUp()
    {
        target.gameObject.transform.localScale = new Vector3(0, 0, 0);
        infoBack.gameObject.transform.localScale = new Vector3(0, 0, 0);

        eventSystem.gameObject.GetComponent<TutorialControl>().clickNext();
    }

}
