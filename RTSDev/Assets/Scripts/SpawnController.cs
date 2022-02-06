using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public enum SpawnerState {SPAWNING, WAITING, COUNTING };

    [SerializeField]
    public class Wave
    {

    }

    public Wave[] waves;
}
