using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float m_StoppingDistance = 1f;
    [SerializeField] private float m_PatrolRadius = 5.0f;
    private Vector2 m_CurrentPatrolPoint;

    public override void OnEnter()
    {
        m_CurrentPatrolPoint = (Random.insideUnitCircle * m_PatrolRadius) + (Vector2)transform.position;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        //Transition
        if(Vector3.Distance(transform.position, m_CurrentPatrolPoint) < m_StoppingDistance)
        {
            Controller.SwitchState(typeof(IdleState));
        }

        //Move
        Controller.Controller.MoveToPosition(new Vector3(m_CurrentPatrolPoint.x, m_CurrentPatrolPoint.y, 0.1f));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }
}
