using UnityEngine;
using UnityEngine.Events;

public class GravityController : MonoBehaviour {
    static public GravityController Instance;
    public UnityEvent ToRestricted;
    public UnityEvent ToUnrestricted;
    [SerializeField] private float force = 9.8f;
    [SerializeField] bool restricted = false;
    private Vector2 gravity;
    float rot45; //==sin(45)==cos(45)
    public bool Restricted {
        get { return restricted; }
        set {
            gravity = Vector3.down * force;
            restricted = value;
        }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start() {
        gravity = Vector3.down * force;
        Physics2D.gravity = gravity;
        rot45 = Mathf.Sin(45f * Mathf.Deg2Rad);
    }

    void FixedUpdate() {
        ChangeGravity();
    }

    private void ChangeGravity() {
        if (!restricted) {
            float angle = Vector2.SignedAngle(gravity, -transform.up);
            if (Mathf.Abs(angle) >= 35f) {
                gravity = new Vector2((gravity.x - gravity.y * Mathf.Sign(angle)),
                    (gravity.x * Mathf.Sign(angle) + gravity.y)) * rot45; //Matrix rotation
                //We multiply by the sign on angle on the sin term because sin(-45) < 0
            }
            gravity = gravity.normalized;
            Physics2D.gravity = gravity * force;
        }
    }

    public void Restrict (Vector2 direction) {
        ToRestricted.Invoke();
        restricted = true;
        gravity = direction.normalized;
        Physics2D.gravity = gravity * force;
    }

    public void Unrestric () {
        ToUnrestricted.Invoke();
        restricted = false;
    }
}