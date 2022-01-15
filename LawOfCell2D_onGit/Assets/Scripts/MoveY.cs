using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveY : MonoBehaviour
{
    public GameObject Btn;
    public GameObject MenuBGImage;
    public GameObject ManGa;
    CanvasGroup ManGaCG;
    public void movey()
    {
        MenuMove.newposy += 770f;
        MenuMove.aniNumber++;
    }
    public void ManGaEnd()
    {

        Destroy(ManGa);

    }

    IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(0.5f);
        ManGaCG = ManGa.GetComponent<CanvasGroup>();
        for (float i = 1; i > 0; i -= 0.1f)
        {
            if (i <= 0.1)
            {
                MenuBGImage.gameObject.SetActive(true);
                ManGa.gameObject.SetActive(false);
                // skipText.GetComponent<Text>().gameObject.SetActive(false);
                // GameObject.Find("skipText").GetComponent<Text>().gameObject.SetActive(false);   //skip text disable
            }
            ManGaCG.alpha = i;
            // txt.color = new Color(1, 1, 1, i);
            // skipText.GetComponent<Text>().color = new Color(1, 1, 1, i);
            // GameObject.Find("skipText").GetComponent<Text>().color = new Color(1, 1, 1, i);  //skip text fade out
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void ToMain()
    {
        StartCoroutine(FadeImage());
        Destroy(Btn);
    }


}
