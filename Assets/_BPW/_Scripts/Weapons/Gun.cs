using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public event Action<GameObject> OnHit;

    [SerializeField] private GameObject m_Muzzle;
    [SerializeField] private float m_MaxRange = 100.0f;
    [SerializeField] private AudioClip m_FireSound;
    [SerializeField] private int m_MagSize = 30;

    

    private void OnFire(InputValue value) {
        var hitResult = Physics2D.Raycast(m_Muzzle.transform.position, m_Muzzle.transform.up, m_MaxRange);
        if (hitResult)
            OnHit?.Invoke(hitResult.transform.gameObject);
    }
}
