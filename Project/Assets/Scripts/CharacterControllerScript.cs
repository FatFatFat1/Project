using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
    public float maxSpeed = 2;
    private Animator animator;
    private bool fliped = true;
    private float lastVSpeed;
    private float lastHSpeed;
    static public float PositionX; /*GetComponent<Transform>().position.x;*/
    static public float PositionY;  /*GetComponent<Transform>().position.y;*/

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Flip()
    {
        if (fliped)
        {
            GetComponent<Transform>().rotation = new Quaternion(GetComponent<Transform>().rotation.x, 
                180, GetComponent<Transform>().rotation.z, GetComponent<Transform>().rotation.w);
        }
        if(!fliped)
        {
            GetComponent<Transform>().rotation = new Quaternion(GetComponent<Transform>().rotation.x, 0, 
                GetComponent<Transform>().rotation.z, GetComponent<Transform>().rotation.w);
        }
        fliped = !fliped;
    }
    
    private void FixedUpdate()
    {
        float vSpeed = Input.GetAxis("Vertical"); 
        float hSpeed = Input.GetAxis("Horizontal");
        bool isMove = vSpeed != 0|| hSpeed != 0;
        if (vSpeed > 0)
        {
            lastVSpeed = 1;
            lastHSpeed = 0;
        }
        else if (vSpeed < 0)
        {
            lastVSpeed = -1;
            lastHSpeed = 0;
        }
        else if (Mathf.Abs(hSpeed) > 0)
        {
            lastHSpeed = 1;
            lastVSpeed = 0;
        }
        animator.SetFloat("vSpeed", lastVSpeed);
        animator.SetFloat("hSpeed", lastHSpeed);
        animator.SetBool("isMove", isMove);
        Rigidbody2D rigidbody;
        
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(vSpeed * maxSpeed, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(hSpeed * maxSpeed, rigidbody.velocity.x);
        //Debug.Log($"vSpeed - {vSpeed}, hSpeed - {hSpeed}, lastVSpeed - {lastVSpeed}, lastHSpeed - {lastHSpeed}");
        if ((hSpeed < 0) && (!fliped))
            Flip();
        else if ((hSpeed > 0) && (fliped))
            Flip();
    }
}