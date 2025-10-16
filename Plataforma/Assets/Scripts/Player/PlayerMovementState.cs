using UnityEngine;
using System;

public class PlayerMovementState : MonoBehaviour
{
    public enum MoveState { Idle, Run, Jump, Fall, Double_Jump, Wall_Jump }

    public MoveState CurrentMoveState { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    private const string idleAnim = "Idle";
    private const string runAnim = "Run";
    private const string jumpAnim = "Jump";
    private const string fallAnim = "Fall";
    private const string doubleJumpAnim = "Double Jump";
    private const string wallJumpAnim = "Wall Jump";
    public static Action<MoveState> OnPlayerMoveStateChanged;
    private float xPosLastFrame;

    private void Update() {
        if (transform.position.x == xPosLastFrame && rigidBody.linearVelocity.y == 0) {
            SetMoveState(MoveState.Idle);
        }
        else if (transform.position.x != xPosLastFrame && rigidBody.linearVelocity.y == 0) {
            SetMoveState(MoveState.Run);
        }
        else if (rigidBody.linearVelocity.y < 0f) {
            SetMoveState(MoveState.Fall);
        }

        xPosLastFrame = transform.position.x;
    }
    
    public void SetMoveState(MoveState moveState) {
        if (CurrentMoveState == moveState) return;
        switch (moveState) {
            case MoveState.Idle:
                animator.Play(idleAnim);
                break;
            case MoveState.Run:
                animator.Play(runAnim);
                break;
            case MoveState.Jump:
                animator.Play(jumpAnim);
                break;
            case MoveState.Fall:
                animator.Play(fallAnim);
                break;
            case MoveState.Double_Jump:
                animator.Play(doubleJumpAnim);
                break;
            case MoveState.Wall_Jump:
                animator.Play(wallJumpAnim);
                break;
            default:
                animator.Play(idleAnim);
                break;
        }

        OnPlayerMoveStateChanged?.Invoke(moveState);
        CurrentMoveState = moveState;
    }
}
