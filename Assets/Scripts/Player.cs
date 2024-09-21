using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    private Animator animator;
    public Rigidbody me;
    public GameObject hologram;
    public Vector3 holopos = new Vector3(0,5,0);
    public Vector3 holopos2 = new Vector3(0,-5,0);
    public float groundCheckDistance = 0.1f;
    private Vector3 gravityDirection = Vector3.down;
    public LayerMask groundLayer;
    public Quaternion holospin;
    public Quaternion holospin2;
    bool Upright;
    bool Preview;
    Quaternion targetRotation;
    Cameracon cameraControl;

    void Start()
    {
        Upright = true;

    }
    private void FixedUpdate()
    {
        
        CheckIfFalling();
    }

    private void Awake()
    {
        cameraControl = Camera.main.GetComponent<Cameracon>();
        animator = GetComponent<Animator>();
        me= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);
        Vector3 spawnpos = transform.position + holopos;

        
        var moveInput = (new Vector3(h,0,v)).normalized;
        var moveDirection = cameraControl.PlanarRotation * moveInput;

        if (moveAmount > 0)
        {
            animator.SetBool("Running",true);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDirection);
        }
        else{
            animator.SetBool("Running",false);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            holo();
            Preview = true;

            if(Preview = true)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Physics.gravity = new Vector2(0f,9.18f);
                    if(Upright == true)
                    {
                        StartCoroutine(Wait());
                    }
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            holo2();
            Preview = true;

            if(Preview = true)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics.gravity = new Vector2(0f,-9.18f);
            if(Upright == false)
            {
                StartCoroutine(Wait());
            }
        }
            }

        }
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        CheckIfFalling();
        
    }

    void FlipUp()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
        Upright = !Upright;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        
        FlipUp();
    }

    void holo()
    {
        Vector3 spawnpos = transform.position + holopos;
        Instantiate(hologram,spawnpos,holospin);
    }
    void holo2()
    {
        Vector3 spawnpos2 = transform.position + holopos2;
        Instantiate(hologram,spawnpos2,holospin2);
    }

    private float GetHeightToFeet()
    {
        return 1f;
    }

    private void CheckIfFalling()
    {
        Vector3 rayDirection = gravityDirection;
        Vector3 rayOrigin = transform.position - (Vector3.up * GetHeightToFeet() / 2);

        Debug.DrawRay(rayOrigin, rayDirection * groundCheckDistance, Color.red);
        bool isFalling = !Physics.Raycast(rayOrigin, rayDirection, groundCheckDistance, groundLayer);
        animator.SetBool("IsFalling", true);
    }
}
