using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float speed;
    [SerializeField] private float height;
    private Rigidbody rb;
    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundMask;
    private bool wantsToJump;
    [SerializeField] private Vector2 minMaxPitch;
    [SerializeField] private float cameraSens;
    [SerializeField] private Transform cameraAnchor;
    
    private float rotX;
    [SerializeField] private bool invertCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        MoveCamera();
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
    
    
    private void MoveCamera()
    {
        float yaw = Input.GetAxis("Mouse X") * cameraSens;
        float pitch = Input.GetAxis("Mouse Y") * cameraSens;
        
        rotX = invertCamera ? rotX + pitch : rotX - pitch;
        rotX = Mathf.Clamp(rotX, minMaxPitch.x, minMaxPitch.y);
        
        cameraAnchor.localRotation = Quaternion.Euler(rotX, 0,0);
        
        gameObject.transform.Rotate(Vector3.up * yaw);
    }
    
}
