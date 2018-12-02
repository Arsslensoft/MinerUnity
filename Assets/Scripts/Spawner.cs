using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject WorkerPrefab;
    public Text Pop;
    private GameObject castle;
	// Use this for initialization
	void Start () {
	    castle = GameObject.FindGameObjectWithTag("Castle");
	    
    }

    public void Spawn()
    {
      Instantiate(WorkerPrefab, castle.transform.position, Quaternion.identity);
        MapGrowth.Population++;
        Pop.text = MapGrowth.Population.ToString();
    }

}
