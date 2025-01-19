using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Vector3 endPoint;    // Bitiþ noktasý
    public float speed = 5.0f;  // Platformun hýzý

    private Vector3 startPoint;
    private Vector3 currentTarget;

    void Start()
    {
        startPoint = transform.position; // Baþlangýç pozisyonunu objenin sahnedeki konumu olarak ayarla
        currentTarget = endPoint;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (transform.position == endPoint)
        {
            currentTarget = startPoint;
        }
        else if (transform.position == startPoint)
        {
            currentTarget = endPoint;
        }
    }
}
