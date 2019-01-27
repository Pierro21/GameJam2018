using System.Collections;
using UnityEngine;

public class Bow : Weapon
{
    public GameObject _arrow;
    [SerializeField] private Transform _target;
    [SerializeField] private Character _parent;
    private Vector3 v_diff;
    private float atan2;

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected override IEnumerator RealActivateTrigger(float duration)
    {
        _isShooting = true;
        GameObject clone;
        clone = Instantiate(_arrow, transform.position, Quaternion.Euler(transform.eulerAngles));
        clone.GetComponent<Rigidbody2D>().AddForce(_target.position * 4, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        _isShooting = false;
    }
}