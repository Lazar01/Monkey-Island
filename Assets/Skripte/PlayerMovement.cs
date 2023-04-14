using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private FixedJoystick joystick;
    private Vector3 moveDir;
    private Vector3 originalPos;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (GameManager.gameState == GameManager.GameState.running)
    //        MyInput();
    //    //SpeedControl();
       
    //}
    private void FixedUpdate()
    {
        if(GameManager.gameState == GameManager.GameState.running)
            MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }
    private void MovePlayer()

    {
        //moveDir = orientation.forward * verticalInput + (orientation.right * horizontalInput);
        moveDir = orientation.forward * joystick.Vertical + (orientation.right * joystick.Horizontal);
        rb.velocity=moveDir * moveSpeed;

        //rb.AddForce(moveDir * moveSpeed * airMultiplier, ForceMode.Force);

    }
}
