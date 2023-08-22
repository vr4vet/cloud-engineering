// <copyright file="MenuScript.cs" company="VR4VET">
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
using UnityEngine;

/// <summary>
/// Class containing the backend for the functionality on the Menu page.
/// </summary>
public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private ComputerManager computerManager;

    /// <summary>
    /// Gets or Sets menuPage.
    /// </summary>
    public ComputerManager ComputerManager { get => this.computerManager; set => this.computerManager = value; }

    /// <summary>
    /// Sets the active page to the menu page.
    /// </summary>
    public void SetMenuActive()
    {
        this.computerManager.ShowCanvas("menuPage");
    }

    /// <summary>
    /// Sets the active page to the skill page.
    /// </summary>
    public void SetSkillActive()
    {
        this.computerManager.ShowCanvas("skillPage");
    }

    /// <summary>
    /// Sets the active page to the ticket page.
    /// </summary>
    public void SetTicketActive()
    {
        this.computerManager.ShowCanvas("ticketPage");
    }

    /// <summary>
    /// Sets the active page to the control page.
    /// </summary>
    public void SetControlActive()
    {
        this.computerManager.ShowCanvas("controlPage");
    }

    /// <summary>
    /// Sets the active page to the close ticket page.
    /// </summary>
    public void SetCloseTicketActive()
    {
        this.computerManager.ShowCanvas("closeTicketPage");
    }
}
