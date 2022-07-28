using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Accessor class that reads in the data from the stored procedures created for Tasks
    /// </summary>
    public class TaskAccessor : ITaskAccessor
    {
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// A method for inserting a new task into the database
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int rowsAffected = 1</returns>
        /// 
        /// Vinayak Deshpande
        /// Update: 2022/02/28
        /// Description: Adding functionality to return taskID instead of rows affected.
        /// and then create volunteer need
        /// 
        /// Derrick Nagy
        /// Update: 2022/03/27
        /// 
        /// Description:
        /// Added check for DateTime.MinValue so that it would pass a sql null if true
        /// 
        public int InsertTasks(Tasks newTask, int numTotalVolunteers)
        {
            int taskID;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_new_task_by_eventID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = newTask.EventID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters["@Name"].Value = newTask.Name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            cmd.Parameters["@Description"].Value = newTask.Description;

            // check to see if due date is min value (unknown date)
            if (newTask.DueDate == DateTime.MinValue)
            {
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime);
                cmd.Parameters["@DueDate"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime);
                cmd.Parameters["@DueDate"].Value = newTask.DueDate;
            }

            cmd.Parameters.Add("@Priority", SqlDbType.Int);
            cmd.Parameters["@Priority"].Value = newTask.Priority;

            try
            {
                conn.Open();
                taskID = Convert.ToInt32(cmd.ExecuteScalar());
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            cmdText = "sp_insert_new_volunteer_need";
            var cmd2 = new SqlCommand(cmdText, conn);
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd2.Parameters["@TaskID"].Value = taskID;
            cmd2.Parameters.Add("@NumTotalVolunteers", SqlDbType.Int);
            cmd2.Parameters["@NumTotalVolunteers"].Value = numTotalVolunteers;

            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return taskID;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// A method for updating a task object in the database
        /// 
        /// Derrick Nagy
        /// Update: 2022/03/27
        /// 
        /// Description:
        /// Added check for DateTime.MinValue so that it would pass a sql null if true
        /// 
        /// </summary>
        /// <param name="oldTask"></param>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public int UpdateTasks(Tasks oldTask, Tasks newTask)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_task";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = newTask.EventID;
            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = oldTask.TaskID;
            cmd.Parameters.AddWithValue("@OldName", oldTask.Name);
            cmd.Parameters.AddWithValue("OldDescription", oldTask.Description);


            // check to see if due date is min value (unknown date)
            if (oldTask.DueDate == DateTime.MinValue)
            {
                cmd.Parameters.Add("@OldDueDate", SqlDbType.DateTime);
                cmd.Parameters["@OldDueDate"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.AddWithValue("OldDueDate", oldTask.DueDate);
                //cmd.Parameters.Add("@OldDueDate", SqlDbType.DateTime);
                //cmd.Parameters["@OldDueDate"].Value = newTask.DueDate;
            }



            cmd.Parameters.AddWithValue("@OldPriority", oldTask.Priority);
            cmd.Parameters.AddWithValue("OldActive", oldTask.Active);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewName"].Value = newTask.Name;
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters["@NewDescription"].Value = newTask.Description;

            // check to see if due date is min value (unknown date)
            if (newTask.DueDate == DateTime.MinValue)
            {
                cmd.Parameters.Add("@NewDueDate", SqlDbType.DateTime);
                cmd.Parameters["@NewDueDate"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@NewDueDate", SqlDbType.DateTime);
                cmd.Parameters["@NewDueDate"].Value = newTask.DueDate;
            }

            //cmd.Parameters.Add("@NewDueDate", SqlDbType.DateTime);
            //cmd.Parameters["@NewDueDate"].Value = newTask.DueDate;



            cmd.Parameters.Add("@NewPriority", SqlDbType.Int);
            cmd.Parameters["@NewPriority"].Value = newTask.Priority;
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newTask.Active;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// A method that returns a list of Priorities
        /// </summary>
        /// <returns>A list of Priorities (Only 3)</returns>
        public List<Priority> SelectAllPriorities()
        {
            List<Priority> priorities = new List<Priority>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_priorities";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        priorities.Add(new Priority()
                        {
                            PriorityID = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return priorities;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Select method that grabs a list of all tasks for an event
        /// </summary>
        /// <returns>List Tasks</returns>
        public List<TasksVM> SelectAllActiveTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_tasks_by_eventID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TasksVM()
                        {
                            TaskID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DueDate = reader.GetDateTime(3),
                            Priority = reader.GetInt32(4),
                            TaskPriority = reader.GetString(5),
                            TaskEventName = reader.GetString(6),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return tasks;
        }

        /// Jace Pettinger
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Insert new Task Assignment into task assignment table with a TaskID
        /// </summary>
        /// <returns>The new TaskAssignment ID</returns>
        public int InsertNewTaskAssignmentByTaskID(int taskID)
        {
            int taskAssignmentID;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_new_taskAssignment_by_taskID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@taskID", SqlDbType.Int);
            cmd.Parameters["@taskID"].Value = taskID;

            try
            {
                conn.Open();
                taskAssignmentID = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return taskAssignmentID;
        }



        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Descripiton:
        /// A method that returns true if the User ID given is allowed to edit/delete tasks
        /// (Should only be the Event Manager and Event Planner)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Boolean value</returns>
        public bool UserCanEditDeleteTask(int userID)
        {
            bool result = false;
            List<Role> roles = new List<Role>();

            var conn = DBConnection.GetConnection();
            var cmdTxt = "sp_select_user_roles_from_event_users_table";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            foreach (Role role in roles)
            {
                if (role.RoleID == "Event Planner" || role.RoleID == "Event Manager")
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select method that gets a list of taskAssginments for a task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>List Tasks</returns>
        public List<TaskAssignmentVM> SelectTaskAssignmentsByTaskID(int taskID)
        {
            List<TaskAssignmentVM> taskAssignments = new List<TaskAssignmentVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_task_assignments_by_task_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        taskAssignments.Add(new TaskAssignmentVM()
                        {
                            TaskAssignmentID = reader.GetInt32(0),
                            DateAssigned = DateTime.Parse(reader[1].ToString()),
                            TaskID = taskID,
                            UserID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2) ,
                            RoleID = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Name = reader.GetString(4) + " " + reader.GetString(5)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskAssignments;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Update a Task Assignment with a volunteer UserID
        /// </summary>
        /// <returns>number of rows affected</returns>
        public int UpdateTaskAssignmentWithUserID(int taskAssignmentID, int userID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_task_assignment_with_userID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskAssignmentID", SqlDbType.Int);
            cmd.Parameters["@TaskAssignmentID"].Value = taskAssignmentID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Deletes a Task from the Tasks table with corresponding TaskID
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>True if task is removed</returns>
        public bool DeleteTaskByTaskID(int taskID)
        {
            bool deleted = false;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_task_by_taskID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;

            try
            {
                conn.Open();
                deleted = (1 == cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return deleted;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/05
        /// 
        /// Description: Returns all tasks tied to an event
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public List<TasksVM> SelectAllTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_tasks_by_eventID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TasksVM()
                        {
                            TaskID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DueDate = (reader.IsDBNull(3)) ? new DateTime() : reader.GetDateTime(3),
                            Priority = reader.GetInt32(4),
                            TaskPriority = reader.GetString(5),
                            TaskEventName = reader.GetString(6),
                            Active = reader.GetBoolean(7)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return tasks;
        }
    }
}
