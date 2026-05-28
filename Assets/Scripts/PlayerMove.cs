using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField] float speed;

    [SerializeField]float orbitRadius;
    [SerializeField] float orbitAngle;


    [Header("Components")]
    public Rigidbody rb;
    public Transform planet;
    [SerializeField] PlayerFuel playerFuel;

    [Header("Estados")]
    public bool isOrbit;
    public bool outFuel = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }

    void Update()
    {
       ExitOrbit();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isOrbit && planet != null && outFuel == false && playerFuel.inGame == true)
        {
            OrbitMove();
        }
        else if(outFuel == false && playerFuel.inGame == true)
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
        if (isOrbit == true && planet != null)
        {
            float angularSpeed = speed / orbitRadius;

            orbitAngle -= angularSpeed * Time.fixedDeltaTime;

            Vector3 offset = new Vector3(
                Mathf.Cos(orbitAngle),
                Mathf.Sin(orbitAngle),
                0
            ) * orbitRadius;

            Vector3 newPos = planet.position + offset;

            rb.MovePosition(newPos);

            Vector3 tangent = new Vector3(
                Mathf.Sin(orbitAngle),
                -Mathf.Cos(orbitAngle),
                0
            ).normalized;

            transform.up = tangent;



        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            planet = other.gameObject.transform;


            Vector3 dir = transform.position - planet.position;

            orbitRadius = dir.magnitude;

            orbitAngle = Mathf.Atan2(dir.y, dir.x);

            isOrbit = true;

            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
        }
    
    }

    void ExitOrbit()
    {
        if (Input.anyKey)
        {
            rb.useGravity = true;
            isOrbit = false;

            planet = null;
  
        }
    }
}
