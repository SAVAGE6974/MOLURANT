using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Text bulletText;
    public Text hpText;
    private int _bulletCount = 5;
    // private int _clip = 0;
    public static float _hp = 100f;

    private void Awake()
    {
        bulletText.text = _bulletCount.ToString();
    }

    public void UseBullet()
    {
        if (_bulletCount > 0)
        {
            _bulletCount--;
            bulletText.text = _bulletCount.ToString();
        }
    }

    public void HitTower()
    {
        _hp--;
        hpText.text = _hp.ToString();
    }
}