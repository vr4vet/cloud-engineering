using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task;

public class AreaTriggerSecurityOffice : MonoBehaviour
{

    public Task.TaskHolder taskHolder;
    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player") && taskHolder.GetTask("Perform Maintenance").Compleated())
        {
            // Player entered the trigger area
            Debug.Log("Player entered the trigger area");
            taskHolder.GetTask("Close Ticket").GetSubtask("Return to the Control Room").SetCompleated(true);
        }
    }
}
