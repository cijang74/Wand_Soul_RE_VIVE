using UnityEngine;

public class BackgroundMoveTrigger : MonoBehaviour
{
    private Vector3 movementVec;

    private void Awake()
    {
        movementVec = new Vector3(76.15f, transform.position.y, 0f); // 최신화할 위치 입력
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            transform.position = movementVec;
        }
    }
}