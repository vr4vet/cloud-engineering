// <copyright file="DataCenterScenario.cs" company="VR4VET">
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

namespace DataCenter
{
    using System;
    using System.Linq;
    using DataCenter.Events;
    using Task;
    using Tablet;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// The main script of the data center scenario. It contains everything that
    /// is needed to run the scenario, including the tasks and activities.
    /// </summary>
    public class DataCenterScenario : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the data center scenario.
        /// </summary>
        private static DataCenterScenario instance;

        /// <summary>
        /// The task holder that is used to hold the tasks and activities.
        /// </summary>
        [SerializeField]
        private TaskHolder taskHolder;

        ///// <summary>
        ///// The tablet manager that contains the canvases of the tablet.
        ///// </summary>
        //[SerializeField]
        //private TabletManager tabletManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCenterScenario"/> class.
        /// Sets the singleton <see cref="Instance"/> to this instance.
        /// </summary>
        private DataCenterScenario()
        {
            instance = this;
        }

        /// <summary>
        /// Gets the singleton instance of the data center scenario. This is a
        /// singleton because it is needed in many places and it is easier to
        /// access it this way. Dependency inversion would be better, but due to
        /// how Unity works, it is not possible to use dependency inversion in
        /// the ScriptableObject Awake and OnEnable functions.
        /// </summary>
        public static DataCenterScenario Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("Datacenter scenario is null");
                    //throw new NullReferenceException("The data center scenario instance is null.");
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the event bus that is used to decouple the data center scenario
        /// from the tasks and activities.
        /// </summary>
        public EventBus EventBus { get; } = new();

        /// <summary>
        /// Gets the introduction task.
        /// </summary>
        //public IntroductionTask IntroductionTask { get; private set; }

        ///// <summary>
        ///// Gets the create ticket task.
        ///// </summary>
        //public CreateTicketTask CreateTicketTask { get; private set; }

        ///// <summary>
        ///// Gets the security clearance task.
        ///// </summary>
        //public SecurityClearanceTask SecurityClearanceTask { get; private set; }

        ///// <summary>
        ///// Gets the perform maintenance task.
        ///// </summary>
        //public PerformMaintenanceTask PerformMaintenanceTask { get; private set; }

        ///// <summary>
        ///// Gets the close ticket task.
        ///// </summary>
        //public CloseTicketTask CloseTicketTask { get; private set; }

        ///// <summary>
        ///// Adds all data center tasks to the task holder.
        ///// </summary>
        ///// <param name="taskHolder">The task holder to add the tasks to.</param>
        //public void AddTasksToTaskHolder(TaskHolder taskHolder)
        //{
        //    taskHolder._taskAndTargerts.Add(this.IntroductionTask.TaskxTarget);
        //    taskHolder._taskAndTargerts.Add(this.CreateTicketTask.TaskxTarget);
        //    taskHolder._taskAndTargerts.Add(this.SecurityClearanceTask.TaskxTarget);
        //    taskHolder._taskAndTargerts.Add(this.PerformMaintenanceTask.TaskxTarget);
        //    taskHolder._taskAndTargerts.Add(this.CloseTicketTask.TaskxTarget);
        //}

        ///// <summary>
        ///// Sets the completion state of an activity and updates the tablet.
        ///// </summary>
        ///// <param name="activity">The activity to set the completion state of.</param>
        ///// <param name="completed">The completion state to set.</param>
        //public void SetActivityCompleted(Activity activity, bool completed)
        //{
        //    activity.AktivitetIsCompeleted = completed;
        //    Debug.Log($"Activity \"{activity.aktivitetName}\" is completed: {completed}");

        //    if (this.tabletManager == null)
        //    {
        //        return;
        //    }

        //    this.tabletManager.aktiviteterPageCanvas.GetComponentsInChildren<Text>()
        //        .Where(t => t.text == activity.aktivitetName)
        //        .Select(t => t.transform.parent.parent.Find("Checkbox"))
        //        .ToList()
        //        .ForEach(transform =>
        //        {
        //            transform.Find("Checked").gameObject.SetActive(completed);
        //            transform.Find("UnChecked").gameObject.SetActive(!completed);
        //        });
        //}

        ///// <summary>
        ///// This method is called when the script instance is being loaded.
        ///// </summary>
        //private void Awake()
        //{
        //    this.IntroductionTask = ScriptableObject.CreateInstance<IntroductionTask>();
        //    this.CreateTicketTask = ScriptableObject.CreateInstance<CreateTicketTask>();
        //    this.SecurityClearanceTask = ScriptableObject.CreateInstance<SecurityClearanceTask>();
        //    this.PerformMaintenanceTask = ScriptableObject.CreateInstance<PerformMaintenanceTask>();
        //    this.CloseTicketTask = ScriptableObject.CreateInstance<CloseTicketTask>();

        //    if (this.taskHolder != null)
        //    {
        //        this.AddTasksToTaskHolder(this.taskHolder);
        //    }
        //}

        /// <summary>
        /// Start is called just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            this.GenerateHardwareProblem();
        }

        /// <summary>
        /// Generates a hardware problem and emits a HardwareProblemGeneratedEvent.
        /// </summary>
        private void GenerateHardwareProblem()
        {
            HardwareProblemGenerator generator = this.GetComponent<HardwareProblemGenerator>();
            HardwareProblem problem = generator.GenerateProblem(new System.Random(), generator.GetAllHardwareProblemTypes());

            Debug.Log($"Generated hardware problem: {problem.Message}");
            taskHolder.GetTask("Perform Maintenance").GetSubtask("Perform Maintenance").Description = "Now that you are prepared for the maintenance it is time to perform it! " + problem.Message + " Don't forget to turn the server off before maintenance on the wall in the server room.";
            taskHolder.GetTask("Perform Maintenance").Description = problem.Message;

            HardwareProblemGeneratedEvent problemGeneratedEvent = new(problem);
            this.EventBus.HardwareProblemGenerated?.Invoke(problemGeneratedEvent);
            this.EventBus.AfterHardwareProblemGenerated?.Invoke(problemGeneratedEvent);
            problem.ProblemType.AddEventListeners(this.EventBus);
        }
    }
}