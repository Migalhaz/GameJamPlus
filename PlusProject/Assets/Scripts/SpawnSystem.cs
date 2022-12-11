using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("Settings")]
    private float tempoSpawn;
    [SerializeField] private List<Transform> m_SpawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> m_EnemyPrefab = new List<GameObject>();


    //private
    public float dataTempoSpawn = 0;
    void Start()
    {


    }

    private void Update()
    {
        tempoSpawn += Time.deltaTime;
        if (tempoSpawn >= dataTempoSpawn)
        {
            if (dataTempoSpawn > 2) dataTempoSpawn -= 0.1f;
            tempoSpawn = 0;
            int randomPos = Random.Range(0, m_SpawnPoints.Count);
            int randomEnemy = Random.Range(0, m_EnemyPrefab.Count);

            for (int i = 0; i < m_SpawnPoints.Count; i++)
            {
                Instantiate(m_EnemyPrefab[randomEnemy], m_SpawnPoints[i].position, m_SpawnPoints[i].rotation);
            }

        }
    }




}