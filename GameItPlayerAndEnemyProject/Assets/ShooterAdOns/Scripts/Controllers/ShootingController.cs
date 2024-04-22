using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ShootingController : MonoBehaviour
{
    #region Fields

    // Private fields
    private CinemachineVirtualCamera aimCamera;
    private CinemachineVirtualCamera mainCamera;
    private GameObject aimReticle;
    private LayerMask aimColliderLayerMask = new LayerMask();
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs playerInputs;

    #endregion

    #region Properties

    // Public properties
    public StarterAssetsInputs PlayerInputs
    {
        get { return playerInputs; }
        set { playerInputs = value; }
    }

    #endregion

    #region Unity Methods

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the StarterAssetsInputs component
        playerInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        // Check if cameras are assigned
        CheckCameraAssigned(aimCamera, "Aim camera");
        CheckCameraAssigned(mainCamera, "Main camera");
    }

    // Update is called every frame
    private void Update()
    {
        // Check if aimCamera is assigned
        if (aimCamera == null)
        {
            return;
        }

        // Enable or disable the aim camera based on the aim input
        SetAimCameraActive(playerInputs.aim);

        SetLookAtwithAim();
    }

    #endregion

    #region Private Methods

    // Checks if a camera is assigned and logs an error if not
    private void CheckCameraAssigned(CinemachineVirtualCamera camera, string cameraName)
    {
        if (camera == null)
        {
            Debug.LogError($"{cameraName} is not assigned in ShootingController.");
        }
    }

    // Sets the active state of the aim camera and starts the ShowReticle coroutine
    private void SetAimCameraActive(bool isActive)
    {
        aimCamera.gameObject.SetActive(isActive);
        mainCamera.gameObject.SetActive(!isActive);
        thirdPersonController.SetRotateOnMove(!isActive);
        aimReticle.SetActive(isActive);
    }

    // Sets the rotation of the character based on the mouse position
    private void SetLookAtwithAim()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (playerInputs.aim) {

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3. Lerp(transform. forward, aimDirection, Time. deltaTime * 207);

        }

    }
    #endregion
}