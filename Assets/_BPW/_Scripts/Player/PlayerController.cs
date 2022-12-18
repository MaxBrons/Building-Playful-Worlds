using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct SpriteStates
{
    public Sprite Sprite;
    public Vector2 Angle;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 7.5f;
    [SerializeField] private List<SpriteStates> m_PlayerSprites;

    private Vector2 m_MovementInputVector;
    private Rigidbody2D m_RigidBody;
    private Vector2 m_CursorPos;
    private Camera m_Cam;
    private SpriteRenderer m_SpriteRenderer;

    public void OnMovement(InputValue value) => m_MovementInputVector = value.Get<Vector2>();
    public void OnCursorMove(InputValue value) => m_CursorPos = value.Get<Vector2>();

    private void Start() {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Cam = Camera.main;
    }
    private void Update() {
        m_RigidBody.MovePosition(transform.position + (Vector3)m_MovementInputVector * m_MovementSpeed * Time.deltaTime);
        UpdateSprites(m_CursorPos);
    }
    private void UpdateSprites(Vector2 position) {
        var stwp = m_Cam.ScreenToWorldPoint(position);
        var signedRot = Mathf.Repeat(Vector2.SignedAngle(stwp, Vector2.up), 360.0f);

        Sprite newSprite = m_PlayerSprites.Find((e) => {
            if(e.Angle.y - e.Angle.x < 0) {
                return signedRot > e.Angle.x || signedRot < e.Angle.y;
            }
            return signedRot > e.Angle.x && signedRot < e.Angle.y;
            }).Sprite;
        
        if (newSprite) {
            m_SpriteRenderer.sprite = newSprite;
        }
    }
}