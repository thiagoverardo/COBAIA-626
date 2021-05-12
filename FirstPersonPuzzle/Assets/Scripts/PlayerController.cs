using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float _baseSpeed;
    public float walking_speed = 15.0f;
    float running_speed;
    float _gravidade = 9.8f;
    private Vector3 playerVelocity;
    private float jumpHeight = 2.0f;
    private bool canJump = false;
    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;
    string object_hit;
    public GameObject hand;
    CharacterController characterController;
    public float stamina = 100.0f;

    // AudioSource walkingSound;
    // public AudioSource tiredSFX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        running_speed = walking_speed * 2.0f;
        // walkingSound = GetComponent<AudioSource>();
    }

    void Update()
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

        // if (x != 0 || z != 0 && characterController.isGrounded)
        // {
        //     if (!walkingSound.isPlaying)
        //         walkingSound.Play();
        // }
        // else
        // {
        //     walkingSound.Stop();
        // }

        //Verificando se é preciso aplicar a gravidade
        float y = 0;

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rotação da câmera
        if (cameraRotation >= -20 && cameraRotation <= 45)
        {
            cameraRotation -= mouse_dY;
            Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        }
        if (cameraRotation < -20)
        {
            cameraRotation = -20;
        }

        if (cameraRotation > 45)
        {
            cameraRotation = 45;
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

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boots")
        {
            walking_speed = walking_speed*2f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}