using EZCameraShake;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    public event Action<bool> OnGunReloadStarted;
    public event Action OnGunReloadEnded;

    [Header("Settings")]
    [SerializeField] private int m_MagSize = 30;
    [SerializeField] private AudioClip m_ReloadSound;

    [Header("Camera Shake")]
    [SerializeField] private float m_ShakeMagnitude = 10.0f;
    [SerializeField] private float m_ShakeRoughness = 0.1f;
    [SerializeField] private Vector2 m_ShakeFadeInOut = new Vector2(0.1f, 0.2f);


    private int m_CurrentClipCount = 0;
    private bool m_IsReloading = false;

    public override void Start() {
        base.Start();
        m_CurrentClipCount = m_MagSize;
    }

    public override void AttackOnce() {
        if (m_CurrentClipCount <= 0)
            return;

        base.AttackOnce();
        m_CurrentClipCount -= 1;

        CameraShaker.Instance.ShakeOnce(m_ShakeMagnitude, m_ShakeRoughness, m_ShakeFadeInOut.x, m_ShakeFadeInOut.y, transform.right, CameraShaker.Instance.DefaultRotInfluence);
    }

    private void OnReload(InputValue value) {
        if (value.isPressed) {
            StartCoroutine(ReloadWeapon(m_ReloadSound.length + 0.1f,
                () => {
                    m_AudioBehaviour.clip = m_ReloadSound;
                    m_AudioBehaviour.Play();
                    m_IsReloading = true;
                    OnGunReloadStarted?.Invoke(m_CurrentClipCount <= 0);
                },
                () => {
                    m_CurrentClipCount = m_MagSize;
                    m_AudioBehaviour.clip = m_AttackSound;
                    m_IsReloading = false;
                    OnGunReloadEnded?.Invoke();
                }));
        }
    }

    private IEnumerator ReloadWeapon(float secondsToWait, Action onStart = null, Action onEnd = null) {
        onStart?.Invoke();
        yield return new WaitForSeconds(secondsToWait);
        onEnd?.Invoke();
    }
}
