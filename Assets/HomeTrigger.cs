using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeTrigger : MonoBehaviour
{
    private GameObject HUD;

    private bool isIncrementing;
    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.FindGameObjectWithTag("Canvas");
        isIncrementing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator IncrementLife()
    {
        isIncrementing = true;
        while (HUD.GetComponentInChildren<Image>().fillAmount <= 0.9f)
        {
            HUD.GetComponentInChildren<Image>().fillAmount += 0.001f;
            if (!isIncrementing)
                yield break;
            yield return new WaitForSeconds(0.03f);
        }
        isIncrementing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isIncrementing)
        {
            StartCoroutine(IncrementLife());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIncrementing = false;
            StopCoroutine(IncrementLife());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("GAME_MANAGER").GetComponent<GameManager>().UpdateHouse();
        }
    }
}
