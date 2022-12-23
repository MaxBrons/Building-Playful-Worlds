using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerArm : MonoBehaviour
{

    [SerializeField] private const float m_RotOffset = 90;

    public void OnCursorMove(InputValue value) {
        var mousePos = value.Get<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        var signedRot = Mathf.Atan2(transform.position.y - mouseWorldPosition.y,
                                    transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg + m_RotOffset;

        transform.rotation = Quaternion.Euler(new(0f, 0f, signedRot));
    }
}