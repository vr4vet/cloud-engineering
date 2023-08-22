// <copyright file="CheckTicket.cs" company="VR4VET">
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
using DataCenter.Events;
using DataCenter.HardwareProblems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class containing the logic for the ticket checking.
/// </summary>
public class CheckTicket : MonoBehaviour
{
    [SerializeField]
    private Text onClickPopup;
    private GameObject errorTaskInputObject;
    private GameObject serverNumberInputObject;
    private GameObject rackNumberInputObject;
    private GameObject hardWareInputObject;

    private TMP_Dropdown errorTaskInput;
    private TMP_Dropdown serverNumberInput;
    private TMP_Dropdown rackNumberInput;
    private TMP_Dropdown hardWareInput;

    private string correctErrorSelected;
    private string correctServerSelected;
    private string correctRackSelected;
    private string correctHardwareSelected;

    [SerializeField]
    private ComputerManager computerManager;
    [SerializeField]
    private GameObject fileButton;

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckTicket"/> class.
    /// Constructor for the CheckTicket class.
    /// </summary>
    /// <param name="correctErrorSelected"> The correct choice for error number. </param>
    /// <param name="correctServerSelected"> The correct choice for server number.</param>
    /// <param name="correctRackSelected"> The correct choice for rack number.</param>
    /// <param name="correctHardwareSelected"> The correct choice for the hardware error.</param>
    public CheckTicket(string correctErrorSelected, string correctServerSelected, string correctRackSelected, string correctHardwareSelected)
    {
        this.correctErrorSelected = correctErrorSelected ?? throw new ArgumentNullException(nameof(correctErrorSelected));
        this.correctServerSelected = correctServerSelected ?? throw new ArgumentNullException(nameof(correctServerSelected));
        this.correctRackSelected = correctRackSelected ?? throw new ArgumentNullException(nameof(correctRackSelected));
        this.correctHardwareSelected = correctHardwareSelected ?? throw new ArgumentNullException(nameof(correctHardwareSelected));
    }

    /// <summary>
    /// Gets the selected text from a given dropdown and returns it in string format.
    /// </summary>
    /// <param name= "input"> The dropdown from which the selected text is retrieved. </param>
    /// <returns> A string containing the selected value. </returns>
    public string GetSelectedText(TMP_Dropdown input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        string output = input.options[input.value].text;
        return output;
    }

    /// <summary>
    /// A method which places the correct options among the ones able to be selected in the ticket.
    /// </summary>
    public void PlaceOptionsDropdowns()
    {
        // Retrieving and placing the options for the server number.
        this.serverNumberInput.ClearOptions();
        List<string> serverList = DataCenterScenario.Instance.GetComponentsInChildren<ServerContainer>().Select(s => s.name).ToList();
        this.serverNumberInput.AddOptions(serverList);

        // Retrieving and placing the options for the rack number.
        this.rackNumberInput.ClearOptions();
        List<string> rackList = DataCenterScenario.Instance.GetComponentsInChildren<Server>().Select(s => s.name).ToList();
        this.rackNumberInput.AddOptions(rackList);

        // Clearing the options for the hardware and task component.
        this.hardWareInput.ClearOptions();
        this.errorTaskInput.ClearOptions();

        // An enumerable of the instances of the abstract HardWareProblemType class.
        HardwareProblemGenerator gen = new HardwareProblemGenerator();
        IEnumerable<Type> hardwareTypes = gen.GetAllHardwareProblemTypes();

        // A list of this enumerable.
        List<Type> hList = hardwareTypes.ToList();

        // Accessing the public static fields which contains the name of the hardware type and the task to be done.
        var hardwareList = hList.Select(s => (string)s.GetProperty("HardwareTypeName")?.GetValue(null));
        var taskList = hList.Select(s => (string)s.GetProperty("HardwareTaskTypeName")?.GetValue(null));
        List<string> hStringList = hardwareList.ToList<string>();
        List<string> tStringList = taskList.ToList<string>();

        // Adding the lists to the dropdowns.
        this.hardWareInput.AddOptions(hStringList);
        this.errorTaskInput.AddOptions(tStringList);
    }

