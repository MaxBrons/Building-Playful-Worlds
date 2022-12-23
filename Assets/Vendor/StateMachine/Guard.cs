using System.Collections;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 3.0f;
    [SerializeField] private float m_ChaseRadius = 8.0f;

    private GameObject m_Player;
    private NavMeshAgent m_Agent;
    private Health m_Health;
    private StateMachine m_StateMachine;

    private const string PLAYERTAG = "Player";
    private void Awake() {
        m_Agent = GetComponent<NavMeshAgent>();
        if (m_Agent) {
            m_Agent.updateRotation = false;
            m_Agent.updateUpAxis = false;
            m_Agent.speed = m_MoveSpeed;
        }
    }
    private void Start() {
        m_StateMachine = new StateMachine(this, GetComponents<State>());
        m_StateMachine.SwitchState(typeof(PatrolState));

        m_Health = GetComponent<Health>();
        if (m_Health) {
            m_Health.OnHealthChanged += OnTakeDamage;
            m_Health.OnHealthDepleted += OnHealthDepleted;
        }

        if (TryGetComponent(out CircleCollider2D col)) {
            col.radius = m_ChaseRadius;
        }
    }

    public void Update() {
        m_StateMachine?.OnUpdate();

        if (m_Player) {
            m_StateMachine.SwitchState(typeof(AttackState));
        }
    }
    public GameObject Player => m_Player;

    public void OnTakeDamage(GameObject obj, float damage) {

    }

    public void OnHealthDepleted() {
        if (gameObject) {
            Destroy(gameObject);
        }
    }

    public void MoveToPosition(Vector3 targetPosition) {
        m_Agent.SetDestination(targetPosition);
    }


    public bool IsPlayerInRange(float range) {
        if (!m_Player)
            return false;

        return (Vector3.Distance(m_Player.transform.position, transform.position) <= range);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (m_Player)
            return;

        if (collision.gameObject.CompareTag(PLAYERTAG)) {
            m_Player = collision.gameObject;
            m_StateMachine.SwitchState(typeof(AttackState));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!m_Player)
            return;

        if (collision.gameObject.CompareTag(PLAYERTAG)) {
            m_Player = null;
        }
    }
}
