using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayerController : MonoBehaviour
{
    public GameObject game;
    public PlayerController player;
    int damage;
    public bool inmune;
    [Range(0.2f,5f)]
    public float timeInmune = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        inmune = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
 
            Destroy(collision.gameObject);
            //Destroy(collision);
        }


        if (collision.gameObject.tag == "Enemy" && !inmune)
        {
            Inmune();
            /*ParticleStop();
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = muerteClip;
            audioPlayer.Play();  */
        }
    }
    void Inmune()
    {
        damage = 1;
        player.HealtPlayer(damage);
        inmune = true;
        player.isInmune = true;
        Invoke("NotInmune", timeInmune);
        

    }
    void NotInmune()
    {
        inmune = false;
        player.isInmune = false;
    }
}
