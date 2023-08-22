// <copyright file="FaceCameraTest.cs" company="VR4VET">
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
/// Test suite for the <see cref="FaceCamera"/> class.
/// </summary>
public class FaceCameraTest : MonoBehaviour
{
    /// <summary>
    /// A test to see if the rest of intro NPC works as intended.
    /// </summary>
    [Test]
    public void TestFaceCamera()
    {
        GameObject attachedObject = new GameObject();
        attachedObject.AddComponent<FaceCamera>();
        Transform trans = new GameObject().transform;
        attachedObject.GetComponent<FaceCamera>().MLookAt = trans;

        attachedObject.GetComponent<FaceCamera>().Start();
        attachedObject.GetComponent<FaceCamera>().FixedUpdate();

        Assert.NotNull(attachedObject);
        Assert.NotNull(attachedObject.GetComponent<FaceCamera>().MLookAt);
    }
}
