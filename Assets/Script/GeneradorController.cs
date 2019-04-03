using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorController : MonoBehaviour
{
    //public List<GameObject> enemyPrefab;
    public GameObject[] enemyPrefab;
    [Range(0.5f,2.5f)]
    public float timerGenerador = 1.57f;
    //public GameObject game;

    ////////////////////////////////////////
    public List<Transform> portales;
    Object[] allPortal;
    float portal;

    float enemy;
    int count = 0;

    void Start()
    {
        //allPortal = GameObject.FindGameObjectsWithTag("Portal");

    }
    void Update()
    {
        
    }

    void CrearEnemy()
    {
        if (count >=20)
        {
            StopGenerator();
        }
        portal = Random.Range(0f, 0.8f)*10;
        enemy = Random.Range(0f, 0.2f) * 10;
        if (portal<8)
        {
            Transform childtransform = transform.GetChild((int)portal);
            Instantiate(enemyPrefab[(int)enemy], childtransform.transform.position, Quaternion.identity);
            count++;
        }
        else
        {
            Debug.Log("sale mal");
        }

    }
    public void StartGenerador()
    {
        InvokeRepeating("CrearEnemy", 0f, timerGenerador);
    }
    public void StopGenerator(bool clean = false)
    {
        CancelInvoke("CrearEnemy");
        if (clean)
        {
            Object[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject Enemigo in allEnemy)
            {
                Destroy(Enemigo);
            }
        }
    }
}
