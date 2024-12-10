using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckEnterence : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // 불러올 씬
    [SerializeField] public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform target; // 플레이어의 Transform 정보
    private Vector2 moveDir;
    private bool start;

    private void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if(start)
        {
            MoveTowardPlayer();
            Vector2 EnemyVelocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
            rb.velocity = EnemyVelocity;
        }
    }

    private void MoveTowardPlayer()
    {
        float moveDirection = target.position.x > transform.position.x ? 1 : -1;
        Vector2 moveVector = new Vector2(moveDirection, 0f);

        Moveto(moveVector);
    }

    private void Moveto(Vector2 targetPosition) // 다른 클래스에서 이동 방향을 입력받는 용도
    {
        moveDir = targetPosition;
    }

    public void SetTruckStart()
    {
        Debug.Log("SetStart");
        start = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
