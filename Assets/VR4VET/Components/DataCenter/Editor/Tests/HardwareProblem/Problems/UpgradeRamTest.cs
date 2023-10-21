// <copyright file="UpgradeRamTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="UpgradeRamTest"/> class.
/// </summary>
public class UpgradeRamTest : MonoBehaviour
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
    /// Verify an exception is thrown when the component of the event is null.
    /// </summary>
    [Test]
    public void OnRamComponentInstalled_ComponentNull()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { emptySlot }, 64);

        //var eventArgs = new HardwareComponentInstalledEvent<RamComponent>(null, emptySlot);
        //Assert.Throws<ArgumentException>(() => problem.OnRamComponentInstalled(eventArgs));
    }

    /// <summary>
    /// Verify the install activity is completed when the component is installed.
    /// </summary>
    [Test]
    public void OnRamComponentInstalled_ActivityCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { emptySlot }, 64);

        //var ramComponentGameObject = Instantiate(this.ramComponentPrefab);
        //var ramComponent = ramComponentGameObject.GetComponent<RamComponent>();
        //Assert.NotNull(ramComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<RamComponent>(ramComponent, emptySlot);
        //problem.OnRamComponentInstalled(eventArgs);

        //Assert.IsTrue(problem.Activities[1].AktivitetIsCompeleted);
    }

    /// <summary>
    /// Verify the install activity is not completed when a component is installed in a wrong slot.
    /// </summary>
    [Test]
    public void OnRamComponentInstalled_WrongSlotActivityNotCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var emptySlot = allSlots[0];
        //var wrongSlot = allSlots[1];

        //var problem = new UpgradeRam(location, new() { emptySlot }, 64);

        //var ramComponentGameObject = Instantiate(this.ramComponentPrefab);
        //var ramComponent = ramComponentGameObject.GetComponent<RamComponent>();
        //Assert.NotNull(ramComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<RamComponent>(ramComponent, wrongSlot);
        //problem.OnRamComponentInstalled(eventArgs);

        //Assert.IsFalse(problem.Activities[0].AktivitetIsCompeleted);
    }

    /// <summary>
    /// Verify the install activity is completed when a RamComponentInstalled event is raised.
    /// </summary>
    [Test]
    public void OnRamComponentInstalled_RamComponentInstalled()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var emptySlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { emptySlot }, 64);

        //var ramComponentGameObject = Instantiate(this.ramComponentPrefab);
        //var ramComponent = ramComponentGameObject.GetComponent<RamComponent>();
        //Assert.NotNull(ramComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<RamComponent>(ramComponent, emptySlot);

        //var eventBus = new EventBus();
        //problem.AddEventListeners(eventBus);
        //eventBus.RamComponentInstalled.Invoke(eventArgs);

        //Assert.IsTrue(problem.Activities[1].AktivitetIsCompeleted);
    }

    /// <summary>
    /// Verify an exception is thrown when the component of the event is null.
    /// </summary>
    [Test]
    public void OnRamComponentRemoved_ComponentNull()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var targetSlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { targetSlot }, 64);

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //var eventArgs = new HardwareComponentRemovedEvent<RamComponent>(null, targetSlot);
        //Assert.Throws<ArgumentException>(() => problem.OnRamComponentRemoved(eventArgs));
    }

    /// <summary>
    /// Verify the remove activity is completed when the component is removed.
    /// </summary>
    [Test]
    public void OnRamComponentRemoved_ActivityCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var targetSlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { targetSlot }, 64);

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //var eventArgs = new HardwareComponentRemovedEvent<RamComponent>(targetSlot.Component, targetSlot);
        //problem.OnRamComponentRemoved(eventArgs);
        //CollectionAssert.AreEqual(new[] { true, false }, problem.Activities.Select(a => a.AktivitetIsCompeleted).ToArray());
    }

    /// <summary>
    /// Verify the removed activity is not completed when a component is removed from a wrong slot.
    /// </summary>
    [Test]
    public void OnRamComponentRemoved_WrongSlotActivityNotCompleted()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var targetSlot = allSlots[0];
        //var wrongSlot = allSlots[1];

        //var problem = new UpgradeRam(location, new() { targetSlot }, 64);

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //var eventArgs = new HardwareComponentRemovedEvent<RamComponent>(wrongSlot.Component, wrongSlot);
        //problem.OnRamComponentRemoved(eventArgs);
        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(a => a.AktivitetIsCompeleted).ToArray());
    }

    /// <summary>
    /// Verify the remove activity is completed when the component a RamComponentRemoved event is raised.
    /// </summary>
    [Test]
    public void OnRamComponentRemoved_RamComponentRemoved()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var targetSlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { targetSlot }, 64);

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //var eventArgs = new HardwareComponentRemovedEvent<RamComponent>(targetSlot.Component, targetSlot);

        //var eventBus = new EventBus();
        //problem.AddEventListeners(eventBus);
        //eventBus.RamComponentRemoved.Invoke(eventArgs);

        //CollectionAssert.AreEqual(new[] { true, false }, problem.Activities.Select(a => a.AktivitetIsCompeleted).ToArray());
    }

    /// <summary>
    /// Verify the remove activity is uncompleted when the original component is removed and then reinstalled.
    /// </summary>
    [Test]
    public void OnRamComponentInstalled_RemoveActivityUncompletedWhenOriginalIsReinstalled()
    {
        //GameObject serverContainerGameObject = new("ServerContainer");
        //ServerContainer serverContainer = serverContainerGameObject.AddComponent<ServerContainer>();

        //GameObject serverGameObject = new("Server");
        //Server server = serverGameObject.AddComponent<Server>();
        //server.transform.parent = serverContainer.transform;

        //ServerLocation location = new(serverContainer, server);

        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject ramComponentSlotGameObject = Instantiate(this.ramComponentSlotPrefab);
        //    ramComponentSlotGameObject.name = $"Slot{i}";
        //    ramComponentSlotGameObject.transform.parent = server.transform;
        //}

        //var allSlots = server.GetHardwareComponentSlots<RamComponent>();
        //var targetSlot = allSlots[0];

        //var problem = new UpgradeRam(location, new() { targetSlot }, 64);

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //problem.OnRamComponentRemoved(new HardwareComponentRemovedEvent<RamComponent>(targetSlot.Component, targetSlot));
        //CollectionAssert.AreEqual(new[] { true, false }, problem.Activities.Select(a => a.AktivitetIsCompeleted).ToArray());

        //problem.OnRamComponentInstalled(new HardwareComponentInstalledEvent<RamComponent>(targetSlot.Component, targetSlot));
        //CollectionAssert.AreEqual(new[] { false, false }, problem.Activities.Select(a => a.AktivitetIsCompeleted).ToArray());
    }
}
