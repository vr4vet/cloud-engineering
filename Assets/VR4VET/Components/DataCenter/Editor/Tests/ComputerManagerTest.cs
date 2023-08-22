// <copyright file="ComputerManagerTest.cs" company="VR4VET">
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
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Test suite for the <see cref="ComputerManager"/> class.
/// </summary>
public class ComputerManagerTest
{
    private GameObject menu;
    private GameObject ticket;
    private GameObject skill;
    private GameObject control;
    private GameObject close;
    private Canvas menuPage;
    private Canvas ticketPage;
    private Canvas skillPage;
    private Canvas controlPage;
    private Canvas closeTicketPage;
    private GameObject screen;
    private ComputerManager cm;

    /// <summary>
    /// Sets up all the gameobjects and their components to correctly test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Create all the gameobjects.
        this.menu = new GameObject("menu");
        this.ticket = new GameObject("ticket");
        this.skill = new GameObject("skill");
        this.control = new GameObject("control");
        this.close = new GameObject("close");
        this.screen = new GameObject("Computer");

        // Add the canvas component to the right objects.
        this.menuPage = this.menu.AddComponent<Canvas>();
        this.ticketPage = this.ticket.AddComponent<Canvas>();
        this.skillPage = this.skill.AddComponent<Canvas>();
        this.controlPage = this.control.AddComponent<Canvas>();
        this.closeTicketPage = this.close.AddComponent<Canvas>();

        // Set the parent of the objects to the screen.
        this.menu.transform.SetParent(this.screen.transform);
        this.ticket.transform.SetParent(this.screen.transform);
        this.skill.transform.SetParent(this.screen.transform);
        this.control.transform.SetParent(this.screen.transform);
        this.close.transform.SetParent(this.screen.transform);

        // Add the computer manager to the screen object.
        this.cm = this.screen.AddComponent<ComputerManager>();

        // Set the serialized fields.
        this.cm.MenuPage = this.menuPage;
        this.cm.TicketPage = this.ticketPage;
        this.cm.SkillPage = this.skillPage;
        this.cm.ControlPage = this.controlPage;
        this.cm.CloseTicketPage = this.closeTicketPage;
    }

    /// <summary>
    /// This method is called after each test and destroys the game objects and components created for testing.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // Destroy the game objects and components created for testing
        UnityEngine.Object.DestroyImmediate(this.menuPage);
        UnityEngine.Object.DestroyImmediate(this.skillPage);
        UnityEngine.Object.DestroyImmediate(this.ticketPage);
        UnityEngine.Object.DestroyImmediate(this.controlPage);
        UnityEngine.Object.DestroyImmediate(this.closeTicketPage);
        UnityEngine.Object.DestroyImmediate(this.menu);
        UnityEngine.Object.DestroyImmediate(this.ticket);
        UnityEngine.Object.DestroyImmediate(this.skill);
        UnityEngine.Object.DestroyImmediate(this.control);
        UnityEngine.Object.DestroyImmediate(this.close);
        UnityEngine.Object.DestroyImmediate(this.screen);
        UnityEngine.Object.DestroyImmediate(this.cm);
    }

    /// <summary>
    /// This tests if the test setup is done correctly.
    /// </summary>
    [Test]
    public void TestSetup()
    {
        // Asserts all pages are created.
        Assert.AreEqual(true, this.menuPage.enabled);
        Assert.AreEqual(true, this.ticketPage.enabled);
        Assert.AreEqual(true, this.skillPage.enabled);
        Assert.AreEqual(true, this.controlPage.enabled);
        Assert.AreEqual(true, this.closeTicketPage.enabled);
    }

    /// <summary>
    /// Tests if all canvases are set to inactive upon function call.
    /// </summary>
    [Test]
    public void HideAllCanvasesTest()
    {
        // Function to test
        this.cm.HideAll();

        // Gain all the canvases.
        Canvas[] canvases = this.cm.GetComponentsInChildren<Canvas>();

        // Assures that every canvas is disable (hidden).
        foreach (Canvas c in canvases)
        {
            Assert.AreEqual(false, c.isActiveAndEnabled);
        }
    }

    /// <summary>
    /// Tests if canvases are set to active correctly.
    /// </summary>
    [Test]
    public void ShowCanvasByTypeTest()
    {
        // Asserts that every canvas is hidden except the menuPage
        this.cm.ShowCanvas(this.menuPage);
        Assert.AreEqual(true, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the ticketPage
        this.cm.ShowCanvas(this.ticketPage);
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the skillPage.
        this.cm.ShowCanvas(this.skillPage);
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the serverPage.
        this.cm.ShowCanvas(this.controlPage);
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the closeTicketPage.
        this.cm.ShowCanvas(this.closeTicketPage);
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.closeTicketPage.isActiveAndEnabled);
    }

    /// <summary>
    /// Tests if canvases are set to active correctly.
    /// </summary>
    [Test]
    public void ShowCanvasByNameTest()
    {
        // Asserts that every canvas is hidden except the menuPage
        this.cm.ShowCanvas("mainPage");
        Assert.AreEqual(true, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the ticketPage
        this.cm.ShowCanvas("ticketPage");
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the skillPage.
        this.cm.ShowCanvas("skillPage");
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the skillPage.
        this.cm.ShowCanvas("controlPage");
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the skillPage.
        this.cm.ShowCanvas("closeTicketPage");
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.closeTicketPage.isActiveAndEnabled);

        // Asserts that every canvas is hidden except the menuPage since it is the default.
        this.cm.ShowCanvas(string.Empty);
        Assert.AreEqual(true, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);
    }
}
