using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Joysticks
{
    Keyboard,
    Joy2
};
public class PlayerController : MonoBehaviour
{

    private float horizontalInput;
    [SerializeField]
    private float jumpImpulse;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float throwSpeed;

    [SerializeField]
    private GameObject myThrownItemPrefab;

    private Rigidbody2D myRigidBody2d;

    private bool jump = false;
    private bool canJump = false;
    private bool canThrow = false;

    public Joysticks joystickType;

    private string inputType;

    void Start()
    {
        if (joystickType == Joysticks.Keyboard)
            inputType = "Keyboard";
        if (joystickType == Joysticks.Joy2)
            inputType = "Joy2";
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown(inputType + "_Jump") && canJump)
        {
            //This needs to be in both here and fixed update or physics get wonky on different hardware, also if it's all in fixed theres delay in hitting the jump btn and it working
            jump = true;
        }
        if (Input.GetButtonDown(inputType + "_Throw"))
        {
            Throw();
        }
        Debug.Log("Can jump?: " + canJump);
    }


    private void FixedUpdate()
    {
        Move();
        if (jump)
        {
            jump = false;
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("Floor found"); //BUG will detect the floor if you hit the bottom of it, i'm not sure whyt this is happening when the trigger is nowhere near the floor
            canJump = true;
        }
    }


    private void Move()
    {
        horizontalInput = Input.GetAxisRaw(inputType + "_Horizontal");
        Vector2 movementVector = new Vector2(horizontalInput * movementSpeed * Time.deltaTime * 100, myRigidBody2d.velocity.y);
        myRigidBody2d.velocity = movementVector;

    }

    private void Jump()
    {
        Vector2 movementVector = new Vector2(myRigidBody2d.velocity.x, jumpImpulse * Time.deltaTime * 100); // The * 100 just makes it simpler to adjust in the editor
        myRigidBody2d.velocity = movementVector;
        canJump = false;
    }

    private void Throw()
    {
        Vector2 myPos = transform.position;
        
        if (inputType == "Keyboard")
        {
            //TODO: Need to change so that mouse position from player doesnt affect speed.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - myPos;
            GameObject thrownItem = (GameObject)Instantiate(myThrownItemPrefab, transform.position, Quaternion.identity);
            thrownItem.GetComponent<Rigidbody2D>().velocity = direction * throwSpeed;
        }
        else
        {
            //TODO: For controller
        }

    }
}
