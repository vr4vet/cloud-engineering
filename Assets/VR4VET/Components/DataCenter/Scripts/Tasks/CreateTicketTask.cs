// <copyright file="CreateTicketTask.cs" company="VR4VET">
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
    using Tablet;

    /// <summary>
    /// A ScriptableObject that represents the creating and filling in a ticket task.
    /// </summary>
    public class CreateTicketTask : Task
    {
        /// <summary>
        /// Gets the object combining this task to a target in the scene of the task.
        /// </summary>
        public TaskxTarget TaskxTarget { get; } = new TaskxTarget();

        /// <summary>
        /// Gets the activity of creating and filling in a ticket.
        /// </summary>
        public Activity CreateAndFillInTicketActivity { get; } = new Activity()
        {
            aktivitetName = "Use the computer in the control room to create a new ticket. " +
                "Fill it in with the information of the problem displayed on the computer screen.",
        };

        /// <summary>
        /// Gets the activity of waiting for the ticket to be approved.
        /// </summary>
        public Activity WaitForTicketApprovalActivity { get; } = new Activity()
        {
            aktivitetName = "Wait for ticket approval.",
        };

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            this.TaskxTarget.task = this;
            this._taskName = "Create and fill in ticket";
            this.description = "During your internship, problems with the server hardware will occur. " +
                "These problems can consist of hardware breaking or customers wanting an upgrade. " +
                "When a problem arises, you have to create and fill in a ticket with the changes that " +
                "have to be made to the server. The supervisor will then review your ticket to make " +
                "sure you will make the correct changes.";

            this.activities.Add(this.CreateAndFillInTicketActivity);
            this.activities.Add(this.WaitForTicketApprovalActivity);
        }
    }
}