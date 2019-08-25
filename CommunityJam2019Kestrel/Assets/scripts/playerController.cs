using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseY = Mathf.Clamp(mouseY, -rotationRestriction, rotationRestriction);
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0f);
    }
}
