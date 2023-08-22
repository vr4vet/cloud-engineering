// <copyright file="DoorControllerTests.cs" company="VR4VET">
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

using NUnit.Framework;
using UnityEngine;

/// <summary>
/// This class is responsible for the tests of the DoorController class.
/// </summary>
[TestFixture]
public class DoorControllerTests
{
    private DoorController doorController;
    private GameObject doorObject;
    private GameObject keyObject;
    private NPCBehavior npcBehavior;

    /// <summary>
    /// This method is called before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        this.doorObject = new GameObject("Door");
        this.doorController = this.doorObject.AddComponent<DoorController>();
        this.doorController.HingeJoint = this.doorObject.AddComponent<HingeJoint>();
        this.doorController.HingeMotor = default(JointMotor);

        this.keyObject = new GameObject("Key");

        this.npcBehavior = this.keyObject.AddComponent<NPCBehavior>(); // Instantiate NPCBehavior
        this.doorController.NPCBehavior = this.npcBehavior; // Assign the instantiated NPCBehavior

        var characterObject = new GameObject("Character");
        characterObject.tag = "Character";
        characterObject.AddComponent<NPCBehavior>(); // Add NPCBehavior component to the character object

        // Additional Setup for FixedUpdate test
        this.npcBehavior.SetKeyInstance(this.keyObject); // Set the key instance in NPCBehavior
        this.doorController.transform.position = new Vector3(0, 0, 0); // Set door position
        this.keyObject.transform.position = new Vector3(1, 0, 0); // Set key position
    }

    /// <summary>
    /// This method is called after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(this.doorObject);
        Object.DestroyImmediate(this.keyObject);
    }

    /// <summary>
    /// This method tests the LockDoor method.
    /// </summary>
    [Test]
    public void LockDoor_ShouldSetIsLockedToTrue()
    {
        // Act
        this.doorController.LockDoor();

        // Assert
        Assert.IsTrue(this.doorController.IsLocked);
    }

    /// <summary>
    /// This method tests hinge motor target velocity.
    /// </summary>
    [Test]
    public void LockDoor_ShouldSetHingeMotorTargetVelocity()
    {
        // Act
        this.doorController.LockDoor();

        // Assert
        Assert.AreEqual(90f, this.doorController.HingeMotor.targetVelocity);
    }

    /// <summary>
    /// This methods tests hinge motor force.
    /// </summary>
    [Test]
    public void LockDoor_ShouldSetHingeMotorForce()
    {
        // Act
        this.doorController.LockDoor();

        // Assert
        Assert.AreEqual(1000, this.doorController.HingeMotor.force);
    }

    /// <summary>
    /// This method tests hinge joint motor if value is set to true when locked.
    /// </summary>
    [Test]
    public void LockDoor_ShouldSetHingeJointUseMotorToTrue()
    {
        // Act
        this.doorController.LockDoor();

        // Assert
        Assert.IsTrue(this.doorController.HingeJoint.useMotor);
    }

    /// <summary>
    /// This method should set to false when unlocked.
    /// </summary>
    [Test]
    public void UnlockDoor_ShouldSetIsLockedToFalse()
    {
        // Arrange
        this.doorController.LockDoor();

        // Act
        this.doorController.UnlockDoor();

        // Assert
        Assert.IsFalse(this.doorController.IsLocked);
    }

    /// <summary>
    /// This method tests hinge motor force.
    /// </summary>
    [Test]
    public void UnlockDoor_ShouldSetHingeMotorForce()
    {
        // Arrange
        this.doorController.LockDoor();

        // Act
        this.doorController.UnlockDoor();

        // Assert
        Assert.AreEqual(0, this.doorController.HingeMotor.force);
    }

    /// <summary>
    /// This method tests the hinge motor target velocity.
    /// </summary>
    [Test]
    public void UnlockDoor_ShouldSetHingeMotorTargetVelocity()
    {
        // Arrange
        this.doorController.LockDoor();

        // Act
        this.doorController.UnlockDoor();

        // Assert
        Assert.AreEqual(0f, this.doorController.HingeMotor.targetVelocity);
    }

    /// <summary>
    /// This method tests the hinge joint motor if value is set to false when unlocked.
    /// </summary>
    [Test]
    public void UnlockDoor_ShouldSetHingeJointUseMotorToFalse()
    {
        // Arrange
        this.doorController.LockDoor();

        // Act
        this.doorController.UnlockDoor();

        // Assert
        Assert.IsFalse(this.doorController.HingeJoint.useMotor);
    }

    /// <summary>
    /// This method tests the Start method.
    /// </summary>
    [Test]
    public void Start_ShouldLockDoor()
    {
        // Arrange
        this.doorController.IsLocked = false;

        // Act
        this.doorController.Start();

        // Assert
        Assert.IsTrue(this.doorController.IsLocked);
    }

    /// <summary>
    /// This method tests the behavior of the FixedUpdate method when the key is far from the door.
    /// </summary>
    [Test]
    public void FixedUpdate_ShouldLockDoorWhenKeyIsFar()
    {
        // Arrange
        this.doorController.IsLocked = false; // Assume the door is initially unlocked
        this.doorController.transform.position = new Vector3(0, 0, 0); // Set door position
        this.keyObject.transform.position = new Vector3(10, 0, 0); // Set key position

        // Act
        this.doorController.FixedUpdate();

        // Assert
        Assert.IsTrue(this.doorController.IsLocked); // Door should be locked when key is far
    }
}
