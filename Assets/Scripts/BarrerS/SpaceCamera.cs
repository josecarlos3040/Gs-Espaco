using Unity.Cinemachine;
using UnityEngine;

public class SpaceCamera : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;

    public CinemachineCamera orbitCam;

    [SerializeField] float orbitCamOffsetY;
    [SerializeField] float orbitCamOffsetZ;

    [SerializeField] float moveSpeed = 50f;

    bool wasOrbit;

    Vector3 targetPosition;
    bool movingCamera;





    void Update()
    {
        // Acabou de entrar em órbita
        if (playerMove.isOrbit && !wasOrbit)
        {
            MoveCamera();
        }

        if (movingCamera)
        {
            orbitCam.transform.position = Vector3.MoveTowards(
                orbitCam.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(orbitCam.transform.position, targetPosition) < 0.01f)
            {
                orbitCam.transform.position = targetPosition;
                movingCamera = false;
            }
        }

        wasOrbit = playerMove.isOrbit;
    }

    void MoveCamera()
    {
        targetPosition = orbitCam.transform.position;

        targetPosition.y = playerMove.orbitCenter.position.y + orbitCamOffsetY;

        targetPosition.z = Mathf.Clamp(
            playerMove.orbitCenter.position.z + orbitCamOffsetZ,
            125f,
            145f
        );

        movingCamera = true;
    }

    public void MoveToInitialPosition()
    {
        movingCamera = false;

        orbitCam.transform.position = new Vector3(
            -54.0999985f,
            209.950012f,
            135.820007f
        );

        wasOrbit = false;
    }
}