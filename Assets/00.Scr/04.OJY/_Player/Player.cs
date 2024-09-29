using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private PlayerCamera   _camera;
    [SerializeField] private Inventory      _inventory;
    [SerializeField] private PlayerAnimator _playerAnimator;
    private CharacterController _cc;

    private bool isCrafting = false;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float jumpHeight;

    private Vector3 moveDirection;
    private float _yVal;
    private float _movementSens = 1;
    bool IsGround => _cc.isGrounded;

    [Header("Interact")]
    [SerializeField] private float interactDistance;

    [Header("layerMasks")]
    [SerializeField] LayerMask _lm_item;
    [SerializeField] LayerMask _lm_interactive;

    private float yRot;
    private float xRot;
    private float ySens = 1;
    private float xSens = 1;

    [Header("Debugging")]
    [SerializeField] private Crafter defaultCrafter;
    public static Crafter CurrentCrafter { get; set; }
    [SerializeField] private Item currentItem;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<PlayerCamera>();
        _inventory = GetComponentInChildren<Inventory>();
    }
    private void Start()
    {
        CurrentCrafter = defaultCrafter;
        InventoryUI.Instance.PlayerInventory = _inventory;
        InventoryUI.Instance.SetActive(false);
    }
    private void Update()
    {
        Transform _camera = this._camera.GetCameraRot;

        Vector3 forwardDirection = _camera.forward; forwardDirection.y = 0; forwardDirection.Normalize();
        Vector3 rightDirection   = _camera.right;
        void L_KeyInput()
        {
            void ItemInspect() =>                   _playerAnimator.PlayAnim(Item.inspectHash);//currentItem.Inspect();
            void ItemInteractNormal() =>            currentItem.OnNormalInteraction();
            void ItemInteractionSpeicial() =>       currentItem.OnSpecialInteraction();
            //if (Input.GetKeyDown(KeyCode.B))        ItemInspect();
            //if (Input.GetKeyDown(KeyCode.Mouse0))   ItemInteractNormal();
            //if (Input.GetKeyDown(KeyCode.Mouse1))   ItemInteractionSpeicial();

            if (Input.GetKeyDown(KeyCode.Space))    _yVal = jumpHeight;
            if (Input.GetKeyDown(KeyCode.E))        RaycastInteract();
            if (Input.GetKeyDown(KeyCode.Tab))      ToggleInventory();
            yRot += Input.GetAxis("Mouse X") * ySens; 
            xRot -= Input.GetAxis("Mouse Y") * xSens; xRot = Mathf.Clamp(xRot, -85, 85);
            moveDirection = forwardDirection * Input.GetAxis("Vertical") + rightDirection * Input.GetAxis("Horizontal");
            moveDirection = moveDirection.sqrMagnitude < 1 ? moveDirection : moveDirection.normalized;
        }
        void L_KeyDebug()
        {
            InventoryUI.Instance.dbg_list[0].text = isCrafting.ToString();
        }
        L_KeyInput();
        L_KeyDebug();
        if (Input.GetKeyDown(KeyCode.M)) DayManager.CanProcess = !DayManager.CanProcess;
        this._camera.SetCameraRotation(Quaternion.Euler(xRot, yRot, 0));
        //_camera.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
    private void FixedUpdate()
    {
float speed = _walkSpeed;// get current state and apply speed

        if(IsGround && _yVal <= 0)
        {
            _yVal = -1;
        }
        else
        {
            print("fall");
            _yVal -= 9.81f * Time.deltaTime;
        }
        Vector3 velocitiy = moveDirection * speed;
        velocitiy.y = _yVal;
        _cc.Move(velocitiy * Time.deltaTime);
        //_rigidbody.velocity = moveDirection * speed;
        //_rigidbody.velocity *= _movementSens;


    }
    public void ToggleInventory()
    {
        isCrafting = InventoryUI.Instance.ToggleInventory();
        _camera.gameObject.SetActive(!isCrafting);
        int initialXValue = 1; //get val from settings;
        int initialYValue = 1; //get val from settings;
        if (isCrafting)
        {
            ySens = 0;
            xSens = 0;
            _movementSens = 0;
        }
        else
        {
            xSens = initialXValue;
            ySens = initialYValue;
            _movementSens = 1;
        }
    }
    private void RaycastInteract()
    {
        if (isCrafting) return;

        Transform _camera = this._camera.GetCameraRot;
        Ray r = new(_camera.position, _camera.forward);
        Debug.DrawRay(r.origin, r.direction, Color.red, interactDistance);
        bool IsInteractable = Physics.Raycast(r, out RaycastHit raycastHit, interactDistance, _lm_interactive);
        if (IsInteractable) raycastHit.transform.GetComponent<IInteractable>().OnPlayerInteract(this);
    }
}
