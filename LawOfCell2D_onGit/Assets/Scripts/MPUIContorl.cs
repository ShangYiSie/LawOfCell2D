using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPUIContorl : MonoBehaviour
{
    public Sprite[] MPImage;

    private float startTime;
    private float alpha = 255.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator UIControl()
    {
        // Debug.Log("魔力值:" + Player.PlayerMP);
        int i = 3;
        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
        {

            if (child.transform == transform)
            {
                continue;
            }

            if (i <= Player.PlayerMP)
            {
                Color tmp = child.color;
                alpha = 0;
                // tmp.a = 0;
                child.color = tmp;
                if (child.sprite == MPImage[1])
                {
                    child.sprite = MPImage[0];
                    while (alpha <= 255)
                    {

                        alpha += 10f;
                        tmp.a = alpha / 255f;
                        child.color = tmp;
                        yield return new WaitForSeconds(0.005f);
                    }
                }

                tmp.a = 255;
                child.color = tmp;
                // alpha = 0f;

                // Debug.Log("亮=" + i);
            }
            else
            {
                Color tmp = child.color;
                alpha = 255;
                for (int k = 0; k < 2; k++)
                {
                    while (alpha <= 255)
                    {

                        alpha += 10f;
                        tmp.a = alpha / 255f;
                        child.color = tmp;
                        yield return new WaitForSeconds(0.01f);
                    }
                    while (alpha > 0 && child.sprite == MPImage[0])
                    {
                        tmp.a = alpha / 255f;
                        alpha -= 10f;
                        child.color = tmp;
                        yield return new WaitForSeconds(0.01f);

                    }

                }
                // while (alpha > 0 && child.sprite == MPImage[0])
                // {
                //     tmp.a = alpha / 255f;
                //     alpha -= 10f;
                //     child.color = tmp;
                //     yield return new WaitForSeconds(0.005f);

                // }

                tmp.a = 255;
                child.color = tmp;
                // alpha = 255f;
                child.sprite = MPImage[1];
                // Debug.Log("暗=" + i);
            }
            i--;



        }
        StopCoroutine(UIControl());
    }


}
