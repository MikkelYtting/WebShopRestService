using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShopRestService.Controllers
{
    // This controller is only accessible to users who have the "Administrator" role.
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // Example of an admin-only action
        [HttpGet("dashboard-data")]
        public IActionResult GetDashboardData()
        {
            // TODO: Implement your logic to fetch and return the dashboard data.
            // This is just a placeholder for demonstration purposes.
            return Ok(new { message = "This is the admin dashboard data." });
        }

        // Add more admin-only actions here
        // ...
    }
}