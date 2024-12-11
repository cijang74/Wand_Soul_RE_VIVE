using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : Singleton<BossHPBar>
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActiveBar()
    {
        gameObject.SetActive(true);
    }
}
