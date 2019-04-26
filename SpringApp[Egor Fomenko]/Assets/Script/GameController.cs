using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("GUI")]
    public GameObject SartFadeScreen;
    public GameObject GameOverScreen;
    public Text textScore;
    public Text finaTextScore;

    [Header("Parametrs")]
    public int score;
    public bool clickFlag;
    public bool camFlag;


    [Header("Platfoprm")]
    public GameObject[] platform;
    public Transform startGeneratePoint;
    public int platfornCol;

    private Vector3 _generatePosition;

    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }

    void Start()
    {
        _generatePosition = startGeneratePoint.position;
    }

    void Update()
    {
        GeneratPlatform();
    }

    public void OnPointerDown()
    {
        if (SartFadeScreen.activeSelf)
        {   //Исчезновение Стартового затемнения
            Debug.Log("FadeOFF");
            SartFadeScreen.SetActive(false);

            MoveSpring.instance.StretchSpring();
            clickFlag = !clickFlag;
        }
        else if(clickFlag && !GameOverScreen.activeSelf)
        {
            MoveSpring.instance.SpringMove();
            clickFlag = !clickFlag;
            camFlag = !camFlag;
        }
        else if(GameOverScreen.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        MoveSpring.instance.Head.simulated = false;
        MoveSpring.instance.Tall.simulated = false;
    }

    public void AddScore()
    {
        score++;
        textScore.text = score.ToString();
        finaTextScore.text = score.ToString();
    }

    public void GeneratPlatform()
    {
        while (platfornCol < 5)
        {
            _generatePosition = _generatePosition + Vector3.right * Random.Range(1.25f, 2.75f);
            int rand = Random.Range(0, 100);
            rand = rand >= 50 ? 0 : 1;
            Instantiate(platform[rand], rand == 0 ? _generatePosition - new Vector3(0, -1.626f, 0) : _generatePosition, platform[rand].transform.rotation);
            platfornCol++;
        }
    }
}