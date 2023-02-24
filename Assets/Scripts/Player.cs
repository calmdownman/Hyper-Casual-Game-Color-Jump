using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 15; //���� ��
    [SerializeField]
    private float moveSpeed = 5; //�̵� �ӵ�

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject playerDieEffect; //�÷��̾� ��� ����Ʈ

    private Rigidbody2D rb2D; //�ӷ� ��� ���� RB2D
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rb2D.velocity = new Vector2(moveSpeed,jumpForce);
        rb2D.isKinematic = true; //����,�߷��� �����Ѵ�
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
            //�÷��̾� x�� ���� ��ȯ
            ReverseXDir();
            //���� ���� ������ �浹�ϴ� ���� �����ϱ� ���� ��� �浹�� ����
            StartCoroutine(nameof(ColliderOnOffAnimation));
            //���� �⵿���� �� GameController������ ó�� (�� �߰�, ���� ���� ��)
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
        //�ÿ��̾� ��� ����Ʈ ����
        Instantiate(playerDieEffect, transform.position, Quaternion.identity);
        //���ӿ��� ó��
        gameController.GameOver();
        //�÷��̾� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
