// <copyright file="ServerPopulatorTest.cs" company="VR4VET">
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
using DataCenter.HardwareProblems;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="ServerPopulatorTest"/> class.
/// </summary>
public class ServerPopulatorTest
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
        this.ramComponentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/RAM.prefab");
        this.ramComponentSlotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/RAM socket.prefab");
        this.hddComponentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/HDD.prefab");
        this.hddComponentSlotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/HDD Socket.prefab");

        Assert.NotNull(this.ramComponentPrefab);
        Assert.NotNull(this.ramComponentSlotPrefab);
        Assert.NotNull(this.hddComponentPrefab);
        Assert.NotNull(this.hddComponentSlotPrefab);
    }

    /// <summary>
    /// Helper function to instantiate a hardware structure with the given number of RAM component slots.
    /// </summary>
    /// <param name="parent">The parent object the hardware will be a child of.</param>
    /// <param name="numRamComponentSlots">The number of RAM memory module slots.</param>
    /// <returns>The instantiated server.</returns>
    public Server InstantiateServerRam(GameObject parent, int numRamComponentSlots)
    {
        GameObject serverObject = new("Server");
        serverObject.transform.parent = parent.transform;
        Server server = serverObject.AddComponent<Server>();
        Assert.NotNull(server);

        for (int ramIndex = 0; ramIndex < numRamComponentSlots; ramIndex++)
        {
            GameObject ramComponentSlotObject = UnityEngine.Object.Instantiate(this.ramComponentSlotPrefab, serverObject.transform);
            ramComponentSlotObject.name = $"RamComponentSlot{ramIndex}";
        }

        return server;
    }

    /// <summary>
    /// Helper function to instantiate a hardware structure with the given number of HDD component slots.
    /// </summary>
    /// <param name="parent">The parent object the hardware will be a child of.</param>
    /// <param name="numHddComponentSlots">The number of HDD slots.</param>
    /// <returns>The instantiated server.</returns>
    public Server InstantiateServerHdd(GameObject parent, int numHddComponentSlots)
    {
        GameObject serverObject = new("Server");
        serverObject.transform.parent = parent.transform;
        Server server = serverObject.AddComponent<Server>();
        Assert.NotNull(server);

        for (int hddIndex = 0; hddIndex < numHddComponentSlots; hddIndex++)
        {
            GameObject hddComponentSlotObject = UnityEngine.Object.Instantiate(this.hddComponentSlotPrefab, serverObject.transform);
            hddComponentSlotObject.name = $"HddComponentSlot{hddIndex}";
        }

        return server;
    }

    /// <summary>
    /// Helper function to instantiate a hardware structure with the given numbers of component slots.
    /// </summary>
    /// <param name="parent">The parent object the hardware will be a child of.</param>
    /// <param name="numRamComponentSlots">The number of RAM memory module slots.</param>
    /// <param name="numHddComponentSlots">The number of HDD slots.</param>
    /// <returns>The instantiated server.</returns>
    public Server InstantiateServerEverything(GameObject parent, int numRamComponentSlots, int numHddComponentSlots)
    {
        GameObject serverObject = new("Server");
        serverObject.transform.parent = parent.transform;
        Server server = serverObject.AddComponent<Server>();
        Assert.NotNull(server);

        for (int ramIndex = 0; ramIndex < numRamComponentSlots; ramIndex++)
        {
            GameObject ramComponentSlotObject = UnityEngine.Object.Instantiate(this.ramComponentSlotPrefab, serverObject.transform);
            ramComponentSlotObject.name = $"RamComponentSlot{ramIndex}";
        }

        for (int hddIndex = 0; hddIndex < numHddComponentSlots; hddIndex++)
        {
            GameObject hddComponentSlotObject = UnityEngine.Object.Instantiate(this.hddComponentSlotPrefab, serverObject.transform);
            hddComponentSlotObject.name = $"HddComponentSlot{hddIndex}";
        }

        return server;
    }

    /// <summary>
    /// Verify that all half of the RAM component slots are populated.
    /// </summary>
    [Test]
    public void PopulateRam_Half()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        serverPopulator.Random = new System.Random(1);

        Server server = this.InstantiateServerRam(serverPopulatorObject, 4);

        serverPopulator.Populate(server);

        HardwareComponentSlot<RamComponent>[] hardwareComponentSlots = server.GetHardwareComponentSlots<RamComponent>();
        bool[] populatedSlots = Array.ConvertAll(hardwareComponentSlots, slot => slot.Component != null);
        Assert.AreEqual(new bool[] { false, true, false, true }, populatedSlots);
    }

    /// <summary>
    /// Verify that all all of the RAM component slots are populated.
    /// </summary>
    [Test]
    public void PopulateRam_All()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        serverPopulator.Random = new System.Random(0);

        Server server = this.InstantiateServerRam(serverPopulatorObject, 4);

        serverPopulator.Populate(server);

        HardwareComponentSlot<RamComponent>[] hardwareComponentSlots = server.GetHardwareComponentSlots<RamComponent>();
        bool[] populatedSlots = Array.ConvertAll(hardwareComponentSlots, slot => slot.Component != null);
        Assert.AreEqual(new bool[] { true, true, true, true }, populatedSlots);
    }

    /// <summary>
    /// Verify that at least one of the HDD component slots is populated.
    /// </summary>
    [Test]
    public void PopulateHdd_AtLeastOne()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.HddComponentPrefab = this.hddComponentPrefab;
        serverPopulator.Random = new System.Random(0);

        for (int attempts = 0; attempts < 8; attempts++)
        {
            Server server = this.InstantiateServerHdd(serverPopulatorObject, 8);
            serverPopulator.PopulateHdd(server);

            Assert.IsTrue(server.GetHardwareComponentSlots<HddComponent>().Any(slot => slot.Component != null));

            GameObject.DestroyImmediate(server);
        }
    }

    /// <summary>
    /// Verify that all of the HDD component slots can be populated.
    /// </summary>
    [Test]
    public void PopulateHdd_All()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.HddComponentPrefab = this.hddComponentPrefab;
        serverPopulator.Random = new System.Random(8);

        Server server = this.InstantiateServerHdd(serverPopulatorObject, 8);
        serverPopulator.PopulateHdd(server);

        HardwareComponentSlot<HddComponent>[] hardwareComponentSlots = server.GetHardwareComponentSlots<HddComponent>();
        bool[] populatedSlots = Array.ConvertAll(hardwareComponentSlots, slot => slot.Component != null);
        Assert.AreEqual(new bool[] { true, true, true, true, true, true, true, true }, populatedSlots);
    }

    /// <summary>
    /// Verify an exception is thrown when the RAM component prefab is null.
    /// </summary>
    [Test]
    public void CreateRamComponent_RamComponentPrefabNull()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = null;
        serverPopulator.Random = new System.Random(0);

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => serverPopulator.CreateRamComponent(8));
        Assert.AreEqual("Ram component prefab is null.", exception.Message);
    }

    /// <summary>
    /// Verify an exception is thrown when the RAM component prefab does not have a RamComponent script.
    /// </summary>
    [Test]
    public void CreateRamComponent_RamComponentPrefabNoRamComponent()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = new GameObject("Wrong prefab");
        serverPopulator.Random = new System.Random(0);

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => serverPopulator.CreateRamComponent(8));
        Assert.AreEqual("Ram component prefab does not have a RamComponent script attached.", exception.Message);
    }

    /// <summary>
    /// Verify an exception is thrown when the HDD component prefab is null.
    /// </summary>
    [Test]
    public void CreateHddComponent_HddComponentPrefabNull()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.HddComponentPrefab = null;
        serverPopulator.Random = new System.Random(0);

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => serverPopulator.CreateHddComponent());
        Assert.AreEqual("HDD component prefab is null.", exception.Message);
    }

    /// <summary>
    /// Verify an exception is thrown when the HDD component prefab does not have a HddComponent script.
    /// </summary>
    [Test]
    public void CreateHddComponent_HddComponentPrefabNoHddComponent()
    {
        GameObject serverPopulatorObject = new();
        ServerPopulator serverPopulator = serverPopulatorObject.AddComponent<ServerPopulator>();
        serverPopulator.HddComponentPrefab = new GameObject("Wrong prefab");
        serverPopulator.Random = new System.Random(0);

        ServerPopulationException exception = Assert.Throws<ServerPopulationException>(() => serverPopulator.CreateHddComponent());
        Assert.AreEqual("HDD component prefab does not have a HddComponent script attached.", exception.Message);
    }

    /// <summary>
    /// Verify all servers are populated with RAM components, except for the server with the hardware problem.
    /// </summary>
    [Test]
    public void PopulateNonProblematicServers()
    {
        GameObject dataCenterScenarioObject = new();
        dataCenterScenarioObject.AddComponent<DataCenterScenario>();
        HardwareProblemGenerator hardwareProblemGenerator = dataCenterScenarioObject.AddComponent<HardwareProblemGenerator>();
        ServerPopulator serverPopulator = dataCenterScenarioObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        serverPopulator.Random = new System.Random(0);

        GameObject serverContainerGameObject = new("ServerContainer");
        serverContainerGameObject.transform.parent = dataCenterScenarioObject.transform;
        serverContainerGameObject.AddComponent<ServerContainer>();

        Server server0 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server1 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server2 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server3 = this.InstantiateServerRam(serverContainerGameObject, 4);

        server0.name = "Server0";
        server1.name = "Server1";
        server2.name = "Server2";
        server3.name = "Server3";

        HardwareProblem hardwareProblem = hardwareProblemGenerator.GenerateProblem(new System.Random(0), new Type[] { typeof(InstallAdditionalRam) });

        Assert.AreEqual("Server3", hardwareProblem.Location.Server.name);

        serverPopulator.PopulateNonProblematicServers(new(hardwareProblem));

        Assert.AreNotEqual(0, server0.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreNotEqual(0, server1.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreNotEqual(0, server2.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreEqual(0, server3.GetComponentsInChildren<RamComponent>().Length);
    }

    /// <summary>
    /// Verify only the server with the hardware problem is populated.
    /// </summary>
    [Test]
    public void PopulateProblematicServer()
    {
        GameObject dataCenterScenarioObject = new();
        dataCenterScenarioObject.AddComponent<DataCenterScenario>();
        HardwareProblemGenerator hardwareProblemGenerator = dataCenterScenarioObject.AddComponent<HardwareProblemGenerator>();
        ServerPopulator serverPopulator = dataCenterScenarioObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        serverPopulator.Random = new System.Random(0);

        GameObject serverContainerGameObject = new("ServerContainer");
        serverContainerGameObject.transform.parent = dataCenterScenarioObject.transform;
        serverContainerGameObject.AddComponent<ServerContainer>();

        Server server0 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server1 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server2 = this.InstantiateServerRam(serverContainerGameObject, 4);
        Server server3 = this.InstantiateServerRam(serverContainerGameObject, 4);

        server0.name = "Server0";
        server1.name = "Server1";
        server2.name = "Server2";
        server3.name = "Server3";

        HardwareProblem hardwareProblem = hardwareProblemGenerator.GenerateProblem(new System.Random(0), new Type[] { typeof(InstallAdditionalRam) });

        Assert.AreEqual("Server3", hardwareProblem.Location.Server.name);

        serverPopulator.PopulateProblematicServer(new(hardwareProblem));

        Assert.AreEqual(0, server0.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreEqual(0, server1.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreEqual(0, server2.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreNotEqual(0, server3.GetComponentsInChildren<RamComponent>().Length);
    }

    /// <summary>
    /// Verify all servers are populated with RAM and HDD components.
    /// </summary>
    [Test]
    public void OnHardwareProblemGenerated_PopulatesAllServers()
    {
        GameObject dataCenterScenarioObject = new();
        dataCenterScenarioObject.AddComponent<DataCenterScenario>();
        HardwareProblemGenerator hardwareProblemGenerator = dataCenterScenarioObject.AddComponent<HardwareProblemGenerator>();
        ServerPopulator serverPopulator = dataCenterScenarioObject.AddComponent<ServerPopulator>();
        serverPopulator.RamComponentPrefab = this.ramComponentPrefab;
        serverPopulator.HddComponentPrefab = this.hddComponentPrefab;
        serverPopulator.Random = new System.Random(0);

        GameObject serverContainerGameObject = new("ServerContainer");
        serverContainerGameObject.transform.parent = dataCenterScenarioObject.transform;
        serverContainerGameObject.AddComponent<ServerContainer>();

        Server server0 = this.InstantiateServerEverything(serverContainerGameObject, 4, 2);
        Server server1 = this.InstantiateServerEverything(serverContainerGameObject, 4, 2);

        server0.name = "Server0";
        server1.name = "Server1";

        HardwareProblem hardwareProblem = hardwareProblemGenerator.GenerateProblem(new System.Random(0), new Type[] { typeof(InstallAdditionalRam) });

        serverPopulator.OnHardwareProblemGenerated(new(hardwareProblem));

        Assert.AreNotEqual(0, server0.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreNotEqual(0, server0.GetComponentsInChildren<HddComponent>().Length);
        Assert.AreNotEqual(0, server1.GetComponentsInChildren<RamComponent>().Length);
        Assert.AreNotEqual(0, server1.GetComponentsInChildren<HddComponent>().Length);
    }
}
