using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ShootingController : MonoBehaviour
{
    #region Fields

    // Private fields
    [SerializeField]private CinemachineVirtualCamera aimCamera;
    [SerializeField]private CinemachineVirtualCamera mainCamera;
    [SerializeField]private GameObject aimReticle;
    [SerializeField]private LayerMask aimColliderLayerMask = new LayerMask();
    private float shootCooldown = 0.4f; // Cooldown period in seconds

    
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs playerInputs;
    public Transform spawnBulletPosition;
    private float shootTimer = 0; // Timer to keep track of cooldown
    private Animator animator;


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

  
    private void Awake()
    {
        
        playerInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        shootTimer = shootCooldown;

        if (aimCamera == null || mainCamera == null)
        {
            Debug.LogError("Cameras are not assigned in ShootingController.");
            enabled = false; // Disable the script if cameras are not assigned
            return;
        }
        
    }
    
    private void Update()
    {
        
        SetAimCameraActive(playerInputs.aim);
        SetLookAtwithAim();

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        Shoot(playerInputs.Shoot);
    }
   
    private void Shoot(bool isShoot)
    {
        if (isShoot && shootTimer <= 0)
        {
            animator.SetTrigger("Shoot");
            animator.SetLayerWeight(2, 1); // Set the weight of the shooting animation layer to 1

            shootTimer = shootCooldown;
            playerInputs.Shoot = false;
        }
    }
    

    #endregion

    #region Private Methods

    private void SetAimCameraActive(bool isActive)
    {
        aimCamera.gameObject.SetActive(isActive);
        mainCamera.gameObject.SetActive(!isActive);
        thirdPersonController.SetRotateOnMove(!isActive);
        aimReticle.SetActive(isActive);
        animator.SetBool("Aim", isActive);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), isActive ? 1f : 0f, Time.deltaTime * 13f));
    
    }

    private void SetLookAtwithAim()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;

        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        // Rotate the character's forward direction towards the aim direction
        transform.forward = Vector3.RotateTowards(transform.forward, aimDirection, Time.deltaTime * 207, 0f);
    }
    #endregion
    
    #region Animation Event Methods
    // Method to be called from the animation event
    public void ShootFromAnimationEvent()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            Vector3 aimDirection = (raycastHit.point - spawnBulletPosition.position).normalized;

            Transform bulletTransform = BulletPooling.Instance.GetBullet(spawnBulletPosition.position);
            bulletTransform.position = spawnBulletPosition.position;
            bulletTransform.rotation = Quaternion.LookRotation(aimDirection);
            Bullet bullet = bulletTransform.GetComponent<Bullet>();

            shootTimer = shootCooldown;
            playerInputs.Shoot = false;
        }
    }
    
    public void ResetShootAnimation()
    {
        animator.ResetTrigger("Shoot");
        animator.SetLayerWeight(2, 0);
    }
    #endregion
}