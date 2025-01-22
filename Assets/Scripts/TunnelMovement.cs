using UnityEngine;

public class tunnelMovement : MonoBehaviour
{
    public GameObject tunnel1; // Reference to the first tunnel
    public GameObject tunnel2; // Reference to the second tunnel
    public float speed = 80f; // Movement speed
    public float tunnelLength = 708.1226f; // Length of the tunnel (distance between their positions)
   

    void Update()
    {
        float x = tunnel1.transform.position.x;
        float x2 = tunnel2.transform.position.x;
        float y = tunnel1.transform.position.y;

        // Move Tunnel1 backward along the z-axis
        tunnel1.transform.position += Vector3.back * speed * Time.deltaTime;
        // Maintain Tunnel1's x and y position
        tunnel1.transform.position = new Vector3(x, y, tunnel1.transform.position.z);

        // Move Tunnel2 backward along the z-axis
        tunnel2.transform.position += Vector3.back * speed * Time.deltaTime;
        // Maintain Tunnel2's x and y position
        tunnel2.transform.position = new Vector3(x2 ,y, tunnel2.transform.position.z);

        // If Tunnel1 moves past its reset position, reposition it
        if (tunnel1.transform.position.z <= -301.8f) // Tunnel2's start position
        {
            tunnel1.transform.position += Vector3.forward * tunnelLength * 2;
        }

        // If Tunnel2 moves past its reset position, reposition it
        if (tunnel2.transform.position.z <= -301.8f) // Tunnel1's start position
        {
            tunnel2.transform.position += Vector3.forward * tunnelLength * 2;
        }
    }
}
