using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Basic-Projectile", menuName = "Tower logic/Attack Logic/Basic Projectile")]
public class TowerAttackBasicProjectile : TowerAttackSOBase
{
    [SerializeField] private GameObject projectile;
    private Transform target;
    private float _timer;
   
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

        if (_timer > tower.attackCooldown)
        {
            tower.currentlyAttacking = true;
            _timer = 0;
            tower.animator.SetTrigger("Attack");
        }
        _timer += Time.deltaTime;

    }

    void Shoot()
    {
        tower.currentlyAttacking = false;
        Projectile proj = Instantiate(projectile, tower.firepoint.position, tower.firepoint.rotation).GetComponent<Projectile>();
        proj.targetEnemy = target;
    }

    public override void Initialize(GameObject _gameObject, Tower _tower)
    {
        base.Initialize(_gameObject, _tower);

        _timer = tower.attackCooldown;

    }
}
