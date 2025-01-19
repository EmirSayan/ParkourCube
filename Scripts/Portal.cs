using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneNumber = 0;
  
    public int GetSceneNumber()
    {
        return sceneNumber;
    }

}
