using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobWork.APIControllers
{
    public class JobsController : ApiController
    {
        [Route("api/Jobs")]
        [HttpGet]
        public IHttpActionResult GetAllJobs()
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JobBoardEntities2"].ToString());
            var JobData = Dapper.SqlMapper.Query(conn, "Select * from Job_Board_Table");
            return Ok(new { JobData = JobData });

        }
        [Route("api/Jobs/{JobId}")]
        [HttpGet]
        public IHttpActionResult GetJobDetails(Decimal JobId)
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JobBoardEntities2"].ToString());
            var JobData = Dapper.SqlMapper.Query(conn, "Select * from Job_Board_Table where (Job_Id=@JobId)",
                new { Job_Id = JobId });
            return Ok(new { JobData = JobData });

        }
        [Route("api/Jobs/Add")]
        [HttpPost]
        public async Task<IHttpActionResult> AddJob([FromBody]dynamic postdata)
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JobBoardEntities2"].ToString());
            try
            {
                HttpRequestMessage request = this.Request;

                String cmd = " INSERT INTO Job_Board_Table(Job_Id,Job_Title,Job_Desc,Job_Type,Job_Skill,Job_Resp,Job_Start_Date,Job_End_Date,Job_Created_By,Job_Created_Date,Job_Updated_By,Job_Updated_Date,Job_Status)" +
                        " VALUES(@Job_Id,@Job_Title,@Job_Desc,@Job_Type,@Job_Skill,@Job_Resp,@Job_Start_Date,@Job_End_Date,@Job_Created_By,@Job_Created_Date,@Job_Updated_By,@Job_Updated_Date,@Job_Status)";
                if (postdata != null && postdata.discussion != null)
                {
                    Decimal Id = (Decimal)postdata.newjob.Job_Id;
                    String Title = (String)postdata.newjob.Job_Title;
                    String Desc = (String)postdata.discussion.Job_Desc;
                    String Type = (String)postdata.discussion.Job_Type;
                    String Skill = (String)postdata.discussion.Job_Skill;
                    String Resp = (String)postdata.discussion.Job_Resp;
                    String StartDate = (String)postdata.discussion.Job_Start_Date;
                    String EndDate = (String)postdata.discussion.Job_End_Date;
                    String CreatedBy = (String)postdata.discussion.Job_Created_By;
                    String UpdatedBy = (String)postdata.discussion.Job_Updated_By;

                    Dapper.SqlMapper.Execute(conn, cmd,
                    new
                    {
                        Job_Id = Id,
                        Job_Title =Title,
                        Job_Desc =Desc,
                        Job_Type =Type ,
                        Job_Skill = Skill ,
                        Job_Resp=Resp,
                        Job_Start_Date=StartDate,
                        Job_End_Date = EndDate ,
                        Job_Created_By=CreatedBy,
                        Job_Created_Date= DateTime.Now,
                        Job_Updated_By=UpdatedBy ,
                        Job_Updated_Date = DateTime.Now,
                        Job_Status = 0
                });
                }
                return Ok(new { result = "Success" });

            }
            catch (Exception ex)
            {
                return Ok(new { result = "Failure", message = ex.Message.ToString() });
            }

        }
        [Route("api/Jobs/Update/{JobId}")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateJob(Decimal JobId ,[FromBody]dynamic postdata)
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JobBoardEntities2"].ToString());
            try
            {
                HttpRequestMessage request = this.Request;

                String cmd = " UPDATE Job_Board_Table(Job_Title = @Job_Title ,Job_Desc=@Job_Desc,Job_Type=@Job_Type,Job_Skill =@Job_Skill,Job_Resp=@Job_Resp,Job_Start_Date =@Job_Start_Date,Job_End_Date=@Job_End_Date,Job_Created_By = @Job_Created_By,Job_Created_Date =@Job_Created_Date,Job_Updated_By =@Job_Updated_By,Job_Updated_Date =@Job_Updated_Date,Job_Status=@Job_Status) WHERE Job_Id =JobId";
                        
                if (postdata != null && postdata.discussion != null)
                {
                    
                    String Title = (String)postdata.newjob.Job_Title;
                    String Desc = (String)postdata.discussion.Job_Desc;
                    String Type = (String)postdata.discussion.Job_Type;
                    String Skill = (String)postdata.discussion.Job_Skill;
                    String Resp = (String)postdata.discussion.Job_Resp;
                    String StartDate = (String)postdata.discussion.Job_Start_Date;
                    String EndDate = (String)postdata.discussion.Job_End_Date;
                    String CreatedBy = (String)postdata.discussion.Job_Created_By;
                    String UpdatedBy = (String)postdata.discussion.Job_Updated_By;

                    Dapper.SqlMapper.Execute(conn, cmd,
                    new
                    {
                        Job_Id =JobId,
                        Job_Title = Title,
                        Job_Desc = Desc,
                        Job_Type = Type,
                        Job_Skill = Skill,
                        Job_Resp = Resp,
                        Job_Start_Date = StartDate,
                        Job_End_Date = EndDate,
                        Job_Created_By = CreatedBy,
                        Job_Created_Date = DateTime.Now,
                        Job_Updated_By = UpdatedBy,
                        Job_Updated_Date = DateTime.Now,
                        Job_Status = 0
                    });
                }
                return Ok(new { result = "Success" });

            }
            catch (Exception ex)
            {
                return Ok(new { result = "Failure", message = ex.Message.ToString() });
            }

        }
        [Route("api/Jobs/Delete/{JobId}")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteJob(Decimal JobId)
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JobBoardEntities2"].ToString());

            Int32 studentID = Dapper.SqlMapper.Query<Int32>(conn, " select top 1 UserID from dnnuser.users u where u.UserName='" + User.Identity.Name + "'").First();

            try
            {
                Dapper.SqlMapper.Execute(conn, " DELETE FROM Job_Board_Table Where Job_Id=" + JobId);

                return Ok(new { result = "Success" });
            }
            catch (Exception ex)
            {
                return Ok(new { result = "Failure", message = ex.Message.ToString() });
            }
        }
    }
}
