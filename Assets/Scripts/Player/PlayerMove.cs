using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField] public float speed;
    [SerializeField] float initialSpeed;

    public float orbitRadius;
    [SerializeField] float orbitAngle;


    [Header("Components")]
    public Rigidbody rb;
    public Transform planet;
    public Transform orbitCenter;

    public Transform orbitPivot;

    [SerializeField] PlayerFuel playerFuel;
    private PlayerControls controls;

    [Header("Estados")]
    public bool isOrbit;
    public bool outFuel = false;
    public bool passedCloud = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
    void Update()
    {
        ExitOrbit();

        if(outFuel == true)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isOrbit && orbitCenter != null && !outFuel && playerFuel.inGame)
        {
            OrbitMove();
        }
        else if (outFuel == false && playerFuel.inGame == true && passedCloud == false)
        {
            rb.linearVelocity = (transform.up * initialSpeed) / 2;
            rb.useGravity = true;
        }
        else if(outFuel == false && playerFuel.inGame == true && passedCloud == true)
        {
            rb.linearVelocity = transform.up * speed;
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }
    }

    void OrbitMove()
    {
            float angularSpeed = speed / orbitRadius;

            orbitAngle -= angularSpeed * Time.fixedDeltaTime;

            // Orbit no plano YZ: usar eixo Z em vez de X
            Vector3 offset = new Vector3(
                0,
                Mathf.Sin(orbitAngle),
                Mathf.Cos(orbitAngle)
            ) * orbitRadius;

            Vector3 newPos = orbitCenter.position + offset;

            rb.MovePosition(newPos);

            // Tangente para movimento no plano YZ (derivada do offset em relação ao ângulo)
            Vector3 tangent = new Vector3(
                0,
                -Mathf.Cos(orbitAngle),
                Mathf.Sin(orbitAngle)
            ).normalized;

            transform.up = tangent;
            //transform.rotation = Quaternion.LookRotation(Vector3.left, tangent);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            Planet planetScript = other.GetComponentInParent<Planet>();

            orbitCenter = planetScript.orbitCenter;
            planet = other.transform;

            Vector3 surfacePoint = other.ClosestPoint(orbitPivot.position);

            Vector3 dir = surfacePoint - orbitCenter.position;

            orbitRadius = dir.magnitude;

            orbitAngle = Mathf.Atan2(dir.y, dir.z);

            isOrbit = true;

            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;

            //rotaciona a nava
            Vector3 euler = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x, 90f, euler.z);
        }

        if (other.gameObject.CompareTag("Planet2"))
        {
            playerFuel.gameOver = true;
            playerFuel.fuelSlider.value = 0;
            rb.useGravity = false;
        }

        if (other.gameObject.CompareTag("InitialSpeed"))
        {
            passedCloud = true;
        }
    }

    public void ExitOrbit()
    {
        if (controls.Player.Go.WasPressedThisFrame())
        {
            rb.useGravity = true;
            isOrbit = false;

            planet = null;
            orbitCenter = null;

        }
    }
}
