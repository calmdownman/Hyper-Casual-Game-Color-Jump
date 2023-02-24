using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 15; //점프 힘
    [SerializeField]
    private float moveSpeed = 5; //이동 속도

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject playerDieEffect; //플레이어 사망 이펙트

    private Rigidbody2D rb2D; //속력 제어를 위한 RB2D
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rb2D.velocity = new Vector2(moveSpeed,jumpForce);
        rb2D.isKinematic = true; //물리,중력을 중지한다
        //StartCoroutine(nameof(UpdateInput));
    }
   
    private IEnumerator Start() 
    { 
        float orginY = transform.position.y;
        float deltaY = 0.5f;
        float moveSpeedY = 2;

        while (true)
        {
            float y = orginY + deltaY*Mathf.Sin(Time.time*moveSpeedY);
            transform.position = new Vector2(transform.position.x, y);

            yield return null;
        }
    }

    public void GameStart()
    {
        rb2D.isKinematic = false;
        rb2D.velocity = new Vector2(moveSpeed, jumpForce);

        StopCoroutine(nameof(Start));
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
            if (collision.GetComponent<SpriteRenderer>().color != spriteRenderer.color)
            {
                PlayerDie();
            }
            else 
            { 
            //플레이어 x축 방향 전환
            ReverseXDir();
            //같은 벽에 여러번 충돌하는 것을 방지하기 위해 잠시 충돌을 꺼둠
            StartCoroutine(nameof(ColliderOnOffAnimation));
            //벽과 출동했을 때 GameController에서의 처리 (벽 추가, 색상 변경 등)
            gameController.CollisionWithWall();
            }
        }
        else if (collision.CompareTag("DeathWall"))
        {
            PlayerDie();
        }
    }

    IEnumerator ColliderOnOffAnimation()
    {
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.1f);
        circleCollider2D.enabled = true;
    }

    private void PlayerDie()
    {
        //플에이어 사망 이펙트 생성
        Instantiate(playerDieEffect, transform.position, Quaternion.identity);
        //게임오버 처리
        gameController.GameOver();
        //플레이어 비활성화
        gameObject.SetActive(false);
    }
}
