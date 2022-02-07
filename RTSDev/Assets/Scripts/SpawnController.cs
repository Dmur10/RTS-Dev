using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public enum SpawnerState {SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] Enemies;
        public int[] count;
        public float rate;
        //public int spawnWidth;
    }

    private SpawnerState state = SpawnerState.COUNTING;
    public Wave[] waves;
    private Transform[] line;

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnerState.SPAWNING;
        Debug.Log("Wave Spawned " + wave.name);

        for (int i = 0; i < wave.count.Length; i++)
        {
            for(int j = 0; j < wave.count[i]; j++)
            {
                SpawnEnemy(wave.Enemies[j]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        state = SpawnerState.WAITING;
        yield break;
    }

    /*void SpawnLine()
    {
        for(int i = 0; i < line.Length; i++)
        {
            SpawnEnemy(line[i]);
        }
    }*/

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawn enemy");
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
