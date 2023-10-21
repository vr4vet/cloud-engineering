// <copyright file="InstallAdditionalHddTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="InstallAdditionalHdd"/> class.
/// </summary>
public class InstallAdditionalHddTest : MonoBehaviour
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
    /// Verify all slots up to the slots the player has to install the HDDs in are empty,
    /// and that RAM is populated.
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

        //var problem = new InstallAdditionalHdd(location, new() { allSlots[3], allSlots[4] });

        //GameObject serverPopulatorObject = new();
        //ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        //serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        //serverPopulator.HddComponentPrefab = this.hddComponentPrefab;
        //serverPopulator.Random = new System.Random(0);
        //problem.PopulateServer(serverPopulator);

        //Assert.NotNull(allSlots[0].Component);
        //Assert.NotNull(allSlots[1].Component);
        //Assert.NotNull(allSlots[2].Component);
        //Assert.Null(allSlots[3].Component);
        //Assert.Null(allSlots[4].Component);
        //Assert.Null(allSlots[5].Component);

        //Assert.True(server.GetHardwareComponentSlots<RamComponent>().Any(slot => slot.Component != null));
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

        //var problem = new InstallAdditionalHdd(location, new() { emptySlot });

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

        //var problem = new InstallAdditionalHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, emptySlot);
        //problem.OnHddComponentInstalled(eventArgs);

        //Assert.IsTrue(problem.Activities[0].AktivitetIsCompeleted);
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

        //var problem = new InstallAdditionalHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, wrongSlot);
        //problem.OnHddComponentInstalled(eventArgs);

        //Assert.IsFalse(problem.Activities[0].AktivitetIsCompeleted);
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

        //var problem = new InstallAdditionalHdd(location, new() { emptySlot });

        //var hddComponentGameObject = Instantiate(this.hddComponentPrefab);
        //var hddComponent = hddComponentGameObject.GetComponent<HddComponent>();
        //Assert.NotNull(hddComponent);

        //var eventArgs = new HardwareComponentInstalledEvent<HddComponent>(hddComponent, emptySlot);

        //var eventBus = new EventBus();
        //problem.AddEventListeners(eventBus);
        //eventBus.HddComponentInstalled.Invoke(eventArgs);

        //Assert.IsTrue(problem.Activities[0].AktivitetIsCompeleted);
    }
}
