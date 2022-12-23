using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private GameObject[] m_EnemyTypes;
    [SerializeField] private Vector2 m_SpawnInterval = new Vector2(1, 1);
    [SerializeField] private int m_MaxEnemiesSpawned = 15;

    private bool m_ShouldSpawn = true;
    private List<GameObject> m_CurrentEnemies = new List<GameObject>();

    private void Start() {
        GameManager.Instance.OnGameOver += OnGameOver;
        StartCoroutine(SpawnEnemy());
    }

    private void OnGameOver() {
        m_ShouldSpawn = false;
    }

    private IEnumerator SpawnEnemy() {
        m_ShouldSpawn = true;
        while (m_ShouldSpawn) {
            if (m_CurrentEnemies.Count < m_MaxEnemiesSpawned) {
                var spawnPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length - 1)];
                var enemyToSpawn = m_EnemyTypes[Random.Range(0, m_EnemyTypes.Length - 1)];
                var enemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                m_CurrentEnemies.Add(enemy);
            }

            yield return new WaitForSeconds(Random.Range(m_SpawnInterval.x, m_SpawnInterval.y));
            m_CurrentEnemies.RemoveAll((e) => e == null || e.IsUnityNull());
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (var point in m_SpawnPoints) {
            Gizmos.DrawWireSphere(point.position, 1.0f);
        }
        Gizmos.color = Color.white;
    }
}
