using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGrowth : MonoBehaviour
{
    public int Generation;
    public GameObject[] Prefabs;
    public float RadiusScaleRatio,Radius;
    public GameObject Forest;
    public float DistanceBetweenTrees = 10.35276f;
    public static List<GameObject> ObjectsToMineFrom = new List<GameObject>();
    public static int Population;
    public int PopulationPerGeneration = 0;


	// Use this for initialization
	void Start ()
	{
	    Generation = 1;
	    Upgrade();
	    

    }
    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
      
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 0;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    public void Upgrade()
    {
        Generation++;
        Radius += RadiusScaleRatio;
        if(Generation > 5)
            transform.localScale = new Vector3(transform.localScale.x + 2*RadiusScaleRatio, transform.localScale.y, transform.localScale.z + 2 * RadiusScaleRatio);

        Vector3 center = new Vector3(-6.95f, -2.269761f, -3.206319f);
        int count = (int)(2*Mathf.PI * Radius / DistanceBetweenTrees);
        for (int i = 0; i < count; i++)
        {
            int a = i * (360 / count);
            
           Vector3 pos = RandomCircle(center, Radius, a);
           var tree = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], pos, Quaternion.identity);
            tree.transform.parent = Forest.transform;
            lock (ObjectsToMineFrom)
            {
                ObjectsToMineFrom.Add(tree);
            }
        }
        PopulationPerGeneration = count * 2;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            ScreenFaderSingleton.Instance.FadeAndLoadPreviousLevel();

        if(Population > (PopulationPerGeneration * Generation))
            Upgrade();
    }

}
