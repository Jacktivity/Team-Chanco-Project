using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Camera _camera;
    [SerializeField]BoxCollider _collider;

    [SerializeField]private float speed = 25f;
    [SerializeField]private float scrollSpeed = 300f;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = _camera.transform.TransformDirection( Input.GetAxisRaw( "Horizontal" ), 0, Input.GetAxisRaw( "Vertical" ) );

        Vector3 posColliderExtents = _collider.transform.position + _collider.bounds.extents;
        Vector3 negColliderExtents = _collider.transform.position - _collider.bounds.extents;

        if(transform.position.x >= posColliderExtents.x || transform.position.x <= negColliderExtents.x) {
            transform.position += speed * new Vector3( direction.x, 0, direction.z ) * Time.deltaTime;
        } else {
            Debug.Log(transform.position.x  + " " + posColliderExtents.x + negColliderExtents.x );
        }

        /*if (Input.GetAxisRaw( "Horizontal" ) != 0 || Input.GetAxisRaw( "Vertical" ) != 0) {
            transform.position += speed * new Vector3( direction.x, 0, direction.z ) * Time.deltaTime;
        }*/

        if (Input.GetAxis( "Mouse ScrollWheel" ) != 0) {
            transform.position += scrollSpeed * new Vector3( 0, -Input.GetAxis( "Mouse ScrollWheel" ), 0 ) * Time.deltaTime;
        }
    }
}
