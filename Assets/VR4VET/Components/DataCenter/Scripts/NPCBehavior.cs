// <copyright file="NPCBehavior.cs" company="VR4VET">
// MIT License
//
// Copyright (c) 2023 VR4VET
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>

using System.Text.RegularExpressions;
using DataCenter;
using DataCenter.Events;
using Tablet;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for the behavior of the NPC.
/// </summary>
public class NPCBehavior : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 2f;
    [SerializeField]
    private GameObject textPrefab;
    [SerializeField]
    private GameObject keyPrefab;

    private string matchedSubstring;
    private string errorMessageHardwareProblem;
    private GameObject textInstance;
    private GameObject keyInstance;
    private Transform playerTransform;
    private bool keyInstantiated = false;
    private bool isPlayerInRange = false;
    private bool ticketAccepted = false;
    private bool ticketFinished = false;

    /// <summary>
    /// This method is used to return the key instance.
    /// </summary>
    /// <returns> Returns the key instance.</returns>
    public GameObject GetKeyInstance()
    {
        return this.keyInstance;
    }

    /// <summary>
    /// This method is used to return the closet.
    /// </summary>
    /// <returns>The closet.</returns>
    public string GetMatchedSubstring()
    {
        return this.matchedSubstring;
    }

    /// <summary>
    /// Sets the key instance.
    /// </summary>
    /// <param name="keyInstance"> Keyinstance.</param>
    public void SetKeyInstance(GameObject keyInstance)
    {
        this.keyInstance = keyInstance;
    }

    /// <summary>
    /// This method is used to return the detection radius.
    /// </summary>
    /// <returns>Returns the detection radius.</returns>
    public float GetDetectionRadius()
    {
        return this.detectionRadius;
    }

    /// <summary>
    /// Sets the detection radius.
    /// </summary>
    /// <param name="detectionRadius"> the radius.</param>
    public void SetDetectionRadius(float detectionRadius)
    {
        this.detectionRadius = detectionRadius;
    }

    /// <summary>
    /// This method is used to instantiate the key.
    /// </summary>
    /// <returns>It returns the text prefab.</returns>
    public GameObject GetTextPrefab()
    {
        return this.textPrefab;
    }

    /// <summary>
    /// This method is used to set the text prefab.
    /// </summary>
    /// <param name="textPrefab">is the text prefab.</param>
    public void SetTextPrefab(GameObject textPrefab)
    {
        this.textPrefab = textPrefab;
    }

    /// <summary>
    /// This method is used to get the text instance.
    /// </summary>
    /// <returns>Returns the text instance.</returns>
    public GameObject GetTextInstance()
    {
        return this.textInstance;
    }

    /// <summary>
    /// This method sets the text instance.
    /// </summary>
    /// <param name="textInstance">Returns the text instance.</param>
    public void SetTextInstance(GameObject textInstance)
    {
        this.textInstance = textInstance;
    }

    /// <summary>
    /// This method is used to instantiate the key.
    /// </summary>
    /// <returns>This returns the key prefab.</returns>
    public GameObject GetKeyPrefab()
    {
        return this.keyPrefab;
    }

    /// <summary>
    /// This method is used to instantiate the key.
    /// </summary>
    /// <param name="prefab"> returns the prefab.</param>
    public void SetKeyPrefab(GameObject prefab)
    {
        this.keyPrefab = prefab;
    }

    /// <summary>
    /// This method is used to instantiate the key.
    /// </summary>
    /// <returns>returns boolean if key is instantiated.</returns>
    public bool GetKeyInstantiated()
    {
        return this.keyInstantiated;
    }

    /// <summary>
    /// Sets the key instantiated.
    /// </summary>
    /// <param name="keyInstantiated">The key instantiated.</param>
    public void SetKeyInstantiated(bool keyInstantiated)
    {
        this.keyInstantiated = keyInstantiated;
    }

    /// <summary>
    /// Gets the player in range.
    /// </summary>
    /// <returns> the accepted ticket.</returns>
    public bool GetTicketAccepted()
    {
        return this.ticketAccepted;
    }

    /// <summary>
    /// Gets the player in range.
    /// </summary>
    /// <returns> a bool.</returns>
    public bool GetPlayerInRange()
    {
        return this.isPlayerInRange;
    }

    /// <summary>
    /// Sets the player in range.
    /// </summary>
    /// <param name="isPlayerInRange"> The boolean if in range.</param>
    public void SetPlayerInRange(bool isPlayerInRange)
    {
        this.isPlayerInRange = isPlayerInRange;
    }

    /// <summary>
    /// This method is set the ticket.
    /// </summary>
    /// <param name="ticketAccepted"> boolean.</param>
    /// <returns>Returns the bool.</returns>
    public bool SetTicketAccepted(bool ticketAccepted)
    {
        return this.ticketAccepted = ticketAccepted;
    }

    /// <summary>
    /// This method assigns the hands.
    /// </summary>
    public void Start()
    {
        this.playerTransform = GameObject.FindGameObjectWithTag("Hands").transform;
    }

    /// <summary>
    /// Gets the player transform.
    /// </summary>
    /// <returns>Returns the location.</returns>
    public Transform GetPlayerTransform()
    {
        return this.playerTransform;
    }

    /// <summary>
    /// Sets the player transform.
    /// </summary>
    /// <param name="playerTransform">returns the transform.</param>
    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    /// <summary>
    /// This method is used to update the NPC behavior.
    /// </summary>
    public void FixedUpdate()
    {
        bool isPlayerClose = this.IsPlayerClose();

        if (isPlayerClose && !this.isPlayerInRange)
        {
            if (!this.ticketAccepted)
            {
                this.DisplayText("Ticket should be accepted first.");
            }
            else if (!this.keyInstantiated)
            {
                this.InstantiateKey();
                this.DisplayText("Here is the key for " + this.matchedSubstring + ".");
                Activity activity = DataCenterScenario.Instance.PerformMaintenanceTask.GetKeyToCabinet;
                DataCenterScenario.Instance.SetActivityCompleted(activity, true);
            }
            else if (this.keyInstantiated && !this.ticketFinished)
            {
                this.DisplayText("Make sure you finish all tasks before giving back the key. And don't forget to close the ticket too!");
            }
            else if (this.keyInstantiated && this.ticketFinished)
            {
                this.DestroyKey();
                this.DisplayText("Thank you for handing in the key.");
                Activity activity = DataCenterScenario.Instance.PerformMaintenanceTask.ReturnCabinetKey;
                DataCenterScenario.Instance.SetActivityCompleted(activity, true);
            }

            this.isPlayerInRange = true;
        }
        else if (!isPlayerClose && this.isPlayerInRange)
        {
            this.HideText();
            this.isPlayerInRange = false;
        }
    }

    /// <summary>
    /// This method displays the text.
    /// </summary>
    /// <param name="message"> The message that will be printed.</param>
    public void DisplayText(string message)
    {
        if (this.textInstance == null)
        {
            this.textInstance = Instantiate(this.textPrefab, this.transform.position + (Vector3.up * 2.5f), Quaternion.identity);
        }

        // Update the text content
        TextMeshProUGUI textMeshPro = this.textInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.text = message;
        }

        this.textInstance.transform.rotation = this.transform.rotation;
    }

    /// <summary>
    /// This method is used to check if the player is close to the NPC.
    /// </summary>
    /// <returns>It returns a bool.</returns>
    public bool IsPlayerClose()
    {
        float distance = Vector3.Distance(this.transform.position, this.playerTransform.position);
        return distance <= this.detectionRadius;
    }

    /// <summary>
    /// This method is used to hide the text.
    /// </summary>
    public void HideText()
    {
        if (this.textInstance != null)
        {
            Destroy(this.textInstance);
        }
    }

    /// <summary>
    /// This method is used to instantiate the key.
    /// </summary>
    public void InstantiateKey()
    {
        if (this.keyInstance == null && this.ticketAccepted)
        {
            Vector3 dropPosition = this.transform.position + (this.transform.forward * 1f) + (Vector3.up * 2.0f);
            this.keyInstance = Instantiate(this.keyPrefab, dropPosition, Quaternion.identity);
            this.keyInstantiated = true;
        }
    }

    /// <summary>
    /// This method is used to destroy the key instance.
    /// </summary>
    public void DestroyKey()
    {
        if (this.keyInstance != null)
        {
            Destroy(this.keyInstance);
            this.keyInstantiated = false;
        }
    }

    /// <summary>
    /// This method is used to subscribe to the event.
    /// </summary>
    public void OnEnable()
    {
        // Subscribe to the event
        DataCenterScenario.Instance.EventBus.TicketAccepted += this.OnTicketAccepted;
        DataCenterScenario.Instance.EventBus.TicketFinished += this.OnTicketFinished;
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated += this.OnHardwareProblemGenerated;
    }

    /// <summary>
    /// This method is used to unsubscribe from the event.
    /// </summary>
    public void OnDisable()
    {
        // Unsubscribe from the event
        DataCenterScenario.Instance.EventBus.TicketAccepted -= this.OnTicketAccepted;
        DataCenterScenario.Instance.EventBus.TicketFinished -= this.OnTicketFinished;
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated -= this.OnHardwareProblemGenerated;
    }

    /// <summary>
    /// This method is used to handle the event.
    /// </summary>
    /// <param name="ticketAcceptedEvent"> The acceptedTicket.</param>
    public void OnTicketAccepted(TicketAcceptedEvent ticketAcceptedEvent)
    {
        // Handle the event
        this.ticketAccepted = true;
    }

    /// <summary>
    /// This method is used to set ticketFinished to true.
    /// </summary>
    /// <param name="ticketFinishedEvent">The ticketFinished.</param>
    public void OnTicketFinished(TicketFinishedEvent ticketFinishedEvent)
    {
        // Handle the event
        this.ticketFinished = true;
    }

    /// <summary>
    /// A method that retrieves the error message from the event.
    /// </summary>
    /// <param name="e"> The genereted hardware event. </param>
    /// <returns> Returns a string containing the message. </returns>
    public string GetErrorMessage(HardwareProblemGeneratedEvent e)
    {
        return e.HardwareProblem.Message;
    }

    /// <summary>
    /// This method is called when a hardware problem is generated.
    /// Once a problem is generated the message should be displayed on the screen.
    /// </summary>
    /// <param name="e">The event.</param>
    public void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.errorMessageHardwareProblem = this.GetErrorMessage(e);
        this.matchedSubstring = Regex.Match(this.errorMessageHardwareProblem, @"Closet (\d+)").Groups[0].Value;
    }
}