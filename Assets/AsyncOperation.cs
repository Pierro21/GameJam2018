using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncOperation : MonoBehaviour
{
    void Start () {
        StartCoroutine (load ());
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds (2f);
        UnityEngine.AsyncOperation asyncOp;
        asyncOp = SceneManager.LoadSceneAsync (2);

        while(!asyncOp.isDone)
        {
            yield return null;
        }
    }
}
