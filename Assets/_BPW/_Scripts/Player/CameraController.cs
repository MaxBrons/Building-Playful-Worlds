using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera m_Cam;
    [SerializeField] private float m_CameraLerpSpeed = 2.0f;
    
    private GameObject m_Player;
    private Rigidbody2D m_CamRB;
    private Vector2 m_MousePos;
    private const string PLAYERTAG = "Player";

    private void Start() {
        m_Player = GameObject.FindGameObjectWithTag(PLAYERTAG);
        m_CamRB = m_Cam.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        m_CamRB.MovePosition(Vector3.Slerp(m_Cam.transform.position, m_Player.transform.position, m_CameraLerpSpeed * Time.fixedDeltaTime));
    }

    public void OnCursorMove(InputValue value) {
        m_MousePos = value.Get<Vector2>();
    }
}
