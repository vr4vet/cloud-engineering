//// <copyright file="CloseTicketTask.cs" company="VR4VET">
//// MIT License
////
//// Copyright (c) 2023 VR4VET
////
//// Permission is hereby granted, free of charge, to any person obtaining a copy
//// of this software and associated documentation files (the "Software"), to deal
//// in the Software without restriction, including without limitation the rights
//// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//// copies of the Software, and to permit persons to whom the Software is
//// furnished to do so, subject to the following conditions:
////
//// The above copyright notice and this permission notice shall be included in all
//// copies or substantial portions of the Software.
////
//// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//// SOFTWARE.
//// </copyright>

//namespace DataCenter.Tasks
//{
//    using Tablet;

//    /// <summary>
//    /// A ScriptableObject that represents the updating and closing a ticket task.
//    /// </summary>
//    public class CloseTicketTask : Task
//    {
//        /// <summary>
//        /// Gets the object combining this task to a target in the scene of the task.
//        /// </summary>
//        public TaskxTarget TaskxTarget { get; } = new TaskxTarget();

//        /// <summary>
//        /// Gets the activity of returning the server cabinet door keys to the security person.
//        /// </summary>
//        public Activity ReturnKeys { get; } = new Activity()
//        {
//            aktivitetName = "Return the server cabinet door keys to the security person.",
//        };

//        /// <summary>
//        /// Gets the activity of returning to the control room.
//        /// </summary>
//        public Activity ReturnToControlRoomActivity { get; } = new Activity()
//        {
//            aktivitetName = "Return to the control room.",
//        };

//        /// <summary>
//        /// Gets the activity of updating and closing a ticket.
//        /// </summary>
//        public Activity UpdateAndCloseTicketActivity { get; } = new Activity()
//        {
//            aktivitetName = "Use the computer in the control room to update the ticket with the " +
//                "maintenance you have performed.",
//        };

//        /// <summary>
//        /// This method is called when the script instance is being loaded.
//        /// </summary>
//        private void Awake()
//        {
//            this.TaskxTarget.task = this;
//            this._taskName = "Update and close ticket";
//            this.description = "After having performed maintenance, you have to document the " +
//                "everything you have done.";

//            this.activities.Add(this.ReturnKeys);
//            this.activities.Add(this.ReturnToControlRoomActivity);
//            this.activities.Add(this.UpdateAndCloseTicketActivity);
//        }
//    }
//}