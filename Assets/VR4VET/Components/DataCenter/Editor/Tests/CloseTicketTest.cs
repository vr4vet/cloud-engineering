// <copyright file="CloseTicketTest.cs" company="VR4VET">
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
/// Playmode test suite for the <see cref="CloseTicket"/> class.
/// </summary>
public class CloseTicketTest
{
    /// <summary>
    /// Method that loads the DataCenter scene to do tests on it.
    /// </summary>
    [OneTimeSetUp]
    public void LoadScene()
    {
        EditorSceneManager.OpenScene("Assets/VR4VET/Scenes/DataCenter.unity");
    }

    /// <summary>
    /// A method to test if the clickClosemethod works.
    /// </summary>
    [Test]
    public void TestClickClose()
    {
        // Finding the feedback text in the scene.
        GameObject confirmationCloseTicket = GameObject.FindGameObjectWithTag("confirmationCloseTicketText");
        Assert.IsNotNull(confirmationCloseTicket);
        Text feedbackText = confirmationCloseTicket.GetComponent<Text>();

        // It should still be empty before the method is called.
        string noConfirmation = feedbackText.text;

        // Calling the method clickClose.
        CloseTicket closeTicket = GameObject.FindGameObjectWithTag("closeTicketCanvas").GetComponent<CloseTicket>();
        closeTicket.ClickClose();

        // The feedback text should no longer be empty.
        Assert.AreNotEqual(noConfirmation, feedbackText.text);
        Assert.AreEqual("Are you certain that you want to close this ticket? Only do so if you think the problem is solved.", feedbackText.text);
    }

    /// <summary>
    /// A method to test if the clickNomethod works.
    /// </summary>
    [Test]
    public void TestClickNo()
    {
        // Finding the feedback text in the scene.
        GameObject confirmationCloseTicket = GameObject.FindGameObjectWithTag("confirmationCloseTicketText");
        Assert.IsNotNull(confirmationCloseTicket);
        Text feedbackText = confirmationCloseTicket.GetComponent<Text>();

        // Calling the method clickClose.
        CloseTicket closeTicket = GameObject.FindGameObjectWithTag("closeTicketCanvas").GetComponent<CloseTicket>();
        closeTicket.ClickClose();

        // It should not be empty before the method is called. The only way you can click the No button is by clicking close first.
        string confirmationText = feedbackText.text;

        closeTicket.ClickNo();

        // The feedback text should now be empty.
        Assert.AreNotEqual(confirmationText, feedbackText.text);
    }
}
