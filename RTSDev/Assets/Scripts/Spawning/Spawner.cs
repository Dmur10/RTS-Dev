using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Spawning
{
    public class Spawner : MonoBehaviour
    {

        [System.Serializable]
        public class Wave
        {
            public string name;
            public Transform[] Enemies;
            public int[] count;
            public float rate;
        }

        public Wave[] waves;

        [SerializeField]
        private Transform firstWayPoint;

        private void Awake()
        {
            //waveEnemies = new List<Transform>();
        }

        public IEnumerator SpawnWave(Wave wave)
        {
            Debug.Log("Wave Spawned " + wave.name);

            for (int i = 0; i < wave.count.Length; i++)
            {
                for (int j = 0; j < wave.count[i]; j++)
                {
                    SpawnEnemy(wave.Enemies[i]);
                    yield return new WaitForSeconds(1f / wave.rate);
                }
            }
            yield break;
        }

       /* public bool EnemiesAlive()
        {
            Debug.Log(waveEnemies.Count);
            if(waveEnemies.Count > 0)
            {
                return true;
            }
            return false;
        }*/

        private void SpawnEnemy(Transform enemy)
        {
            Debug.Log("Spawn enemy");
            GameObject spawnedEnemy = Instantiate(enemy.gameObject, transform.position, transform.rotation);
            Units.Enemy.EnemyUnit eu = spawnedEnemy.GetComponent<Units.Enemy.EnemyUnit>();
            eu.waypoint = firstWayPoint;
            eu.transform.parent = GameObject.Find("WaveEnemies").transform;//"E" + eu.unitType.type.ToString()).transform;
        }
    }
}

