using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriterender;

    bool isGrounded;
    bool isAttacking = false;
    [SerializeField] Transform GroundCheck;
    [SerializeField] private float runspeed = 10;
    [SerializeField] private float jumpforce = 12.5f;
    [SerializeField] GameObject AttackHitBox;
    [SerializeField] private AudioSource walking;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource kick;

    public bool canDash = true;
    public float dashTime;
    public float dashSpeed;
    public float dashJumpIncrease;
    public float timeBtwDashes;
    public int extraJumps = 1;

    int jumpCount = 0;
    float jumpCoolDown;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriterender = GetComponent<SpriteRenderer>();
        AttackHitBox.SetActive(false);

    }
    private void Footstep()
    {
        walking.Play();
    }

    private void Jump()
    {
        jump.Play();
    }

    private void Attack()
    {
        attack.Play();
    }
    private void Dash()
    {
        dash.Play();
    }
    private void Kick()
    {
        kick.Play();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isAttacking)
        {
            isAttacking = true;
            float delay = .5f;
            

            if (!isGrounded)
            {
                animator.Play("player flykick");
                delay = .4f;
                Kick();
            }
            else
            {
                Attack();
                int index = UnityEngine.Random.Range(1, 4);
                animator.Play("player attack" + index);
            }
            
            

            StartCoroutine(DoAttack(delay));
        }
       
    }
    IEnumerator DoAttack(float delay)
    {
        AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(.8f);
        AttackHitBox.SetActive(false);
        isAttacking = false;

    }

    private void FixedUpdate()
    {
        if (isGrounded && !isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                DashAbility();
                Dash();
            }
        }
        
        if (Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
        }
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }



        else
        {
            isGrounded = false;
            if (!isAttacking)
            {
                animator.Play("jump");
            }

        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //Right
            rb2d.velocity = new Vector2(runspeed, rb2d.velocity.y);
            if (isGrounded && !isAttacking)
                animator.Play("run");

            transform.localScale = new Vector3(0.1086237f, 0.1086237f, 0.1086237f);

        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //Left
            rb2d.velocity = new Vector2(-runspeed, rb2d.velocity.y);
            if (isGrounded && !isAttacking)
                animator.Play("run");

            transform.localScale = new Vector3(-0.1086237f, 0.1086237f, 0.1086237f);

        }
        else
        {
            if (isGrounded && !isAttacking)
                animator.Play("idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        //Jump
        if (Input.GetKey("space") && isGrounded)
        {

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
            animator.Play("jump");
            Jump();

        }
        void DashAbility()
        {
            if (canDash)
            {
                StartCoroutine(Dash());
                Dash();
            }
        }
        IEnumerator Dash()
        {
            canDash = false;
            runspeed = dashSpeed;
            jumpforce = dashJumpIncrease;
            yield return new WaitForSeconds(dashTime);
            runspeed = 5;
            jumpforce = 15;
            Dash();
            yield return new WaitForSeconds(timeBtwDashes);
            canDash = true;
        }

    }

}
