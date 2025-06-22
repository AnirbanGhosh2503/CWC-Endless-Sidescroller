using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; // Store the initial position of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Get the width of the background from its collider
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x-repeatWidth) // Check if the background has moved past a certain point
        {
            transform.position = startPos; // Reset the position to the starting point
        }
    }
}
