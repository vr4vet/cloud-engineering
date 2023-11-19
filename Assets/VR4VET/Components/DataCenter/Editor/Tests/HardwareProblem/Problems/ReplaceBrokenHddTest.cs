// <copyright file="ReplaceBrokenHddTest.cs" company="VR4VET">
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
using System.Linq;
using DataCenter;
using DataCenter.Events;
using DataCenter.HardwareProblems;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="ReplaceBrokenHdd"/> class.
/// </summary>
public class ReplaceBrokenHddTest : MonoBehaviour
{
    private GameObject ramComponentPrefab;
    private GameObject ramComponentSlotPrefab;
    private GameObject hddComponentPrefab;
    private GameObject hddComponentSlotPrefab;

    /// <summary>
    /// Initializes the test suite.
    /// </summary>
    [SetUp]
    public void Init()
    {
        //this.ramComponentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/RAM.prefab");
        //this.ramComponentSlotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/RAM socket.prefab");
        //this.hddComponentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/HDD.prefab");
        //this.hddComponentSlotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/HDD Socket.prefab");

        //Assert.NotNull(this.ramComponentPrefab);
        //Assert.NotNull(this.ramComponentSlotPrefab);
        //Assert.NotNull(this.hddComponentPrefab);
        //Assert.NotNull(this.hddComponentSlotPrefab);
    }

    /// <summary>
    /// Verify all slots are populated with HDDs, and the broken ones have IsBroken set to true.
    /// </summary>
    [Test]
    public void PopulateServer()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 6; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;

        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();

        //var problem = new ReplaceBrokenHdd(location, new() { allSlots[3], allSlots[4] });

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.HddComponentPrefab = this.hddComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //foreach (var slot in allSlots)
        //{
        //    Assert.NotNull(slot.Component);
        //}

        //CollectionAssert.AreEqual(new[] { false, false, false, true, true, false }, allSlots.Select(slot => slot.Component.IsBroken));
    }

    /// <summary>
    /// Verify the problem is correctly randomized.
    /// </summary>
    [Test]
    public void GenerateRandom()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 8; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //ReplaceBrokenHdd problem = ReplaceBrokenHdd.GenerateRandom(location, new System.Random(0));
        //CollectionAssert.AreEqual(new[] { "Slot2", "Slot7" }, problem.Slots.Select(slot => slot.name).ToArray());
    }

    /// <summary>
    /// Verify an exception is thrown when the component of the event is null.
    /// </summary>
    [Test]
    public void OnHddComponentInstalled_ComponentNull()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(null, emptySlot);
        //Assert.Throws<ArgumentException>(() => problem.OnHddComponentInstalled(eventArgs));
    }

    /// <summary>
    /// Verify the activity is completed when the component is installed.
    /// </summary>
    [Test]
    public void OnHddComponentInstalled_ActivityCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentInstalled(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, true }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the activity is not completed when a component is installed in a wrong slot.
    /// </summary>
    [Test]
    public void OnHddComponentInstalled_WrongSlotActivityNotCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];
        //var wrongSlot = allSlots[1];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, wrongSlot);
        //problem.OnHddComponentInstalled(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the activity is completed when a HddComponentInstalled event is raised.
    /// </summary>
    [Test]
    public void OnHddComponentInstalled_HddComponentInstalled()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, emptySlot);

        //var eventBus = new EventBus();
        //problem.AddEventListeners(eventBus);
        //eventBus.HddComponentInstalled.Invoke(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, true }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the remove activity is uncompleted when a broken HDD is installed.
    /// </summary>
    [Test]
    public void OnHddComponentInstalled_BrokenHddUncompletesRemoveActivity()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);
        //hddComponent.IsBroken = true;

        //problem.Activities[0].AktivitetIsCompeleted = true;

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentInstalled(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify an exception is thrown when the component of the event is null.
    /// </summary>
    [Test]
    public void OnHddComponentRemoved_ComponentNull()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var eventArgs = new HardwareComponentRemovedEvent<HddComponent>(null, emptySlot);
        //Assert.Throws<ArgumentException>(() => problem.OnHddComponentRemoved(eventArgs));
    }

    /// <summary>
    /// Verify the remove activity is completed when the component is installed.
    /// </summary>
    [Test]
    public void OnHddComponentRemoved_ActivityCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);
        //hddComponent.IsBroken = true;

        //var eventArgs = new HardwareComponentRemovedEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentRemoved(eventArgs);

        //CollectionAssert.AreEqual(new[] { true, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the activity is not completed when a component is installed in a wrong slot.
    /// </summary>
    [Test]
    public void OnHddComponentRemoved_WrongSlotActivityNotCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];
        //var wrongSlot = allSlots[1];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentRemovedEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentRemoved(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the remove activity is completed when a HddComponentRemoved event is raised.
    /// </summary>
    [Test]
    public void OnHddComponentRemoved_HddComponentRemoved()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);
        //hddComponent.IsBroken = true;

        //var eventArgs = new HardwareComponentRemovedEvent<HddComponent>(hddComponent, emptySlot);

        //var eventBus = new EventBus();
        //problem.AddEventListeners(eventBus);
        //eventBus.HddComponentRemoved.Invoke(eventArgs);

        //CollectionAssert.AreEqual(new[] { true, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }

    /// <summary>
    /// Verify the install activity is uncompleted when a working HDD is removed.
    /// </summary>
    [Test]
    public void OnHddComponentRemoved_WorkingHddUncompletesInstallActivity()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject hddComponentSlotGameObject = Instantiate(this.hddComponentSlotPrefab);
        //    hddComponentSlotGameObject.name = $"Slot{i}";
        //    hddComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<HddComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new ReplaceBrokenHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);
        //hddComponent.IsBroken = false;

        //problem.Activities[1].AktivitetIsCompeleted = true;

        //var eventArgs = new HardwareComponentRemovedEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentRemoved(eventArgs);

        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(activity => activity.AktivitetIsCompeleted));
    }
}
