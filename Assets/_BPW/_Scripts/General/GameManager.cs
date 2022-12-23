using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Action OnGameStarted;
    public Action OnGameOver;

    [SerializeField] private Camera m_Cam;
    private GameObject m_Player;
    
    private const string PLAYERTAG = "Player";

    private void Awake() {
        Instance = Instance != null ? Instance : this;
        m_Player = GameObject.FindGameObjectWithTag(PLAYERTAG);
    }

    private void Start() {
        StartGame();
    }

    public void StartGame() {
        InputManager.Instance.SetPlayerInputActive(true);
        Time.timeScale = 1.0f;
        OnGameStarted?.Invoke();
    }

    public void GameOver() {
        InputManager.Instance.SetPlayerInputActive(false);
        Time.timeScale = 0.0f;
        OnGameOver?.Invoke();
    }

    public GameObject Player => m_Player;
    public Camera MainCamera => m_Cam;

    private void OnApplicationQuit() {
        Inventory.Instance.Dispose();
    }
}
