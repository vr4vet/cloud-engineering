// <copyright file="ComputerManager.cs" company="VR4VET">
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
using DataCenter;
using DataCenter.Events;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enum for the different modes of the computer.
/// </summary>
public enum Mode
{
    /// <summary>
    /// The computermanager is used for a computer in the control room.
    /// </summary>
    Computer,

    /// <summary>
    /// The computer manager is used for a control screen in the server room.
    /// </summary>
    Server,
}

/// <summary>
/// Manager class controlling which canvas gets shown on the computer screen.
/// </summary>
public class ComputerManager : MonoBehaviour
{
    [SerializeField]
    private Canvas menuPage;
    [SerializeField]
    private Canvas skillPage;
    [SerializeField]
    private Canvas ticketPage;
    [SerializeField]
    private Canvas controlPage;
    [SerializeField]
    private Mode mode;
    private HardwareProblem hardwareProblem;
    [SerializeField]
    private Canvas closeTicketPage;

    /// <summary>
    /// Gets or Sets mode.
    /// </summary>
    public Mode Mode { get => this.mode; set => this.mode = value; }

    /// <summary>
    /// Gets or Sets menuPage.
    /// </summary>
    public Canvas MenuPage { get => this.menuPage; set => this.menuPage = value; }

    /// <summary>
    /// Gets or Sets ticketPage.
    /// </summary>
    public Canvas TicketPage { get => this.ticketPage; set => this.ticketPage = value; }

    /// <summary>
    /// Gets or Sets skillPage.
    /// </summary>
    public Canvas SkillPage { get => this.skillPage; set => this.skillPage = value; }

    /// <summary>
    /// Gets or Sets controlPage.
    /// </summary>
    public Canvas ControlPage { get => this.controlPage; set => this.controlPage = value; }

    /// <summary>
    /// Gets or sets the hardware problem.
    /// </summary>
    public HardwareProblem HardwareProblem { get => this.hardwareProblem; set => this.hardwareProblem = value; }

    /// <summary>
    /// Gets or Sets closeTicketPage.
    /// </summary>
    public Canvas CloseTicketPage { get => this.closeTicketPage; set => this.closeTicketPage = value; }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        if (this.mode.Equals(Mode.Computer))
        {
            this.ShowCanvas("menuPage");
        }
        else if (this.mode.Equals(Mode.Server))
        {
            this.ShowCanvas("controlPage");
            Destroy(this.controlPage.transform.GetChild(0).GetChild(2).gameObject);
        }
    }

    /// <summary>
    /// This function disables all canvases except the desired one.
    /// </summary>
    /// <param name="canvas"> The canvas you want to be active. </param>.
    public void ShowCanvas(Canvas canvas)
    {
        this.HideAll();
        canvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// This function sets all canvases to inactive.
    /// </summary>
    public void HideAll()
    {
        if (this.mode.Equals(Mode.Computer))
        {
            this.HideAllComputer();
        }
        else if (this.mode.Equals(Mode.Server))
        {
            this.HideAllServer();
        }
    }

    /// <summary>
    /// This function creates an endpoint for the other classes so they can set correct canvas.
    /// </summary>
    /// <param name="name">Name of the desired canvas. </param>
    public void ShowCanvas(string name)
    {
        switch (name)
        {
            case "mainPage":
                this.ShowCanvas(this.menuPage);
                break;

            case "skillPage":
                this.ShowCanvas(this.skillPage);
                break;

            case "ticketPage":
                this.ShowCanvas(this.ticketPage);
                break;

            case "controlPage":
                this.ShowCanvas(this.controlPage);
                break;

            case "closeTicketPage":
                this.ShowCanvas(this.closeTicketPage);
                break;

            default:
                if (this.mode.Equals(Mode.Computer))
                {
                    this.ShowCanvas(this.menuPage);
                }
                else if (this.mode.Equals(Mode.Server))
                {
                    this.ShowCanvas(this.controlPage);
                }

                break;
        }
    }

    /// <summary>
    /// Hides all computer canvases.
    /// </summary>
    private void HideAllComputer()
    {
        this.menuPage.gameObject.SetActive(false);
        this.skillPage.gameObject.SetActive(false);
        this.ticketPage.gameObject.SetActive(false);
        this.controlPage.gameObject.SetActive(false);
        this.closeTicketPage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Hides all server canvases.
    /// </summary>
    private void HideAllServer()
    {
        this.controlPage.gameObject.SetActive(false);
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
    /// It also calls GetHardwareProblemLocation to retrieve the server location of the problem.
    /// </summary>
    /// <param name="e"> The event.</param>
    private void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.GetHardwareProblemLocation(e);
    }

    /// <summary>
    /// Sets the server location of the hardware problem.
    /// </summary>
    /// <param name="e"> Event containing the problem. </param>
    private void GetHardwareProblemLocation(HardwareProblemGeneratedEvent e)
    {
        this.HardwareProblem = e.HardwareProblem;
    }
}
