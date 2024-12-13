using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("DontDestroy") || obj.CompareTag("Player")) // Ensure you assign the tag in the Editor
            {
                Destroy(obj);
            }
        }
    }
}
