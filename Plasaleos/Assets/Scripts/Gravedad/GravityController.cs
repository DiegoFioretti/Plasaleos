using UnityEngine;
using UnityEngine.Events;

public class GravityController : MonoBehaviour {
    static public GravityController Instance;
    public UnityEvent RestrictionChange;
    [SerializeField] private float force = 9.8f;
    [SerializeField] bool restricted = false;
    private Vector2 gravity;
    Vector2 forcedDirection;
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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start() {
        forcedDirection = Vector3.down;
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
        if (!restricted) {
            if (Mathf.Abs(angle) >= 35f) {
                gravity = new Vector2((gravity.x - gravity.y * Mathf.Sign(angle)),
                    (gravity.x * Mathf.Sign(angle) + gravity.y)) * rot45; //Matrix rotation
                //We multiply by the sign on angle on the sin term because sin(-45) < 0
            }
        } else {
            float maxAngle = Vector2.SignedAngle(gravity, forcedDirection);
            if (Mathf.Abs(angle) >= 25f && (maxAngle == 0f || //Check for angle not going over 35
                ((Mathf.Sign(angle) == Mathf.Sign(maxAngle) && Mathf.Abs(maxAngle) == 35f)))) {

                gravity = new Vector2(((gravity.x * cos35) - (gravity.y * sin35 * Mathf.Sign(angle))),
                    ((gravity.x * sin35 * Mathf.Sign(angle)) + (gravity.y * cos35))); //Matrix rotation
            }
        }
        gravity = gravity.normalized;
        Physics2D.gravity = gravity * force;
    }

    public void Restrict (Vector2 direction) {
        restricted = true;
        forcedDirection = direction.normalized;
        gravity = direction.normalized;
        Physics2D.gravity = gravity * force;
        RestrictionChange.Invoke();
    }

    public void Unrestric () {
        restricted = false;
        RestrictionChange.Invoke();
    }

    public Vector2 GetForcedDirection(){
        return forcedDirection;
    }
}