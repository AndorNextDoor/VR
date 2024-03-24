using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy;

public class Tower : MonoBehaviour, IDamagable
{
    [Header("Tower Settings")]
    public int towerCost;
    public string towerName;
    public Sprite towerSprite;
    public bool needToRotateBase = true;
    public float attackCooldown;
    public float towerRange;
    public bool currentlyAttacking;

    [Header("Change Scale On Awake")]
    [SerializeField] private bool needToPlaySizeAnimation;
    [SerializeField] private float scaleChangeDuration;
    [SerializeField] private Vector3 startingScale = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 targetScale;
    

    [SerializeField] public GameObject TowerPlacementPrefab;

    [field: SerializeField] public float maxHealth { get; set; }
    
    [Header("Tower Scriptable Settings")]
    [SerializeField] private TowerAttackSOBase towerAttackBase;
    [SerializeField] private TowerIdleSOBase towerIdleBase;
    private bool IsInitialized = false;

    [Header("Assign Theese")]
    public Transform firepoint;
    public Animator animator;



    [HideInInspector] public Enemy targetEnemy;
    public float currentHealth { get; set; }
    #region ScriptableObject Variables


    public TowerAttackSOBase towerAttackBaseInstance { get; set; }

    public TowerIdleSOBase towerIdleBaseInstance { get; set; }

    #endregion

    #region State Machine Variables

    public TowerStateMachine StateMachine { get; set; }
    public TowerAttackState attackState { get; set; }
    public TowerIdleState idleState { get; set; }


    #endregion

    #region
    [Header("Sounds")]
    public AudioManager.AudioSounds attackSound;

    #endregion

    private void Awake()
    {
        if (needToPlaySizeAnimation)
        {
            StartCoroutine(ScaleCoroutine());
            return;
        }

        InitialazeTower();
    }

    private void InitialazeTower()
    {
        towerAttackBaseInstance = Instantiate(towerAttackBase);
        towerIdleBaseInstance = Instantiate(towerIdleBase);

        StateMachine = new TowerStateMachine();

        idleState = new TowerIdleState(this, StateMachine);
        attackState = new TowerAttackState(this, StateMachine);

        IsInitialized = true;
    }

    private IEnumerator ScaleCoroutine()
    {
        targetScale = transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < scaleChangeDuration)
        {
            // Calculate the current scale based on lerp between starting and target scale
            float t = elapsedTime / scaleChangeDuration;
            transform.localScale = Vector3.Lerp(startingScale, new Vector3(targetScale.x, targetScale.y, targetScale.z), t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we reach exactly the target scale at the end of the coroutine
        transform.localScale = new Vector3(targetScale.x, targetScale.y, targetScale.z);

        InitialazeTower();

    }

    private void Start()
    {
        StartCoroutine(StarUpTowerStates());

    }

    IEnumerator StarUpTowerStates()
    {
        if (needToPlaySizeAnimation)
        {
            yield return new WaitForSeconds(scaleChangeDuration);
        }
        else
        {
            yield return null;
        }

        currentHealth = maxHealth;

        towerIdleBaseInstance.Initialize(gameObject, this);
        towerAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(idleState);

        InvokeRepeating("UpdateTarget", 0, 0.5f);

    }

    private void Update()
    {
        if (!IsInitialized) return;

        if (needToRotateBase)
        {
            RotateBase();
        }

        StateMachine.currentTowerState.FrameUpdate();
    }

    private void AnimationTriggerEvent(AnimationTowerTriggerType triggerType)
    {
        StateMachine.currentTowerState.AnimationTriggerEvent(triggerType);
    }


    #region Take Damage
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        transform.parent.parent.GetComponent<Tile>().isOccupied = false;
        Destroy(transform.parent.gameObject);
    }
    #endregion
    public enum AnimationTowerTriggerType
    {
        Attack,
        Farm,
        AttackSound,
        StopAttack
    }

    private void RotateBase()
    {
        if (targetEnemy != null)
        {
            Vector3 dir = targetEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }

    private void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            Vector3 _enemyPos = enemy.transform.position;
            float distanceToEnemy = Vector3.Distance(transform.position, _enemyPos);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= towerRange)
        {
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
            StateMachine.ChangeState(attackState);
        }
        else if(!currentlyAttacking)
        {
            StateMachine.ChangeState(idleState);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
