using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerMovementV2 : MonoSingleton<PlayerMovementV2>
{
    
    public Rigidbody rb;

    public Transform cameraRoot;

    public bool isSliding;

    [SerializeField]
    float _maxSpeed = 10, _accelation = 25, _jumpPower = 5, _damp=3,_plHeight=2,_plRaius=0.26f; //PlayerState로 뺄 예정밍
    [SerializeField]
    float _mouseSpeed = 5f;//PlayerSettingMing 으로 뺴야디


    public bool _isCanJump, _isGround;

    [SerializeField]
    LayerMask _whatIsGround;

    Vector2 _mouseTmp;
    Vector3 _movDir;

    RaycastHit _groundCheck;

    Collider[] _groundCheckCols = new Collider[1];

    private void Start()
    {
        PlayerInput.Instance.Jump += Jump;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCamera();
    }
    private void FixedUpdate()
    {
        _isCanJump = false;
        _isGround = false;

        Vector3 input = BashUtils.V2toV3(PlayerInput.Instance.movement);
        input = (Quaternion.Euler(0, _mouseTmp.x, 0) * input);

        //땅에 닿기 직전 점프를 구현하기 위해 닿기 직전(Overlap)과 완전히 닿은 상태(SphereCast)로 나누었다.
        if (Physics.OverlapSphereNonAlloc(transform.position-Vector3.up*_plHeight/2,_plRaius+0.1f,_groundCheckCols,_whatIsGround) > 0) //땅인지 체크(널널한 판정)
        {
            _isCanJump = true;

            if (Physics.SphereCast(transform.position, _plRaius, -transform.up, out _groundCheck, _plHeight / 2, _whatIsGround))//땅인지 체크(빡빡한 판정)
            {
                _isGround = true;

                //OnGorund(
                //감속O 점프O
                MoveOnGorund(ref input);
            }
        }

        _movDir = input * Mathf.Lerp(1, 0, (Vector3.Project(input, rb.velocity)+rb.velocity).magnitude / _maxSpeed);
        _movDir *= Player.IsCrafting ? 0 : 1;
        rb.velocity += _movDir;
    }

    private void Jump()
    {
        if(_isCanJump) 
        rb.AddForce(transform.up*_jumpPower,ForceMode.Impulse);
    }

    private void MoveOnGorund(ref Vector3 input)
    {
        Vector3 horizontalSpeed = BashUtils.V3X0Z(rb.velocity);
        rb.velocity = Vector3.up * rb.velocity.y
           + Vector3.Lerp(horizontalSpeed, Vector3.zero, _damp * Time.fixedDeltaTime);
        //감속

        input = Vector3.ProjectOnPlane(input,_groundCheck.normal);
        //투영으로 경사 오를 수 있게하는 코드;
    }

    private void MoveOnAir()
    {

    }

    private void RotateCamera()
    {
        //_mouseTmp += PlayerManager.Instance.playerInput.mouseMov*_mouseSpeed;
        _mouseTmp.x += PlayerInput.Instance.mouseMov.x * _mouseSpeed * Time.deltaTime;
        _mouseTmp.y -= PlayerInput.Instance.mouseMov.y * _mouseSpeed*Time.deltaTime;
        _mouseTmp.y = Mathf.Clamp(_mouseTmp.y, -89, 89);

        if(!Player.IsCrafting)
            cameraRoot.rotation = Quaternion.Euler(_mouseTmp.y, _mouseTmp.x, 0);
    }
}
