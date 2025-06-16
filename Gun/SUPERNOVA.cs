using System.Collections;
using UnityEngine;

public class SuperNova : MonoBehaviour
{
    private Vector3 _originalPosition;
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 20f;

    public static string gunName;

    private float _keyHoldTime = 0f;  // E 키를 누른 시간

    // _hasTriggered를 static 변수로 유지하여, 여러 인스턴스에서 공유
    public bool _hasTriggered = false;  // 모든 SuperNova 인스턴스에서 공유됨
    
    public GameObject destorySidebarPannel;

    private void Awake()
    {
        _originalPosition = transform.localPosition;
        gunName = LockinManager.lastSelectedCharacter;
    }

    private void Start()
    {
        if (gunName != "AL-1S")
        {
            Destroy(this.gameObject);
            // Destroy(this.destorySidebarPannel);
            destorySidebarPannel.SetActive(false);
        }
    }

    private void Update()
    { 
        useE();
    }

    public void useE()
    {
        // 쿨타임이 끝났고, 이전에 스킬을 한 번 썼다면 초기화 e스킬
        if (NEWGamemenager.AL_1S_canUseSkillE && _hasTriggered)
        {
            _hasTriggered = false;
            shakeAmount = 0.05f;
        }

        // E 키 누르기 처리
        if (NEWGamemenager.AL_1S_canUseSkillE)
        {
            UpNDown();
        }
        else
        {
            // 쿨타임 중에는 리셋만 해줌
            transform.localPosition = _originalPosition;
            _keyHoldTime = 0f;
        }
    }
    

    private void UpNDown()
    {
        if (!NEWGamemenager.AL_1S_canUseSkillE)
        {
            // 쿨타임 중이면 아무 동작도 하지 않음
            transform.localPosition = _originalPosition;
            _keyHoldTime = 0f;
            _hasTriggered = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _keyHoldTime += Time.deltaTime;

            if (_keyHoldTime < 3f)
            {
                float yOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
                transform.localPosition = _originalPosition + new Vector3(0, yOffset, 0);
            }

            if (_keyHoldTime >= 3f && !_hasTriggered)
            {
                NEWGamemenager.AL1S_skillE();
                _hasTriggered = true;
                shakeAmount = 0f;
            }
        }
        else
        {
            transform.localPosition = _originalPosition;
            _keyHoldTime = 0f;
            _hasTriggered = false;
        }
    }

}