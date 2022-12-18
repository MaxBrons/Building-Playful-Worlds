using EZCameraShake;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    public event Action OnGunFired;

    [Header("Settings")]
    [SerializeField] private int m_MagSize = 30;
    [SerializeField] private AudioClip m_ReloadSound;
    [SerializeField] private float m_ShakeScale = 1;

    private int m_CurrentClipCount = 0;

    public override void Start() {
        base.Start();
        m_CurrentClipCount = m_MagSize;
    }

    public override void AttackOnce() {
        if (m_CurrentClipCount <= 0) 
            return;

        base.AttackOnce();
        m_CurrentClipCount -= 1;

        CameraShaker.Instance.ShakeOnce(3.0f, .1f, 0.1f, .2f, transform.up, new Vector3(1,1,0));
    }

    private void OnReload(InputValue value) {
        if (value.isPressed) {
            StartCoroutine(ReloadWeapon(m_ReloadSound.length + 0.1f,
                () => {
                    m_AudioBehaviour.clip = m_ReloadSound;
                    m_AudioBehaviour.Play();
                },
                () => {
                    m_CurrentClipCount = m_MagSize;
                    m_AudioBehaviour.clip = m_AttackSound;
                }));
        }
    }

    private IEnumerator ReloadWeapon(float secondsToWait, Action onStart = null, Action onEnd = null) {
        onStart?.Invoke();
        yield return new WaitForSeconds(secondsToWait);
        onEnd?.Invoke();
    }
}
