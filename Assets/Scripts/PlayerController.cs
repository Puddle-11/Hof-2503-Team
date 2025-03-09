using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    private float speed;
    [SerializeField] private float height;
    private Rigidbody rb;
    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundMask;
    private bool wantsToJump;

    public Follow camFollow;
    [SerializeField] private Vector3 baseCameraPos;
    [SerializeField] private Vector3 sprintCameraPos;
    [SerializeField] private float staminaRegenSpeed;
    [SerializeField] private float staminaDecaySpeed;
    private float maxStamina;
    private float currStamina;
    private bool tired;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Gradient staminaColor;
    [SerializeField] private AnimationCurve smoothCurveSprintCam;
    [SerializeField] private float cameraChangeSpeed;
    private float cameraBalance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = baseSpeed;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Two instances of player controller found");
            Destroy(gameObject);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        maxStamina = DreamInvntory.instance.dreamBar / DreamInvntory.instance.dreamsNeeded;
        if (currStamina == 0)
        {
            tired = true;
        }
        else if (currStamina >= maxStamina)
        {
            tired = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && !tired)
        {
            cameraBalance = Mathf.MoveTowards(cameraBalance, 1, cameraChangeSpeed * Time.deltaTime);
            speed = sprintSpeed;
            currStamina = Mathf.Clamp(currStamina - Time.deltaTime * staminaDecaySpeed, 0, maxStamina);
        }
        else
        {
            cameraBalance = Mathf.MoveTowards(cameraBalance, 0, cameraChangeSpeed * Time.deltaTime);
            speed = baseSpeed;
            currStamina = Mathf.Clamp(currStamina + Time.deltaTime * staminaRegenSpeed, 0, maxStamina);

        }
        camFollow.offset = Vector3.Lerp(baseCameraPos, sprintCameraPos, smoothCurveSprintCam.Evaluate(cameraBalance));
        staminaSlider.value = currStamina;
        float temp = maxStamina > 0 ? currStamina * (1 / maxStamina) : 0;
        staminaSlider.fillRect.GetComponent<Image>().color = staminaColor.Evaluate(!tired ? temp : 0);
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), speed);
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsToJump = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            wantsToJump = false;
        }
        if (wantsToJump)
        {
            if (IsGrounded())
            {
                Jump(height);
            }
        }
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, groundRayDistance, groundMask))
        {
            return true;
        }
        return false;
    }
    
    void Move(Vector2 _axis, float _speed)
    {
        Vector3 move = _axis.y * transform.forward * _speed + _axis.x * transform.right * _speed;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;
    }
    
    void Jump(float _height)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, _height, rb.linearVelocity.z);
    }
    
    

    
}
