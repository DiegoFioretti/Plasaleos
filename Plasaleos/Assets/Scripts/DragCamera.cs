using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    public float speed = 0.1F;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
        }
        if(transform.position.x < left.position.x)
        {
            transform.position = new Vector3(left.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > right.position.x)
        {
            transform.position = new Vector3(right.position.x, transform.position.y, transform.position.z);
        }
    }
}