// <copyright file="MoveJoint.cs" company="VR4VET">
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

using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

/// <summary>
/// This is specifically created for moving levers when called, and given the time to move the lever in realtime.
/// </summary>
public class MoveJoint : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 10f;
    [SerializeField]
    private float waitSeconds = 1f;
    private Lever lever;

    /// <summary>
    /// Gets or Sets Lever.
    /// </summary>
    public Lever Lever { get => this.lever; set => this.lever = value; }

    /// <summary>
    /// Starts the coroutine for moving the lever, which will return the lever back to center in realtime.
    /// </summary>
    public void StartMovingLever()
    {
        this.StartCoroutine(this.MoveLever());
    }

    /// <summary>
    /// Will move the lever at a chosen speed, and disables automatic locking after a certain time.
    /// </summary>
    private IEnumerator MoveLever()
    {
        this.lever = this.GetComponent<Lever>();
        this.lever.ReturnLookSpeed = this.lookSpeed;
        this.lever.ReturnToCenter = true;

        yield return new WaitForSeconds(this.waitSeconds);

        this.lever.ReturnToCenter = false;
    }
}
