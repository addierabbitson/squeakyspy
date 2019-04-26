using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

[AddComponentMenu("First Person AIO")]
public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Input Settings

    #endregion

    #region Look Settings
    [Header("Mouse Rotation Settings")]

    [Space(8)]
    public bool enableCameraMovement;
    public Vector2 rotationRange = new Vector2(170, Mathf.Infinity);
    [Range(0.01f, 100)] public float mouseSensitivity = 10f;
    [Range(0.01f, 100)] public float dampingTime = 0.2f;
    public bool relativeMotionMode = true;
    public bool lockAndHideMouse = false;
    public Transform playerCamera;
    public bool enableCameraShake = false;
    internal Vector3 cameraStartingPosition;


    [SerializeField] private bool autoCrosshair;

    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Vector3 originalRotation;
    [Space(15)]

    #endregion

    #region Movement Settings
    [Header("Movement Settings")]
    [Space(8)]

    public bool playerCanMove = true;
    [SerializeField] private bool walkByDefault = true;
    public float walkSpeed = 4f;
    [Range(0.1f, 20)] public float sprintSpeed = 8f;
    [Range(0.1f, 15)] public float jumpPower = 5f;
    public bool canHoldJump;
    [SerializeField] private bool useStamina = true;
    [SerializeField] [Range(0.1f, 9)] private float staminaDepletionMultiplier = 2f;
    [SerializeField] [Range(0, 100)] private float Stamina = 50;
    [HideInInspector] public float speed;
    private float backgroundStamina;
    [SerializeField] private bool useCrouch = true;
    [System.Serializable]
    public class CrouchModifiers
    {
        [SerializeField] public string CrouchInputAxis;
        [SerializeField] [Range(0.1f, 4f)] internal float walkSpeedWhileCrouching = 2f;
        [SerializeField] [Range(0.1f, 8f)] internal float sprintSpeedWhileCrouching = 2f;
        [SerializeField] [Range(0f, 5f)] internal float jumpPowerWhileCrouching = 0f;
        [SerializeField] internal bool useExternalModifier = false;
        internal float defaultWalkSpeed;
        internal float defaultSprintSpeed;
        internal float defaultStrafeSpeed;
        internal float defaultJumpPower;
        [HideInInspector] public float walkSpeed_External;
        [HideInInspector] public float sprintSpeed_External;
        [HideInInspector] public float jumpPower_External;
    }
    public CrouchModifiers _crouchModifiers = new CrouchModifiers();
    [System.Serializable]
    public class FOV_Kick
    {
        [SerializeField] internal bool useFOVKick = false;
        [SerializeField] [Range(0, 10)] internal float FOVKickAmount = 4;
        [SerializeField] [Range(0.01f, 5)] internal float changeTime = 0.1f;
        [SerializeField] internal AnimationCurve KickCurve;
        internal bool fovKickReady = true;
        internal float fovStart;
    }
    public FOV_Kick fOVKick = new FOV_Kick();
    [System.Serializable]
    public class AdvancedSettings
    {
        [Range(0.1f, 100)] public float gravityMultiplier = 1.0f;
        public PhysicMaterial zeroFrictionMaterial;
        public PhysicMaterial highFrictionMaterial;
    }
    [SerializeField] private AdvancedSettings advanced = new AdvancedSettings();
    private CapsuleCollider capsule;
    private const float jumpRayLength = 0.7f;
    public bool IsGrounded { get; private set; }
    Vector2 inputXY;
    public bool isCrouching { get; private set; }
    bool isSprinting = false;

    [HideInInspector] public Rigidbody fps_Rigidbody;

    #endregion

    #region BETA Settings
    [System.Serializable]
    public class BETA_SETTINGS
    {

    }

    [Space(15)]
    [Space(5)]
    public BETA_SETTINGS betaSettings = new BETA_SETTINGS();

    #endregion

    #endregion

    private void Awake()
    {
        #region Look Settings - Awake
        originalRotation = transform.localRotation.eulerAngles;

        #endregion 

        #region Movement Settings - Awake
        capsule = GetComponent<CapsuleCollider>();
        IsGrounded = true;
        isCrouching = false;
        fps_Rigidbody = GetComponent<Rigidbody>();
        _crouchModifiers.defaultWalkSpeed = walkSpeed;
        _crouchModifiers.defaultSprintSpeed = sprintSpeed;
        _crouchModifiers.defaultStrafeSpeed = walkSpeed;
        _crouchModifiers.defaultJumpPower = jumpPower;
        #endregion

        #region BETA_SETTINGS - Awake

        #endregion

    }

    private void Start()
    {
        #region Look Settings - Start

        cameraStartingPosition = playerCamera.localPosition;
        if (lockAndHideMouse) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        #endregion

        #region Movement Settings - Start
        backgroundStamina = Stamina;

        #endregion

        #region BETA_SETTINGS - Start
        fOVKick.fovStart = playerCamera.GetComponent<Camera>().fieldOfView;
        #endregion
    }

    private void Update()
    {
        #region Look Settings - Update

        if (enableCameraMovement)
        {
            float mouseXInput;
            float mouseYInput;
            if (relativeMotionMode)
            {

                mouseXInput = Input.GetAxis("Mouse Y");
                mouseYInput = Input.GetAxis("Mouse X");
                if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
                if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
                targetAngles.y += mouseYInput * mouseSensitivity;
                targetAngles.x += mouseXInput * mouseSensitivity;
                targetAngles.y = Mathf.Clamp(targetAngles.y, -0.5f * rotationRange.y, 0.5f * rotationRange.y);
                targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * rotationRange.x, 0.5f * rotationRange.x);
            }
            else
            {

                mouseXInput = Input.mousePosition.y;
                mouseYInput = Input.mousePosition.x;
                targetAngles.y = Mathf.Lerp(rotationRange.y * -0.5f, rotationRange.y * 0.5f, mouseXInput / Screen.width);
                targetAngles.x = Mathf.Lerp(rotationRange.x * -0.5f, rotationRange.x * 0.5f, mouseXInput / Screen.height);
            }
        }
        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
        playerCamera.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x, 0, 0);
        transform.localRotation = Quaternion.Euler(0, followAngles.y + originalRotation.y, 0);


        #endregion

        #region Movement Settings - Update

        #endregion

        #region Headbobbing Settings - Update

        #endregion

        #region BETA_SETTINGS - Update

        #endregion
    }

    private void FixedUpdate()
    {
        #region Look Settings - FixedUpdate

        #endregion

        #region Movement Settings - FixedUpdate

        bool wasWalking = !isSprinting;
        if (useStamina)
        {
            if (backgroundStamina > 0) { if (!isCrouching) { isSprinting = Input.GetKey(KeyCode.LeftShift); } }
            if (isSprinting == true && backgroundStamina > 0) { backgroundStamina -= staminaDepletionMultiplier; } else if (backgroundStamina < Stamina && !Input.GetKey(KeyCode.LeftShift)) { backgroundStamina += staminaDepletionMultiplier / 2; }
        }
        else { isSprinting = Input.GetKey(KeyCode.LeftShift); }

        float inrSprintSpeed;
        inrSprintSpeed = sprintSpeed;

        speed = walkByDefault ? isCrouching ? walkSpeed : (isSprinting ? inrSprintSpeed : walkSpeed) : (isSprinting ? walkSpeed : inrSprintSpeed);
        Ray ray = new Ray(transform.position, -transform.up);
        if (IsGrounded || fps_Rigidbody.velocity.y < 0.1)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength);
            float nearest = float.PositiveInfinity;
            IsGrounded = false;
            for (int i = 0; i < hits.Length; i++)
            {
                if (!hits[i].collider.isTrigger && hits[i].distance < nearest)
                {
                    IsGrounded = true;
                    nearest = hits[i].distance;
                }
            }
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        inputXY = new Vector2(horizontalInput, verticalInput);
        if (inputXY.magnitude > 1) { inputXY.Normalize(); }
        Vector3 dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeed;
        float yv = fps_Rigidbody.velocity.y;
        bool didJump = canHoldJump ? Input.GetButton("Jump") : Input.GetButtonDown("Jump");

        if (IsGrounded && didJump && jumpPower > 0)
        {
            yv += jumpPower;
            IsGrounded = false;
            didJump = false;
        }

        if (playerCanMove)
        {
            fps_Rigidbody.velocity = dMove + Vector3.up * yv;
        }
        else { fps_Rigidbody.velocity = Vector3.zero; }

        if (dMove.magnitude > 0 || !IsGrounded)
        {
            GetComponent<Collider>().material = advanced.zeroFrictionMaterial;
        }
        else { GetComponent<Collider>().material = advanced.highFrictionMaterial; }

        fps_Rigidbody.AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
        if (fOVKick.useFOVKick && wasWalking == isSprinting && fps_Rigidbody.velocity.magnitude > 0.1f && !isCrouching)
        {
            StopAllCoroutines();
            StartCoroutine(wasWalking ? FOVKickOut() : FOVKickIn());
        }

        if (useCrouch && _crouchModifiers.CrouchInputAxis != string.Empty)
        {


            if (Input.GetButton(_crouchModifiers.CrouchInputAxis))
            {
                if (isCrouching == false)
                {
                    capsule.height /= 2;

                    walkSpeed = _crouchModifiers.walkSpeedWhileCrouching;
                    sprintSpeed = _crouchModifiers.sprintSpeedWhileCrouching;
                    jumpPower = _crouchModifiers.jumpPowerWhileCrouching;


                    isCrouching = true;

                }
            }
            else if (isCrouching == true)
            {
                capsule.height *= 2;
                if (!_crouchModifiers.useExternalModifier)
                {
                    walkSpeed = _crouchModifiers.defaultWalkSpeed;
                    sprintSpeed = _crouchModifiers.defaultSprintSpeed;
                    jumpPower = _crouchModifiers.defaultJumpPower;
                }
                else
                {
                    walkSpeed = _crouchModifiers.walkSpeed_External;
                    sprintSpeed = _crouchModifiers.sprintSpeed_External;
                    jumpPower = _crouchModifiers.jumpPower_External;
                }
                isCrouching = false;

            }

        }

        #endregion

        #region BETA_SETTINGS - FixedUpdate

        #endregion

    }

    public void UpdateAndApplyExternalCrouchModifies()
    {
        walkSpeed = _crouchModifiers.walkSpeed_External;
        sprintSpeed = _crouchModifiers.sprintSpeed_External;
        jumpPower = _crouchModifiers.jumpPower_External;
    }

    public IEnumerator FOVKickOut()
    {
        float t = Mathf.Abs((playerCamera.GetComponent<Camera>().fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while (t < fOVKick.changeTime)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FOVKickIn()
    {
        float t = Mathf.Abs((playerCamera.GetComponent<Camera>().fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while (t > 0)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart;
    }

    public IEnumerator CameraShake(float Duration, float Magnitude)
    {
        float elapsed = 0;
        while (elapsed < Duration)
        {
            playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, new Vector3(cameraStartingPosition.x + Random.Range(-1, 1) * Magnitude, cameraStartingPosition.y + Random.Range(-1, 1) * Magnitude, cameraStartingPosition.z), Magnitude * 2);
            yield return new WaitForSecondsRealtime(0.001f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = cameraStartingPosition;
    }
}



