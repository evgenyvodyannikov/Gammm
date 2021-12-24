using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    public PlayerController player;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void LoadMenu()
    {
        Debug.Log("Load");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void SaveGame()
    {
        string Name = "SavedGame #" + (Int32.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT COUNT(*) FROM SavedGames")) + 1).ToString();
        int hp = player.health;// playercontroller.health;
        int mana = player.mana; // playercontroller.mana;
        Debug.Log("INSERT INTO SavedGames(Name, HP, Mana, DateSaved,PosX,PosY) VALUES('" + Name + "', " + hp.ToString() + ", " + mana.ToString() + ",'" + DateTime.UtcNow.ToString() + "','" + player.transform.position.x + "','" + player.transform.position.y + "')");
        MyDataBase.ExecuteQueryWithoutAnswer("INSERT INTO SavedGames(Name, HP, Mana, DateSaved,PosX,PosY) VALUES('" + Name + "', " + hp.ToString() + ", " + mana.ToString() + ",'" + DateTime.UtcNow.ToString() + "','" + player.transform.position.x + "','" + player.transform.position.y + "')");
        Debug.Log(MyDataBase.ExecuteQueryWithAnswer("Select Name From SavedGames Where ID =" + MyDataBase.ExecuteQueryWithAnswer("SELECT COUNT(*) FROM SavedGames")));
    }
}

