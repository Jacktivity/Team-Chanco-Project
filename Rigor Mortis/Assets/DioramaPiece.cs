using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioramaPiece : MonoBehaviour
{
    [SerializeField] private GameObject min, max, center;

    public Vector3 Min => min.transform.position;
    public Vector3 Max => max.transform.position;
    public Vector3 Center => center.transform.position;
}
