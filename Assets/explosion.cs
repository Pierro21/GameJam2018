using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private void Start()
    {
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}
