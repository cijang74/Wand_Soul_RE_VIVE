using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExit : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActiveExit()
    {
        gameObject.SetActive(true);
    }
    
}
