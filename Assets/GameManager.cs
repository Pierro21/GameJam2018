using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Houses;
    public AudioSource HouseUpdateClip;

    public Vector2[] SpawnPositions;

    public Canvas canvas;
    public GameObject fireworks;
    public GameObject labelEnd;
    
    private Character player;
    private Dictionary<string, uint> items;
    private List<string> backpack;
    private List<string> houseStock;

    private uint houseIndex = 0;

    void Start()
    {
        items = new Dictionary<string, uint> {{"barrel", 0}, {"rock", 0}, {"log", 0}};
        houseStock = new List<string>();
        backpack = new List<string>();
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        
        StartCoroutine(DecrementLife());
    }

    void Update()
    {
        if (canvas.GetComponentInChildren<Image>().fillAmount <= 0f)
        {
            StartCoroutine(DecrementLife());
        }
        
        if ((player._life <= 0 || player == null) && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public bool catchItem(string name)
    {
        if (backpack.Count == 3)
        {
            return false;
        }
        items[name] += 1;
        GameObject.Find(name + "_value").GetComponent<Text>().text = items[name].ToString();
        backpack.Add(name);
        return true;
    }

    private void resetbackpack()
    {
        string[] itemArray = {"barrel", "log", "rock"};
        int sum = 0;

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i + " -> " + itemArray[i] + ": " + items[itemArray[i]]);
            sum = (int) (items[itemArray[i]] + 
                         long.Parse(GameObject.Find(itemArray[i] + "_value_panel").GetComponent<TextMesh>().text));
            GameObject.Find(itemArray[i] + "_value_panel").GetComponent<TextMesh>().text = sum.ToString();
            items[itemArray[i]] = 0;
            GameObject.Find(itemArray[i] + "_value").GetComponent<Text>().text = "0";
        }

        backpack.Clear();
    }

    public void UpdateHouse()
    {
        int barrel, log, rock;

        if (backpack.Count == 0)
            return;
        foreach (var item in backpack)
        {
            houseStock.Add(item);
        }
        
        barrel = houseStock.Count(x => x.Equals("barrel"));
        log = houseStock.Count(x => x.Equals("log"));
        rock = houseStock.Count(x => x.Equals("rock"));
        resetbackpack();
        if (rock >= 3 && log >= 3 && houseIndex == 0)
        {
            houseIndex++;
            Houses[houseIndex].SetActive(true);
            HouseUpdateClip.Play();
        } else if (rock >= 4 && log >= 7 && barrel >= 4 && houseIndex == 1)
        {
            houseIndex++;
            Houses[houseIndex].SetActive(true);
            HouseUpdateClip.Play();
            fireworks.SetActive(true);
            labelEnd.GetComponent<TextMesh>().text = "Well done !";
        }
    }

    IEnumerator DecrementLife()
    {
        while (player._life >= 0f)
        {
            player._life -= 0.0001f;
            yield return player.CheckLife();
            Debug.Log(player._life);
            canvas.GetComponentInChildren<Image>().fillAmount = player._life;
            yield return new WaitForSeconds(0.03f);
        }

        if (player._life <= 0f)
        {
            canvas.GetComponentInChildren<Image>().fillAmount = 0;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void skipTutorial()
    {
        if (GameObject.Find("Letter1").GetComponent<Text>().IsActive())
        {
            GameObject.Find("Letter1").GetComponent<Text>().enabled = false;
            GameObject.Find("Letter2").GetComponent<Text>().enabled = true;
        }
        else
        {
            GameObject.Find("Letter2").GetComponent<Text>().enabled = false;
            GameObject.Find("Tutorial").SetActive(false);
        }
    }
}
