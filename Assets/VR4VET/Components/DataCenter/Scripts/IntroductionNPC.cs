// <copyright file="IntroductionNPC.cs" company="VR4VET">
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
/// Class containing the logic the introduction NPC.
/// </summary>
public class IntroductionNPC : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 3f;
    [SerializeField]
    private GameObject textPrefab;
    [SerializeField]
    [TextArea(5, 20)]
    private string pageOne = "First page";
    [SerializeField]
    private float pageOneFontSize = 0.25f;
    [SerializeField]
    [TextArea(5, 20)]
    private string pageTwo = "Second page";
    [SerializeField]
    private float pageTwoFontSize = 0.5f;

    [SerializeField]
    private Transform playerTransform;

    /// <summary>
    /// Gets or Sets TextPrefab.
    /// </summary>
    public GameObject TextPrefab { get => this.textPrefab; set => this.textPrefab = value; }

    /// <summary>
    /// Gets or Sets DetectionRadius.
    /// </summary>
    public float DetectionRadius { get => this.detectionRadius; set => this.detectionRadius = value; }

    /// <summary>
    /// This method changes the displayed text to one of the assigned strings.
    /// </summary>
    public void NextPage()
    {
        // Checks if it is currently displaying page one, and moves onto page two
        if (this.textPrefab.GetComponent<TextMeshProUGUI>().text.Equals(this.pageOne))
        {
            this.textPrefab.GetComponent<TextMeshProUGUI>().fontSize = this.pageTwoFontSize;
            this.textPrefab.GetComponent<TextMeshProUGUI>().text = this.pageTwo;
        }

        // If it is displaying the final page, it moves to the first page (not default page)
        else
        {
            this.textPrefab.GetComponent<TextMeshProUGUI>().fontSize = this.pageOneFontSize;
            this.textPrefab.GetComponent<TextMeshProUGUI>().text = this.pageOne;
        }
    }

    /// <summary>
    /// This method finds the player and assigns it.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// This method checks if it should enable or disable the canvas.
    /// </summary>
    public void FixedUpdate()
    {
        if (this.IsPlayerClose())
        {
            this.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            this.GetComponent<Canvas>().enabled = false;
        }
    }

    /// <summary>
    /// This method checks if the player is within the detection radius.
    /// </summary>
    /// <returns>It returns a bool showing if the player is in range or not.</returns>
    public bool IsPlayerClose()
    {
        float distance = Vector3.Distance(this.transform.position, this.playerTransform.position);
        return distance <= this.detectionRadius;
    }
}
