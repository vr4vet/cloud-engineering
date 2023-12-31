// <copyright file="CloseTicket.cs" company="VR4VET">
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataCenter;
using UnityEngine;
using UnityEngine.UI;
using Task;

/// <summary>
/// Class containing the logic for closing the ticket.
/// </summary>
public class CloseTicket : MonoBehaviour
{
    [SerializeField]
    private Text feedbackText;
    [SerializeField]
    private GameObject yesButton;
    [SerializeField]
    private GameObject noButton;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject updateButton;
    [SerializeField]
    private GameObject listSubtaskPrefab;
    [SerializeField]
    private Transform listSubtaskHolder;
    [SerializeField]
    private ComputerManager computerManager;
    public Task.TaskHolder taskHolder;

    /// <summary>
    /// Method that is called when the close button is clicked. A confirmation pop-up shows up on screen when this method is called.
    /// </summary>
    public void ClickClose()
    {
        this.feedbackText.text = "Are you certain that you want to close this ticket? Only do so if you think the problem is solved.";
        this.yesButton.SetActive(true);
        this.noButton.SetActive(true);
    }

    /// <summary>
    /// Method that is called when the no button is clicked. The confirmation pop-up is removed from the screen when this method is called.
    /// </summary>
    public void ClickNo()
    {
        this.feedbackText.text = string.Empty;
        this.yesButton.SetActive(false);
        this.noButton.SetActive(false);
    }

    /// <summary>
    /// Method that is called when the yes button is clicked. Checks to see if all activities are completed.
    /// If all activities are completed the player is congratulated.
    /// If they are not, they are told to redo those.
    /// </summary>
    public void ClickYes()
    {
        this.yesButton.SetActive(false);
        this.noButton.SetActive(false);
        bool completed = taskHolder.GetTask("Perform Maintenance").Compleated() && taskHolder.GetTask("Create a Ticket").Compleated();

        // If completed is true, show a message that the experience is done.
        if (completed)
        {
            this.feedbackText.text = "Good job! You have completed your tasks and fixed the problem. Please bring back the key to end the experience.";
            taskHolder.GetTask("Close Ticket").GetSubtask("Close Ticket on Computer").SetCompleated(true);
            DataCenterScenario.Instance.EventBus.TicketFinished?.Invoke(new TicketFinishedEvent());
            this.yesButton.GetComponent<Button>().interactable = false;
            this.noButton.GetComponent<Button>().interactable = false;
            this.closeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            if (taskHolder.GetTask("Create Ticket"))
            {
                this.feedbackText.text = "It looks like you have not completed the maintenance yet... Look at the tablet to see what still needs to be done";
            } else
            {
                this.feedbackText.text = "You can't close a ticket if you have not created one yet... Go back to the main menu to create a ticket.";
            }
        }
    }

    /// <summary>
    /// Method that removes all the activities from the scrollview and retrieves the updated ones.
    /// </summary>
    public void UpdateClick()
    {
        // Removing all the objects currently in the scrollview.
        var children = this.listSubtaskHolder.Cast<Transform>().ToArray();
        foreach (var child in children)
        {
            UnityEngine.Object.DestroyImmediate(child.gameObject);
        }

        this.DisplaySubtasks();
        this.feedbackText.text = "The activities completion values have been updated";

    }

    /// <summary>
    /// Sets the active page to the menu page.
    /// </summary>
    public void SetMenuActive()
    {
        this.computerManager.ShowCanvas("menuPage");
    }

    /// <summary>
    /// This method fills in the data from the activities into the scrolling view.
    /// </summary>
    public void DisplaySubtasks()
    {
        float yPosAdded = 0;
        foreach (Task.Task a in taskHolder.taskList)
        {
            GameObject activityObject = Instantiate(this.listSubtaskPrefab, this.listSubtaskHolder);
            RectTransform uiRectTransform = activityObject.GetComponent<RectTransform>();
            uiRectTransform.anchoredPosition = new Vector2(160.0f, 410.0f - yPosAdded);
            activityObject.transform.Find("ActivityName").GetComponentInChildren<Text>().text = a.TaskName;
            activityObject.transform.Find("ActivityCompleted").GetComponentInChildren<Text>().text = a.Compleated().ToString();
            yPosAdded += 100;
        }

        //TODO: Find out where this is on the screen
        Debug.Log("Are the tasks on the screen?");
    }

    /// <summary>
    /// Called at the start. Gets a list of all the activities and displays them.
    /// </summary>
    private void Start()
    {
        this.DisplaySubtasks();
        this.yesButton.SetActive(false);
        this.noButton.SetActive(false);
    }
}
