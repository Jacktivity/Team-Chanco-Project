using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    //[SerializeField]BoxCollider _collider;
    [SerializeField] private Transform boomArm;
    [SerializeField] private Vector3 movementSpeed = new Vector3(25, 1, 25);
    [SerializeField] private Vector2 rotationSpeed = new Vector2(0.1f, 1f);
    [SerializeField] private int minXRotation, maxXRotation;
    public Dictionary<Vector2, float> yPositionDict;
    private float boomLerp = 0f;
    private Vector3 posColliderExtents, negColliderExtents, maxBoomLength, minBoomLength, previousMousePos;

    // Start is called before the first frame update
    void Start()
    {
        boomArm = transform.parent;
        GridManager.mapGenerated += GenerateCameraBoundary;

        yPositionDict = new Dictionary<Vector2, float>();
        _camera = GetComponent<Camera>();
        maxBoomLength = transform.localPosition;
        minBoomLength = transform.localPosition * 0.1f;
        //posColliderExtents = _collider.transform.position + _collider.bounds.extents;
        //negColliderExtents = _collider.transform.position - _collider.bounds.extents;
        previousMousePos = Input.mousePosition;
    }

    private void OnDestroy()
    {
        GridManager.mapGenerated -= GenerateCameraBoundary;
    }

    public void GenerateCameraBoundary(object sender, BlockScript[] e)
    {
        var mapOrdered = e.OrderBy(s => new Vector2(s.coordinates.x, s.coordinates.z).magnitude);
        var topLeft = mapOrdered.First();
        var bottomRight = mapOrdered.Last();

        posColliderExtents = topLeft.gameObject.transform.position;
        negColliderExtents = bottomRight.gameObject.transform.position;

        boomArm = transform.parent;

        foreach (var tile in e)
        {
            var position = new Vector2(tile.coordinates.x, tile.coordinates.z);

            if(yPositionDict.ContainsKey(position))
            {
                var current = yPositionDict[position];
                if (tile.transform.position.y > current)
                    yPositionDict[position] = tile.transform.position.y;
            }
            else
            {
                yPositionDict.Add(position, tile.transform.position.y);
            }
        }

        if (mapOrdered.Any(s => s.placeable))
            boomArm.transform.position = mapOrdered.First(t => t.placeable).transform.position;
        else
            boomArm.transform.position = mapOrdered.First().transform.position;


    }

    private void OnCollisionStay(Collision collision)
    {
        //Requires tidyup
        var collisionLength = collision.collider.transform.position - transform.position;
        if(collisionLength.z < 0)
        {
            var liftCamera = 10f;
            ClampXRotation(liftCamera);

            if (boomLerp - 0.01f >= 0)
                boomLerp -= 0.01f;
            else if (liftCamera > 0)
            {
                boomArm.transform.Rotate(new Vector3(liftCamera, 0, 0), Space.Self);
            }
        }
        else
        {
            var liftCamera = 10f;
            ClampXRotation(liftCamera);

            if (liftCamera > 0)
                boomArm.transform.Rotate(new Vector3(liftCamera, 0, 0), Space.Self);
            else if (boomLerp - 0.01f >= 0)
                boomLerp -= 0.01f;
        }
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
        var z = Input.GetAxis("Vertical");

        //Debug.Log("Sin(" + (int)boomArm.transform.rotation.eulerAngles.y + ")" + Mathf.Sin((int)boomArm.transform.rotation.eulerAngles.y));


        var testX = (Mathf.Sin(boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * z) + (Mathf.Cos(-boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * x);
        var testY = (Mathf.Cos(boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * z) + (Mathf.Sin(-boomArm.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * x);

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

        var y = boomArm.transform.position.y;

        var rawPosition = new Vector2(boomArm.transform.position.x, boomArm.transform.position.z);
        var currentPosition = new Vector2((int)rawPosition.x, (int)rawPosition.y);

        if (yPositionDict.ContainsKey(currentPosition))
        {
            y = yPositionDict[currentPosition];
            Vector2 lerpYPosition = currentPosition;

            //Edit the value of the lerpYPosition to a connecting block
            if (currentPosition.x > boomArm.transform.position.x)
                lerpYPosition.x++;
            else if (currentPosition.x < boomArm.transform.position.x)
                lerpYPosition.x--;

            if (currentPosition.y > boomArm.transform.position.z)
                lerpYPosition.y++;
            else if (currentPosition.y < boomArm.transform.position.z)
                lerpYPosition.y--;


            //Ensures that a lerpYPosition is a valid key
            if (yPositionDict.ContainsKey(lerpYPosition) == false)
            {
                if (yPositionDict.ContainsKey(new Vector2(lerpYPosition.x, currentPosition.y)))
                {
                    lerpYPosition = new Vector2(lerpYPosition.x, currentPosition.y);
                }
                else if (yPositionDict.ContainsKey(new Vector2(currentPosition.x, lerpYPosition.y)))
                {
                    lerpYPosition = new Vector2(currentPosition.x, lerpYPosition.y);
                }
                else
                    lerpYPosition = currentPosition;
            }

            if(currentPosition != lerpYPosition)
            {
                float alpha;

                if (currentPosition.magnitude > lerpYPosition.magnitude)
                {
                    alpha = (currentPosition - rawPosition).magnitude / ((currentPosition - lerpYPosition).magnitude);
                }
                else
                {
                    alpha = (lerpYPosition - rawPosition).magnitude / ((currentPosition - lerpYPosition).magnitude);
                }

                y = Mathf.Lerp(yPositionDict[currentPosition], yPositionDict[lerpYPosition], 1 - alpha);
            }
        }

        boomArm.transform.position = new Vector3(xInput, y, yInput);

    }
}
