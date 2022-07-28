using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using LogicLayer;
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using System.Collections.Generic;

namespace LogicLayerTests
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Test Class to test the Tasks Logic Layer methods
    /// </summary>
    [TestClass]
    public class TaskManagerTests
    {
        private ITaskManager _taskManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _taskManager = new TaskManager(new TaskAccessorFakes());
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Test that returns true if successful
        /// </summary>
        [TestMethod]
        public void TestInsertTaskReturnsTrueIfCreated()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean Something",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today,
                Priority = 2
            };

            int expectedResult = 999995;
            int actualResult;

            //act
            actualResult = _taskManager.AddTask(task, 1);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Name is over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfNameIsLongerThan50Characters()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Cleannnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn Something",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            _taskManager.AddTask(task, 1);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Description is over 255 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDescriptionIsLongerThan255Characters()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me but lonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnger",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            _taskManager.AddTask(task, 1);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous day
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousDay()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddDays(-1),
                Priority = 2
            };

            //act
            _taskManager.AddTask(task, 1);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous hour
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousHour()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddHours(-1),
                Priority = 2
            };

            //act
            _taskManager.AddTask(task, 1);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous minutes
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousMinute()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddMinutes(-1),
                Priority = 2
            };

            //act
            _taskManager.AddTask(task, 1);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Test that checks if the correct number of items in a list of tasks will be returned
        /// Should be four according to TaskAccessorFakes
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDReturnsCorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 1000000;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _taskManager.RetrieveAllActiveTasksByEventID(eventID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Test that checks if the method returned the correct number of list items
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDFailsWithIncorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 100000;
            const int expectedCount = 2;
            int actualCount;


            // act
            actualCount = _taskManager.RetrieveAllActiveTasksByEventID(eventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description: 
        /// Test that fails because an incorrect list was passed
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDFailsWithIncorrectListBeingReturned()
        {
            // arrange
            const int badEventID = 100001;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _taskManager.RetrieveAllActiveTasksByEventID(badEventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test that returns true if EditTask() ran successfully
        /// </summary>
        [TestMethod]
        public void TestUpdateTaskReturnsTrueIfSuccessful()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                EventID = 1000000,
                TaskID = 999999,
                TaskEventName = "Test Event 1",
                Name = "Mop",
                Description = "Mop up spilled drink",
                DueDate = DateTime.Today,
                Priority = 3,
                TaskPriority = "High",
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                EventID = 1000000,
                TaskID = 999999,
                TaskEventName = "Test Event 27",
                Name = "Mop",
                Description = "Test",
                DueDate = DateTime.Today,
                Priority = 1,
                TaskPriority = "Low",
                Active = false
            };
            bool expectedResult = true;
            bool actualResult;

            // act
            actualResult = _taskManager.EditTask(oldTask, newTask);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails because Name holds an empty value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateThrowsExceptionWithEmptyNameValue()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails be cause the value of name has more than
        /// 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateThrowsExceptionIfNameHasMoreThan50Characters()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails with an empty Description value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTaskThrowsExceptionWithEmptyDescriptionValue()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "test",
                Description = "",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails with a Description value that has more than
        /// 250 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTaskThrowExceptionIfDescriptionHasMoreThan255Characters()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "test",
                Description = "Loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong sweeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeep ovvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvveeeeeeeeeeeeeer heeeeeere",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails because DueDate value is set to the previous day
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTaskThrowsExceptionIfDueDateIsThePreviousDay()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Test",
                Description = "More test",
                DueDate = DateTime.Today.AddDays(-1),
                Priority = 2,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails because DueDate value is set to the previous hour
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTaskThrowsExceptionIfDueDateIsThePreviousHour()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Test",
                Description = "More test",
                DueDate = DateTime.Today.AddHours(-1),
                Priority = 2,
                Active = true
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that fails because DueDate value is set to the previous minute
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTaskThrowsExceptionIfDueDateIsPreviousMinute()
        {
            // arrange
            TasksVM oldTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                Active = true
            };

            TasksVM newTask = new TasksVM()
            {
                TaskID = 999997,
                Name = "Test",
                Description = "More test",
                DueDate = DateTime.Today.AddMinutes(-1),
                Priority = 2,
                Active = false
            };

            // act
            _taskManager.EditTask(oldTask, newTask);

            // assert
            // exception checking
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test that returns a list of the correct size
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskAssignmentsByTaskIDReturnsCorrectList()
        {
            // arrange
            const int taskID = 999999;
            List<TaskAssignmentVM> expectedList = new List<TaskAssignmentVM>();
            expectedList.Add(new TaskAssignmentVM()
            {
                TaskAssignmentID = 1,
                DateAssigned = new DateTime(2022, 1, 20),
                TaskID = 999999,
                RoleID = "Event Planner",
                UserID = 999999,
                Name = "Tess Data"
            });
            expectedList.Add(new TaskAssignmentVM()
            {
                TaskAssignmentID = 2,
                DateAssigned = new DateTime(2022, 2, 3),
                TaskID = 999999,
                RoleID = "Volunteer",
                UserID = 999998,
                Name = "Tess Data"
            });

            List<TaskAssignmentVM> actualList;

            // act
            
            actualList = _taskManager.RetrieveTaskAssignmentsByTaskID(taskID);

            // assert
            ReflectionHelper.AssertReflectiveEqualsEnumerable(expectedList, actualList);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Test that returns a list of the correct size
        /// </summary>
        [TestMethod]
        public void TestRetrieveTaskAssignmentsByTaskIDReturnsEmptyListWithInvalidTaskID()
        {
            // arrange
            const int taskID = 1;
            const int expectedCount = 0;
            int actualCount;

            // act
            actualCount = _taskManager.RetrieveTaskAssignmentsByTaskID(taskID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Descripiton:
        /// Test that returns true when the user has the correct role
        /// </summary>
        [TestMethod]
        public void TestUserCanAddEditOrDeleteTaskReturnsTrue()
        {
            // arrange
            const int userID = 100000;
            const bool expectedResult = true;
            bool actualResult;

            // act
            actualResult = _taskManager.UserCanEditDeleteTask(userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/31
        /// 
        /// Description: 
        /// Test that adding a task assignment will return the task assignment id
        /// </summary>
        [TestMethod]
        public void TestInsertNewTaskAssignmentReturnsCorrectTaskAssignmentID()
        {
            // arrange
            const int testTaskID = 999996;
            const int expectedTaskAssignmentID = 999996;
            int actualTaskAssignmentID;

            // act
            actualTaskAssignmentID = _taskManager.AddTaskAssignment(testTaskID);

            // assert
            Assert.AreEqual(expectedTaskAssignmentID, actualTaskAssignmentID);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Test that returns false because the user does not have correct role
        /// </summary>
        [TestMethod]
        public void TestUserCanAddEditOrDeleteTaskReturnsFalse()
        {
            // arrange
            const int userID = 100001;
            const bool expectedResult = false;
            bool actualResult;

            // act
            actualResult = _taskManager.UserCanEditDeleteTask(userID);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Test the returns true with valid TaskID
        /// </summary>
        [TestMethod]
        public void TestDeleteTaskByTaskIDReturnsTrue()
        {
            // arrange
            const int taskID = 999999;
            const bool expected = true;
            bool actual;

            // act
            actual = _taskManager.RemoveTaskByTaskID(taskID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// Jace Pettinger
        /// Created: 2022/01/31
        /// 
        /// Description: 
        /// Test that volunteer can be successfully added to a task assignment
        /// </summary>
        [TestMethod]
        public void TestAddVolunteerToTaskAssignmentReturnsTrue()
        {
            // arrange
            const int taskAssignmentID = 999997;
            const int userID = 999999;
            const bool expected = true;
            bool actual;

            // act
            actual = _taskManager.AddVolunteerToTaskAssignment(taskAssignmentID, userID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Test that returns with invalid TaskID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteTaskByTaskIDReturnsFalse()
        {
            // arrange
            const int taskID = 1000000;

            // act
            _taskManager.RemoveTaskByTaskID(taskID);

            // assert
            // Exception checking

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Test that checks if the correct number of items in a list of tasks will be returned
        /// Should be four according to TaskAccessorFakes
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllTasksByEventIDReturnsCorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 1000000;
            const int expectedCount = 4;
            int actualCount;

            // act
            actualCount = _taskManager.RetrieveAllTasksByEventID(eventID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Test that checks if the method returned the correct number of list items
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllTasksByEventIDFailsWithIncorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 100000;
            const int expectedCount = 2;
            int actualCount;


            // act
            actualCount = _taskManager.RetrieveAllTasksByEventID(eventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Test that fails because an incorrect list was passed
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllTasksByEventIDFailsWithIncorrectListBeingReturned()
        {
            // arrange
            const int badEventID = 100001;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _taskManager.RetrieveAllTasksByEventID(badEventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }
    }
}
