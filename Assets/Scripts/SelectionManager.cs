using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    private void OnMouseDown()
    {
            Camera.main.GetComponent<CameraControl>().lookAt = gameObject.transform;
    }

  
}
