using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public abstract class Weapon : MonoBehaviour
{
    public event Action<GameObject> OnHit;
    public event Action<float> OnDurabilityChanged;
    public event Action OnAttack;

    [Header("Base")]
    [SerializeField] protected GameObject m_WeaponAttachPoint;
    [SerializeField] protected AudioClip m_AttackSound;
    [SerializeField] protected LayerMask m_AttackMask;

    [Header("Settings Base")]
    [SerializeField] protected float m_MaxRange = 100.0f;
    [SerializeField] protected float m_Durability = 100.0f;
    [SerializeField] protected Vector2 m_DamageRange;
    [SerializeField] protected int m_AttackSpeed = 100;

    protected AudioSource m_AudioBehaviour;
    protected bool m_AttackingContinuous = false;
    protected bool m_OnCooldown = false;

    public virtual void Start() {
        m_AudioBehaviour = GetComponent<AudioSource>();
        if (m_AudioBehaviour) {
            m_AudioBehaviour.clip = m_AttackSound;
        }
    }

    public virtual void OnFire(InputValue value) {
        m_AttackingContinuous = value.isPressed;

        if (m_AttackingContinuous)
            StartCoroutine(AttackContinuous());
    }

    public virtual void AttackOnce() {
        var hitResults = Physics2D.RaycastAll(transform.position + (transform.position - m_WeaponAttachPoint.transform.position), m_WeaponAttachPoint.transform.right, m_MaxRange, m_AttackMask);
        Debug.DrawLine(transform.position, transform.position + m_WeaponAttachPoint.transform.right * 100, Color.red, 3f);
        print(hitResults.Length);
        //Play gunshot
        if (m_AudioBehaviour)
            m_AudioBehaviour.PlayOneShot(m_AttackSound);

        List<GameObject> handledObjects = new List<GameObject>();
        //Decrease health of the hit enemy and call the OnHit event
        foreach (var hitResult in hitResults) {
            if (hitResult) {
                if (hitResult.collider.isTrigger || handledObjects.Contains(hitResult.transform.gameObject))
                    continue;

                var healthComp = hitResult.transform.GetComponent<Health>();

                if (healthComp) {
                    healthComp.DecreaseHealth((int)UnityEngine.Random.Range(m_DamageRange.x, m_DamageRange.y) + 1);

                    handledObjects.Add(hitResult.transform.gameObject);
                    OnHit?.Invoke(hitResult.transform.gameObject);
                    continue;
                }
            }
        }

        m_Durability -= 0.1f;
        OnDurabilityChanged?.Invoke(m_Durability);
        OnAttack?.Invoke();
    }

    public virtual IEnumerator AttackContinuous() {
        if (!m_OnCooldown) {
            m_OnCooldown = true;
            while (m_AttackingContinuous) {
                AttackOnce();
                yield return new WaitForSeconds(1 / ((float)m_AttackSpeed / 60));
            }
            m_OnCooldown = false;
        }
        yield return null;
    }
}
