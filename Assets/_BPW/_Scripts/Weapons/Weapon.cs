using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(AudioSource), typeof(WeaponAnimation))]
public abstract class Weapon : MonoBehaviour
{
    public event Action<GameObject> OnHit;
    public event Action OnAttack;

    [Header("Base")]
    [SerializeField] protected GameObject m_WeaponAttackDirPoint;
    [SerializeField] protected AudioClip m_AttackSound;
    [SerializeField] protected LayerMask m_AttackMask;

    [Header("Settings Base")]
    [SerializeField] protected float m_MaxRange = 100.0f;
    [SerializeField] protected float m_Durability = 100.0f;
    [SerializeField] protected Vector2 m_DamageRange;
    [SerializeField] protected int m_AttackSpeed = 100;

    protected AudioSource m_AudioBehaviour;
    protected WeaponAnimation m_AnimationHandler;
    protected bool m_AttackingContinuous = false;
    protected bool m_OnCooldown = false;

    private List<GameObject> m_HandledObjects = new List<GameObject>(5);

    public virtual void Start() {
        m_AudioBehaviour = GetComponent<AudioSource>();
        m_AudioBehaviour.clip = m_AttackSound;

        m_AnimationHandler = GetComponent<WeaponAnimation>();
    }

    public virtual void OnFire(InputValue value) {
        m_AttackingContinuous = value.isPressed;

        if (m_AttackingContinuous)
            StartCoroutine(AttackContinuous());
    }

    public virtual void AttackOnce() {
        var hitResults = Physics2D.RaycastAll(m_WeaponAttackDirPoint.transform.position, m_WeaponAttackDirPoint.transform.up, m_MaxRange, m_AttackMask);

        //Play gunshot
        if (m_AudioBehaviour)
            m_AudioBehaviour.PlayOneShot(m_AttackSound);

        HandleObjects(hitResults);

        //Decrease health of the hit enemy and call the OnHit event
        if (m_HandledObjects.Count > 0) {
            foreach (var hitResult in m_HandledObjects) {
                var healthComp = hitResult.transform.GetComponent<IDamagable>();
                healthComp.Damage((int)UnityEngine.Random.Range(m_DamageRange.x, m_DamageRange.y) + 1);

                OnHit?.Invoke(hitResult.transform.gameObject);
            }
        }

        m_Durability -= 0.1f;
        OnAttack?.Invoke();
    }

    private void HandleObjects(RaycastHit2D[] hitResults) {
        for (int i = 0; i < m_HandledObjects.Count || i < hitResults.Length; i++) {
            if (hitResults.Length <= 0)
                return;

            if (!hitResults[i])
                continue;

            if (hitResults[i].collider.isTrigger)
                continue;

            if (i > m_HandledObjects.Count) {
                m_HandledObjects.Append(hitResults[i].transform.gameObject);
                continue;
            }

            if (i < hitResults.Length) {
                m_HandledObjects[i] = hitResults[i].transform.gameObject;
                continue;
            }

            m_HandledObjects[i] = null;
        }
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
