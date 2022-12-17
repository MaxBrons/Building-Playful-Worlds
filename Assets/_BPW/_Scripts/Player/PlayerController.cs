using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 7.5f;
    [SerializeField] private Sprite[] m_PlayerSprites;

    private Vector2 m_MovementInputVector;
    private Rigidbody2D m_RigidBody;
    private Vector2 m_CursorPos;

    public void OnMovement(InputValue value) => m_MovementInputVector = value.Get<Vector2>();
    public void OnCursorMove(InputValue value) => m_CursorPos = value.Get<Vector2>();

    private void Start() {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        m_RigidBody.MovePosition(transform.position + (Vector3)m_MovementInputVector * m_MovementSpeed * Time.deltaTime);
        UpdateSprites(Camera.main.ScreenToWorldPoint(m_CursorPos));
    }
    private void UpdateSprites(Vector2 position) {
        var rot = (transform.position - (Vector3)position).normalized;
        GetComponent<SpriteRenderer>().color = new(rot.x, rot.y, rot.z, 1);
    }
}