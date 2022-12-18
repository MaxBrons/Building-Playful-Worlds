using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string PLAYERTAG = "Player";
    private static GameObject m_Player;
    private void Start() {
        m_Player = GameObject.FindGameObjectWithTag(PLAYERTAG);
    }
    public static GameObject GetPlayer() {
        return m_Player;
    }
}
