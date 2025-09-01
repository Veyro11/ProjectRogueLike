using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyApplyForce;
    private AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        stateMachine.WantsToContinueCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttakData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }
        }
        else
        {
            if (stateMachine.WantsToContinueCombo && attackInfoData.ComboStateIndex != -1)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ComboIndex = 0;
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    protected override void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.WantsToContinueCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        Vector2 forceDirection = stateMachine.Player.transform.right * stateMachine.Player.transform.localScale.x;
        stateMachine.Player.ForceReceiver.AddForce(forceDirection * attackInfoData.Force);
    }
}