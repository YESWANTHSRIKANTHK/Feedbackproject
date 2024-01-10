using FeedBackPro.DAL;
using Microsoft.AspNetCore.Mvc;
using FeedBackPro.Models;
using Newtonsoft.Json.Linq;

namespace FeedBackPro.Controllers
{
    [Route("api/QandA")]
    [ApiController]
    public class QandAController : ControllerBase
    {
        private readonly QandA_DAL _dal;

        public QandAController(QandA_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]

        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _dal.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        [HttpPost]

        public IActionResult CreateUser(QandA user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Data is invalid" });
                }

                bool result = _dal.CreateUser(user);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not saved" });
                }

                return Ok(new { successMessage = "Data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }




        [HttpPost("Admin")]

        public IActionResult AdminUser(Yeswanth user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorMessage = "Data is invalid" });
                }

                bool result = _dal.AdminUser(user);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not saved" });
                }

                return Ok(new { successMessage = "Data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }



        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                bool result = _dal.DeleteUser(Id);
                if (!result)
                {
                    return BadRequest(new { errorMessage = "Data is not deleted" });
                }

                return Ok(new { successMessage = "Data deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }
     


        // Modify QandAController.cs







        //[HttpPut("{Id}")]
        //public IActionResult UpdateUser(int Id, QandA user)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new { errorMessage = "Data is invalid" });
        //        }

        //        // Set the Qid from the route parameter
        //        user.Qid = Id;

        //        bool result = _dal.UpdateUser(user);

        //        if (!result)
        //        {
        //            return BadRequest(new { errorMessage = "Data is not updated" });
        //        }

        //        return Ok(new { successMessage = "Data updated" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { errorMessage = ex.Message });
        //    }
        //}
    }

}

