using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteroite : MonoBehaviour
{
    public GameObject Meter;
    public GameObject b;
    public GameObject p;
    public GameObject w;
    public GameObject y;
    public void hideMete()
    {
        Meter.SetActive(false);
    }
    public void showMete()
    {
        Meter.SetActive(true);
    }
    public void showMeteorp()
    {
        p.SetActive(true);
    }
    public void hideMetep()
    {
        p.SetActive(false);
    }

    public void showMeteorb()
    {
        b.SetActive(true);
    }
    public void hideMeteb()
    {
        b.SetActive(false);
    }

    public void showMeteorw()
    {
        w.SetActive(true);
    }
    public void hideMetew()
    {
        w.SetActive(false);
    }
    public void showMeteory()
    {
        y.SetActive(true);
    }
    public void hideMeteory()
    {
        y.SetActive(false);
    }
}