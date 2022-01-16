using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.UI.HUD
{

    public class ActionTimer : MonoBehaviour
    {
        public static ActionTimer instance = null;

        private void Awake()
        {
            instance = this;
        }

        public IEnumerator SpawnQueueTimer()
        {
            if (ActionFrame.instance.spawnTimers.Count>0)
            {
                Debug.Log($"Waiting for {ActionFrame.instance.spawnTimers[0]}");
                yield return new WaitForSeconds(ActionFrame.instance.spawnTimers[0]);
                ActionFrame.instance.spawnTimers.Remove(ActionFrame.instance.spawnTimers[0]);
            }

            if (ActionFrame.instance.spawnTimers.Count > 0)
            {
                StartCoroutine(SpawnQueueTimer());
            }
        }
    }

}