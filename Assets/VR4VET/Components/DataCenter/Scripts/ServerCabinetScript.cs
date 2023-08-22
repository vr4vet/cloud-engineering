// <copyright file="ServerCabinetScript.cs" company="VR4VET">
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
using Tablet;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// This creates the logic for the server cabinet page to show the status of every server the cabinet contains.
/// </summary>
public class ServerCabinetScript : MonoBehaviour
{
    private GameObject serverButtonPrefab;
    private Transform listTransform;
    private Transform computerScreenTransform;
    private GameObject serverPagePrefab;

    private ComputerManager computerManager;

    private ServerContainer serverContainer;
    private Server[] servers;

    private GameObject activeServerPage;

    private bool requiresMaintenance;
    private Server problemServer;
    private HardwareProblem hardwareProblem;

    /// <summary>
    /// Gets or Sets serverContainer.
    /// </summary>
    public ServerContainer ServerContainer { get => this.serverContainer; set => this.serverContainer = value; }

    /// <summary>
    /// Gets or sets a value indicating whether server container RequiresMaintenance.
    /// </summary>
    public bool RequiresMaintenance { get => this.requiresMaintenance; set => this.requiresMaintenance = value; }

    /// <summary>
    /// Gets or sets ProblemServer.
    /// </summary>
    public Server ProblemServer { get => this.problemServer; set => this.problemServer = value; }

    /// <summary>
    /// Gets or sets the hardware problem.
    /// </summary>
    public HardwareProblem HardwareProblem { get => this.hardwareProblem; set => this.hardwareProblem = value; }

    /// <summary>
    /// This function will create a server page where users can view the server and its components' status.
    /// </summary>
    /// <param name="server_index"> The index of the button pressed. </param>
    public void ActivateServerButton(int server_index)
    {
        // Activate server button

        // - Create a page for cabinet #cabinet_index.
        // __ Instantiate prefab of server cabinet screen.
        this.activeServerPage = GameObject.Instantiate(this.serverPagePrefab, this.computerScreenTransform);

        // Set title of the page
        this.activeServerPage.transform.GetChild(0).GetChild(0)
                                        .GetComponentInChildren<Text>().text = this.servers[server_index].name + " Status";

        // Give server object to ServerStatusScript
        Server server = this.servers[server_index];
        ServerStatusScript serverStatusScript = this.activeServerPage.GetComponent<ServerStatusScript>();
        serverStatusScript.Server = this.servers[server_index];
        serverStatusScript.PreviousPage = this.gameObject;
        serverStatusScript.HardwareProblem = this.hardwareProblem;
        if (this.requiresMaintenance && this.problemServer != null)
        {
            serverStatusScript.RequiresMaintenance = this.problemServer.Equals(server);
        }

        // - Navigate to that created page.
        // __ Set created page to active and all others to deactive.
        this.gameObject.SetActive(false);
        this.computerManager.HideAll();
    }

    /// <summary>
    /// Sets the active page to the server control page.
    /// </summary>
    public void SetServerControlActive()
    {
        this.computerManager.ShowCanvas("controlPage");
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.serverButtonPrefab = (GameObject)Resources.Load("UI/ServerControlButton");
        this.listTransform = this.gameObject.transform.GetChild(0).GetChild(1);

        this.serverPagePrefab = (GameObject)Resources.Load("UI/ServerPageCanvas");

        this.servers = this.serverContainer.GetServers();

        this.computerManager = this.transform.parent.GetComponent<ComputerManager>();
        this.computerScreenTransform = this.transform.parent;

        int i = 0;
        foreach (Server server in this.servers)
        {
            this.CreateButton(server, i);
            i++;
        }

        this.UpdateServerStatuses();

        this.transform.GetChild(0)
                      .GetChild(2)
                      .GetComponentInChildren<Button>().onClick
                      .AddListener(() => Destroy(this.gameObject));
    }

    private void CreateButton(Server server, int i)
    {
        // Create Button for the canvas.
        GameObject buttonGO = Instantiate(this.serverButtonPrefab, this.listTransform);
        Button button = buttonGO.GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { this.ActivateServerButton(i); });
        buttonGO.GetComponentInChildren<Text>().text = server.name;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.UpdateServerStatuses();
    }

    /// <summary>
    /// Updates the statuses of all servers.
    /// </summary>
    private void UpdateServerStatuses()
    {
        foreach ((Server server, int i) in this.servers.Select((value, i) => (value, i)))
        {
            bool valid = server.AreAllComponentsValid() && server.IsOnline;
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
