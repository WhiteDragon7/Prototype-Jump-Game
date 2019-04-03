using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int score = 0;

    public Text scoreText;
    public Text recordText;

    int maxhealt;
    public RawImage healt1,healt2,healt3;
    //public GameObject ui;
    public GameObject uiInGame;
    public GameObject uiDontGame;
    public GameObject player;
    public GameObject generador;

    /////////////////////////////////
    public enum GameState {Stoped,Playing, Death, Ready};
    public GameState estadojuego = GameState.Stoped;

    private AudioSource audioPrincipal;

    void Start()
    {
        audioPrincipal = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        bool userAccion = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

        if (userAccion && estadojuego == GameState.Stoped)
        {
            IniciarJuego();
        }
        
        else if (userAccion && estadojuego == GameState.Ready)
        {
            RestartGame();
            
        }
        if (estadojuego == GameState.Death)
        {
            uiDontGame.SetActive(true);
            uiInGame.SetActive(false);
        }
        
    }



    private void IniciarJuego()
    {
        estadojuego = GameState.Playing;
        //ui.SetActive(false);
        uiDontGame.SetActive(false);
        uiInGame.SetActive(true);
        player.SendMessage("UpdateEstado", "Walk");
        generador.SendMessage("StartGenerador");
        audioPrincipal.Play();
        recordText.text = "Record: "+GetMaxScore();

    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("Principal");
    }

    public void IncrementarScore()
    {
        scoreText.text = (++score).ToString();

        if (score> GetMaxScore())
        {
            recordText.text = "Record: "+score.ToString();
            SaveMaxScore(score);
        }
    }
    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
        
    }
    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MaxScore",0);
    }
    public void SaveMaxScore(int currentScore)
    {
        PlayerPrefs.SetInt("MaxScore", currentScore);
    }
    public void SetHeal(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            
            if (maxhealt==0)
            {
                break;
            }
            maxhealt -= 1;
            switch (maxhealt)
            {
                case 3:
                    healt1.color = Color.white;
                    healt2.color = Color.white;
                    healt3.color = Color.white;
                    break;
                case 2:
                    healt1.color = Color.black;
                    healt2.color = Color.white;
                    healt3.color = Color.white;
                    break;
                case 1:
                    healt1.color = Color.black;
                    healt2.color = Color.black;
                    healt3.color = Color.white;
                    break;
                case 0:
                    healt1.color = Color.black;
                    healt2.color = Color.black;
                    healt3.color = Color.black;
                    break;
                default:
                    healt1.color = Color.white;
                    healt2.color = Color.white;
                    healt3.color = Color.white;
                    break;
            }
            
        }
    }
}
