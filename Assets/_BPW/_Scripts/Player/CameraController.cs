using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_CamHandle;
    [SerializeField] private float m_CameraLerpSpeed = 2.0f;
    
    private GameObject m_Player;
    private Rigidbody2D m_CamRB;

    private void Start() {
        m_Player = GameManager.Instance.Player;
        m_CamRB = m_CamHandle.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        m_CamRB.MovePosition(Vector3.Slerp(m_CamHandle.transform.position, m_Player.transform.position, m_CameraLerpSpeed * Time.fixedDeltaTime));
    }
}
