using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    //[SerializeField]BoxCollider _collider;
    [SerializeField] private GameObject boomArm;
    [SerializeField] private Vector3 movementSpeed = new Vector3(25, 1, 25);
    [SerializeField] private Vector2 rotationSpeed = new Vector2(0.1f, 1f);
    [SerializeField] private int minXRotation, maxXRotation;
    
    private float boomLerp = 0f;
    private Vector3 posColliderExtents, negColliderExtents, maxBoomLength, minBoomLength, previousMousePos;

    // Start is called before the first frame update
    void Start()
    {
        GridManager.mapGenerated += GenerateCameraBoundary;
        _camera = GetComponent<Camera>();
        maxBoomLength = transform.localPosition;
        minBoomLength = transform.localPosition * 0.1f;
        //posColliderExtents = _collider.transform.position + _collider.bounds.extents;
        //negColliderExtents = _collider.transform.position - _collider.bounds.extents;
        previousMousePos = Input.mousePosition;
    }

    private void GenerateCameraBoundary(object sender, BlockScript[] e)
    {
        var mapOrdered = e.OrderBy(s => new Vector2(s.coordinates.x, s.coordinates.z).magnitude);
        var topLeft = mapOrdered.First();
        var bottomRight = mapOrdered.Last();

        posColliderExtents = topLeft.gameObject.transform.position;
        negColliderExtents = bottomRight.gameObject.transform.position;


        boomArm.transform.position = mapOrdered.First(t => t.placeable).transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Move boom arm back to point where camera will not clip with other object
        //collision.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
        MoveBoomLength();
        RotateCameraArm();
    }

    private void RotateCameraArm()
    {
        var motion = Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            var deltaPosition = previousMousePos - motion;

            boomArm.transform.Rotate(new Vector3(0, -deltaPosition.x * rotationSpeed.x, 0), Space.World);

            var xRotation = deltaPosition.y * rotationSpeed.y;
            xRotation = ClampXRotation(xRotation);

            boomArm.transform.Rotate(new Vector3(xRotation, 0, 0), Space.Self);
        }

        previousMousePos = motion;
    }

    private float ClampXRotation(float xRotation)
    {
        var currentXRotation = boomArm.transform.rotation.eulerAngles.x;

        var rotation = xRotation + currentXRotation;

        if (rotation > maxXRotation && rotation < minXRotation)
        {
            if (rotation <= maxXRotation + ((minXRotation - maxXRotation) / 2))
            {
                xRotation = maxXRotation - currentXRotation;
            }
            else
            {
                xRotation = minXRotation - currentXRotation;
            }
        }

        return xRotation;
    }

    private void MoveBoomLength()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * movementSpeed.y + boomLerp;

        if (scroll < 0)
            scroll = 0;
        else if (scroll > 1)
            scroll = 1;

        boomLerp = scroll;
        transform.localPosition = Vector3.Lerp(maxBoomLength, minBoomLength, boomLerp);
    }

    private void MoveCamera()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        //Debug.Log("Sin(" + (int)boomArm.transform.rotation.eulerAngles.y + ")" + Mathf.Sin((int)boomArm.transform.rotation.eulerAngles.y));


        var testX = (Mathf.Sin(boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * y) + (Mathf.Cos(-boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * x);
        var testY = (Mathf.Cos(boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * y) + (Mathf.Sin(-boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * x);

        var xInput = (testX * movementSpeed.x * Time.deltaTime) + boomArm.transform.position.x;
        var yInput = (testY * movementSpeed.z * Time.deltaTime) + boomArm.transform.position.z;
       

        if (xInput <= posColliderExtents.x)
            xInput = posColliderExtents.x;
        else if (xInput >= negColliderExtents.x)
            xInput = negColliderExtents.x;

        if (yInput <= posColliderExtents.z)
            yInput = posColliderExtents.z;
        else if (yInput >= negColliderExtents.z)
            yInput = negColliderExtents.z;

        boomArm.transform.position = new Vector3(xInput, boomArm.transform.position.y, yInput);
    }
}
