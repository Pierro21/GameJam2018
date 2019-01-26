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
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var isDead = other.GetComponent<Character>().GetDamage(10);
            if (isDead)
                Debug.Log("I'm happy!!!");
        }
    }
}
