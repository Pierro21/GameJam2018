using UnityEngine;

public class BowArrow : Weapon
{
    private void Start()
    {
        Destroy(gameObject, 3);
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void FixedUpdate()
    {
        //transform.Rotate(new Vector3(0, 0,360f - Vector3.Angle(transform.right, GetComponent<Rigidbody2D>().velocity.normalized)));
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            Debug.Log("touched! by" + gameObject.name + " to " + other.gameObject.name + "(Tag: " + _tag + "/" + other.tag + ")");
            other.GetComponent<Character>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}