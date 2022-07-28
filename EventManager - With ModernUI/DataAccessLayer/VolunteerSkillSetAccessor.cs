using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created 2022/03/08
    /// 
    /// Description
    /// Accessor for Volunteer Skill Set
    /// </summary>
    public class VolunteerSkillSetAccessor : IVolunteerSkillSetAccessor
    {
        /// <summary>
        /// Austin Timmerman
        /// Created 2022/03/08
        /// 
        /// Description
        /// Method to select the skills that match a volunteerID passed to it
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>List of VolunteerSkillSet objects</returns>
        public List<VolunteerSkillSet> SelectSkillSetByVolunteerID(int volunteerID)
        {
            List<VolunteerSkillSet> volunteerSkills = new List<VolunteerSkillSet>();

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_skill_set_by_volunteerID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);

            cmd.Parameters["@VolunteerID"].Value = volunteerID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteerSkills.Add(new VolunteerSkillSet()
                        {
                            VolunteerID = volunteerID,
                            SkillSetID = reader.GetString(0),
                            SkillSetDescription = reader.GetString(1)
                        });

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return volunteerSkills;
        }
    }
}
