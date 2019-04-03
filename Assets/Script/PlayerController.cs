using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 5f)]
    public float speed = 2f;
    [Range(5f, 15f)]
    public float jumpForce = 5f;

    public GameObject game;
    public GameObject generador;
    public PlatformControlller[] platform;


    //public AudioClip saltoClip;
    //public AudioClip muerteClip;
    //public AudioClip ptoClip;
    //public ParticleSystem particle_1;
    //private AudioSource audioPlayer;

    private Animator animator;
    private Rigidbody2D riBody2D;
    private Collider2D col2D;
    ///////////


    //int platformLayer, playerLayer;

    /////other

    public int healt = 3;
    public int movmentDirection = 1;
    public bool inGround;
    public bool isInmune;
    public bool jumped;
    public bool jumpOff;


    public bool playing = false;

    void Start()
    {
        //audioPlayer = GetComponent<AudioSource>();
        riBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col2D = GetComponent<Collider2D>();
        //platformLayer = LayerMask.NameToLayer("Platform");
        //playerLayer = LayerMask.NameToLayer("Player");


    }

    void Update()
    {
        /////////Update salto/////////////

        //bool estado = game.GetComponent<GameController>().estadojuego == GameController.GameState.Playing;

        

        if (Input.GetKeyDown(KeyCode.UpArrow) && inGround)
        {
            jumped = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && inGround)
        {

        }
        animator.SetBool("inGround",inGround);
        animator.SetBool("isInmune", isInmune);
    }

    private void FixedUpdate()
    {
        if (playing == true)
        { 
            MovmentPlayer();
            if (jumped)
            {
                riBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumped = false;
            }

        }
        //Debug.Log(jumped);
    }

    public void UpdateEstado(string state = null)
    {
        playing = true;
        if (state != null)
        {
            animator.Play(state);
        }
    }


    ////////////////////Update restart easy///////////// 

    private void GameListo()
    {
        game.GetComponent<GameController>().estadojuego = GameController.GameState.Ready;
        
    }
    void restartGame()
    {
        //animator.Play();
    }

    private void MovmentPlayer()
    {
        float h = Input.GetAxis("Horizontal");       
        if (h != 0)
        {
            if (h < 0)
            {
                movmentDirection = -1;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                movmentDirection = 1;
            }
        }       
        riBody2D.velocity = new Vector2(speed * movmentDirection, riBody2D.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && inGround)
        {
            if (collision.gameObject.name == platform[1].name)
            {
                platform[1].SendMessage("OffCollider", col2D);
            }
            if (collision.gameObject.name == platform[2].name)
            {
                platform[2].SendMessage("OffCollider", col2D);
            }
            if (collision.gameObject.name == platform[0].name)
            {
                platform[0].SendMessage("OffCollider", col2D);
            }

        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Coin")
        {
            game.SendMessage("IncrementarScore");
            Destroy(collision.gameObject);
            //Destroy(collision);
        }*/
    }


    /*IEnumerator JumpOff()
    {
        jumpOff = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);

        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer,false);
        jumpOff = false;
    }*/

    public void HealtPlayer(int damage)
    {
        
        game.SendMessage("SetHeal", damage);
        healt -= damage;
        
        if (healt == 0)
        {
            
            UpdateEstado("Death");
            game.GetComponent<GameController>().estadojuego = GameController.GameState.Death;
            generador.SendMessage("StopGenerator", true);
            playing = false;
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;

            /*ParticleStop();
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = muerteClip;
            audioPlayer.Play();  */
        }

    }
    
}
