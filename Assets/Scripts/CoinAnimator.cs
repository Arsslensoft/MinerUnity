using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAnimator : MonoBehaviour {
    public Sprite[] sprites;
    public float animationSpeed;

    public IEnumerator nukeMethod()
    {
        //destroy all game objects
        for (int i = 0; i < sprites.Length; i++)
        {
            GetComponent<Image>().sprite = sprites[i];
            yield return new WaitForSeconds(animationSpeed);
        }
        StartCoroutine(nukeMethod());
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(nukeMethod());
    }
	

}
