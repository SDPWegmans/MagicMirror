using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MirrorWebService.Models.Settings;

namespace MirrorWebService.Controllers
{
    public class NoteSettingsController : ApiController
    {
        private NoteSettingsContext db = new NoteSettingsContext();

        // GET: api/NoteSettings
        public IQueryable<NoteSettings> GetNoteSettings()
        {
            return db.NoteSettings;
        }

        // GET: api/NoteSettings/5
        [ResponseType(typeof(NoteSettings))]
        public async Task<IHttpActionResult> GetNoteSettings(int id)
        {
            NoteSettings noteSettings = await db.NoteSettings.FindAsync(id);
            if (noteSettings == null)
            {
                return NotFound();
            }

            return Ok(noteSettings);
        }

        // PUT: api/NoteSettings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNoteSettings(int id, NoteSettings noteSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteSettings.Id)
            {
                return BadRequest();
            }

            db.Entry(noteSettings).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteSettingsExists(id))
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

        // POST: api/NoteSettings
        [ResponseType(typeof(NoteSettings))]
        public async Task<IHttpActionResult> PostNoteSettings(NoteSettings noteSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NoteSettings.Add(noteSettings);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = noteSettings.Id }, noteSettings);
        }

        // DELETE: api/NoteSettings/5
        [ResponseType(typeof(NoteSettings))]
        public async Task<IHttpActionResult> DeleteNoteSettings(int id)
        {
            NoteSettings noteSettings = await db.NoteSettings.FindAsync(id);
            if (noteSettings == null)
            {
                return NotFound();
            }

            db.NoteSettings.Remove(noteSettings);
            await db.SaveChangesAsync();

            return Ok(noteSettings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoteSettingsExists(int id)
        {
            return db.NoteSettings.Count(e => e.Id == id) > 0;
        }
    }
}