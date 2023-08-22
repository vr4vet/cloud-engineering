// <copyright file="ChangeSnapStatus.cs" company="VR4VET">
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

using BNG;
using UnityEngine;

/// <summary>
/// This class checks if two levers are activated or not, and disables the grabbable in a GameObject corresponding to that. The grabbable is
/// only enabled when both levers are pulled down (one lever is mirrored, hence one lever is less than 50% and the other more than 50%).
/// </summary>
public class ChangeSnapStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject snapPoint;
    [SerializeField]
    private GameObject handleOther;

    private Lever leverThis;
    private Lever leverOther;
    private float percThis;
    private float percOther;

    /// <summary>
    /// Gets or Sets Lever for LeverThis.
    /// </summary>
    public Lever LeverThis { get => this.leverThis; set => this.leverThis = value; }

    /// <summary>
    /// Gets or Sets Lever for LeverOther.
    /// </summary>
    public Lever LeverOther { get => this.leverOther; set => this.leverOther = value; }

#if UNITY_EDITOR
    /// <summary>
    /// This method sets the serialized field objects to test objects.
    /// </summary>
    /// <param name="testSnapPoint">the test snap point.</param>
    /// <param name="testHandleOther">the test second handle.</param>
    public void SetTestData(GameObject testSnapPoint, GameObject testHandleOther)
    {
        this.snapPoint = testSnapPoint;
        this.handleOther = testHandleOther;
    }
#endif

    /// <summary>
    /// Loads the Levers from GameObjects.
    /// </summary>
    public void LoadLevers()
    {
        this.leverOther = this.handleOther.GetComponent<Lever>();
        this.leverThis = this.GetComponent<Lever>();
    }

    /// <summary>
    /// Checks if the levers is open halfway or more, and that it is not already enabled/disabled.
    /// </summary>
    public void LeverCheck()
    {
        if (this.percThis <= 50f && this.percOther >= 50f)
        {
            this.EnableRemovable();
        }
        else
        {
            this.DisableRemovable();
        }
    }

    /// <summary>
    /// Loads the LeverPercentages into their fields.
    /// </summary>
    public void LoadLeverPercentage()
    {
        this.percOther = this.leverOther.LeverPercentage;
        this.percThis = this.leverThis.LeverPercentage;
    }

    /// <summary>
    /// Loads the levers on startup.
    /// </summary>
    private void Start()
    {
        this.LoadLevers();
    }

    /// <summary>
    /// Loads lever percentage and checks every set frame.
    /// </summary>
    private void FixedUpdate()
    {
        this.LoadLeverPercentage();
        this.LeverCheck();
    }

    /// <summary>
    /// Disables Grabbable script inside of the snap point.
    /// </summary>
    private void DisableRemovable()
    {
        this.snapPoint.GetComponent<Grabbable>().enabled = false;
        this.snapPoint.GetComponent<BoxCollider>().enabled = false;
        this.snapPoint.GetComponent<SphereCollider>().enabled = false;
    }

    /// <summary>
    /// Enables Grabbable script inside of the snap point.
    /// </summary>
    private void EnableRemovable()
    {
        this.snapPoint.GetComponent<Grabbable>().enabled = true;
        this.snapPoint.GetComponent<SphereCollider>().enabled = true;

        if (this.snapPoint.GetComponent<SnapZone>().HeldItem != null)
        {
            this.snapPoint.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            this.snapPoint.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
