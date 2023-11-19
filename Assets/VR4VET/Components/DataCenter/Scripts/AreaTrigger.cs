using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task;

public class AreaTrigger : MonoBehaviour
{

    public Task.TaskHolder taskHolder;
    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player"))
        {
            // Player entered the trigger area
            Debug.Log("Player entered the trigger area");
            taskHolder.GetTask("Perform Maintenance").GetSubtask("Perform Maintenance").GetStep("Enter Server Room").SetCompleated(true);
        }
    }
}
