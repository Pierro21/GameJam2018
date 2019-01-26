using UnityEngine;
using UnityStandardAssets._2D;

public class Character : MonoBehaviour
{
    private int _life = 100;

    public bool GetDamage(int damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Destroy(this);
            return true;
        }
        Debug.Log("Get hitt! Life:" + _life);
        return false;
    }
}