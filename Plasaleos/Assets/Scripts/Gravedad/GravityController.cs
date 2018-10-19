using UnityEngine;

public class GravityController : MonoBehaviour {

    [SerializeField] private float force = 9.8f;
    private Vector2 gravity;
    float sin35;
    float cos35;

    void Awake() {
        gravity = Vector3.down * force;
        Physics2D.gravity = gravity;
        sin35 = Mathf.Sin(35f * Mathf.Deg2Rad);
        cos35 = Mathf.Cos(35f * Mathf.Deg2Rad);
        print(gravity);
    }

    void FixedUpdate() {
        ChangeGravity();
    }

    private void ChangeGravity() {
        float angle = Vector2.SignedAngle(gravity, -transform.up);
        float maxAngle = Vector2.SignedAngle(gravity, Vector3.down);
        print(maxAngle);
        if (Mathf.Abs(angle) >= 30f && (maxAngle == 0f || //Check for angle not going over 35
                ((Mathf.Sign(angle) == Mathf.Sign(maxAngle) && Mathf.Abs(maxAngle) == 35f)))) {

            gravity = new Vector2(((gravity.x * cos35) - (gravity.y * sin35 * Mathf.Sign(angle))),
                ((gravity.x * sin35 * Mathf.Sign(angle)) + (gravity.y * cos35))); //Matrix rotation
            //We multiply by the sign on angle on the sin term because sin(angle < 0) < 0
            gravity = gravity.normalized;
            print(gravity);
            Physics2D.gravity = gravity * force;
        }
    }
}