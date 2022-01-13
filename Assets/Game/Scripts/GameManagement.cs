using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    #region Singleton

    public static GameManagement Instance { get { return instance; } }
    private static GameManagement instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region GameState
    public enum GameStates
    {
        INMENU,
        START,
        WIN,
        LOSE
    }
    public GameStates GameState;
    #endregion

    public void StartTheGame()
    {
        GameState = GameStates.START;
    }

    public void WinTheGame()
    {
        GameState = GameStates.WIN;

        UIManager.Instance.WinTheGame();
    }

    public void LoseTheGame()
    {
        GameState = GameStates.LOSE;

        UIManager.Instance.LoseTheGame();
    }
}
