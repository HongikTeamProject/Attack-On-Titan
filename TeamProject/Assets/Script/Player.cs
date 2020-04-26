using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;

    Vector3 moveDirection;


    private float mouseSensitivity = 2.0f;
    private float cameraRotation_Y;

    float speed;
    float jumpPower;

    Transform weapon;
    Transform getItemParent;

    float AttackCooltime;
    float ObjectCooltime;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Move", 0);
        animator.SetBool("JumpAble", false);
        animator.SetBool("Dash", false);

        moveDirection = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        jumpPower = 7;
        weapon = GameObject.Find("Weapon").transform;
        AttackCooltime = 0.0f;
        ObjectCooltime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        FPRotate();

        Move();
        Attack();
    }

    private void FPRotate()
    {
        cameraRotation_Y = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(0f, cameraRotation_Y, 0f);
    }


    void Move()
    {
        speed = 5.0f;
        transform.GetChild(0).localPosition = Vector3.zero;

        animator.SetBool("Dash", false);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.6f;
            animator.SetBool("Dash", true);
        }

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection) * speed;

            //animator.SetBool("JumpAble", true);
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpPower;
                animator.SetBool("JumpAble", false);
            }

        }
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.isGrounded)
            animator.SetFloat("Move", characterController.velocity.magnitude);
        else
            animator.SetFloat("Move", 0);

    }

    void Attack()
    {
        if (AttackCooltime > 0)
            AttackCooltime -= Time.deltaTime;

        animator.SetBool("Attack", false);

        if (Input.GetMouseButtonDown(0) && AttackCooltime <= 0)
        {
            animator.SetBool("Attack", true);
            AttackCooltime = 0.5f;
        }

    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (ObjectCooltime > 0)
            ObjectCooltime -= Time.deltaTime;

        if (hit.gameObject.tag != "Item")
            animator.SetBool("JumpAble", true);

        if (Input.GetKeyDown(KeyCode.F) && getItemParent && ObjectCooltime <= 0)
        {
            Vector3 temp = transform.position - new Vector3(0, transform.position.y, 0);
            weapon.GetChild(0).position = temp + transform.forward * 3.0f;
            weapon.GetChild(0).rotation = Quaternion.identity;
            weapon.GetChild(0).gameObject.tag = "Object";
            weapon.GetChild(0).SetParent(getItemParent);
            getItemParent = null;

            ObjectCooltime = 0.2f;
        }
        if (hit.gameObject.tag == "Object" && Input.GetKeyDown(KeyCode.F) && !getItemParent && ObjectCooltime <= 0)
        {
            getItemParent = hit.transform.parent;
            hit.transform.SetParent(weapon);
            hit.transform.localPosition = Vector3.zero;
            hit.transform.localEulerAngles = Vector3.zero;
            hit.gameObject.tag = "Item";

            ObjectCooltime = 0.2f;
        }
    }
}
