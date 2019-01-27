using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _damage;
    protected bool _isShooting = false;
    [SerializeField] protected string _tag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            Debug.Log("touched! by" + gameObject.name + " to " + other.gameObject.name + "(Tag: " + _tag + "/" +
                      other.tag + ")");
            other.GetComponent<Character>().GetDamage(_damage);
        }
    }

    protected virtual IEnumerator RealActivateTrigger(float duration)
    {
        Debug.Log("Trigger");
        GetComponent<PolygonCollider2D>().enabled = true;
        yield return new WaitForSeconds(duration);
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return null;
    }

    public void ActivateTrigger(float duration)
    {
        Debug.Log("ici");
        if (!_isShooting)
        {
            
            StartCoroutine(RealActivateTrigger(duration));
        }
    }
}