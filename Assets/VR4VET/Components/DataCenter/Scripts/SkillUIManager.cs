// <copyright file="SkillUIManager.cs" company="VR4VET">
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
using Tablet;
using UnityEngine;
using UnityEngine.UI;
using Task;

/// <summary>
/// Class controlling the skill UI of the computer prefab.
/// </summary>
public class SkillUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject listItemPrefab;
    [SerializeField]
    private Transform listItemHolder;
    [SerializeField]
    private ComputerManager computerManager;
    private Task.TaskHolder th;

    /// <summary>
    /// Gets or Sets the private field "listItemPrefab".
    /// </summary>
    public GameObject ListItemPrefab { get => this.listItemPrefab; set => this.listItemPrefab = value; }

    /// <summary>
    /// Gets or Sets the private field "listItemHolder".
    /// </summary>
    public Transform ListItemHolder { get => this.listItemHolder; set => this.listItemHolder = value; }

    /// <summary>
    /// Gets or Sets the private field "th".
    /// </summary>
    public TaskHolder TaskHolder { get => this.th; set => this.th = value; }

    /// <summary>
    /// Sets the active page to the menu page.
    /// </summary>
    public void SetMenuActive()
    {
        this.computerManager.ShowCanvas("menuPage");
    }

    /// <summary>
    /// This method instantiates the list of skills and its points. This data will be filled in into the UI.
    /// </summary>
    public void InstantiateList()
    {
    }

    /// <summary>
    /// This method is called before the first frame.
    /// </summary>
    private void Start()
    {
        this.th = GameObject.FindObjectsOfType<TaskHolder>()[0];
        this.InstantiateList();
    }
}