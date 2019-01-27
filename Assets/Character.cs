using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Character : MonoBehaviour
{
    public float _life = 1;
    private bool _isAlive = true;
    protected Animator _anim; // Reference to the player's animator component.
    [SerializeField] protected float m_MaxSpeed = 10f; // The fastest the player can travel in the x axis.
    [SerializeField] protected GameObject _weapon; // The fastest the player can travel in the x axis.
    [SerializeField] protected float m_JumpForce = 10000f; // Amount of force added when the player jumps.

    [Range(0, 1)] [SerializeField]
    protected float m_AttackSpeed = .36f; // Amount of maxSpeed applied to attacking movement. 1 = 100%

    [SerializeField] protected bool m_AirControl = true; // Whether or not a player can steer while jumping;
    [SerializeField] protected LayerMask m_WhatIsGround;

    // A mask determining what is ground to the character

    protected Transform m_GroundCheck; // A position marking where to check if the player is grounded.
    protected const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    protected bool m_Grounded; // Whether or not the player is grounded.
    protected Transform m_CeilingCheck; // A position marking where to check for ceilings

    protected const float
        k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

    protected Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true; // For determining which way the player is currently facing.
    protected bool _isShooting = false;
    public GameObject game_over;
    private IEnumerator RealGetDamage(float damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Debug.Log("I'm dead!!");
            game_over.SetActive(true);
            _anim.SetBool("Die", true);
            GetComponent<Animation>().Play();
            yield return new WaitForSeconds( GetComponent<Animation>().clip.length);
            Destroy(transform.gameObject);
            _isAlive = false;
            yield break;
        }


        _anim.SetBool("Hit", true);
        Debug.Log("Get hitt! Life:" + _life);
        Debug.Log(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _anim.SetBool("Hit", false);
    }

    public bool GetDamage(float damage)
    {
        if (!_anim.GetBool("Hit"))
        {
            StartCoroutine(RealGetDamage(damage));
        }

        return !_isAlive;
    }

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        _anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        _anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        _anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    public void Move(float move, bool attack, bool attack2, bool jump)
    {
        if (attack || attack2)
        {
            Debug.Log("attack in progress");
            _weapon.GetComponent<Weapon>().ActivateTrigger(_anim.GetNextAnimatorClipInfo(0).Length);
            
        }

        // If attacking, check to see if the character can stand up
        if (!attack && _anim.GetBool("Attack"))
        {
            
            // If the character has a ceiling preventing them from standing up, keep them attacking
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                attack = true;
            }
        }

        // If attacking, check to see if the character can stand up
        if (!attack2 && _anim.GetBool("Attack2"))
        {
            // If the character has a ceiling preventing them from standing up, keep them attacking
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                attack2 = true;
            }
        }
        // Set whether or not the character is attacking in the animator
        _anim.SetBool("Attack", attack);
        _anim.SetBool("Attack2", attack2);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if attacking by the attackSpeed multiplier
            move = (attack ? move * m_AttackSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            _anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);


            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (m_Grounded && jump && _anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            _anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}