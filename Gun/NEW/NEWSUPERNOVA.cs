using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWSUPERNOVA : MonoBehaviour
{
    private float eKeyHoldTime = 0f;
    private bool hasTriggered = false;

    private float floatSpeed = 2f;
    private float floatAmount = 0.05f;
    private Vector3 originalLocalPosition;

    private void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (NEWGamemenager.AL_1S_canUseSkillE)
            UpNDownSkillE();
    }

    private void UpNDownSkillE()
    {
        if (Input.GetKey(KeyCode.E))
        {
            eKeyHoldTime += Time.deltaTime;

            if (eKeyHoldTime < 3f)
            {
                float offsetX = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
                float offsetY = Mathf.Cos(Time.time * floatSpeed) * floatAmount;
                transform.localPosition = originalLocalPosition + new Vector3(offsetX, offsetY, 0);
            }
            else
            {
                transform.localPosition = originalLocalPosition;

                // if (!hasTriggered)
                // {
                //     Debug.Log("E 키를 3초 동안 누름!");
                //     hasTriggered = true;
                //
                //     // 여기에서 광범위 레이저 발사
                //     NEWSampleLaserClick.fireE();
                //     NEWGamemenager.AL_1S_canUseSkillE = false;
                // }
                if (!hasTriggered)
                {
                    Debug.Log("E 키를 3초 동안 누름!");
                    hasTriggered = true;

                    // 쿨타임 시작 + 레이저 발사
                    NEWGamemenager.AL1S_skillE();
                }
            }
        }
        else
        {
            transform.localPosition = originalLocalPosition;
            eKeyHoldTime = 0f;
            hasTriggered = false;
        }
    }
}