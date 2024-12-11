using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other) 
    {
        Debug.Log(123);
        //40번 정도 맞음, 강화는 30번 조금 넘게 맞음
        //적 체력 깎는 함수

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
