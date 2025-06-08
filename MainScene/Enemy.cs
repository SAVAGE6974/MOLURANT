using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NEWUI mainUI; // NEWUI 스크립트가 붙은 오브젝트를 넣어야 함
    public Transform tower; // 타워의 Transform을 넣어야 함

    [Header("Movement")]
    public float speed = 3f;
    public float stopDistance = 1f;

    [Header("Stats")]
    public float enemyHp = 100f;
    public static float AL_1S_damage = 100f;
    public static float Wakamo_damage = 80f;

    [Header("AOE")]
    public float aoeRange = 15f;
    public float aoeDamage = 100f;
    public float Wakamo_C_directHitDamage = 100f;

    private bool isAttacking = false;
    private Coroutine wakamoBuffCoroutine;

    private bool isClone = false; // 복제 여부 판단용

    private void Awake()
    {
        aoeDamage = 100f;
        isClone = gameObject.name.Contains("(Clone)");
    }

    private void Start()
    {
        if (mainUI == null)
        {
            mainUI = FindObjectOfType<NEWUI>();
            if (mainUI == null)
            {
                Debug.LogWarning("mainUI가 할당되지 않았고, 씬에서 NEWUI를 찾을 수 없습니다.");
            }
        }
    }

    private void Update()
    {
        if (isClone)
        {
            GoAttack();
        }
    }

    public void Enemyhit()
    {
        if (!isClone) return;

        if (LockinManager.lastSelectedCharacter == "Wakamo")
        {
            if (NEWGamemenager.Wakamo_useE)
            {
                ActivateWakamoBuff();
            }

            Debug.Log("Wakamo");

            if (NEWGamemenager.Wakamo_useC)
            {
                enemyHp -= Wakamo_C_directHitDamage;
                ApplyAOEDamage();
            }
            else
            {
                enemyHp -= Wakamo_damage;
                Debug.Log(enemyHp);
            }

            if (enemyHp <= 0)
                gameObject.SetActive(false);
        }
        else if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            enemyHp -= AL_1S_damage;
            Debug.Log("EnemyHp: " + enemyHp);

            if (enemyHp <= 0)
                gameObject.SetActive(false);
        }
    }

    private void ActivateWakamoBuff()
    {
        if (wakamoBuffCoroutine != null)
            return;

        wakamoBuffCoroutine = StartCoroutine(WakamoBuffRoutine());
    }

    private IEnumerator WakamoBuffRoutine()
    {
        Wakamo_damage = 100;
        Debug.Log("Wakamo 버프 적용됨 (15초간 데미지 100)");
        yield return new WaitForSeconds(15f);

        Wakamo_damage = 80;
        NEWGamemenager.Wakamo_useE = false;
        wakamoBuffCoroutine = null;

        Debug.Log("Wakamo 버프 종료됨 (데미지 80으로 복원)");
    }

    private void ApplyAOEDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, aoeRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy") && enemy.gameObject != this.gameObject)
            {
                NEWEnemy newEnemy = enemy.GetComponent<NEWEnemy>();
                if (newEnemy != null)
                {
                    newEnemy.TakeAOEDamage(aoeDamage);
                }
            }
        }
    }

    public void TakeAOEDamage(float damage)
    {
        enemyHp -= damage;
        Debug.Log($"{gameObject.name} AOE 데미지 {damage} 받음! 현재 체력: {enemyHp}");

        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void GoAttack()
    {
        if (NEWUI.canGoAttack)
        {
            if (tower == null) return;

            Vector3 direction = tower.position - transform.position;
            direction.y = 0;

            float distance = direction.magnitude;

            if (distance > stopDistance)
            {
                Vector3 moveDir = direction.normalized;
                transform.position += moveDir * (speed * Time.deltaTime);
            }
            else
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackTowerRoutine());
                }
            }
        }
    }

    private IEnumerator AttackTowerRoutine()
    {
        if (!isClone) yield break;

        isAttacking = true;

        while (NEWUI.canGoAttack)
        {
            if (mainUI != null)
                mainUI.HitTower();

            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }
}
