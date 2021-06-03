using UnityEngine;
using System.Collections;

public class HairdryerUse : MonoBehaviour
{
    public PlayerController playerCont;
    public CharacterController charCont;
    public Camera cam;
    private Vector3 targetDirection;
    private float distance = 100f;
    private float mass;
    public bool shot = false;
    public int maxAmmo = 1;
    private int currentAmmo;
    public float reloadTime = 40f;
    private bool isReloading = false;
    public Animator animator;
    public AudioSource useSfx;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        shot = false;
        animator.SetBool("Reloading", false);
        animator.SetBool("Shot", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shot)
        {
            if (charCont.isGrounded)
            {
                playerCont.playerVelocity = new Vector3(0, 0, 0);
                shot = false;
            }
        }

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (playerCont.goItem != null)
        {
            if (playerCont.goItem.name == "Hairdryer")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Shoot();
                }
            }
        }
    }

    void LateUpdate()

    {
        RaycastHit hit = cam.GetComponent<ObjectInteraction>().hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            targetDirection = cam.GetComponent<ObjectInteraction>().ray.direction;
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            playerCont.playerVelocity = -targetDirection * 15;
            currentAmmo--;
            shot = true;
            animator.SetBool("Shot", true);
            useSfx.Play();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Shot", false);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
