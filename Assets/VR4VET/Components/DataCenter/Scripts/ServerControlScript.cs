// <copyright file="ServerControlScript.cs" company="VR4VET">
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
using System.Runtime.CompilerServices;
using DataCenter;
using DataCenter.Events;
using Task;
using Tablet;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// This creates the logic for the server control page to show the status of every server cabinet the scene contains.
/// </summary>
public class ServerControlScript : MonoBehaviour
{
    private GameObject containerButtonPrefab;
    private Transform listTransform;

    private GameObject serverCabinetPrefab;
    private Transform screenTransform;

    private GameObject activeCabinetPage;
    private ServerCabinetScript activeCabinetScript;

    private HardwareProblemGenerator hardwareProblemGenerator;
    private HardwareProblem hardwareProblem;

    private ServerContainer[] serverContainers;
    private ComputerManager computerManager;

    /// <summary>
    /// Gets or Sets activeCabinetPage.
    /// </summary>
    public GameObject ActiveCabinetPage { get => this.activeCabinetPage; set => this.activeCabinetPage = value; }

    /// <summary>
    /// Gets or Sets listTransform.
    /// </summary>
    public Transform ListTransform { get => this.listTransform; set => this.listTransform = value; }

    /// <summary>
    /// Gets or Sets containterButtonPrefab.
    /// </summary>
    public GameObject ContainerButtonPrefab { get => this.containerButtonPrefab; set => this.containerButtonPrefab = value; }

    /// <summary>
    /// Gets or Sets screenTransform.
    /// </summary>
    public Transform ScreenTransform { get => this.screenTransform; set => this.screenTransform = value; }

    /// <summary>
    /// Gets or Sets serverCabinetPrefab.
    /// </summary>
    public GameObject CabinetPagePrefab { get => this.serverCabinetPrefab; set => this.serverCabinetPrefab = value; }

    /// <summary>
    /// Gets or Sets serverContainers.
    /// </summary>
    public ServerContainer[] ServerCabinets { get => this.serverContainers; set => this.serverContainers = value; }

    /// <summary>
    /// Gets or sets hardwareProblem.
    /// </summary>
    public HardwareProblem HardwareProblem { get => this.hardwareProblem; set => this.hardwareProblem = value; }

    /// <summary>
    /// Gets problemServer.
    /// </summary>
    public Server Server { get => this.hardwareProblem.Location.Server; }

    /// <summary>
    /// Gets or Sets computerManager.
    /// </summary>
    public ComputerManager ComputerManager { get => this.computerManager; set => this.computerManager = value; }

    /// <summary>
    /// This function will create a cabinet page where users can view the cabinet and its servers' status.
    /// </summary>
    /// <param name="cabinet_index"> The index of the button pressed. </param>
    public void ActivateServerCabinetButton(int cabinet_index)
    {
        // - Create a page for cabinet #cabinet_index.
        // __ Instantiate prefab of server cabinet screen.
        this.activeCabinetPage = GameObject.Instantiate(this.serverCabinetPrefab, this.screenTransform);

        // Add listener for back button
        this.activeCabinetPage.transform.GetChild(0)
                                        .GetChild(2)
                                        .GetComponentInChildren<Button>().onClick
                                        .AddListener(() => this.SetServerControlActive());

        // Set title of the page
        this.activeCabinetPage.transform.GetChild(0)
                                         .GetChild(0)
                                         .GetComponentInChildren<Text>()
                                         .text = this.serverContainers[cabinet_index].name + " Status";

        // Give servercontainer object to ServerCabinetScript
        this.activeCabinetScript = this.activeCabinetPage.GetComponent<ServerCabinetScript>();
        this.activeCabinetScript.ServerContainer = this.serverContainers[cabinet_index];
        this.activeCabinetScript.HardwareProblem = this.hardwareProblem;

        // Set the server container to requiresMainteenace if it has a hardware problem.
        if (this.serverContainers[cabinet_index].Equals(this.hardwareProblem.Location.ServerContainer))
        {
            this.activeCabinetPage.GetComponent<ServerCabinetScript>().RequiresMaintenance = true;
        }

        // - Navigate to that created page.
        // __ Set created page to active and all others to deactive.
        this.computerManager.HideAll();
    }

    /// <summary>
    /// Sets the active page to the menu page.
    /// </summary>
    public void SetMenuActive()
    {
        this.computerManager.ShowCanvas("menuPage");
    }

    /// <summary>
    /// Sets the active page to the server control page.
    /// </summary>
    public void SetServerControlActive()
    {
        this.computerManager.ShowCanvas("controlPage");
    }

    /// <summary>
    /// Create a button for a specific servercontain page.
    /// </summary>
    /// <param name="sc"> The specific server container object. </param>
    /// <param name="i"> The index of the button. </param>
    public void CreateButton(ServerContainer sc, int i)
    {
        // Create Button for the canvas.
        // - Make server cabinet button
        // - Edit the name and status of the button.
        GameObject buttonGO = Instantiate(this.containerButtonPrefab, this.listTransform);
        Button button = buttonGO.GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { this.ActivateServerCabinetButton(i); });
        buttonGO.GetComponentInChildren<Text>().text = sc.name;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.containerButtonPrefab = (GameObject)Resources.Load("UI/ServerControlButton");
        this.listTransform = this.transform.GetChild(0).GetChild(1);

        this.serverCabinetPrefab = (GameObject)Resources.Load("UI/ServerCabinetCanvas");

        this.screenTransform = this.transform.parent;
        this.computerManager = this.transform.parent.GetComponent<ComputerManager>();

        this.hardwareProblem = this.computerManager.HardwareProblem;
        this.hardwareProblemGenerator = GameObject.FindObjectOfType<HardwareProblemGenerator>();
        this.serverContainers = this.hardwareProblemGenerator.GetServerContainers();

        int i = 0;
        foreach (ServerContainer sc in this.serverContainers)
        {
            this.CreateButton(sc, i);
            i++;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.UpdateServerContainerStatuses();
    }

    /// <summary>
    /// Updates the statuses of all server containers.
    /// </summary>
    private void UpdateServerContainerStatuses()
    {
        foreach ((ServerContainer serverContainer, int i) in this.serverContainers.Select((value, i) => (value, i)))
        {
            bool valid = serverContainer.GetServers().All(server => server.AreAllComponentsValid() && server.IsOnline);
            Image background = this.listTransform.GetChild(i).GetComponentInChildren<Image>();

            if (valid)
            {
                background.color = Color.green * 0.8F;
            }
            else
            {
                background.color = Color.red * 0.8F;
            }
        }
    }
}
