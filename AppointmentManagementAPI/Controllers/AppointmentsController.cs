using AppointmentManagementAPI.Data;
using AppointmentManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            if (appointment.AppointmentDate <= DateTime.Now)
            {
                return BadRequest("The appointment date must be in the future.");
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return Ok(appointment);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, Appointment updatedAppointment)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            if (updatedAppointment.AppointmentDate <= DateTime.Now)
            {
                return BadRequest("The appointment date must be in the future.");
            }

            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.Status = updatedAppointment.Status;
            _context.SaveChanges();
            return Ok(appointment);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
