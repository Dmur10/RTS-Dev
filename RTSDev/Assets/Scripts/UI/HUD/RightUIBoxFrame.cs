using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightUIBoxFrame : MonoBehaviour
{
    public static RightUIBoxFrame instance = null;

    [SerializeField]private List<Image> imageQueue;
}
