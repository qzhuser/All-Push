using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    bool o_isPlayerInRange = false;
    public EndingTrigger gameend;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            o_isPlayerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            o_isPlayerInRange = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (o_isPlayerInRange) {
            Ray ray = new Ray(transform.position,transform.forward);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player") {
                    gameend.ZhuaDaoPlayer();
                }
            }
        }
    }
}
