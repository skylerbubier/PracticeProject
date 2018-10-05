using UnityEngine;
using System.Collections;

public class InfiniteScroll : MonoBehaviour
{
    private float scrollSpeed = 1;
    private Vector3 currentPosition;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        float offset = Time.deltaTime * scrollSpeed;
        //Debug.Log(offset);
        transform.position = currentPosition - Vector3.right * offset;
        if(transform.position.x < 0)
        {
            Vector3 temp = new Vector3(30, 0, 0);
            transform.position += temp;
        }
        currentPosition = transform.position;

    }
}