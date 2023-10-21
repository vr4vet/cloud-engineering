// <copyright file="SkillUIManagerTest.cs" company="VR4VET">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Tablet;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Task;

/// <summary>
/// Test suite for the <see cref="SkillUIManager"/> class.
/// </summary>
public class SkillUIManagerTest
{
    private TaskHolder th;
    private GameObject itemPrefab;
    private Transform listTransform;
    private List<Skill> skillList;
    private Skill skill1;
    private Skill skill2;

    /// <summary>
    /// Creates the setup for the tests.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        //this.skill1 = ScriptableObject.CreateInstance<Skill>();
        //this.skill1.skillName = "Efficiency";
        //this.skill1.skillDescription = "Being target oriented.";
        //this.skill1.totalPoints = 15;

        //this.skill2 = ScriptableObject.CreateInstance<Skill>();
        //this.skill2.skillName = "Speed";
        //this.skill2.skillDescription = "Completing tasks quickly.";
        //this.skill2.totalPoints = 20;

        //this.skillList = new List<Skill>();
        //this.skillList.Add(this.skill1);
        //this.skillList.Add(this.skill2);

        //GameObject ob = new("TaskHolder");
        //this.th = ob.AddComponent<TaskHolder>();
        //Type type = typeof(TaskHolder);
        //FieldInfo fieldInfo = type.GetField("_skillList", BindingFlags.NonPublic | BindingFlags.Instance);
        //fieldInfo.SetValue(this.th, this.skillList);

        //this.itemPrefab = (GameObject)Resources.Load("UI/SkillPageItem");
        //this.listTransform = UnityEngine.Object.Instantiate(
        //                                                    (GameObject)Resources.Load("UI/SkillCanvas"),
        //                                                    Vector3.zero,
        //                                                    Quaternion.identity)
        //    .transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    /// <summary>
    /// Checks if the initialization of the skill list.
    /// </summary>
    [Test]
    public void CorrectFill()
    {
    //    GameObject skillCanvas = UnityEngine.Object.Instantiate((GameObject)Resources.Load("UI/SkillCanvas"));
    //    SkillUIManager skillUIManager = skillCanvas.AddComponent<SkillUIManager>();
    //    skillUIManager.ListItemHolder = this.listTransform;
    //    skillUIManager.ListItemPrefab = this.itemPrefab;
    //    skillUIManager.TaskHolder = this.th;

    //    skillUIManager.InstantiateList();

    //    for (int i = 0; i < skillUIManager.ListItemHolder.childCount; i++)
    //    {
    //        string points = this.skillList[i].GetAchievedPoeng().ToString() + " / " + this.skillList[i].GetTotalPoeng().ToString();
    //        Assert.AreEqual("SkillPageItem(Clone)", skillUIManager.ListItemHolder.GetChild(i).gameObject.name);
    //        Assert.AreEqual(this.skillList[i].GetFerdighetName(), skillUIManager.ListItemHolder.GetChild(i).Find("SkillName").GetComponentInChildren<Text>().text);
    //        Assert.AreEqual(points, skillUIManager.ListItemHolder.GetChild(i).Find("SkillPoints").GetComponentInChildren<Text>().text);
    //    }
    }
}
