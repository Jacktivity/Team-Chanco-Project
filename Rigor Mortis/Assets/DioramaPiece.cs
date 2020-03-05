using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioramaPiece : MonoBehaviour
{
    [SerializeField] private GameObject min, max;

    public Vector3 Min => min.transform.position;
    public Vector3 Max => max.transform.position;
}
