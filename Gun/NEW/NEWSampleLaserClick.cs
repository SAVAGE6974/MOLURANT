using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NEWSampleLaserClick : MonoBehaviour
{
    public AudioSource WakamoSound;
    public AudioSource Al1Sound;
    
    public Image targetUIImage;
    
    public static NEWSampleLaserClick instance;
    public NEWUI ui;

    [Header("레이저 오브젝트 할당")]
    public GameObject laserObject;

    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private Camera playerCamera;
    public float laserDistance = 100f;
    public float laserDuration = 0.1f;

    public static int WakamogunbulletNum = 8;
    public static int AL1SgunbulletNum = 8;

    private Coroutine disableLaserCoroutine;

    private void Awake()
    {
        instance = gameObject.GetComponent<NEWSampleLaserClick>();
        Debug.Log("Line 28" + laserObject != null);

        if (laserObject != null)
        {
            _collider = laserObject.GetComponent<Collider>();
            Debug.Log("Line 32" + _collider != null);
            _meshRenderer = laserObject.GetComponent<MeshRenderer>();

            if (_collider == null)
            {
                Debug.LogError("laserObject에 Collider 컴포넌트가 없습니다.");
            }
            else
            {
                _collider.isTrigger = false;
                _collider.enabled = false;
            }

            if (_meshRenderer == null)
            {
                Debug.LogError("laserObject에 MeshRenderer 컴포넌트가 없습니다.");
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }

        if (ui == null)
        {
            ui = FindObjectOfType<NEWUI>();
        }

        if (playerCamera == null)
        {
            playerCamera = Camera.main;

            if (playerCamera == null)
            {
                Debug.LogError("Player Camera가 할당되지 않았고, 씬에 MainCamera 태그가 없습니다.");
            }
        }
    }

    public static void useWakamoSkillX()
    {
        if (instance == null || instance.laserObject == null) return;

        Debug.Log("Wakamo X 스킬 발동: 20초 동안 공격 범위 증가");

        // CapsuleCollider가 존재한다면 radius 키움
        if (instance._collider is CapsuleCollider capsule)
        {
            capsule.radius = 1f;
        }

        // 레이저 오브젝트 스케일 증가 (폭, 깊이)
        Vector3 scale = instance.laserObject.transform.localScale;
        scale.x = 2f;
        scale.z = 2f;
        instance.laserObject.transform.localScale = scale;

        // 시각적 효과: 레이저 활성화
        if (instance._collider != null)
            instance._collider.enabled = true;

        if (instance._meshRenderer != null)
            instance._meshRenderer.enabled = true;

        // 레이저 발사 실행
        instance.FireWideLaser();

        // ⚠️ 20초 동안 범위 확대 유지
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DisableLaserAfter20Seconds());
    }

    private IEnumerator DisableLaserAfter20Seconds()
    {
        // ▶ 20초 동안 실행됨
        yield return new WaitForSeconds(20f);
        NEWEnemy.Wakamo_damage += 20;

        // 레이저 시각적 비활성화
        if (_collider != null)
            _collider.enabled = false;

        if (_meshRenderer != null)
            _meshRenderer.enabled = false;

        // 스케일 원상복구
        if (laserObject != null)
            laserObject.transform.localScale = new Vector3(0.1f, 1.25f, 0.1f);

        if (_collider is CapsuleCollider capsule)
        {
            capsule.radius = 0.5f;
        }

        Debug.Log("Wakamo X 스킬 종료: 공격 범위 정상화");
        NEWEnemy.Wakamo_damage -= 20;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && NEWUI.canFire)
        {
            if (LockinManager.lastSelectedCharacter == "Wakamo")
            {
                if (WakamogunbulletNum > 0 && Open_buy_phase_Wakamo.Wakamo_isOpen == false)
                {
                    ActivateLaser();
                    FireLaser();
                    StartCoroutine(PlayWakamoSound()); // 변경된 부분

                    ui.UseBullet();
                    Debug.Log("Wakamo 총알 수 감소: " + WakamogunbulletNum);
                    WakamogunbulletNum--;
                    if (targetUIImage.color == Color.cyan)
                    {
                        targetUIImage.color = new Color(0.96f, 0.96f, 0.96f, 1f);
                    }
                }
            }
            else if (LockinManager.lastSelectedCharacter == "AL-1S")
            {
                if (AL1SgunbulletNum > 0 && Open_buy_phase_AL1S.AL1S_isOpen == false)
                {
                    ActivateLaser();
                    FireLaser();
                    Al1Sound.Play();

                    ui.UseBullet();
                    Debug.Log("AL-1S 총알 수 감소: " + AL1SgunbulletNum);
                    AL1SgunbulletNum--;
                }
            }
        }
    }

    private IEnumerator PlayWakamoSound()
    {
        WakamoSound.Play();
        yield return new WaitForSeconds(WakamoSound.clip.length);
        Debug.Log("사운드 재생 완료");
    }

    public void AL1S_use_Q()
    {
        Debug.Log("FireLaserFor8Seconds 시작");
        StartCoroutine(FireLaserFor8Seconds());
    }

    public IEnumerator FireLaserFor8Seconds()
    {
        float duration = 8f;
        float elapsed = 0f;
        float interval = 0.2f;

        while (elapsed < duration)
        {
            ActivateLaser();
            FireLaser();

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        Debug.Log("8초 동안 레이저 발사 완료");
    }

    private void ActivateLaser()
    {
        _collider.enabled = true;
        _meshRenderer.enabled = true;

        if (disableLaserCoroutine != null)
        {
            StopCoroutine(disableLaserCoroutine);
        }
        disableLaserCoroutine = StartCoroutine(DisableLaserTemporarily());
    }

    private IEnumerator DisableLaserTemporarily(bool wasScaledUp = false)
    {
        yield return new WaitForSeconds(laserDuration);

        if (laserObject == null) yield break;

        if (_collider != null)
            _collider.enabled = false;

        if (_meshRenderer != null)
            _meshRenderer.enabled = false;

        if (wasScaledUp)
        {
            laserObject.transform.localScale = new Vector3(0.1f, 1.25f, 0.1f);

            if (_collider is CapsuleCollider capsule)
            {
                capsule.radius = 0.5f;
            }
        }
    }

    public void FireLaser()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, laserDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Hit");

                NEWEnemy enemy = hit.collider.GetComponent<NEWEnemy>();
                if (enemy != null)
                {
                    enemy.Enemyhit();
                }
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
        }
    }

    public void FireLaserX()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, laserDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("궁극기로 Enemy Hit");

                NEWEnemy enemy = hit.collider.GetComponent<NEWEnemy>();
                if (enemy != null)
                {
                    NEWEnemy.AL_1S_damage = 356;
                    enemy.Enemyhit();
                    Debug.Log("궁 데미지: " + NEWEnemy.AL_1S_damage);

                    NEWEnemy.AL_1S_damage = 100;
                }
            }

            Debug.DrawLine(ray.origin, hit.point, Color.magenta, 1f);
        }
    }

    public static void fireE()
    {
        if (instance == null || instance.laserObject == null) return;

        if (instance._collider is CapsuleCollider capsule)
        {
            capsule.radius = 1f;
        }

        Vector3 scale = instance.laserObject.transform.localScale;
        scale.x = 2f;
        scale.z = 2f;
        instance.laserObject.transform.localScale = scale;

        if (instance._collider != null)
            instance._collider.enabled = true;

        if (instance._meshRenderer != null)
            instance._meshRenderer.enabled = true;

        instance.FireWideLaser();

        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DisableLaserTemporarily(true));
    }

    private void FireWideLaser()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        float radius = 1.5f;
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, laserDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("광범위 스킬로 Enemy Hit");

                NEWEnemy enemy = hit.collider.GetComponent<NEWEnemy>();
                if (enemy != null)
                {
                    enemy.Enemyhit();
                }
            }

            Debug.DrawLine(ray.origin, hit.point, Color.yellow, 1f);
        }
    }
}