    /// <summary>
    /// Once the button in the scene is clicked, the selected values from the dropdowns are retrieved.
    /// These options are checked against the correct values.
    /// After which feedback message is shown, either accepting or rejecting the ticket.
    /// </summary>
    public void Click()
    {
        string errorSelected = this.GetSelectedText(this.errorTaskInput);
        string serverSelected = this.GetSelectedText(this.serverNumberInput);
        string rackSelected = this.GetSelectedText(this.rackNumberInput);
        string hardwareSelected = this.GetSelectedText(this.hardWareInput);

        // If the options selected are the same as correct options, the button should display the text "accepted"
        if (errorSelected.Equals(this.correctErrorSelected) && serverSelected.Equals(this.correctServerSelected) &&
            rackSelected.Equals(this.correctRackSelected) && hardwareSelected.Equals(this.correctHardwareSelected))
        {
            this.onClickPopup.text = "The ticket is accepted, you filled it in correctly. Take a look at the tablet on your left hip to see what you should do next.";
            DataCenterScenario.Instance.EventBus.TicketAccepted?.Invoke(new TicketAcceptedEvent());

            // When the ticket is accepted the buttons and dropdowns should no longer be interactable.
            this.fileButton.GetComponent<Button>().interactable = false;
            this.errorTaskInput.interactable = false;
            this.serverNumberInput.interactable = false;
            this.rackNumberInput.interactable = false;
            this.hardWareInput.interactable = false;
        }
        else
        { // Otherwise the ticket was not filled in correctly and the button should display "rejected, try again"
            this.onClickPopup.text = "The ticket is rejected, you did not fill it in correctly. Try again.";
        }
    }

    /// <summary>
    /// Method that gets the correct ticket options from the hardwareproblemgeneratedevent.
    /// </summary>
    /// <param name="e"> The generated hardware problem. </param>
    /// <returns> Returns a string with the correct options. </returns>
    public string[] GetCorrectTicketOptions(HardwareProblemGeneratedEvent e)
    {
        this.correctServerSelected = e.HardwareProblem.Location.ServerContainer.name;
        this.correctRackSelected = e.HardwareProblem.Location.Server.name;
        this.correctHardwareSelected = e.HardwareProblem.ProblemType.GetHardwareTypeName;
        this.correctErrorSelected = e.HardwareProblem.ProblemType.GetHardwareTaskTypeName;
        return new string[] { this.correctServerSelected, this.correctRackSelected, this.correctHardwareSelected, this.correctErrorSelected };
    }

    /// <summary>
    /// Sets the active canvas to the menu.
    /// </summary>
    public void SetMenuActive()
    {
        this.computerManager.ShowCanvas("menuPage");
    }

    /// <summary>
    /// This method gets the correct dropdowns from the scene through tags.
    /// It is called before the first frame update.
    /// </summary>
    public void InitializeDropdowns()
    {
        // The correct gameobject is found through a tag
        this.errorTaskInputObject = GameObject.FindWithTag("errorTask");

        // Then the dropdown component is found through getcomponent.
        // Since it is a textmeshpro dropdown, it needs to be TMP_Dropdown.
        this.errorTaskInput = this.errorTaskInputObject.GetComponent<TMP_Dropdown>();

        this.serverNumberInputObject = GameObject.FindGameObjectWithTag("serverNumber");
        this.serverNumberInput = this.serverNumberInputObject.GetComponent<TMP_Dropdown>();
        this.rackNumberInputObject = GameObject.FindGameObjectWithTag("rackNumber");
        this.rackNumberInput = this.rackNumberInputObject.GetComponent<TMP_Dropdown>();
        this.hardWareInputObject = GameObject.FindGameObjectWithTag("hardwareComponent");
        this.hardWareInput = this.hardWareInputObject.GetComponent<TMP_Dropdown>();
    }

    /// <summary>
    /// This method gets the correct dropdowns from the scene through tags.
    /// It also places the available options in the dropdowns.
    /// It is called before the first frame update.
    /// </summary>
    private void Start()
    {
        this.InitializeDropdowns();
        this.PlaceOptionsDropdowns();
    }

    // The three functions below add the listener for the event.

    /// <summary>
    /// This function is called when the task is created.
    /// </summary>
    private void OnEnable()
    {
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated += this.OnHardwareProblemGenerated;
    }

    /// <summary>
    /// This function is called when the task will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated -= this.OnHardwareProblemGenerated;
    }

    /// <summary>
    /// This method is called when a hardware problem is generated.
    /// It also calls GetCorrectTicketOptions to get the options a user should pick to fill in the ticket with.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.GetCorrectTicketOptions(e);
    }
}
