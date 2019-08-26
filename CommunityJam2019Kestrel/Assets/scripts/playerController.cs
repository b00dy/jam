using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed;
    private float upRot;
    private float mouseX;
    private float mouseY;
    private float calcX;
    private float calcY;
    private float how;
    private float ver;
    public float rotationRestriction;
    public float rotationSpeed;
    private Rigidbody rb;
    private Transform cameraValues;
    public float interactRayDistance;
    public LayerMask interactionLayers;
    public Text interactionText;
    private bool piecesGathethered;

    private int passcodeNumbersCollected;



    [SerializeField] private string hori;
    [SerializeField] private string vert;
    [SerializeField] private float speed1;
    private CharacterController char_;
    [SerializeField] private float m;



    [SerializeField] private string imputX, inputY;
    [SerializeField] private float s;
    private float clampX;

    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        lookC(); clampX = 0.0f;
        rb = GetComponent<Rigidbody>();
        piecesGathethered = false;
        char_ = GetComponent<CharacterController>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraValues = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        mouseY = Mathf.Clamp(mouseY, -rotationRestriction, rotationRestriction);
        mouseX += Input.GetAxis("Mouse X") / 10 * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") / 10 * rotationSpeed;
        how = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if (passcodeNumbersCollected == 4)
        {
            piecesGathethered = true;
        }
        move();
        //rot();
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Interaction process initiated");
            RaycastHit hit;
            if (Physics.Raycast(cameraValues.position, cameraValues.forward, out hit, interactRayDistance, interactionLayers))
            {
                if (hit.collider.gameObject.CompareTag("safe") && piecesGathethered == false)
                {
                    Debug.Log("Should display text");
                    interactionText.text = "Can't open without combination...";
                    Animator anim = interactionText.GetComponent<Animator>();
                    anim.SetTrigger("fade");
                }
                if (hit.collider.gameObject.CompareTag("safe") && piecesGathethered == true)
                {
                    Animator animi = hit.collider.gameObject.GetComponent<Animator>();
                    animi.SetTrigger("open");
                }
                if (hit.collider.gameObject.CompareTag("drawer"))
                {
                    Animator animi = hit.collider.gameObject.GetComponent<Animator>();
                    animi.SetTrigger("interact");
                }
                if (hit.collider.gameObject.CompareTag("passcodeNumber"))
                {
                    Destroy(hit.collider.gameObject);
                    passcodeNumbersCollected += 1;
                }
            }
        }
    }
    private void move()
    {
        float h = Input.GetAxis("Horizontal") * speed;
        float v = Input.GetAxis("Vertical") * speed;

        Vector3 forw = transform.forward * v;
        Vector3 right = transform.right * h;


        char_.SimpleMove(forw + right);

    }
    private void
        lookC()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void rot()
    {
        float x = Input.GetAxis("Mouse X") * s * Time.deltaTime;

        float y = Input.GetAxis("Mouse Y") * s * Time.deltaTime;
        transform.Rotate(Vector3.left * y);
        player.Rotate(Vector3.up * x);
        clampX += y;

        if (clampX > 90.0f)
        {
            clampX = 90.0f;
            y = 0.0f;
            clamp(270.0f);
        }
        else if (clampX < -90.0f)
        {
            clampX = -90.0f;
            y = 0.0f;
            clamp(90.0f);
        }
    }
    private void clamp(float v)
    {
        Vector3 eul = transform.eulerAngles;
        eul.x = v;
        eul.z = 0;
        transform.eulerAngles = eul;
    }
}
