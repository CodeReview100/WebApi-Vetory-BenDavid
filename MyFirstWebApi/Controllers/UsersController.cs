﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Servicies;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Zxcvbn;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        IUserServicies userServices;

        public UsersController(IUserServicies _userServices)
        {
            userServices = _userServices;
        }


        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get([FromQuery] string userName, [FromQuery] string code)
        {
            Users user = userServices.getUserByPasswordAndUserName(code, userName);
            if (user == null)
                 return NoContent();
            return Ok(user);
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //POST api/<UserController>
        [HttpPost("makeuser")]
        public ActionResult Post([FromBody] Users user)
        {
            try {
                Users newUser = userServices.addUser(user);
                
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            catch (Exception ex){
                throw ex;
             }       
           
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public Users Put(int id, [FromBody] Users userToUpdate)
        {
            Users user = userServices.updateUser(id, userToUpdate);
            return user;
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
