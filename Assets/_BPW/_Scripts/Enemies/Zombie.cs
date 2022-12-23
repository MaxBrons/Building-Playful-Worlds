using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    [SerializeField] private List<SpriteStates> m_EnemySprites;
    private NavMeshAgent m_Agent;
    private SpriteRenderer m_Renderer;

    private void Start() {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        var stwp = m_Agent.destination;
        var signedRot = Mathf.Repeat(Mathf.Atan2(stwp.y - transform.position.y,
                                    stwp.x - transform.position.x) * Mathf.Rad2Deg - 90.0f, 360.0f);

        var spriteItem = m_EnemySprites.Find((e) => {
            if (e.Angle.y - e.Angle.x < 0) {
                return signedRot > e.Angle.x || signedRot < e.Angle.y;
            }
            return signedRot > e.Angle.x && signedRot < e.Angle.y;
        });

        Sprite newSprite = spriteItem.Sprite;

        if (newSprite) {
            m_Renderer.sprite = newSprite;
            m_Renderer.flipX = spriteItem.Flipped;
        }
    }
}
