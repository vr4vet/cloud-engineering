// <copyright file="MenuScriptTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="MenuScript"/> class.
/// </summary>
public class MenuScriptTest
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
    private MenuScript ms;

    /// <summary>
    /// Sets up all the gameobjects and their components to correctly test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        this.menu = new GameObject("menu");
        this.ticket = new GameObject("ticket");
        this.skill = new GameObject("skill");
        this.control = new GameObject("control");
        this.close = new GameObject("close");
        this.screen = new GameObject("Computer");

        this.menuPage = this.menu.AddComponent<Canvas>();
        this.ticketPage = this.ticket.AddComponent<Canvas>();
        this.skillPage = this.skill.AddComponent<Canvas>();
        this.controlPage = this.control.AddComponent<Canvas>();
        this.closeTicketPage = this.close.AddComponent<Canvas>();

        this.menu.transform.SetParent(this.screen.transform);
        this.ticket.transform.SetParent(this.screen.transform);
        this.skill.transform.SetParent(this.screen.transform);
        this.control.transform.SetParent(this.screen.transform);
        this.close.transform.SetParent(this.screen.transform);

        this.cm = this.screen.AddComponent<ComputerManager>();
        this.cm.MenuPage = this.menuPage;
        this.cm.TicketPage = this.ticketPage;
        this.cm.SkillPage = this.skillPage;
        this.cm.ControlPage = this.controlPage;
        this.cm.CloseTicketPage = this.closeTicketPage;

        this.ms = this.menu.AddComponent<MenuScript>();
        this.ms.ComputerManager = this.cm;
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
        UnityEngine.Object.DestroyImmediate(this.closeTicketPage);
        UnityEngine.Object.DestroyImmediate(this.menu);
        UnityEngine.Object.DestroyImmediate(this.ticket);
        UnityEngine.Object.DestroyImmediate(this.skill);
        UnityEngine.Object.DestroyImmediate(this.screen);
        UnityEngine.Object.DestroyImmediate(this.close);
        UnityEngine.Object.DestroyImmediate(this.cm);
        UnityEngine.Object.DestroyImmediate(this.ms);
    }

    /// <summary>
    /// Tests if the setup is done correctly.
    /// </summary>
    [Test]
    public void SetupTest()
    {
        // Asserts that every canvas is created.
        Assert.AreEqual(true, this.menuPage.enabled);
        Assert.AreEqual(true, this.ticketPage.enabled);
        Assert.AreEqual(true, this.skillPage.enabled);
        Assert.AreEqual(true, this.controlPage.enabled);
        Assert.AreEqual(true, this.closeTicketPage.enabled);
    }

    /// <summary>
    /// Tests if the menu page is set active.
    /// </summary>
    [Test]
    public void SetMenuActiveTest()
    {
        // Asserts that every canvas is hidden except the menuPage.
        this.ms.SetMenuActive();
        Assert.AreEqual(true, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);
    }

    /// <summary>
    /// Tests if the ticket page is set active.
    /// </summary>
    [Test]
    public void SetTicketActive()
    {
        // Asserts that every canvas is hidden except the ticketPage.
        this.ms.SetTicketActive();
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);
    }

    /// <summary>
    /// Tests if the skill page is set active.
    /// </summary>
    [Test]
    public void SetSkillActiveTest()
    {
        // Asserts that every canvas is hidden except the skillPage.
        this.ms.SetSkillActive();
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);
    }

    /// <summary>
    /// Tests if the control page is set active.
    /// </summary>
    [Test]
    public void SetControlActiveTest()
    {
        // Asserts that every canvas is hidden except the skillPage.
        this.ms.SetControlActive();
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.closeTicketPage.isActiveAndEnabled);
    }

    /// <summary>
    /// Tests if the control page is set active.
    /// </summary>
    [Test]
    public void SetCloseTicketActiveTest()
    {
        // Asserts that every canvas is hidden except the skillPage.
        this.ms.SetCloseTicketActive();
        Assert.AreEqual(false, this.menuPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.ticketPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.skillPage.isActiveAndEnabled);
        Assert.AreEqual(false, this.controlPage.isActiveAndEnabled);
        Assert.AreEqual(true, this.closeTicketPage.isActiveAndEnabled);
    }
}