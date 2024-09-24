using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private PlayerCamera _camera;
    [SerializeField] private Inventory _inventory;
    private Rigidbody _rigidbody;

    private bool isCrafting = false;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private Vector3 moveDirection;
    private float _movementSens = 1;

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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            if (Input.GetKeyDown(KeyCode.E))   RaycastInteract();
            if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventory();
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

        this._camera.SetCameraRotation(Quaternion.Euler(xRot, yRot, 0));
        //_camera.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
    private void FixedUpdate()
    {
float speed = _walkSpeed;// get current state and apply speed

        _rigidbody.velocity = moveDirection * speed;
        _rigidbody.velocity *= _movementSens;
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
