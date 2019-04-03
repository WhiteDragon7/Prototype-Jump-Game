using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range (0,5f)]
    public float speed = 4f;
    public GameObject coinPrefab;

    private Rigidbody2D riBody2D;
    private Animator animator;
    private Collider2D col2D;

    ///
    
    private GameObject chill;

    //////////////
    private int movmentDirection = 1;
    private bool isDeath = false;

    void Start()
    {
        riBody2D = GetComponent<Rigidbody2D>();
        riBody2D.velocity = Vector2.left * speed;
        
        animator = GetComponent<Animator>();
        col2D = GetComponent<Collider2D>();
        chill = transform.Find("Otro").gameObject;
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MovmentEnemy();
    }

    void EnemyDeath()
    {
        chill.gameObject.SetActive(false);
        gameObject.tag = "Finish";
        
        isDeath = true;
        riBody2D.velocity = Vector2.zero;
        riBody2D.isKinematic = true;
        col2D.enabled = false;
        animator.Play("Death");      
        CreateCoin();
        
        Destroy(gameObject, 0.5f);
    }
    void CreateCoin()
    {
        Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
    }
    private void MovmentEnemy()
    {
        if (isDeath == false)
        {
            riBody2D.velocity = new Vector2(speed * movmentDirection, riBody2D.velocity.y);
        }
        /*
        //rb2D.AddRelativeForce(Vector2.right * speed* h);        
        //rb2D.AddRelativeForce(Vector2.right * speed*n);
        //Debug.Log(h);
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            movmentDirection *= -1;
            if (movmentDirection < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyDeath();
        }
    }
    
}
