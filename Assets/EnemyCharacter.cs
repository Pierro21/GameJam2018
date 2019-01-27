using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SALUT TOI");
    }

    // Update is called once per frame

    private IEnumerator Shoot()
    {
        _isShooting = true;
        Move(0, true, false, false);
        yield return new WaitForSeconds(2);
        _isShooting = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isShooting)
                StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        var tmp = _weapon.transform.eulerAngles.z;
        //Debug.Log(m_FacingRight + ":::::" + tmp);
        if (tmp < 250 && tmp > 110)
        {
            if (m_FacingRight)
                Flip();
        }
        else if ((tmp < 70 || tmp > 290) && !m_FacingRight)
            Flip();
    }
}