using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Houses;
    public AudioSource HouseUpdateClip;

    public Vector2[] SpawnPositions;

    public Canvas canvas;

    private uint houseIndex = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        UpdateHouse();
    }

    void UpdateHouse()
    {
        houseIndex++;
        Houses[houseIndex].SetActive(true);
        HouseUpdateClip.Play();
    }
}
