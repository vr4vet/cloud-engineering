// <copyright file="ServerStatusScript.cs" company="VR4VET">
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

using System.Collections.Generic;
using System.Linq;
using DataCenter;
using DataCenter.Events;
using Tablet;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This creates the logic for the server status screen.
/// </summary>
public class ServerStatusScript : MonoBehaviour
{
    private HardwareProblemGenerator hardwareProblemGenerator;
    private HardwareProblem hardwareProblem;
    private GameObject ramStatus;
    private GameObject hardDriveStatus;
    private Transform ramTransform;
    private Transform hddTransform;
    private HardwareComponentSlot<RamComponent>[] ramSlots;
    private Server server;
    private bool requiresMaintenance;

    private GameObject previousPage;
    private HardwareComponentSlot<HddComponent>[] hardDriveSlots;

    /// <summary>
    /// Gets or Sets server.
    /// </summary>
    public Server Server { get => this.server; set => this.server = value; }

    /// <summary>
    /// Gets or Sets the page to go back to.
    /// </summary>
    public GameObject PreviousPage { get => this.previousPage; set => this.previousPage = value; }

    /// <summary>
    /// Gets or sets a value indicating whether server requires maintenance.
    /// </summary>
    public bool RequiresMaintenance { get => this.requiresMaintenance; set => this.requiresMaintenance = value; }

    /// <summary>
    /// Gets or sets hardware problem.
    /// </summary>
    public HardwareProblem HardwareProblem { get => this.hardwareProblem; set => this.hardwareProblem = value; }

    /// <summary>
    /// This toggles the power of the server.
    /// </summary>
    public void TogglePower()
    {
        this.server.IsOnline = !this.server.IsOnline;
        this.UpdatePower();

        if (this.server.Equals(this.hardwareProblem.Location.Server))
        {
            if (this.server.IsOnline)
            {
                //Activity activity = DataCenterScenario.Instance.PerformMaintenanceTask.TurnServerOn;
                //DataCenterScenario.Instance.SetActivityCompleted(activity, true);

                // TODO: Set turn server on to complete
                Debug.Log("Still need to set server turned on to complete");
            }
            else
            {
                //Activity activity = DataCenterScenario.Instance.PerformMaintenanceTask.ShutServerOff;
                //DataCenterScenario.Instance.SetActivityCompleted(activity, true);

                // TODO: Set turn server off to complete
                Debug.Log("Still need to set server turned off to complete");
            }
        }
    }

    /// <summary>
    /// Updates the power status of the server in the UI.
    /// </summary>
    private void UpdatePower()
    {
        Transform button = this.transform.GetChild(0).GetChild(4);
        Image background = button.GetComponent<Image>();
        Text text = button.GetComponentInChildren<Text>();

        if (this.server.IsOnline)
        {
            background.color = Color.green * 0.8F;
            text.text = "ON";
        }
        else
        {
            background.color = Color.red * 0.8F;
            text.text = "OFF";
        }
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        this.hardwareProblemGenerator = GameObject.FindObjectOfType<HardwareProblemGenerator>();
        this.ramStatus = (GameObject)Resources.Load("UI/RamStatus");
        this.hardDriveStatus = (GameObject)Resources.Load("UI/HddStatus");
        this.ramTransform = this.gameObject.transform.GetChild(0).GetChild(1);
        this.hddTransform = this.gameObject.transform.GetChild(0).GetChild(2);
        this.requiresMaintenance = this.hardwareProblem.Location.Server.Equals(this.server);

        this.InstantiateRamStatusUi();
        this.InstantiateHddStatusUi();

        this.UpdatePower();

        // Destroys this page when leaving it.
        this.transform.GetChild(0)
                      .GetChild(3)
                      .GetComponentInChildren<Button>().onClick
                      .AddListener(() =>
                      {
                          this.previousPage.SetActive(true);
                          Destroy(this.gameObject);
                      });
    }

    /// <summary>
    /// Instantiates the HDD status UI elements for all slots and updates them.
    /// </summary>
    private void InstantiateHddStatusUi()
    {
        this.hardDriveSlots = this.server.GetHardwareComponentSlots<HddComponent>();
        foreach (HardwareComponentSlot<HddComponent> hardDriveSlot in this.hardDriveSlots)
        {
            Instantiate(this.hardDriveStatus, this.hddTransform);
            this.UpdateHddComponentSlotStatus(hardDriveSlot);
        }
    }

    /// <summary>
    /// Instantiates the RAM status UI elements for all slots and updates them.
    /// </summary>
    private void InstantiateRamStatusUi()
    {
        this.ramSlots = this.server.GetHardwareComponentSlots<RamComponent>();
        foreach (HardwareComponentSlot<RamComponent> ramSlot in this.ramSlots)
        {
            Instantiate(this.ramStatus, this.ramTransform);
            this.UpdateRamComponentSlotStatus(ramSlot);
        }
    }

