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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        piecesGathethered = false;
    }

    // Update is called once per frame
    void Update()
    {
        cameraValues = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        mouseY = Mathf.Clamp(mouseY, -rotationRestriction, rotationRestriction);
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        how = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if(passcodeNumbersCollected == 4)
        {
            piecesGathethered = true;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0f);
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Interaction process initiated");
            RaycastHit hit;
            if (Physics.Raycast(cameraValues.position, cameraValues.forward, out hit, interactRayDistance, interactionLayers))
            {
                if(hit.collider.gameObject.CompareTag("safe") && piecesGathethered == false)
                {
                    Debug.Log("Should display text");
                    interactionText.text = "Can't open without combination...";
                    Animator anim = interactionText.GetComponent<Animator>();
                    anim.SetTrigger("fade");
                }
                if(hit.collider.gameObject.CompareTag("safe") && piecesGathethered == true)
                    {
                    Animator animi = hit.collider.gameObject.GetComponent<Animator>();
                    animi.SetTrigger("open");
                }
                if(hit.collider.gameObject.CompareTag("drawer"))
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
}
