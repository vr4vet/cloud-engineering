// <copyright file="ParticleEffectOnSnap.cs" company="VR4VET">
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
/// The ParticleEffectOnSnap class is used to play a particle effect when an object is snapped to a snap zone.
/// </summary>
public class ParticleEffectOnSnap : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem snapParticleEffect;

    private float particleEffectDuration = 5f; // Duration in seconds

    /// <summary>
    /// This method is used to get the particle effect duration.
    /// </summary>
    /// <returns>It returns the particleEfectDuration.</returns>
    public float GetParticleEffectDuration()
    {
        return this.particleEffectDuration;
    }

    /// <summary>
    /// This method is used to set the particle effect duration.
    /// </summary>
    /// <param name="duration"> This is the duration of the effect.</param>
    /// <returns> It returns the duration of the effect.</returns>
    public float SetParticleEffectDuration(float duration)
    {
        return this.particleEffectDuration = duration;
    }

    /// <summary>
    /// This method is used to get the particle effect.
    /// </summary>
    /// <returns>It returns the snapParticleEffect.</returns>
    public ParticleSystem GetSnapParticleEffect()
    {
        return this.snapParticleEffect;
    }

    /// <summary>
    /// This method is used to set the particle effect.
    /// </summary>
    /// <param name="particleEffect"> Desired particleEffect.</param>
    /// <returns>It returns the set particleSystem. </returns>
    public ParticleSystem SetSnapParticleEffect(ParticleSystem particleEffect)
    {
        return this.snapParticleEffect = particleEffect;
    }

    /// <summary>
    /// The PlaySnapParticleEffect method is used to play the snap particle effect.
    /// </summary>
    /// <param name="grabbedObject"> grabbedObject is the object that is grabbed for example a RAM stick. </param>
    public void PlaySnapParticleEffect(Grabbable grabbedObject)
    {
        if (this.snapParticleEffect != null)
        {
            this.snapParticleEffect.transform.position = this.transform.position; // Set the position of the particle system
            this.snapParticleEffect.Play();
            this.StartCoroutine(this.StopSnapParticleEffectCoroutine());
        }
    }

    /// <summary>
    /// The PlayDetachParticleEffect method is used to play the detach particle effect.
    /// </summary>
    /// <param name="detachedObject"> detachedObject is the object that is detached. </param>
    public void PlayDetachParticleEffect(Grabbable detachedObject)
    {
        // Implement the detach particle effect logic here
    }

    private System.Collections.IEnumerator StopSnapParticleEffectCoroutine()
    {
        yield return new WaitForSeconds(this.particleEffectDuration);
        if (this.snapParticleEffect != null && this.snapParticleEffect.isPlaying)
        {
            this.snapParticleEffect.Stop();
        }
    }
}
