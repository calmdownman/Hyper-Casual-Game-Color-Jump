using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 15; //점프 힘
    [SerializeField]
    private float moveSpeed = 5; //이동 속도

    private Rigidbody2D rb2D; //속력 제어를 위한 RB2D
    private CircleCollider2D circleCollider2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2D.velocity = new Vector2(moveSpeed,jumpForce);

        StartCoroutine(nameof(UpdateInput));
    }
   
    IEnumerator UpdateInput()
    {
        while (true) 
        { 
         if (Input.GetMouseButtonDown(0))
            {
                JumpTo();
            }
        
        yield return null;
        }
    }

    void JumpTo()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }
 
    void ReverseXDir()
    {
        float x = -Mathf.Sign(rb2D.velocity.x);
        rb2D.velocity = new Vector2(x * moveSpeed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            ReverseXDir(); //플레이어 x축 방향 전환
            StartCoroutine(nameof(ColliderOnOffAnimation)); //같은 벽에 여러번 충돌하는 것을 방지하기 위해 잠시 충돌을 꺼둠
        }
    }

    IEnumerator ColliderOnOffAnimation()
    {
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.1f);
        circleCollider2D.enabled = true;
    }
}
