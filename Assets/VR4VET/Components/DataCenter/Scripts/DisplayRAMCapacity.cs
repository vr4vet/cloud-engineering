// <copyright file="DisplayRAMCapacity.cs" company="VR4VET">
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

using TMPro;
using UnityEngine;

/// <summary>
/// This class displayes a specific text on the RAM component.
/// </summary>
public class DisplayRAMCapacity : MonoBehaviour
{
    [SerializeField]
    private GameObject textPrefab;

    /// <summary>
    /// Displays RAM capacity on startup, if available.
    /// </summary>
    private void Start()
    {
        int capacity;
        if (this.GetComponent<RamComponent>() == null)
        {
            capacity = 0;
        }
        else
        {
            capacity = this.GetComponent<RamComponent>().Capacity;
        }

        this.textPrefab.GetComponent<TextMeshPro>().text = "" + capacity + "\nGiB";
    }
}
