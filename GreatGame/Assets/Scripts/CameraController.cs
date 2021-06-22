using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && string.Compare(hit.transform.gameObject.tag, "DynamicObject") == 0)
            {
                hit.transform.SetPositionAndRotation(new Vector2(2.41f, 3.64f), new Quaternion());
                hit.rigidbody.velocity = new Vector2();
                
            }
        }
    }
}
