// <copyright file="ServerPopulator.cs" company="VR4VET">
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
using System.Linq;
using BNG;
using DataCenter;
using DataCenter.Events;
using UnityEngine;

/// <summary>
/// Class used to populate the server with hardware components.
/// </summary>
public class ServerPopulator : MonoBehaviour
{
    /// <summary>
    /// The prefab of the RAM module.
    /// </summary>
    [SerializeField]
    private GameObject ramComponentPrefab;

    /// <summary>
    /// The prefab of the HDD.
    /// </summary>
    [SerializeField]
    private GameObject hddComponentPrefab;

    /// <summary>
    /// Sets the prefab of the RAM module.
    /// </summary>
    public GameObject RamComponentPrefab
    {
        set => this.ramComponentPrefab = value;
    }

    /// <summary>
    /// Sets the prefab of the HDD.
    /// </summary>
    public GameObject HddComponentPrefab
    {
        set => this.hddComponentPrefab = value;
    }

    /// <summary>
    /// Sets the random number generator to use. Usefull for testing.
    /// </summary>
    public System.Random Random { private get; set; } = new();

    /// <summary>
    /// Populates the server with hardware components.
    /// </summary>
    /// <param name="server">The server to populate.</param>
    public void Populate(Server server)
    {
        this.PopulateRam(server);
        this.PopulateHdd(server);
    }

    /// <summary>
    /// Populates the server with RAM modules.
    /// </summary>
    /// <param name="server">The server to populate.</param>
    public void PopulateRam(Server server)
    {
        HardwareComponentSlot<RamComponent>[] allSlots = server.GetHardwareComponentSlots<RamComponent>();
        List<HardwareComponentSlot<RamComponent>> slots = new();

        // Either half or all of the RAM slots are populated.
        if (this.Random.Next(2) == 0)
        {
            // Populate half of the RAM slots, selecting every other slot, skipping the first slot.
            for (int i = 1; i < allSlots.Length; i += 2)
            {
                slots.Add(allSlots[i]);
            }
        }
        else
        {
            // Populate all of the RAM slots.
            slots.AddRange(allSlots);
        }

        // For simplicity, the RAM modules are all the same capacity.
        int log2Capacity = this.Random.Next(3, 6); // 2^3 = 8 GiB, 2^4 = 16 GiB, 2^5 = 32 GiB
        int capacity = (int)Math.Pow(2, log2Capacity);

        foreach (HardwareComponentSlot<RamComponent> slot in slots)
        {
            RamComponent ramComponent = this.CreateRamComponent(capacity);
            slot.TargetComponent = slot.Component = ramComponent;
        }
    }

    /// <summary>
    /// Populates the server with HDDs.
    /// </summary>
    /// <param name="server">The server to populate.</param>
    public void PopulateHdd(Server server)
    {
        HardwareComponentSlot<HddComponent>[] allSlots = server.GetHardwareComponentSlots<HddComponent>();

        int installedAmount = this.Random.Next(1, allSlots.Length + 1);

        foreach (HardwareComponentSlot<HddComponent> slot in allSlots.Take(installedAmount))
        {
            HddComponent hddComponent = this.CreateHddComponent();
            slot.TargetComponent = hddComponent;
            try
            {
                slot.Component = hddComponent;

            }
            catch (Exception ex)
            {

            }
            slot.GetComponentInChildren<GreenLightLogic>().TurnControlLightOn();
        }
    }

    /// <summary>
    /// Creates a prefab instance of a RAM module.
    /// </summary>
    /// <param name="capacity">The capacity of the RAM memory module in gibibytes (2^30 bytes).</param>
    /// <returns>The created RAM module.</returns>
    /// <exception cref="ServerPopulationException">Thrown when the RAM component prefab is null or does not have a RamComponent script attached.</exception>
    public RamComponent CreateRamComponent(int capacity)
    {
        if (this.ramComponentPrefab == null)
        {
            throw new ServerPopulationException("Ram component prefab is null.");
        }

        GameObject gameObject = Instantiate(this.ramComponentPrefab);

        if (!gameObject.TryGetComponent(out RamComponent ramComponent))
        {
            throw new ServerPopulationException("Ram component prefab does not have a RamComponent script attached.");
        }

        ramComponent.Capacity = capacity;

        return ramComponent;
    }

    /// <summary>
    /// Creates a prefab instance of a HDD.
    /// </summary>
    /// <returns>The created HDD.</returns>
    /// <param name="isBroken">Whether the HDD is broken.</param>
    /// <exception cref="ServerPopulationException">Thrown when the HDD component prefab is null or does not have a HddComponent script attached.</exception>
    public HddComponent CreateHddComponent(bool isBroken = false)
    {
        if (this.hddComponentPrefab == null)
        {
            throw new ServerPopulationException("HDD component prefab is null.");
        }

        GameObject gameObject = Instantiate(this.hddComponentPrefab);

        if (!gameObject.TryGetComponent(out HddComponent hddComponent))
        {
            throw new ServerPopulationException("HDD component prefab does not have a HddComponent script attached.");
        }

        hddComponent.IsBroken = isBroken;

        return hddComponent;
    }

    /// <summary>
    /// Populates all servers except the server of the hardware problem with hardware components.
    /// </summary>
    /// <param name="e">The event containing the hardware problem.</param>
    public void PopulateNonProblematicServers(HardwareProblemGeneratedEvent e)
    {
        Server problemServer = e.HardwareProblem.Location.Server;

        foreach (Server server in DataCenterScenario.Instance.GetComponentsInChildren<Server>())
        {
            if (server == problemServer)
            {
                continue;
            }

            this.Populate(server);
        }
    }

    /// <summary>
    /// Populates the server of the hardware problem with hardware components.
    /// </summary>
    /// <param name="e">The event containing the hardware problem.</param>
    public void PopulateProblematicServer(HardwareProblemGeneratedEvent e)
    {
        e.HardwareProblem.ProblemType.PopulateServer(this);
    }

    /// <summary>
    /// This method is called when a hardware problem is generated.
    /// </summary>
    /// <param name="e">The event.</param>
    public void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
    {
        this.PopulateNonProblematicServers(e);
        this.PopulateProblematicServer(e);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated += this.OnHardwareProblemGenerated;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        DataCenterScenario.Instance.EventBus.HardwareProblemGenerated -= this.OnHardwareProblemGenerated;
    }
}
