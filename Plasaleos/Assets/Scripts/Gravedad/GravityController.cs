using UnityEngine;

public class GravityController : MonoBehaviour {

    [SerializeField] private float force = 9.8f;
    [SerializeField] bool restricted = false;
    private Vector2 gravity;
    float rot45; //==sin(45)==cos(45)
    float sin35;
    float cos35;
    public bool Restricted {
        get { return restricted; }
        set {
            gravity = Vector3.down * force;
            restricted = value;
        }
    }

    void Start() {
        gravity = Vector3.down * force;
        Physics2D.gravity = gravity;
        rot45 = Mathf.Sin(45f * Mathf.Deg2Rad);
        sin35 = Mathf.Sin(35f * Mathf.Deg2Rad);
        cos35 = Mathf.Cos(35f * Mathf.Deg2Rad);
    }

    void FixedUpdate() {
        ChangeGravity();
    }

    private void ChangeGravity() {
        float angle = Vector2.SignedAngle(gravity, -transform.up);
        if (restricted) {
            gravity = Vector2.down;
            // float maxAngle = Vector2.SignedAngle(gravity, Vector3.down);
            // if (Mathf.Abs(angle) >= 25f && (maxAngle == 0f || //Check for angle not going over 35
            //         ((Mathf.Sign(angle) == Mathf.Sign(maxAngle) && Mathf.Abs(maxAngle) == 35f)))) {

            //     gravity = new Vector2(((gravity.x * cos35) - (gravity.y * sin35 * Mathf.Sign(angle))),
            //         ((gravity.x * sin35 * Mathf.Sign(angle)) + (gravity.y * cos35))); //Matrix rotation
            //     //We multiply by the sign on angle on the sin term because sin(angle < 0) < 0
            // }
        } else {
            if (Mathf.Abs(angle) >= 35f) {
                gravity = new Vector2((gravity.x - gravity.y * Mathf.Sign(angle)),
                    (gravity.x * Mathf.Sign(angle) + gravity.y)) * rot45; //Matrix rotation
                //We multiply by the sign on angle on the sin term because sin(-45) < 0
            }
        }
        gravity = gravity.normalized;
        Physics2D.gravity = gravity * force;
    }
}