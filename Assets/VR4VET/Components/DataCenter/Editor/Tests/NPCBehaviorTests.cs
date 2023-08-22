// <copyright file="NPCBehaviorTests.cs" company="VR4VET">
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

using Newtonsoft.Json.Bson;
using NUnit.Framework;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is responsible for the tests of the NPCBehavior class.
/// </summary>
public class NPCBehaviorTests
{
    private GameObject npcGameObject;
    private NPCBehavior npcBehavior;

    /// <summary>
    /// This method is called before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Create necessary objects for NPCBehavior
        this.npcBehavior = new GameObject().AddComponent<NPCBehavior>();

        // Initialize any required dependencies or objects
        // For example, if IsPlayerClose method depends on a player object, you can create and assign it here
        GameObject playerObject = new GameObject(); // Replace this with the actual player object
        this.npcBehavior.SetPlayerTransform(playerObject.transform);
    }

    /// <summary>
    /// This method is called after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // Clean up the created game objects
        Object.DestroyImmediate(this.npcGameObject);
    }

    /// <summary>
    /// This tests the GetKeyInstance method.
    /// </summary>
    [Test]
    public void GetKeyInstance_ReturnsNull_WhenKeyNotInstantiated()
    {
        // Arrange

        // Act
        var keyInstance = this.npcBehavior.GetKeyInstance();

        // Assert
        Assert.IsNull(keyInstance);
    }

    /// <summary>
    /// This tests the GetTextInstance method.
    /// </summary>
    [Test]
    public void IsPlayerClose_ReturnsTrue_WhenPlayerIsWithinDetectionRadius()
    {
        // Arrange
        this.npcBehavior.transform.position = Vector3.zero;
        Transform playerTransform = new GameObject().transform;
        playerTransform.position = new Vector3(2f, 0f, 0f);
        this.npcBehavior.SetPlayerTransform(playerTransform);

        // Act
        bool isPlayerClose = this.npcBehavior.IsPlayerClose();

        // Assert
        Assert.IsTrue(isPlayerClose);
    }

    /// <summary>
    /// This test tests the IsPlayerClose method.
    /// </summary>
    [Test]
    public void IsPlayerClose_ReturnsFalse_WhenPlayerIsOutsideDetectionRadius()
    {
        // Arrange
        this.npcBehavior.transform.position = Vector3.zero;
        Transform playerTransform = new GameObject().transform;
        playerTransform.position = new Vector3(5f, 0f, 0f);
        this.npcBehavior.SetPlayerTransform(playerTransform);

        // Act
        bool isPlayerClose = this.npcBehavior.IsPlayerClose();

        // Assert
        Assert.IsFalse(isPlayerClose);
    }

    /// <summary>
    /// This test tests the IsPlayerClose method.
    /// </summary>
    [Test]
    public void GetDetectionRadius_ReturnsCorrectValue()
    {
        // Arrange
        float expectedRadius = 2f;

        // Act
        float actualRadius = this.npcBehavior.GetDetectionRadius();

        // Assert
        Assert.AreEqual(expectedRadius, actualRadius);
    }

    /// <summary>
    /// This test tests the GetTextPrefab method.
    /// </summary>
    [Test]
    public void GetTextPrefab_ReturnsTextPrefab()
    {
        // Arrange
        GameObject expectedPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(expectedPrefab);

        // Act
        GameObject actualPrefab = this.npcBehavior.GetTextPrefab();

        // Assert
        Assert.AreEqual(expectedPrefab, actualPrefab);
    }

    /// <summary>
    /// This test tests the SetTextPrefab method.
    /// </summary>
    [Test]
    public void SetTextPrefab_SetsTextPrefab()
    {
        // Arrange
        GameObject expectedPrefab = new GameObject();

        // Act
        this.npcBehavior.SetTextPrefab(expectedPrefab);
        GameObject actualPrefab = this.npcBehavior.GetTextPrefab();

        // Assert
        Assert.AreEqual(expectedPrefab, actualPrefab);
    }

    /// <summary>
    /// This test tests the GetKeyPrefab method.
    /// </summary>
    [Test]
    public void GetKeyPrefab_ReturnsKeyPrefab()
    {
        // Arrange
        GameObject expectedPrefab = new GameObject();
        this.npcBehavior.SetKeyPrefab(expectedPrefab);

        // Act
        GameObject actualPrefab = this.npcBehavior.GetKeyPrefab();

        // Assert
        Assert.AreEqual(expectedPrefab, actualPrefab);
    }

    /// <summary>
    /// This test tests the SetKeyPrefab method.
    /// </summary>
    [Test]
    public void SetKeyPrefab_SetsKeyPrefab()
    {
        // Arrange
        GameObject expectedPrefab = new GameObject();

        // Act
        this.npcBehavior.SetKeyPrefab(expectedPrefab);
        GameObject actualPrefab = this.npcBehavior.GetKeyPrefab();

        // Assert
        Assert.AreEqual(expectedPrefab, actualPrefab);
    }

    /// <summary>
    /// This method tests the GetPlayerTransform method.
    /// </summary>
    [Test]
    public void GetPlayerTransform_ReturnsPlayerTransform()
    {
        // Arrange
        Transform expectedPlayerTransform = new GameObject().transform;
        this.npcBehavior.SetPlayerTransform(expectedPlayerTransform);

        // Act
        Transform actualPlayerTransform = this.npcBehavior.GetPlayerTransform();

        // Assert
        Assert.AreEqual(expectedPlayerTransform, actualPlayerTransform);
    }

    /// <summary>
    /// This method tests the SetPlayerTransform method.
    /// </summary>
    [Test]
    public void SetTextInstance_SetsTextInstance()
    {
        // Arrange
        GameObject expectedTextInstance = new GameObject();

        // Act
        this.npcBehavior.SetTextInstance(expectedTextInstance);
        var actualTextInstance = this.npcBehavior.GetTextInstance();

        // Assert
        Assert.AreEqual(expectedTextInstance, actualTextInstance);
    }

    /// <summary>
    /// This method tests the GetTextInstance method.
    /// </summary>
    [Test]
    public void GetTextInstance_ReturnsTextInstance()
    {
        // Arrange
        GameObject expectedTextInstance = new GameObject();
        this.npcBehavior.SetTextInstance(expectedTextInstance);

        // Act
        var actualTextInstance = this.npcBehavior.GetTextInstance();

        // Assert
        Assert.AreEqual(expectedTextInstance, actualTextInstance);
    }

    /// <summary>
    /// This tests when the text instance is null.
    /// </summary>
    [Test]
    public void DisplayText_InstantiatesTextPrefab_WhenTextInstanceIsNull()
    {
        // Arrange
        GameObject textPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(textPrefab);

        // Act
        this.npcBehavior.DisplayText("Here is the key for server 3.");
        var actualTextInstance = this.npcBehavior.GetTextInstance();

        // Assert
        Assert.IsNotNull(actualTextInstance);
        Assert.IsTrue(actualTextInstance.name.Contains(textPrefab.name));
    }

    /// <summary>
    /// This tests when the text instance is not null.
    /// </summary>
    [Test]
    public void DisplayText_UpdatesTextContent_WhenTextMeshProComponentExists()
    {
        // Arrange
        GameObject textPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(textPrefab);
        GameObject textInstance = new GameObject();
        textInstance.AddComponent<TextMeshProUGUI>();
        this.npcBehavior.SetTextInstance(textInstance);

        // Act
        this.npcBehavior.DisplayText("Here is the key for server 3.");
        var textMeshPro = textInstance.GetComponentInChildren<TextMeshProUGUI>();
        var actualTextContent = textMeshPro.text;

        // Assert
        Assert.AreEqual("Here is the key for server 3.", actualTextContent);
    }

    /// <summary>
    /// This tests the onEnable method.
    /// </summary>
    [Test]
    public void OnEnable_EnablesTextInstance_WhenTextInstanceExists()
    {
        // Arrange
        GameObject textPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(textPrefab);
        GameObject textInstance = new GameObject();
        textInstance.AddComponent<TextMeshProUGUI>();
        this.npcBehavior.SetTextInstance(textInstance);

        // Act
        this.npcBehavior.OnEnable();
        var actualTextInstance = textInstance.activeSelf;

        // Assert
        Assert.IsTrue(actualTextInstance);
    }

    /// <summary>
    /// Tests the GetTicketAccepted.
    /// </summary>
    [Test]
    public void GetTicketAccepted_ReturnsTicketAccepted()
    {
        // Arrange
        bool expectedTicketAccepted = false;

        // Act
        var actualTicketAccepted = this.npcBehavior.GetTicketAccepted();

        // Assert
        Assert.AreEqual(expectedTicketAccepted, actualTicketAccepted);
    }

    /// <summary>
    /// Tests the text display when the ticket is accepted.
    /// </summary>
    [Test]
    public void DisplayText_WhenTicketAcceptedIsTrue()
    {
        // Arrange
        GameObject textPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(textPrefab);
        GameObject textInstance = new GameObject();
        textInstance.AddComponent<TextMeshProUGUI>();
        this.npcBehavior.SetTextInstance(textInstance);
        this.npcBehavior.SetTicketAccepted(true);

        // Act
        this.npcBehavior.DisplayText("Thank you for returning the key!");
        var textMeshPro = textInstance.GetComponentInChildren<TextMeshProUGUI>();
        var actualTextContent = textMeshPro.text;

        // Assert
        Assert.AreEqual("Thank you for returning the key!", actualTextContent);
    }

    /// <summary>
    /// Tests the text display when the ticket is not accepted.
    /// </summary>
    [Test]
    public void DisplayText_WhenTicketAcceptedIsFalse_AndThankYouIsTrue()
    {
        // Arrange
        GameObject textPrefab = new GameObject();
        this.npcBehavior.SetTextPrefab(textPrefab);
        GameObject textInstance = new GameObject();
        textInstance.AddComponent<TextMeshProUGUI>();
        this.npcBehavior.SetTextInstance(textInstance);
        this.npcBehavior.SetTicketAccepted(false);

        // Act
        this.npcBehavior.DisplayText("Ticket not yet accepted!");
        var textMeshPro = textInstance.GetComponentInChildren<TextMeshProUGUI>();
        var actualTextContent = textMeshPro.text;

        // Assert
        Assert.AreEqual("Ticket not yet accepted!", actualTextContent);
    }
}
