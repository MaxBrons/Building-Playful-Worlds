using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[Serializable]
public struct SpriteStates
{
    public Sprite Sprite;
    public Vector2 Angle;
}

public class PlayerController : MonoBehaviour, IInputHandler
{
    [SerializeField] private float m_MovementSpeed = 7.5f;
    [SerializeField] private List<SpriteStates> m_PlayerSprites;

    private Vector2 m_MovementInputVector;
    private Rigidbody2D m_RigidBody;
    private Vector2 m_CursorPos;
    private Camera m_Cam;
    private SpriteRenderer m_SpriteRenderer;
    private Health m_Health;

    public void OnMovement(InputValue value) => m_MovementInputVector = value.Get<Vector2>();
    public void OnCursorMove(InputValue value) => m_CursorPos = value.Get<Vector2>();

    private void Start() {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Cam = GameManager.Instance.MainCamera;
        
        m_Health = GetComponent<Health>();
        if (m_Health) {
            m_Health.OnHealthDepleted += GameManager.Instance.GameOver;
        }
    }

    private void Update() {
        m_RigidBody.MovePosition(transform.position + (Vector3)m_MovementInputVector * m_MovementSpeed * Time.deltaTime);
        UpdateSprites(m_CursorPos);
    }
    private void UpdateSprites(Vector2 position) {
        var stwp = m_Cam.ScreenToWorldPoint(position);
        var signedRot = Mathf.Repeat(Mathf.Atan2(stwp.y - transform.position.y,
                                    stwp.x - transform.position.x) * Mathf.Rad2Deg - 90.0f, 360.0f);

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

    public void OnInputEnabled() {
    }

    public void OnInputDisabled() {
        m_MovementInputVector = Vector2.zero;
        m_CursorPos = Vector2.zero;
    }
}