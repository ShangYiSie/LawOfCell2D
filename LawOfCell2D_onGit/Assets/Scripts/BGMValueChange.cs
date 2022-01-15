using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMValueChange : MonoBehaviour
{
    public void BGMChange()
    {
        Level.BGM = gameObject.GetComponent<Slider>().value;
    }
}
