using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditorDelet : MonoBehaviour
{
    int spriteName;
    int cardidnumber;
    public void BtnCardDelet()
    {
        spriteName = System.Convert.ToInt32(this.gameObject.GetComponent<Image>().sprite.name);
        if (spriteName != 0)
        {
            cardidnumber = System.Convert.ToInt32(this.gameObject.name);
            CardEditorInfo.CardDelet(spriteName, cardidnumber);
        }

    }
}

