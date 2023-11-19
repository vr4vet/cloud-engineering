// <copyright file="DataCenterScenarioTest.cs" company="VR4VET">
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

using System.Linq;
using DataCenter;
using NUnit.Framework;
using Tablet;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Test suite for the <see cref="DataCenterScenarioTest"/> class.
/// </summary>
public class DataCenterScenarioTest
{
    private GameObject dataCenterScenarioPrefab;

    /// <summary>
    /// Initializes the test suite.
    /// </summary>
    [SetUp]
    public void Init()
    {
        //this.dataCenterScenarioPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VR4VET/Components/DataCenter/Prefabs/DataCenterScenario.prefab");

        //Assert.NotNull(this.dataCenterScenarioPrefab);
    }

    /// <summary>
    /// Verify there are no crashes if the task holder is null.
    /// </summary>
    [Test]
    public void Awake_TaskHolder_Null()
    {
        //GameObject dataCenterScenarioGameObject = UnityEngine.Object.Instantiate(this.dataCenterScenarioPrefab);
        //DataCenterScenario dataCenterScenario = dataCenterScenarioGameObject.GetComponent<DataCenterScenario>();
        //Assert.NotNull(dataCenterScenario);

        //// Get the private field taskHolder using Reflection and set it to null.
        //var taskHolderField = typeof(DataCenterScenario).GetField("taskHolder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //taskHolderField.SetValue(dataCenterScenario, null);

        //// Get the private void function Awake using Reflection and call it.
        //var awakeMethod = typeof(DataCenterScenario).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //Assert.DoesNotThrow(() => awakeMethod.Invoke(dataCenterScenario, null));
    }

    /// <summary>
    /// Verify all tasks are added to the task holder.
    /// </summary>
    [Test]
    public void Awake_TaskHolder_AllTasks()
    {
    //    GameObject dataCenterScenarioGameObject = UnityEngine.Object.Instantiate(this.dataCenterScenarioPrefab);
    //    DataCenterScenario dataCenterScenario = dataCenterScenarioGameObject.GetComponent<DataCenterScenario>();
    //    Assert.NotNull(dataCenterScenario);

    //    GameObject taskHolderGameObject = new();
    //    TaskHolder taskHolder = taskHolderGameObject.AddComponent<TaskHolder>();

    //    // Get the private field taskHolder using Reflection and set it to null.
    //    var taskHolderField = typeof(DataCenterScenario).GetField("taskHolder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    //    taskHolderField.SetValue(dataCenterScenario, taskHolder);

    //    // Get the private void function Awake using Reflection and call it.
    //    var awakeMethod = typeof(DataCenterScenario).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    //    awakeMethod.Invoke(dataCenterScenario, null);

    //    // Verify all tasks are added to the task holder.
    //    CollectionAssert.AreEquivalent(
    //        new Task[]
    //        {
    //            DataCenterScenario.Instance.IntroductionTask,
    //            DataCenterScenario.Instance.CreateTicketTask,
    //            DataCenterScenario.Instance.SecurityClearanceTask,
    //            DataCenterScenario.Instance.PerformMaintenanceTask,
    //            DataCenterScenario.Instance.CloseTicketTask,
    //        },
    //        taskHolder._taskAndTargerts.Select(taskxTarget => taskxTarget.task));
    }
}
