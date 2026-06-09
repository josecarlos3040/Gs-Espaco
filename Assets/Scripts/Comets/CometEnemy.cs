using UnityEngine;

public class CometEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;

    [Header("Random Distance")]
    [SerializeField] float minDistance = 5f;
    [SerializeField] float maxDistance = 20f;

    Vector3 startPosition;
    Vector3 targetPosition;

    bool goingToTarget = true;

    void Start()
    {
        startPosition = transform.position;
        PickNewTarget();
    }

    void Update()
    {
        Vector3 destination;

        if (goingToTarget)
        {
            destination = targetPosition;
        }
        else
        {
            destination = startPosition;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            if (goingToTarget)
            {
                goingToTarget = false;
            }
            else
            {
                startPosition = transform.position;
                PickNewTarget();
                goingToTarget = true;
            }
        }
    }

    void PickNewTarget()
    {
        float distance = Random.Range(minDistance, maxDistance);

        bool moveOnZ;

        if (Random.value > 0.5f)
        {
            moveOnZ = true;
        }
        else
        {
            moveOnZ = false;
        }

        float direction;

        if (Random.value > 0.5f)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        Vector3 offset;

        if (moveOnZ)
        {
            offset = new Vector3(0f, 0f, distance * direction);
        }
        else
        {
            offset = new Vector3(0f, distance * direction, 0f);
        }

        targetPosition = startPosition + offset;
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Planet") || other.CompareTag("Planet2") || other.CompareTag("Moon") || other.CompareTag("Satelite"))
    {
        if (goingToTarget)
        {
            goingToTarget = false;
        }
        else
        {
            goingToTarget = true;
        }
    }
}
}