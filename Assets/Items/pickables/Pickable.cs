using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    private bool _isColliding = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, 8, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isColliding)
        {
            _isColliding = true;
            if (GameObject.Find("GAME_MANAGER").GetComponent<GameManager>().catchItem(gameObject.name))
                Destroy(gameObject);
            else
                _isColliding = false;
        }
    }
}
