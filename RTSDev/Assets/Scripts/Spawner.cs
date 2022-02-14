using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public class Spawner : MonoBehaviour
    {
        public enum SpawnerState { SPAWNING, WAITING, COUNTING };

        [System.Serializable]
        public class Wave
        {
            public string name;
            public Transform[] Enemies;
            public int[] count;
            public float rate;
        }

        private SpawnerState state = SpawnerState.COUNTING;
        public Wave[] waves;
        public int maxWaves = 10;
        public int waveNum;
        public float counter;

        public Transform firstWayPoint;

        IEnumerator SpawnWave(Wave wave)
        {
            state = SpawnerState.SPAWNING;
            Debug.Log("Wave Spawned " + wave.name);

            for (int i = 0; i < wave.count.Length; i++)
            {
                for (int j = 0; j < wave.count[i]; j++)
                {
                    SpawnEnemy(wave.Enemies[i]);
                    yield return new WaitForSeconds(1f / wave.rate);
                }
            }
            state = SpawnerState.WAITING;
            yield break;
        }

        private void Update()
        {
            switch (state)
            {
                case SpawnerState.COUNTING:
                    counter -= Time.deltaTime;
                    if (counter <= 0)
                    {
                        state = SpawnerState.SPAWNING;
                    }
                    break;
                case SpawnerState.SPAWNING:
                    waveNum += 1;
                    SpawnWave(waves[waveNum - 1]);
                    break;
                case SpawnerState.WAITING:
                    break;
            }
        }

        void SpawnEnemy(Transform enemy)
        {
            Debug.Log("Spawn enemy");
            Instantiate(enemy, transform.position, transform.rotation);
            enemy.gameObject.GetComponent<Units.Enemy.EnemyUnit>().currentWaypoint = firstWayPoint;
        }
    }
}

