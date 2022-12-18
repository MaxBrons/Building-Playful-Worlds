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
        var signedRot = Mathf.Atan2(transform.position.y - mouseWorldPosition.y, 
                                    transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg + 90.0f;

        transform.rotation = Quaternion.Euler(new(0f, 0f, signedRot));
    }

    public void OnCursorMove(InputValue value) => m_MousePos = value.Get<Vector2>();
}