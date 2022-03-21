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
            if (ActionFrame.instance.spawnQueue.Count>0)
            {
                Debug.Log($"Waiting for {ActionFrame.instance.spawnQueue[0].queue}");
                yield return new WaitForSeconds(ActionFrame.instance.spawnQueue[0].queue);
                ActionFrame.instance.SpawnObject();
                Debug.Log("spawn");
            }

            if (ActionFrame.instance.spawnQueue.Count > 0)
            {
                StartCoroutine(SpawnQueueTimer());
            }
        }
    }

}