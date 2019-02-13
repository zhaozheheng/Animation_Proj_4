using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        GetComponent<SkinnedMeshRenderer>().bones[0].Rotate(new Vector3(0, 6, 0));
    }
}


