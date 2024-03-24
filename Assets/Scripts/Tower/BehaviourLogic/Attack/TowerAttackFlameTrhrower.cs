using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Flamethrower", menuName = "Tower logic/Attack Logic/Attack-Flamethrower")]
public class TowerAttackFlameTrhrower : TowerAttackSOBase
{
    [SerializeField] private GameObject projectile;
    private GameObject projInstance;
    private Transform target;
    private float _timer;
    private bool IsAttacking;

    public override void DoAnimationTriggerEventLogic(Tower.AnimationTowerTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        switch (triggerType)
        {
            case Tower.AnimationTowerTriggerType.Attack:
                Shoot();
                break;


            case Tower.AnimationTowerTriggerType.AttackSound:
                AudioManager.Instance.PlaySound(tower.attackSound);
                break;


            case Tower.AnimationTowerTriggerType.StopAttack:
                StopAttacking();
                break;
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (tower.targetEnemy != null)
        {
            target = tower.targetEnemy.transform;
        }

        if (IsAttacking) return;

        if (_timer > tower.attackCooldown)
        {
            tower.currentlyAttacking = true;
            _timer = 0;
            tower.animator.SetTrigger("Attack");
            IsAttacking = true;
        }
        _timer += Time.deltaTime;

    }

    void Shoot()
    {
        tower.currentlyAttacking = false;

        projInstance = Instantiate(projectile, tower.firepoint.position, tower.firepoint.rotation);
    }

    void StopAttacking()
    {
        Destroy(projInstance);
        IsAttacking = false;
    }

    public override void Initialize(GameObject _gameObject, Tower _tower)
    {
        base.Initialize(_gameObject, _tower);

        _timer = tower.attackCooldown;

    }
}
