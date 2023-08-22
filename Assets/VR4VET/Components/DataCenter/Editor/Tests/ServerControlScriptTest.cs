// <copyright file="ServerControlScriptTest.cs" company="VR4VET">
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
using NUnit.Framework;
using Palmmedia.ReportGenerator.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Tests the server control script.
/// </summary>
public class ServerControlScriptTest
{
    private GameObject serverControl;
    private ServerControlScript serverControlScript;
    private GameObject menu;
    private GameObject ticket;
    private GameObject skill;
    private GameObject close;
    private Canvas menuPage;
    private Canvas ticketPage;
    private Canvas skillPage;
    private Canvas serverPage;
    private Canvas closeTicketPage;
    private GameObject screen;
    private ComputerManager cm;
    private HardwareProblemGenerator hpg;
    private GameObject closet1;
    private GameObject cabinetPage;
    private HardwareProblemGenerator hardwareProblemGenerator;
    private HardwareProblem hardwareProblem;
    private GameObject server1;

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
        this.screen = new GameObject("ComputerScreenTicket");
        this.close = new GameObject("close");

        // Get the a server control canvas and its server control script.
        this.serverControl = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("UI/ServerControlCanvas"), this.screen.transform);
        this.serverControlScript = this.serverControl.GetComponent<ServerControlScript>();

        // Add the canvas component to the right objects.
        this.menuPage = this.menu.AddComponent<Canvas>();
        this.ticketPage = this.ticket.AddComponent<Canvas>();
        this.skillPage = this.skill.AddComponent<Canvas>();
        this.serverPage = this.serverControl.GetComponent<Canvas>();
        this.closeTicketPage = this.close.AddComponent<Canvas>();

        // Set the parent of the objects to the screen.
        this.menu.transform.SetParent(this.screen.transform);
        this.ticket.transform.SetParent(this.screen.transform);
        this.skill.transform.SetParent(this.screen.transform);
        this.close.transform.SetParent(this.screen.transform);
        this.serverControl.transform.SetParent(this.screen.transform);

        // Add the computer manager to the screen object.
        this.cm = this.screen.AddComponent<ComputerManager>();

        // Set the serialized fields.
        this.cm.MenuPage = this.menuPage;
        this.cm.TicketPage = this.ticketPage;
        this.cm.SkillPage = this.skillPage;
        this.cm.ControlPage = this.serverPage;
        this.cm.Mode = Mode.Computer;
        this.cm.CloseTicketPage = this.closeTicketPage;

        // Hardwareproblemgenerator and a server cabinet.
        this.hpg = new GameObject("HPG").AddComponent<HardwareProblemGenerator>();
        this.closet1 = new GameObject("Closet 1");
        this.closet1.transform.SetParent(this.hpg.transform);
        this.closet1.AddComponent<ServerContainer>();
        this.server1 = new GameObject("Server 1");
        this.server1.AddComponent<Server>();
        this.server1.transform.SetParent(this.closet1.transform);
        GameObject ramslot1 = new GameObject("RAM Slot 1");
        ramslot1.transform.SetParent(this.server1.transform);
        ramslot1.AddComponent<RamComponentSlot>();
        GameObject ramslot2 = new GameObject("RAM Slot 2");
        ramslot2.transform.SetParent(this.server1.transform);
        ramslot2.AddComponent<RamComponentSlot>();
        Assert.NotNull(this.hpg.GetAllHardwareProblemTypes());
        this.hardwareProblem = this.hpg.GenerateProblem(new System.Random(), this.hpg.GetAllHardwareProblemTypes());

        // Set prefabs and transform.
        this.serverControlScript.ContainerButtonPrefab = (GameObject)Resources.Load("UI/ServerControlButton");
        this.serverControlScript.ListTransform = this.serverControl.transform.GetChild(0).GetChild(1);
        this.serverControlScript.CabinetPagePrefab = (GameObject)Resources.Load("UI/ServerCabinetCanvas");
        this.serverControlScript.ScreenTransform = this.screen.transform;
        this.serverControlScript.ServerCabinets = new ServerContainer[] { this.closet1.GetComponent<ServerContainer>() };
        this.serverControlScript.ComputerManager = this.cm;
        this.serverControlScript.HardwareProblem = this.hardwareProblem;
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
        UnityEngine.Object.DestroyImmediate(this.serverControl);
        UnityEngine.Object.DestroyImmediate(this.serverPage);
        UnityEngine.Object.DestroyImmediate(this.menu);
        UnityEngine.Object.DestroyImmediate(this.ticket);
        UnityEngine.Object.DestroyImmediate(this.skill);
        UnityEngine.Object.DestroyImmediate(this.close);
        UnityEngine.Object.DestroyImmediate(this.screen);
        UnityEngine.Object.DestroyImmediate(this.cm);
        UnityEngine.Object.DestroyImmediate(this.hpg);
        UnityEngine.Object.DestroyImmediate(this.closet1);
    }

    /// <summary>
    /// Tests the ActivateCabinetButton functionality from the server control script.
    /// </summary>
    [Test]
    public void ActivateServerCabinetButtonTest()
    {
        this.serverControlScript.CreateButton(this.closet1.GetComponent<ServerContainer>(), 0);
        this.serverControl.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.Invoke();
        this.serverControlScript.ActivateServerCabinetButton(0);

        this.cabinetPage = this.serverControlScript.ActiveCabinetPage;
        Assert.AreEqual(true, this.cabinetPage.activeSelf);
        Assert.AreEqual(false, this.serverControl.activeSelf);
        Assert.AreEqual("Closet 1 Status", this.cabinetPage.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text);
    }

    /// <summary>
    /// Tests the CreateButton functionality from the server control script.
    /// </summary>
    [Test]
    public void CreateButtonTest()
    {
        this.serverControlScript.CreateButton(this.closet1.GetComponent<ServerContainer>(), 0);

        GameObject button = this.serverControl.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;

        Assert.AreNotEqual(null, button);
        Assert.AreEqual("ServerControlButton(Clone)", button.name);
        Assert.AreEqual(true, button.TryGetComponent<Button>(out Button buttonComponent));
        Assert.AreEqual(true, button.transform.GetChild(1).TryGetComponent<Text>(out Text txt));
        Assert.AreEqual("Closet 1", txt.text);
    }
}
