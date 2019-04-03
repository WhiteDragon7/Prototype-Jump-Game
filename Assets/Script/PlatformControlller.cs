using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControlller : MonoBehaviour
{
    public float reactivarCollider = 0.2f;


    private PlatformEffector2D platform;
    private Collider2D pl;

    /*
     * 0 = nothig
     * 1 = default
     * 2 = transparentFx
     * 3 = default+transparentFx
     * 4 = ignoreRaycast
     * 5 = default+ignoreRaycast
     * 6 = transparentFx+ignoreRaycast
     * 7 = default+transparentFx+ignoreRaycast
     * -1025 = all-player

    */
    //public int n =-1 ;
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        //OffCollider();
    }
    
    void OffCollider(Collider2D player)
    {
        //platform.colliderMask = n;
        pl = player;        
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), player, true);
        Invoke("OnCollider",reactivarCollider);
    }
    void OnCollider()
    {
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), pl, false);
    }
}
