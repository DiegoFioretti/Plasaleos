using UnityEngine;

public class GravityController : MonoBehaviour {

    [SerializeField] private float force = 9.8f;
    private Vector2 gravity;
    float rot45; //==sin(45)==cos(45)

    void Start() {
        gravity = -transform.up * force;
        Physics2D.gravity = gravity;
        rot45 = Mathf.Sin(45f);
    }

    void FixedUpdate() {
        ChangeGravity();
    }

    private void ChangeGravity() {
        float angle = Vector2.SignedAngle(gravity, -transform.up);
        if (Mathf.Abs(angle) >= 35f) {
            gravity = new Vector2((gravity.x - gravity.y) * rot45,
                (gravity.x + gravity.y) * rot45);
            gravity = gravity.normalized * force;
            Physics2D.gravity = gravity;
        }
    }
}