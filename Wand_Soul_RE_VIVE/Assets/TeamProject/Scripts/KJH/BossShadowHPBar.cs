using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadowHPBar : Singleton<BossShadowHPBar>
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActiveShadowBar()
    {
        gameObject.SetActive(true);
    }
}
