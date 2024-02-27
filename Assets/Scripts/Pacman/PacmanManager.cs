using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PacmanManager : MonoBehaviour
{
    [SerializeField] GameObject winGameObject;
    [SerializeField] GameObject loseGameObject;
    GameObject player;
    [SerializeField] GameObject pacmanGame;
    [SerializeField] GameObject pacmanPrefab;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TMP_Text timerText;
    public float timer = 11;
    bool timerIsRunning;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pacmanGame.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            timerIsRunning = false;
        }

        if (timerIsRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer >= 10)
                {
                    timerText.text = "00:" + Mathf.FloorToInt(timer).ToString();
                }
                else
                {
                    timerText.text = "00:0" + Mathf.FloorToInt(timer).ToString();
                }
            }
            else
            {
                timer = 0;
                timerIsRunning = false;
                timerText.text = "00:00";
            }
        }

        if (pacmanGame.transform.childCount > 0)
        {
            player = pacmanGame.transform.GetChild(0).Find("Player").gameObject;
        }

        if (player != null && pacmanGame.activeInHierarchy)
        {
            if (player.GetComponent<PlayerController>().ateAll && timer >= 0)
            {
                winGameObject.SetActive(true);
            }
            if (!player.GetComponent<PlayerController>().ateAll && timer == 0)
            {
                loseGameObject.SetActive(true);
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void RestartGame()
    {
        if (pacmanGame.transform.childCount > 0)
        {
            Destroy(pacmanGame.transform.GetChild(0).gameObject);
        }
        Instantiate(pacmanPrefab, pacmanGame.transform);
        timer = 30;
        timerIsRunning = true;
    }
}
