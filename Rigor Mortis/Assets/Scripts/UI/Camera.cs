using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Camera _camera;
    [SerializeField]BoxCollider _collider;

    [SerializeField]private float speed = 25f;
    [SerializeField]private float scrollSpeed = 50f;

    [SerializeField] private float scrollOffset = 4;

    Vector3 posColliderExtents;
    Vector3 negColliderExtents;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        posColliderExtents = _collider.transform.position + _collider.bounds.extents;
        negColliderExtents = _collider.transform.position - _collider.bounds.extents;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Camera Movement
        Vector3 direction = _camera.transform.TransformDirection( Input.GetAxisRaw( "Horizontal" ), 0, Input.GetAxisRaw( "Vertical" ) );
        Vector3 scroll = _camera.transform.TransformDirection(0, -Input.GetAxis("Mouse ScrollWheel"), 0);

        if ((transform.position.x + direction.x) >= posColliderExtents.x || (transform.position.x + direction.x) <= negColliderExtents.x)
        {
            direction.x = 0;
        }
        if((transform.position.z + direction.z) >= posColliderExtents.z || (transform.position.z + direction.z) <= negColliderExtents.z)
        {
            direction.z = 0;
        }

        transform.position += speed * new Vector3( direction.x, 0, direction.z ) * Time.deltaTime;

        //Zoom Movement
        if (scroll.y != 0) {
            scroll.x = -(scroll.x / scrollOffset);
            scroll.z = -(scroll.z / scrollOffset);

            if((transform.position.x + scroll.x) >= posColliderExtents.x || (transform.position.x + scroll.x) <= negColliderExtents.x)
            {
                scroll.x = 0;
            }
            if ((transform.position.y + scroll.y) >= posColliderExtents.y || (transform.position.y + scroll.y) <= negColliderExtents.y)
            {
                scroll.y = 0;
            }
            if ((transform.position.z + scroll.z) >= posColliderExtents.z || (transform.position.z + scroll.z) <= negColliderExtents.z)
            {
                scroll.z = 0;
            }
            transform.position += scrollSpeed * new Vector3(scroll.x, scroll.y, scroll.z) * Time.fixedDeltaTime;
        }
    }
}