    /// <summary>
    /// This function is called when the task is created.
    /// </summary>
    private void OnEnable()
    {
        DataCenterScenario.Instance.EventBus.RamComponentInstalled += this.OnRamComponentInstalled;
        DataCenterScenario.Instance.EventBus.RamComponentRemoved += this.OnRamComponentRemoved;
        DataCenterScenario.Instance.EventBus.HddComponentInstalled += this.OnHddComponentInstalled;
        DataCenterScenario.Instance.EventBus.HddComponentRemoved += this.OnHddComponentRemoved;
    }

    /// <summary>
    /// This function is called when the task will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        DataCenterScenario.Instance.EventBus.RamComponentInstalled -= this.OnRamComponentInstalled;
        DataCenterScenario.Instance.EventBus.RamComponentRemoved -= this.OnRamComponentRemoved;
        DataCenterScenario.Instance.EventBus.HddComponentInstalled -= this.OnHddComponentInstalled;
        DataCenterScenario.Instance.EventBus.HddComponentRemoved -= this.OnHddComponentRemoved;
    }

    /// <summary>
    /// This method is called when the player has installed a RAM module.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnRamComponentInstalled(HardwareComponentInstalledEvent<RamComponent> e)
    {
        this.UpdateRamComponentSlotStatus(e.Slot);
    }

    /// <summary>
    /// This method is called when the player has removed a RAM module.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnRamComponentRemoved(HardwareComponentRemovedEvent<RamComponent> e)
    {
        this.UpdateRamComponentSlotStatus(e.Slot);
    }

    /// <summary>
    /// This method is called when the player has installed a hard disk drive.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnHddComponentInstalled(HardwareComponentInstalledEvent<HddComponent> e)
    {
        this.UpdateHddComponentSlotStatus(e.Slot);
    }

    /// <summary>
    /// This method is called when the player has removed a hard disk drive.
    /// </summary>
    /// <param name="e">The event.</param>
    private void OnHddComponentRemoved(HardwareComponentRemovedEvent<HddComponent> e)
    {
        this.UpdateHddComponentSlotStatus(e.Slot);
    }

    /// <summary>
    /// Updates the display of the specified RAM component slot.
    /// </summary>
    /// <param name="slot">The RAM component slot.</param>
    private void UpdateRamComponentSlotStatus(HardwareComponentSlot<RamComponent> slot)
    {
        var allSlots = this.server.GetHardwareComponentSlots<RamComponent>().ToList();
        if (!allSlots.Contains(slot))
        {
            return;
        }

        int index = allSlots.IndexOf(slot) + 1;
        var ramTransform = this.transform.GetChild(0).GetChild(1).GetChild(index);

        Image background = ramTransform.GetComponentInChildren<Image>();
        Text ramName = ramTransform.GetChild(0).GetComponent<Text>();
        Text capacity = ramTransform.GetChild(1).GetComponent<Text>();

        if (slot.Component != null)
        {
            ramName.text = "RAM";
            capacity.text = slot.Component.Capacity.ToString() + " GiB";
        }
        else
        {
            ramName.text = "Empty";
            capacity.text = "-";
        }

        if (slot.IsComponentValid())
        {
            if (slot.Component != null)
            {
                background.color = Color.green * 0.8F;
            }
            else
            {
                background.color = Color.gray;
            }
        }
        else
        {
            background.color = Color.red * 0.8F;
        }
    }

    /// <summary>
    /// Updates the display of the specified HDD component slot.
    /// </summary>
    /// <param name="slot">The HDD component slot.</param>
    private void UpdateHddComponentSlotStatus(HardwareComponentSlot<HddComponent> slot)
    {
        var allSlots = this.server.GetHardwareComponentSlots<HddComponent>().ToList();
        if (!allSlots.Contains(slot))
        {
            return;
        }

        int index = allSlots.IndexOf(slot) + 1;
        var componentTransform = this.hddTransform.GetChild(index);

        Image background = componentTransform.GetComponent<Image>();
        Text hddName = componentTransform.Find("HddName").GetComponent<Text>();

        if (slot.Component != null)
        {
            hddName.text = $"{slot.name}: HDD";
        }
        else
        {
            hddName.text = $"{slot.name}: Empty";
        }

        if (slot.IsComponentValid())
        {
            if (slot.Component != null)
            {
                background.color = Color.green * 0.8F;
            }
            else
            {
                background.color = Color.gray;
            }
        }
        else
        {
            background.color = Color.red * 0.8F;
        }
    }
}