﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float _baseSpeed;
    public float walking_speed = 15.0f;
    float running_speed;
    float _gravidade = 9.8f;
    public Vector3 playerVelocity;
    private float jumpHeight = 2.0f;
    private bool canJump = false;
    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;
    string object_hit;
    public Inventory inventory;
    public GameObject hand;
    CharacterController characterController;
    public HUD hud;
    public GameObject but1;
    public GameObject but2;
    public GameObject but3;
    public float stamina = 100.0f;
    public BazookaUse bazooka;

    // AudioSource walkingSound;
    // public AudioSource tiredSFX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        inventory.ItemUsed += Inventory_ItemUsed;
        running_speed = walking_speed * 2.0f;
        // walkingSound = GetComponent<AudioSource>();
    }

    private IInventoryItem mCurrentItem = null;
    public GameObject goItem;
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (mCurrentItem != null)
        {
            goItem.SetActive(false);
        }

        IInventoryItem item = e.Item;

        if (item != null)
        {
            goItem = (item as MonoBehaviour).gameObject;
            goItem.SetActive(true);
            Rigidbody goItemRB = goItem.GetComponent<Rigidbody>();
            goItemRB.isKinematic = true;

            goItem.transform.parent = hand.transform;
            goItem.transform.localPosition = (item as InventoryItemBase).PickPosition;
            goItem.transform.localEulerAngles = (item as InventoryItemBase).PickRotation;

            mCurrentItem = e.Item;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Button bbut1 = but1.GetComponent<Button>();
            bbut1.Select();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Button bbut2 = but2.GetComponent<Button>();
            bbut2.Select();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Button bbut3 = but3.GetComponent<Button>();
            bbut3.Select();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (mItemToPickup != null && inventory.mItems.Count < 6)
            {
                inventory.AddItem(mItemToPickup);
                mItemToPickup.OnPickup();
                hud.CloseMessagePanel();
            }
        }

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        if(!bazooka.shot)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if ((x != 0 || z != 0) && Input.GetKey(KeyCode.LeftShift) && stamina > .5f && walking_speed > 8)
            {
                _baseSpeed = running_speed;
                stamina -= Time.deltaTime * 17.5f;
                if (stamina < 0.0f)
                    stamina -= 25.0f;
                // else if (stamina < 30.0f)
                // {
                //     tiredSFX.mute = false;
                //     // https://www.fesliyanstudios.com/royalty-free-sound-effects-download/person-sighing-160
                //     if (!tiredSFX.isPlaying)
                //         tiredSFX.Play();
                // }
            }
            else
            {
                _baseSpeed = walking_speed;
                if (stamina < 100)
                    stamina += Time.deltaTime * 5;
            }
            //Verificando se é preciso aplicar a gravidade
            float y = 0;
            Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
            characterController.Move(direction * _baseSpeed * Time.deltaTime);
        }
        
        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
        
        // if (x != 0 || z != 0 && characterController.isGrounded)
        // {
        //     if (!walkingSound.isPlaying)
        //         walkingSound.Play();
        // }
        // else
        // {
        //     walkingSound.Stop();
        // }

        

        //Tratando a rotação da câmera
        if (cameraRotation >= -40 && cameraRotation <= 55)
        {
            cameraRotation -= mouse_dY;
            Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        }
        if (cameraRotation < -40)
        {
            cameraRotation = -40;
        }

        if (cameraRotation > 55)
        {
            cameraRotation = 55;
        }

        if (characterController.isGrounded)
        {
            canJump = true;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * -_gravidade);
            canJump = false;
        }

        playerVelocity.y += -_gravidade * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    IInventoryItem mItemToPickup = null;
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            mItemToPickup = item;
            hud.OpenMessagePanel("Pressione F para pegar");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            hud.CloseMessagePanel();
            mItemToPickup = null;

        }
    }
}