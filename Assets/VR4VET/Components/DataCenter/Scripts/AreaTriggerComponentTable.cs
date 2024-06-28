using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task;

public class AreaTriggerComponentTable : MonoBehaviour
{

    public Task.TaskHolder taskHolder;
    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        // Check if the entering collider is the player
        if (other.CompareTag("Player") && taskHolder.GetTask("Perform Maintenance").GetSubtask("Prepare for Maintenance").Compleated())
        {
            // Player entered the trigger area
            taskHolder.GetTask("Perform Maintenance").GetSubtask("Perform Maintenance").GetStep("Check Out Component Table").SetCompleated(true);
        }
    }
}
