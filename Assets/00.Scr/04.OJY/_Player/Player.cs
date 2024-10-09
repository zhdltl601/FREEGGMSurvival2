using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("General")]
    //[SerializeField] private PlayerCamera   _camera;
    [SerializeField] private Inventory      _inventory;
    //[SerializeField] private PlayerAnimator _playerAnimator;
    //private CharacterController _cc;

    public static bool IsCrafting { get; private set; } = false;
    //public bool IsGround => _cc.isGrounded;

    [Header("Interact")]
    [SerializeField] private float interactDistance;

    [Header("layerMasks")]
    [SerializeField] private LayerMask _lm_item;
    [SerializeField] private LayerMask _lm_interactable;


    [Header("Debugging")]
    [SerializeField] private Crafter defaultCrafter;
    public static Crafter CurrentCrafter { get; set; }
    private void Awake()
    {
        //_cc = GetComponent<CharacterController>();
        //_camera = GetComponentInChildren<PlayerCamera>();
        _inventory = GetComponentInChildren<Inventory>();
        //Application.targetFrameRate = 50;
    }
    private void Start()
    {
        CurrentCrafter = defaultCrafter;
        InventoryUI.Instance.PlayerInventory = _inventory;
    }
    private void Update()
    {
        //Transform _camera = this._camera.GetCameraRot;

        //Vector3 forwardDirection = _camera.forward; forwardDirection.y = 0; forwardDirection.Normalize();
        //Vector3 rightDirection   = _camera.right;
        void L_KeyInput()
        {
            //void ItemInspect() =>                   _playerAnimator.PlayAnim(Item.inspectHash);//currentItem.Inspect();
            //void ItemInteractNormal() =>            currentItem.OnNormalInteraction();
            //void ItemInteractionSpeicial() =>       currentItem.OnSpecialInteraction();
            //if (Input.GetKeyDown(KeyCode.B))        ItemInspect();
            //if (Input.GetKeyDown(KeyCode.Mouse0))   ItemInteractNormal();
            //if (Input.GetKeyDown(KeyCode.Mouse1))   ItemInteractionSpeicial();

            if (Input.GetKeyDown(KeyCode.E))        RaycastInteract();
            if (Input.GetKeyDown(KeyCode.Tab))      ToggleInventory();
            if (Input.GetKeyDown(KeyCode.B))        UIManager.Instance.ToggleQuest();
        }
        void L_KeyDebug()
        {
            //InventoryUI.Instance.dbg_list[0].text = isCrafting.ToString();
            if (Input.GetKeyDown(KeyCode.M)) SceneManager.LoadScene("Map"); //DayManager.CanProcess = !DayManager.CanProcess;
        }
        L_KeyInput();
        L_KeyDebug();
    }
    public void ToggleInventory()
    {
        IsCrafting = InventoryUI.Instance.ToggleInventory();
Transform _camera = PlayerMovementV2.Instance.cameraRoot;//getcam
        _camera.gameObject.SetActive(!IsCrafting);
        //int initialXValue = 1; //get val from settings;
        //int initialYValue = 1; //get val from settings;
        if (IsCrafting)
        {
            //ySens = 0;
            //xSens = 0;
        }
        else
        {
            //xSens = initialXValue;
            //ySens = initialYValue;
        }
    }
    private void RaycastInteract()
    {
        if (IsCrafting) return;

        Transform _camera = PlayerMovementV2.Instance.cameraRoot;
        Ray r = new(_camera.position, _camera.forward);
        Debug.DrawRay(r.origin, r.direction * interactDistance, Color.red, 1);
        bool IsInteractable = Physics.Raycast(r, out RaycastHit raycastHit, interactDistance, _lm_interactable);
        if (IsInteractable) raycastHit.transform.GetComponent<IInteractable>().OnPlayerInteract(this);
    }
}
