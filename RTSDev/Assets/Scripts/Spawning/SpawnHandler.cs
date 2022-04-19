using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Spawning
{
    public class SpawnHandler : MonoBehaviour
    {
        public static SpawnHandler instance;

        public enum SpawnerState { SPAWNING, WAITING, COUNTING };

        public Transform spawners;
        public Transform WaveEnemies;

        private SpawnerState state = SpawnerState.COUNTING;

        private float counter;
        private int currentWave;

        [SerializeField]
        private int TotalWaves;

        [SerializeField]
        private float[] WaveCounter;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            counter = WaveCounter[0];
            currentWave = 1;
        }

        private void Update()
        {
            if (AllWavesCompleted())
            {
                return;
            }
            switch (state)
            {
                case SpawnerState.COUNTING:
                    counter -= Time.deltaTime;
                    if (counter <= 0)
                    {
                        StartCoroutine(SpawnWave());
                    }
                    break;
                case SpawnerState.SPAWNING:
                    break;
                case SpawnerState.WAITING:
                    if (WaveComplete())
                    {
                        counter = WaveCounter[currentWave];
                        currentWave++;
                        state = SpawnerState.COUNTING;
                    }
                    break;
            }
        }

        private bool WaveComplete()
        {
            if(WaveEnemies.childCount == 0)
            {
                return true;
            }
            return false;
            /*foreach(Transform spawnerTF in spawners)
            {
                Spawner spawner = spawnerTF.GetComponent<Spawner>();
                if (spawner.EnemiesAlive())
                {
                    return false;
                }
            }
            return true;*/
        }

        public bool AllWavesCompleted()
        {
            if(currentWave > TotalWaves)
            {
                return true;
            }
            return false;
        }

        public string GetWaveInfo()
        {
            return "Wave " + currentWave.ToString() + ": " + ((int)counter).ToString();
        }

        IEnumerator SpawnWave()
        {
            state = SpawnerState.SPAWNING;
            Debug.Log("Wave Started");

            foreach(Transform spawnerTF in spawners) //for (int i = 0; i < spawners.Length; i++)
            {
                Spawner spawner = spawnerTF.GetComponent<Spawner>();
                StartCoroutine(spawner.SpawnWave(spawner.waves[currentWave-1]));
            }
            state = SpawnerState.WAITING;
            yield break;
        }
    }
}
