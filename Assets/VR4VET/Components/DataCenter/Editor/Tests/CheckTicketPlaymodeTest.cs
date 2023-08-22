// <copyright file="CheckTicketPlaymodeTest.cs" company="VR4VET">
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

using System.Collections;
using System.Collections.Generic;
using DataCenter;
using DataCenter.Events;
using NUnit.Framework;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

/// <summary>
/// Playmode test suite for the <see cref="CheckTicket"/> class.
/// </summary>
public class CheckTicketPlaymodeTest
{
    private HardwareProblemGeneratedEvent hardwareEvent;

    /// <summary>
    /// Method that loads the DataCenter scene to do tests on it.
    /// </summary>
    [OneTimeSetUp]
    public void LoadScene()
    {
        EditorSceneManager.OpenScene("Assets/VR4VET/Scenes/DataCenter.unity");
    }

    /// <summary>
    /// A method to test if the clickmethod works.
    /// </summary>
    [Test]
    public void TestClick()
    {
        // Finding the feedback text in the scene.
        GameObject feedbackTicket = GameObject.FindGameObjectWithTag("feedbackTicketText");
        Assert.IsNotNull(feedbackTicket);
        Text feedbackText = feedbackTicket.GetComponent<Text>();

        string noFeedback = feedbackText.text;

        // Calling the method click.
        CheckTicket checkTicket = GameObject.FindGameObjectWithTag("ticketScreenCanvas").GetComponent<CheckTicket>();
        checkTicket.InitializeDropdowns();
        checkTicket.PlaceOptionsDropdowns();

        checkTicket.Click();

        // The feedback text should no longer be empty.
        Assert.AreNotEqual(noFeedback, feedbackText.text);
    }

    /// <summary>
    /// Test to see if the dropdowns get populated.
    /// </summary>
    [Test]
    public void TestDropdownsPopulated()
    {
        // Finding the dropdowns of the computer ticket.
        TMP_Dropdown errorTaskInput = GameObject.FindWithTag("errorTask").GetComponent<TMP_Dropdown>();
        TMP_Dropdown serverNumberInput = GameObject.FindGameObjectWithTag("serverNumber").GetComponent<TMP_Dropdown>();
        TMP_Dropdown rackNumberInput = GameObject.FindGameObjectWithTag("rackNumber").GetComponent<TMP_Dropdown>();
        TMP_Dropdown hardWareInput = GameObject.FindGameObjectWithTag("hardwareComponent").GetComponent<TMP_Dropdown>();

        // Calling the method click.
        CheckTicket checkTicket = GameObject.FindGameObjectWithTag("ticketScreenCanvas").GetComponent<CheckTicket>();
        checkTicket.InitializeDropdowns();
        checkTicket.PlaceOptionsDropdowns();

        Assert.IsNotNull(errorTaskInput.options);
        Assert.IsNotNull(serverNumberInput.options);
        Assert.IsNotNull(rackNumberInput.options);
        Assert.IsNotNull(hardWareInput.options);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    /// <summary>
    /// test example. Makes sure at least a frame is played in the scene.
    /// </summary>
    /// <returns> return something. </returns>
    [UnityTest]
    public IEnumerator CheckTicketPlaymodeTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    /// <summary>
    /// A test to see if the correct ticket options are found.
    /// </summary>
    [Test]
    public void TestGetCorrectTicketOptions()
    {
        CheckTicket checkTicket = GameObject.FindGameObjectWithTag("ticketScreenCanvas").GetComponent<CheckTicket>();

        HardwareProblemGenerator generator = GameObject.FindObjectOfType<HardwareProblemGenerator>();
        HardwareProblem problem = generator.GenerateProblem(new System.Random(0), generator.GetAllHardwareProblemTypes());
        HardwareProblemGeneratedEvent hardwareEvent = new(problem);

        // The array contains the correct options for the ticket from the event.
        string[] options = checkTicket.GetCorrectTicketOptions(hardwareEvent);

        Assert.IsNotNull(options);

        string testCorrectServerSelected = hardwareEvent.HardwareProblem.Location.ServerContainer.name;
        string testCorrectRackSelected = hardwareEvent.HardwareProblem.Location.Server.name;
        string testCorrectHardwareSelected = hardwareEvent.HardwareProblem.ProblemType.GetHardwareTypeName;
        string testCorrectErrorSelected = hardwareEvent.HardwareProblem.ProblemType.GetHardwareTaskTypeName;

        Assert.AreEqual(options.GetValue(0), testCorrectServerSelected);
        Assert.AreEqual(options.GetValue(1), testCorrectRackSelected);
        Assert.AreEqual(options.GetValue(2), testCorrectHardwareSelected);
        Assert.AreEqual(options.GetValue(3), testCorrectErrorSelected);
    }
}
