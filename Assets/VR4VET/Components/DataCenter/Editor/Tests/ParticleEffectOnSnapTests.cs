// <copyright file="ParticleEffectOnSnapTests.cs" company="VR4VET">
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
using BNG;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Tests for the ParticleEffectOnSnap class.
/// </summary>
public class ParticleEffectOnSnapTests
{
    private ParticleEffectOnSnap particleEffectOnSnap;
    private GameObject particleEffectObject;
    private ParticleSystem particleEffect;

    /// <summary>
    /// This method is called before each test and creates a new game object with ParticleEffectOnSnap script attached.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Create a new game object with ParticleEffectOnSnap script attached
        this.particleEffectObject = new GameObject("ParticleEffectObject");
        this.particleEffectOnSnap = this.particleEffectObject.AddComponent<ParticleEffectOnSnap>();

        // Create a particle effect
        this.particleEffect = new GameObject("ParticleEffect").AddComponent<ParticleSystem>();
        this.particleEffectOnSnap.SetSnapParticleEffect(this.particleEffect);
    }

    /// <summary>
    /// This method is called after each test and destroys the game objects and components created for testing.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // Destroy the game objects and components created for testing
        Object.DestroyImmediate(this.particleEffectObject);
        Object.DestroyImmediate(this.particleEffect);
    }

    /// <summary>
    /// This method is used to test the GetParticleEffectDuration method.
    /// </summary>
    [Test]
    public void GetParticleEffectDuration_ReturnsCorrectValue()
    {
        // Arrange
        float expectedDuration = 5f;

        // Act
        float actualDuration = this.particleEffectOnSnap.GetParticleEffectDuration();

        // Assert
        Assert.AreEqual(expectedDuration, actualDuration);
    }

    /// <summary>
    /// This method is used to test the SetSnapParticleEffect method.
    /// </summary>
    [Test]
    public void GetSnapParticleEffect_ReturnsCorrectParticleSystem()
    {
        // Act
        ParticleSystem actualParticleEffect = this.particleEffectOnSnap.GetSnapParticleEffect();

        // Assert
        Assert.AreEqual(this.particleEffect, actualParticleEffect);
    }

    /// <summary>
    /// This method is used to test the SetSnapParticleEffect method.
    /// </summary>
    [Test]
    public void PlaySnapParticleEffect_PlaysParticleEffect()
    {
        // Arrange
        Grabbable grabbedObject = new GameObject("GrabbedObject").AddComponent<Grabbable>();
        this.particleEffect.Play(); // Ensure the particle effect is not already playing

        // Act
        this.particleEffectOnSnap.PlaySnapParticleEffect(grabbedObject);

        // Wait for a short duration
        // You can adjust this value based on the duration of the particle effect
        float waitDuration = 0.1f;
        float elapsedTime = 0f;
        while (elapsedTime < waitDuration)
        {
            elapsedTime += Time.deltaTime;
        }

        // Assert
        Assert.IsTrue(this.particleEffect.isPlaying);
    }

    /// <summary>
    /// This method is used to test the SetSnapParticleEffect method.
    /// </summary>
    [Test]
    public void SetParticleEffectDuration_ChangesDuration()
    {
        // Arrange
        float newDuration = 10f;

        // Act
        float result = this.particleEffectOnSnap.SetParticleEffectDuration(newDuration);

        // Assert
        Assert.AreEqual(newDuration, result);
        Assert.AreEqual(newDuration, this.particleEffectOnSnap.GetParticleEffectDuration());
    }

    /// <summary>
    /// This method is used to test the SetSnapParticleEffect method.
    /// </summary>
    [Test]
    public void PlayDetachParticleEffect_DoesNotThrowException()
    {
        // Arrange
        Grabbable detachedObject = new GameObject("DetachedObject").AddComponent<Grabbable>();

        // Act & Assert
        Assert.DoesNotThrow(() => this.particleEffectOnSnap.PlayDetachParticleEffect(detachedObject));
    }
}