// <copyright file="DoorController.cs" company="VR4VET">
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
using UnityEngine;

/// <summary>
/// This class is responsible for the behavior of the door.
/// </summary>
public class DoorController : MonoBehaviour
{
    private new HingeJoint hingeJoint;
    private JointMotor hingeMotor;
    private NPCBehavior npcBehavior;

    /// <summary>
    /// Gets or sets a value indicating whether the door is locked.
    /// </summary>
    public bool IsLocked { get; set; } = true;

    /// <summary>
    /// Gets or sets the hinge motor.
    /// </summary>
    public JointMotor HingeMotor
    {
        get { return this.hingeMotor; }
        set { this.hingeMotor = value; }
    }

    /// <summary>
    /// Gets or sets the hinge joint.
    /// </summary>
    public HingeJoint HingeJoint
    {
        get { return this.hingeJoint; }
        set { this.hingeJoint = value; }
    }

    /// <summary>
    /// Gets or sets the NPC behavior.
    /// </summary>
    public NPCBehavior NPCBehavior
    {
        get { return this.npcBehavior; }
        set { this.npcBehavior = value; }
    }

    /// <summary>
    /// Locks the door.
    /// </summary>
    public void LockDoor()
    {
        this.IsLocked = true;
        this.hingeMotor.targetVelocity = 90f;
        this.hingeMotor.force = 1000;
        this.hingeJoint.motor = this.hingeMotor;
        this.hingeJoint.useMotor = true;

        // Debug.Log("Locking door...");
    }

    /// <summary>
    /// Unlocks the door.
    /// </summary>
    public void UnlockDoor()
    {
        // Debug.Log("UnlockDoor method started");
        this.IsLocked = false;
        this.hingeMotor.force = 0;
        this.hingeMotor.targetVelocity = 0f;
        this.hingeJoint.motor = this.hingeMotor;
        this.hingeJoint.useMotor = false;

        // Debug.Log("Unlocking door...");
    }

    /// <summary>
    /// This method is called when the script is loaded.
    /// </summary>
    public void Start()
    {
        this.hingeJoint = this.GetComponent<HingeJoint>();
        this.hingeMotor = this.hingeJoint.motor;
        this.LockDoor();

        this.npcBehavior = GameObject.FindGameObjectWithTag("Character").GetComponent<NPCBehavior>();
    }

    /// <summary>
    /// This method is called once per frame.
    /// </summary>
    public void FixedUpdate()
    {
        if (this.npcBehavior != null && this.npcBehavior.GetKeyInstance() != null)
        {
            GameObject key = this.npcBehavior.GetKeyInstance();
            bool isKeyNearDoor = Vector3.Distance(this.transform.position, key.transform.position) < 2f;
            if ((isKeyNearDoor || this.hingeJoint.angle < -3) && this.npcBehavior.GetMatchedSubstring() == this.gameObject.transform.parent.transform.parent.gameObject.name.ToString())
            {
                this.UnlockDoor();
            }
            else
            {

                this.LockDoor();
            }
        }
    }
}