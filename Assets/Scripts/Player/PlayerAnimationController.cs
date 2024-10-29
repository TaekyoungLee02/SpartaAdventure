using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerController playerController;

    private int isDash;
    private int isMove;
    private int isJump;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        isJump = Animator.StringToHash("isJump");
        isDash = Animator.StringToHash("isDash");
        isMove = Animator.StringToHash("isMove");

        playerController.onMove += OnMove;
        playerController.onJump += OnJump;
        playerController.onDash += OnDash;
    }

    private void OnMove(bool moving)
    {
        playerAnimator.SetBool(isMove, moving);
    }
    private void OnDash(bool dashing)
    {
        playerAnimator.SetBool(isDash, dashing);
    }

    private void OnJump()
    {
        playerAnimator.SetTrigger(isJump);
    }
}
