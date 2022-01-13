using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance { get { return instance; } }
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
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

    [SerializeField] GameObject TapToStartPanel;
    [SerializeField] GameObject GameInPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;

    public void StartTheGame()
    {
        TapToStartPanel.SetActive(false);

        GameInPanel.SetActive(true);

        GameManagement.Instance.StartTheGame();
    }

    public void WinTheGame()
    {
        WinPanel.SetActive(true);

        GameInPanel.SetActive(false);
    }

    public void LoseTheGame()
    {
        LosePanel.SetActive(true);

        GameInPanel.SetActive(false);
    }
}
