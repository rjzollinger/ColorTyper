using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float speed;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject manager = GameObject.Find("Manager");
        manager.GetComponent<Manager>().gameLost = true;
    }
}
