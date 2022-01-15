using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMove : MonoBehaviour
{
    public GameObject Moveobj;
    Vector3 movpose;
    static public float newposy;
    public Animator[] AniAry = new Animator[4];
    static public int aniNumber = -1;
    // Start is called before the first frame update
    void Start()
    {
        movpose = Moveobj.transform.localPosition;
        newposy = movpose.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (newposy != movpose.y)
        {
            movpose.y += 5f;//* Time.deltaTime;
            Moveobj.transform.localPosition = movpose;
        }
        else
        {
            if (aniNumber != -1)
            {
                AniAry[aniNumber].SetTrigger("Play");
            }
        }
    }



}
