// <copyright file="PerformMaintenanceTask.cs" company="VR4VET">
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

namespace DataCenter.Tasks
{
    using DataCenter.Events;
    using Tablet;

    /// <summary>
    /// A ScriptableObject that represents the perform maintenance task.
    /// </summary>
    public class PerformMaintenanceTask : Task
    {
        /// <summary>
        /// Gets the object combining this task to a target in the scene of the task.
        /// </summary>
        public TaskxTarget TaskxTarget { get; } = new TaskxTarget();

        /// <summary>
        /// Gets the activity of entering the server room.
        /// </summary>
        public Activity EnterServerRoomActivity { get; } = new Activity()
        {
            aktivitetName = "Enter the server room.",
        };

        /// <summary>
        /// Gets the activity of locating the server.
        /// </summary>
        public Activity LocateServerActivity { get; } = new Activity()
        {
            aktivitetName = "Locate the server requiring maintenance.",
        };

        /// <summary>
        /// Gets the activity of shutting the server off.
        /// </summary>
        public Activity ShutServerOff { get; } = new Activity()
        {
            aktivitetName = "Shut the server off.",
        };

        /// <summary>
        /// Gets the activity of opening the server cabinet door.
        /// </summary>
        public Activity GetKeyToCabinet { get;  } = new Activity()
        {
            aktivitetName = "Get the key to the server cabinet.",
        };

        /// <summary>
        /// Gets the activity of unlocking and opening the server cabinet door.
        /// </summary>
        public Activity UnlockAndOpenCabinetDoorActivity { get; } = new Activity()
        {
            aktivitetName = "Unlock and open the server cabinet door.",
        };

        /// <summary>
        /// Gets the activity of sliding back the server drawer.
        /// </summary>
        public Activity SlideBackServerDrawerActivity { get; } = new Activity()
        {
            aktivitetName = "Slide back the server drawer.",
        };

        /// <summary>
        /// Gets the activity of pushing back the server drawer.
        /// </summary>
        public Activity PushBackServerDrawerActivity { get; } = new Activity()
        {
            aktivitetName = "Push back the server drawer.",
        };

        /// <summary>
        /// Gets the activity of closing and locking the server cabinet door.
        /// </summary>
        public Activity CloseAndLockCabinetDoorActivity { get; } = new Activity()
        {
            aktivitetName = "Close and lock the cabinet door.",
        };

        /// <summary>
        /// Gets the activity of setting the server on.
        /// </summary>
        public Activity TurnServerOn { get; } = new Activity()
        {
            aktivitetName = "Turn the server on.",
        };

        /// <summary>
        /// Gets the activity of returning the cabinet key.
        /// </summary>
        public Activity ReturnCabinetKey { get; } = new Activity()
        {
            aktivitetName = "Return the cabinet key.",
        };

        /// <summary>
        /// Gets the index at which the maintenance activities should be inserted.
        /// </summary>
        public int MaintenanceActivityInsertionIndex => this.activities.IndexOf(this.TurnServerOn);

        /// <summary>
        /// Gets the number of maintenance activities.
        /// </summary>
        public int MaintenanceActivityCount { get; private set; }

        /// <summary>
        /// Inserts the activities of the given hardware problem into the task. Multiple activities can be inserted.
        /// </summary>
        /// <param name="hardwareProblem">The hardware problem to insert the activities of.</param>
        public void InsertHardwareProblemActivities(HardwareProblem hardwareProblem)
        {
            this.activities.InsertRange(this.MaintenanceActivityInsertionIndex, hardwareProblem.ProblemType.Activities);
        }

        /// <summary>
        /// Method for calculating the number of maintenance activities in the task.
        /// </summary>
        /// <param name="hardwareProblem"> The current hardwareproblem. </param>
        /// <returns> The number of tasks. </returns>
        public int GetHardwareActionCount(HardwareProblem hardwareProblem)
        {
            return hardwareProblem.ProblemType.Activities.Count - 1;
        }

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            this.TaskxTarget.task = this;
            this._taskName = "Perform maintenance";
            this.description = "In order to resolve the problem described in the ticket, " +
                "you will now perform maintenance to the server.";

            // this.activities.Add(this.EnterServerRoomActivity);
            // this.activities.Add(this.LocateServerActivity);
            this.activities.Add(this.ShutServerOff);
            this.activities.Add(this.GetKeyToCabinet);

            // this.activities.Add(this.UnlockAndOpenCabinetDoorActivity);
            // this.activities.Add(this.SlideBackServerDrawerActivity);
            // this.activities.Add(this.PushBackServerDrawerActivity);
            // this.activities.Add(this.CloseAndLockCabinetDoorActivity);
            // this.activities.Add(this.ReturnCabinetKey);
            this.activities.Add(this.TurnServerOn);
        }

        /// <summary>
        /// This function is called when the task is created.
        /// </summary>
        private void OnEnable()
        {
            DataCenterScenario.Instance.EventBus.HardwareProblemGenerated += this.OnHardwareProblemGenerated;
        }

        /// <summary>
        /// This function is called when the task will be destroyed.
        /// </summary>
        private void OnDestroy()
        {
            DataCenterScenario.Instance.EventBus.HardwareProblemGenerated -= this.OnHardwareProblemGenerated;
        }

        /// <summary>
        /// This method is called when a hardware problem is generated.
        /// </summary>
        /// <param name="e">The event.</param>
        private void OnHardwareProblemGenerated(HardwareProblemGeneratedEvent e)
        {
            this.InsertHardwareProblemActivities(e.HardwareProblem);
            this.MaintenanceActivityCount = this.GetHardwareActionCount(e.HardwareProblem);
            this.TaskxTarget.target = e.HardwareProblem.Location.Server.gameObject;
        }
    }
}