using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fakeLoader : MonoBehaviour
{
    private Text m_Text;

    void Start () {
        m_Text = gameObject.GetComponent<Text> ();
        gameObject.GetComponent<Text> ().text = "";
        StartCoroutine (fakeload ());
    }

    IEnumerator fakeload()
    {
        yield return new WaitForSeconds (0.5f);
        m_Text.text = ".";
        yield return new WaitForSeconds (0.5f);
        m_Text.text = "..";
        yield return new WaitForSeconds (0.5f);
        m_Text.text = "...";
        yield return new WaitForSeconds (0.5f);
        m_Text.text = "";
        StartCoroutine (fakeload ());
    }
}
