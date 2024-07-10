using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField] private LayerMask whatIsGround;

    [Header("Setting Value")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float jumpForce;

    [Header("GetComponent")]
    private Rigidbody playerRigid;


    [Header("Test")]
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Animator playerRunningAnimation;
    private Quaternion _targetRotation;
    private Vector3 _velocity;
    [SerializeField] private GameObject Blade;
    [SerializeField] private Transform bladeRoot;
    [SerializeField] private Transform handRoot;
    private Vector3 bladePos;
    private Vector3 bladeAng;

    private int adhd;
    //변수
    private bool isRun;
    public bool isAttacked;

    private void Awake()
    {
        //Getcomponent
        playerRigid = GetComponent<Rigidbody>();


        //Action


        //Function
        Init();
    }

    private void OnDestroy()
    {
        //Action

    }

    private void Update()
    {
        MovementInput();
    }

    private void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //bladePos = Blade.transform.localPosition;
        //bladeAng = Blade.transform.localEulerAngles;
    }
    private void MovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * x;
        Vector3 moveVertical = transform.forward * z;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized;

        Movement(velocity);
    }

    private void Movement(Vector3 velocity)
    {
        if (isAttacked == true) return;

        _velocity = velocity;

        if (isRun == true)
        {
            _velocity *= playerRunSpeed;
        }
        if (isRun == false)
        {
            _velocity *= playerSpeed;
        }

        _velocity += (Vector3.up * playerRigid.velocity.y);
        playerRigid.velocity = _velocity;

        SetMovement(velocity);
        ApplyRotation();
    }

    private void Jumping()
    {
        if (IsGround() == false) return;

        if (IsGround()) playerRigid.velocity = transform.up * jumpForce;
        SetJumpTrigger();
        Debug.Log("점프밍");
    }

    private bool IsGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMovement(Vector3 movement, bool isRotation = true)
    {
        _velocity = movement * Time.fixedDeltaTime;
        if (_velocity.sqrMagnitude > 0 && isRotation)
        {
            _targetRotation = Quaternion.LookRotation(_velocity);
        }
    }
    private void ApplyRotation()
    {
        float rotationSpeed = 8f;
        playerVisual.rotation = Quaternion.Lerp(
                            playerVisual.rotation,
                            _targetRotation,
                            Time.fixedDeltaTime * rotationSpeed);
    }

    private void SetAnim(float velocityMagnitude, bool isRun)
    {
        playerRunningAnimation.SetFloat("velocity", velocityMagnitude);
        playerRunningAnimation.SetFloat("velocityY", playerRigid.velocity.y);
        playerRunningAnimation.SetBool("isGround", IsGround());
        playerRunningAnimation.SetBool("isRun", isRun);
    }

    private void SetJumpTrigger()
    {
        playerRunningAnimation.SetTrigger("jumpTrigger");
    }

    private void SetBladePosition(float mag)
    {
        float rs = playerRunSpeed, ns = playerSpeed;
        if (IsAttack())
        {
            Blade.transform.parent = bladeRoot;
            Blade.transform.localPosition = bladePos;
            Blade.transform.localEulerAngles = bladeAng;

            Debug.Log("ALD");
            StartCoroutine(AttackCoroutine());
            Blade.transform.parent = handRoot;
        }
        else if (mag > 0)
        {
            Blade.transform.parent = bladeRoot;
            Blade.transform.localPosition = bladePos;
            Blade.transform.localEulerAngles = bladeAng;
        }
    }

    private bool IsAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    private IEnumerator AttackCoroutine()
    {
        if (isAttacked == true) yield return null;
        isAttacked = true;
        playerRigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        isAttacked = false;

    }
}
