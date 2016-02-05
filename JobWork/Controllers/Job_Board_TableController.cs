using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JobWork;

namespace JobWork.Controllers
{
    public class Job_Board_TableController : ApiController
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: api/Job_Board_Table
        public IQueryable<Job_Board_Table> GetJob_Board_Table()
        {
            return db.Job_Board_Table;
        }

        // GET: api/Job_Board_Table/5
        [ResponseType(typeof(Job_Board_Table))]
        public IHttpActionResult GetJob_Board_Table(decimal id)
        {
            Job_Board_Table job_Board_Table = db.Job_Board_Table.Find(id);
            if (job_Board_Table == null)
            {
                return NotFound();
            }

            return Ok(job_Board_Table);
        }

        // PUT: api/Job_Board_Table/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob_Board_Table(decimal id, Job_Board_Table job_Board_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job_Board_Table.Job_Id)
            {
                return BadRequest();
            }

            db.Entry(job_Board_Table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Job_Board_TableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Job_Board_Table
        
        [ResponseType(typeof(Job_Board_Table))]
        public IHttpActionResult PostJob_Board_Table(Job_Board_Table job_Board_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Job_Board_Table.Add(job_Board_Table);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Job_Board_TableExists(job_Board_Table.Job_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = job_Board_Table.Job_Id }, job_Board_Table);
        }

        // DELETE: api/Job_Board_Table/5
        [ResponseType(typeof(Job_Board_Table))]
        public IHttpActionResult DeleteJob_Board_Table(decimal id)
        {
            Job_Board_Table job_Board_Table = db.Job_Board_Table.Find(id);
            if (job_Board_Table == null)
            {
                return NotFound();
            }

            db.Job_Board_Table.Remove(job_Board_Table);
            db.SaveChanges();

            return Ok(job_Board_Table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Job_Board_TableExists(decimal id)
        {
            return db.Job_Board_Table.Count(e => e.Job_Id == id) > 0;
        }
    }
}