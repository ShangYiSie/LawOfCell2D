using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image BtnPlay;
    public Sprite[] BtnPlayimage;
    public GameObject skipText;
    public Text txt;
    public Image TextBack;
    public GameObject MenuBGImage;
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    // Start is called before the first frame update
    void Start()
    {
        // Btn_Enter.gameObject.SetActive(false);
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.Lan_CN)
        {
            BtnPlay.sprite = BtnPlayimage[1];
        }
        else
        {
            BtnPlay.sprite = BtnPlayimage[0];
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            txt.text = currentText;
            if (i == fullText.Length - 1)
            {
                StartCoroutine(FadeImage());
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator FadeImage()
    {
        for (float i = 1; i > 0; i -= 0.1f)
        {
            if (i <= 0.1)
            {
                MenuBGImage.gameObject.SetActive(true);
                TextBack.gameObject.SetActive(false);
                skipText.GetComponent<Text>().gameObject.SetActive(false);
                // GameObject.Find("skipText").GetComponent<Text>().gameObject.SetActive(false);   //skip text disable
            }
            TextBack.color = new Color(0, 0, 0, i);
            txt.color = new Color(1, 1, 1, i);
            skipText.GetComponent<Text>().color = new Color(1, 1, 1, i);
            // GameObject.Find("skipText").GetComponent<Text>().color = new Color(1, 1, 1, i);  //skip text fade out
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void ToMain()
    {
        StartCoroutine(FadeImage());
    }

    #region 場景跳轉
    public void ToMap()//跳轉至Map場景
    {
        Time.timeScale = 1;
        Application.LoadLevel("Map");
    }
    #endregion



}
