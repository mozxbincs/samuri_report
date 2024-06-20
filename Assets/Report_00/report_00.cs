using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class report_00 : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private Vector2 velocity;
    public float speed = 1.0f;
    public float jumpForce = 5.0f;
    [SerializeField] private GameObject bodyObject;
    public LayerMask groundLayer;
    private bool isGrounded;
    private JumpState jumpState;
    private float groundCheckRadius = 0.2f;
    private Transform groundCheck;
    private bool jumpRequest;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = bodyObject.GetComponent<Animator>();
        groundCheck = transform.Find("groundCheck");

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck 오브젝트를 찾을 수 없습니다. GroundCheck 오브젝트가 제대로 설정되었는지 확인하세요.");
        }
    }

    void Update()
    {
        float _hozInput = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(_hozInput, 0).normalized * speed;

        // 이동 애니메이션 설정
        animator.SetBool("iswalk", velocity.x != 0);

        // 이동 방향 설정
        if (_hozInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_hozInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
            jumpState = JumpState.Jumping;  // 점프 상태를 먼저 설정
            animator.SetBool("isJumping", true);  // 애니메이터 파라미터 설정
        }

        // 상태 업데이트
        UpdateJumpState();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);

        // 점프 요청 처리
        if (jumpRequest)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpRequest = false; // 점프 요청 초기화
        }
    }

    void UpdateJumpState()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && jumpState != JumpState.Jumping)
        {
            jumpState = JumpState.Grounded;
        }
        else if (rigidbody.velocity.y > 0)
        {
            jumpState = JumpState.Rising;
        }
        else if (rigidbody.velocity.y < 0 && !isGrounded)
        {
            jumpState = JumpState.Falling; // 떨어지고 있는 경우
        }
    }

    void UpdateAnimator()
    {
        if (jumpState == JumpState.Grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRising", false);
            animator.SetBool("isFalling", false);
        }
        else if (jumpState == JumpState.Rising)
        {
            animator.SetBool("isRising", true);
            animator.SetBool("isFalling", false);
        }
        else if (jumpState == JumpState.Falling)
        {
            animator.SetBool("isRising", false);
            animator.SetBool("isFalling", true);
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

public enum JumpState
{
    Grounded,
    Jumping,
    Rising,
    Falling
}
