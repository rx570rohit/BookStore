﻿using BussinessLayer.Interfaces;
using DatabaseLayer.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
      
        IbookstoreContext context;
        readonly IAddressBl addressbl;
        

        public AddressController(IAddressBl addressbl, IbookstoreContext context)
        {
            this.addressbl =addressbl;

            this.context = context;
        }


        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddBook(AddressPostModel addressPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = await this.addressbl.AddAddress(userId,addressPostModel);
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Address Save Successfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Address Not Saved Successfully" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }

        [HttpPut("UpdateBook")]

        public async Task<IActionResult> UpdateBook(AddressPostModel addressPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var resp = await addressbl.UpdateAddress(userId,addressPostModel);
                if (resp != null)
                {

                    return this.Ok(new { Status = true, Message = " Address Updated Succeessfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = " Address not Updated " });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpGet("getallbook")]

        public Task<IEnumerable>  GetAllAddresses()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;

                var res = this.addressbl.GetAddresses(userId);
                return res;

            }

            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpDelete("DeleteBook")]

        public async Task<IActionResult> RemoveAddress(AddressPostModel addressPostModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase)).Value;
                bool resp = await this.addressbl.RemoveAddress(userId,addressPostModel);
                if (resp != false)
                {

                    return this.Ok(new { Status = true, Message = " Address Removed Successfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Address Not Removed Successfully" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
    }
}

