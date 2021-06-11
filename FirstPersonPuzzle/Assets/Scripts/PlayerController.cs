using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;
    public LevelLoader loader;
    float _baseSpeed;
    public float walking_speed = 15.0f;
    float running_speed;
    float _gravidade = 9.8f;
    public Vector3 playerVelocity;
    private float jumpHeight = 2.0f;
    private bool canJump = false;
    private bool wasJumping = false;
    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    public ObjectInteraction obInt;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;
    string object_hit;
    public Inventory inventory;
    public GameObject hand;
    CharacterController characterController;
    public HUD hud;
    public GameObject but1;
    public GameObject but2;
    public float stamina = 100.0f;
    public HairdryerUse hairdryer;
    private GameMaster gm;
    public bool restarting = false;
    public Consumer cons;
    private bool canEat = false;
    GameObject myEventSystem;
    public AudioSource jumpSfx;
    private bool onCube;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        inventory.ItemUsed += Inventory_ItemUsed;
        running_speed = walking_speed * 2.0f;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        myEventSystem = GameObject.Find("EventSystem");
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
        m_animator.SetBool("Grounded", characterController.isGrounded);
        if (characterController.isGrounded)
        {
            if(wasJumping){
                wasJumping = false;
                m_animator.SetTrigger("Land");
            }
            
            if(onCube){
                canJump = false;
            }
            else{
                canJump = true;
            }
            playerVelocity.y = 0;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * -_gravidade);
            m_animator.SetTrigger("Jump");
            canJump = false;
            wasJumping = true;
            jumpSfx.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            restarting = true;
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hud.CloseMessagePanel();
            hud.OpenMessagePanel("Pressione E para usar");
            StartCoroutine(hud.CloseMessagePanelCoroutine());
            Button bbut1 = but1.GetComponent<Button>();
            bbut1.Select();
            bbut1.onClick.Invoke();
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hud.CloseMessagePanel();
            hud.OpenMessagePanel("Pressione E para usar");
            StartCoroutine(hud.CloseMessagePanelCoroutine());
            Button bbut2 = but2.GetComponent<Button>();
            bbut2.Select();
            bbut2.onClick.Invoke();
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canEat)
            {
                cons.cons = true;
                loader.LoadGameOver();
            }
            if (mItemToPickup != null && inventory.mItems.Count < 6)
            {
                inventory.AddItem(mItemToPickup);
                mItemToPickup.OnPickup();
                hud.CloseMessagePanel();
                hud.OpenMessagePanel("pressione 1 e 2 para segurar");
                StartCoroutine(hud.CloseMessagePanelCoroutine());
            }
        }

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        if (!hairdryer.shot)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            _baseSpeed = walking_speed;
                
            //Verificando se é preciso aplicar a gravidade
            float y = 0;
            Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
            characterController.Move(direction * _baseSpeed * Time.deltaTime);
            m_animator.SetFloat("MoveSpeed", x + z);
        }

        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

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

        playerVelocity.y += -_gravidade * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    IInventoryItem mItemToPickup = null;

    void OnControllerColliderHit(ControllerColliderHit hit){
        if (hit.transform.tag == "PickUp"){
            onCube = true;
        }
        else{
            onCube = false;
        }
    }      
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            mItemToPickup = item;
            hud.OpenMessagePanel("Pressione F para pegar");
        }

        if (other.name == "Cake")
        {
            hud.OpenMessagePanel("Pressione F para comer");
            canEat = true;
        }

        if (other.name == "Secret")
        {
            loader.LoadGameOver();
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

    public void Restart()
    {
        
        characterController.enabled = false;
        transform.position = gm.lastCheckPoint + new Vector3(0, 1, 0);
        characterController.enabled = true;
        restarting = false;
    }
}