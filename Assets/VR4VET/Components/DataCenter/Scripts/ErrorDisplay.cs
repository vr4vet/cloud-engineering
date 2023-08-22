// <copyright file="ErrorDisplay.cs" company="VR4VET">
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
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for displaying a message on a screen.
/// </summary>
public class ErrorDisplay : MonoBehaviour
{
    [SerializeField]
    private Text errorMessage;
    private string errorMessageHardwareProblem;

    /// <summary>
    /// In this method the text on an object is changed to the error message.
    /// </summary>
    /// <param name="errorBox"> The Textbox in the scene for which the text should change. </param>
    /// <param name="error"> The error message to be displayed. </param>
    public void ChangeText(Text errorBox, string error)
    {
        errorBox.text = error;
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
    /// Once a problem is generated the message should be displayed on the screen.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.errorMessageHardwareProblem = this.GetErrorMessage(e);
        this.ChangeText(this.errorMessage, this.errorMessageHardwareProblem);
    }
}
