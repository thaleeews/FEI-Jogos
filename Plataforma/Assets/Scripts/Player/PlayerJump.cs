using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpForce = 6;
    [SerializeField] private float doubleJumpForce = 6f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(4f, 8f);
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float wallJumpMovementCooldown = 0.2f;
    private PlayerMovement playerMovement;
    private float playerHalfHeight;
    private float playerHalfWidth;
    private bool canDoubleJump;
    private bool isGrounded;

    private void Start() {
        playerHalfHeight = spriteRenderer.bounds.extents.y;
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            CheckJumpType();
        }        
    }

    private void CheckJumpType() {
        bool isGrounded = GetIsGrounded();

        if (isGrounded) {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);
            Jump(jumpForce);
        }
        else {
            int direction = GetWallJumpDirection();
            if (direction == 0 && canDoubleJump) {
                DoubleJump();
            }
            else if (direction != 0) {
                WallJump(direction);
            }            
        }        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GetIsGrounded();
    }

    private int GetWallJumpDirection() {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"));
        if (hitLeft) {
            return 1;
        }

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, playerHalfWidth + 0.1f, LayerMask.GetMask("Ground"));
        if (hitRight) {
            return -1;
        }

        return 0;
    }

    private bool GetIsGrounded() {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        if (hit) {
            canDoubleJump = true;
        }
        
        return hit;
    }

    private void DoubleJump() {
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        Jump(doubleJumpForce);
        canDoubleJump = false;
        playerMovementState.SetMoveState(PlayerMovementState.MoveState.Double_Jump);
    }

    private void WallJump(int direction) {        
        Vector2 force = wallJumpForce;
        force.x *= direction;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        playerMovement.wallJumpCooldown = wallJumpMovementCooldown;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
        playerMovementState.SetMoveState(PlayerMovementState.MoveState.Wall_Jump);
    }

    private void Jump(float force) {
        rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
