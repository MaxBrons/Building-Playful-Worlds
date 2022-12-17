using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerArm : MonoBehaviour
{
    private Vector3 m_MousePos;
    
    private const float m_RotOffset = 90;

    private void Update() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(m_MousePos);

        float atanX = transform.position.x - mouseWorldPosition.x;
        float atanY = transform.position.y - mouseWorldPosition.y;
        float angle = Mathf.Atan2(atanY, atanX) * Mathf.Rad2Deg + m_RotOffset;

        transform.rotation = Quaternion.Euler(new(0f, 0f, angle));
    }

    public void OnCursorMove(InputValue value) => m_MousePos = value.Get<Vector2>();
}