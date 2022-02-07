using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public enum SpawnerState {SPAWNING, WAITING, COUNTING };

    [SerializeField]
    public class Wave
    {
        public string name;
        public Transform[] Enemies;
        public int[] count;
        public float rate;
    }

    private SpawnerState state = SpawnerState.COUNTING;
    public Wave[] waves;

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnerState.SPAWNING;
        Debug.Log("Wave Spawned " + wave.name);

        for (int i = 0; i < wave.count.Length; i++)
        {
            for(int j = 0; j < wave.count[i]; j++)
            {
                SpawnEnemy(wave.Enemies[i]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        state = SpawnerState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawn enemy");
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
