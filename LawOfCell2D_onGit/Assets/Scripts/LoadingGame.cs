using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGame : MonoBehaviour
{
    public GameObject level1Enemy;
    public GameObject level2Enemy;
    public GameObject level3Enemy;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        level1Enemy.SetActive(false);
        level2Enemy.SetActive(false);
        level3Enemy.SetActive(false);
        Boss.SetActive(false);
        switch (Level.level)
        {
            case 0://第一關
                {
                    level1Enemy.SetActive(true);
                }
                break;
            case 1://第二關
                {
                    level2Enemy.SetActive(true);
                }
                break;
            case 2://第三關
                {
                    level3Enemy.SetActive(true);
                }
                break;
            case 3://Boss
                {
                    Boss.SetActive(true);
                }
                break;

            default:

                break;
        }


    }


}
