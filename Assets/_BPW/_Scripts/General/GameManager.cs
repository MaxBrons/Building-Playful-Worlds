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

    [SerializeField] private Camera m_Cam;
    private GameObject m_Player;
    
    private const string PLAYERTAG = "Player";
   
    private void Start() {
        Instance = Instance != null ? Instance : this;
        m_Player = GameObject.FindGameObjectWithTag(PLAYERTAG);
    }

    public GameObject Player => m_Player;
    public Camera MainCamera => m_Cam;

    private void OnApplicationQuit() {
        Inventory.Instance.Dispose();
    }
}
