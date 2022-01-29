using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Game
{
    public class GameHandler : MonoBehaviour
    {

        private static GameHandler instance;
        [SerializeField] private Transform resourceTransform;
        [SerializeField] private Transform storageTransform;

        private void Awake()
        {
            instance = this;
        }

        private Transform GetResourceNode()
        {
            return resourceTransform;
        }

        private Transform GetStorageNode()
        {
            return storageTransform;
        }

        public static Transform GetResourceNode_Static()
        {
            return instance.GetResourceNode();
        }

        public static Transform GetStorageNode_Static()
        {
            return instance.GetStorageNode();
        }
    }
}


