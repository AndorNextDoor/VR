using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Move straight", menuName = "Enemy Logic/Move Logic/Straight")]
public class EnemyMoveStraight : EnemyMoveSOBase
{
    private int currentWayPointIndex;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("IsMoving", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("IsMoving", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        MoveEnemy();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }


    public void MoveEnemy()
    {
        if (Path.wayPoints.Count == 0)
        {
            Debug.LogError("No waypoints found.");
            return;
        }

        if (currentWayPointIndex >= Path.wayPoints.Count)
        {
            // If the enemy has reached the last waypoint, you can reset the index to start over.
            currentWayPointIndex = 0;
            return;
        }

        // Get the current waypoint position
        Vector3 targetPosition = Path.wayPoints[currentWayPointIndex].position;
        targetPosition.y = transform.position.y;

        // Move the enemy towards the current waypoint
        float step = enemy.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // If the enemy is close enough to the current waypoint, move to the next one
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            currentWayPointIndex++;
            transform.LookAt(Path.wayPoints[currentWayPointIndex]);
        }
    }
}
