using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float m_MaxDistanceFromPlayer = 15.0f;
    [SerializeField] private float m_AttackRange = 2.0f;
    [SerializeField] private int m_AttackDamage = 25;
    [SerializeField] private float m_AttackInterval = 1.0f;

    private Transform m_TransformToMoveTo;
    private bool m_IsAttacking = false;

    private void Start() {
    }

    public override void OnEnter() {
        m_TransformToMoveTo = Controller.Controller.Player.transform;
    }

    public override void OnExit() {
        m_TransformToMoveTo = null;
    }

    public override void OnUpdate() {
        if (m_TransformToMoveTo && !m_IsAttacking) {
            Controller.Controller.MoveToPosition(m_TransformToMoveTo.position);
        }

        if (Controller.Controller.IsPlayerInRange(m_AttackRange)) {
            AttackPlayer();
        }

        if (!Controller.Controller.IsPlayerInRange(m_MaxDistanceFromPlayer)) {
            Controller.SwitchState(typeof(PatrolState));
        }
    }

    public void AttackPlayer() {
        if (m_IsAttacking) 
            return;

        StartCoroutine(AttackPlayerDelayed(m_AttackInterval));
    }

    private IEnumerator AttackPlayerDelayed(float delay) {
        m_IsAttacking = true;

        var player = Controller.Controller.Player;
        if (player) {
            var health = player.GetComponent<Health>();

            if (health) {
                health.DecreaseHealth(m_AttackDamage);
            }
        }
        yield return new WaitForSeconds(delay);
        m_IsAttacking = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        Gizmos.DrawWireSphere(transform.position, m_MaxDistanceFromPlayer);
    }
}