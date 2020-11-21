using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    
    
    [Serializable]
    public class WavePattern {
        // public int numOfEnemy;
        // public GameObject enemyType;
        public WavePattern(List<GameObject> enemies, List<int> numOfEnemy)
        {
            this.enemies = enemies;
            this.numOfEnemy = numOfEnemy;
        }
        
        [SerializeField]
        public List<GameObject> enemies;
        [SerializeField]
        public List<int> numOfEnemy;
    }

    public WaveEnumerator waveEnumerator;
    private WavePattern pattern;
    private List<GameObject> spawnPoints = new List<GameObject>();
    private List<int> usedSpawnPoint = new List<int>();
    public GameObject player;
    public GameObject playerBase;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            spawnPoints.Add(spawnPoint);
        }
        LoadNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) //if there are no more enemies
        {
            Debug.Log("load wave");
            LoadNextWave();
        }
    }

    public void LoadNextWave()
    {
        pattern = waveEnumerator.NextWave();
        if (pattern == null)
        {
            return;
        }
        
        for (int i = 0; i < pattern.enemies.Count; i++)
        {
            Spawn(i);
        }
    }
    
    public void Spawn(int index)
    {
        int numOfEnemy = pattern.numOfEnemy[index];
        for (int i = 0; i < numOfEnemy; i++)
        {
            GameObject spawnPoint =  ChooseSpawnPoint();
            var enemy = Instantiate(pattern.enemies[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
            var controller = enemy.GetComponent<BasicEnemyMovement>();
            controller.strategy =  (TargetStrategy)UnityEngine.Random.Range(0, 3);
            controller.player = player.transform;
            controller.playerBase = playerBase.transform;
            controller.rigidbody = enemy.GetComponent<Rigidbody2D>();
        }
        usedSpawnPoint.Clear();
    }

    public GameObject ChooseSpawnPoint() {
        int numOfAll = spawnPoints.Count;
        int numOfUnused = numOfAll - usedSpawnPoint.Count;
        int r = UnityEngine.Random.Range(0, numOfUnused);
        int counter = 0;
        int assigned;
        for (assigned = r;
            usedSpawnPoint.Contains(assigned) || counter < numOfAll;
            counter++, assigned = (assigned + 1) % numOfAll);
        usedSpawnPoint.Add(assigned);
        return spawnPoints[assigned];
    }

    [Serializable]
    public class WaveEnumerator
    {
        [SerializeField] public WavePattern[] waves;
        private int index;

        public WaveEnumerator()
        {
            index = 0;
        }

        public WavePattern NextWave()
        {
            if (index == waves.Length)
            {
                return null;
            }
            return waves[index++];
        }

        public int NumWaves()
        {
            return waves.Length;
        }
        
    }
    
}
