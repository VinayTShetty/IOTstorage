using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Azure.Storage.Queues.Models;
using IotHubStorage.Repository;

namespace IoT_Storage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpPost("AddQueue")]
        public async Task<string> AddQueue(string queueName)
        {
            await Queue.CreateQueue(queueName);
            return null;

        }

        [HttpPut("InsertMessage")]
        public async Task<string> InsertMessage(string queueName,string msg)
        {
            await Queue.InsertMessage(queueName,msg);
            return null;

        }

        [HttpGet("PeekMessage")]
        public async Task<PeekedMessage[]>PeekMessage(string queueName)
        {
           var data= await Queue.PeekMessage(queueName);
            return data;

        }

        [HttpPut("UpdateMessage")]
        public async Task<string> UpdateMessage(string queueName,string msg)
        {
            await Queue.UpdateMessage(queueName,msg);
            return null;

        }

        [HttpPut("DequeueMessage")]
        public async Task<string> DequeueMessage(string queueName)
        {
            await Queue.DequeueMessage(queueName);
            return null;

        }

        [HttpDelete("DeleteQueue")]
        public async Task<string> DeleteQueue(string queueName)
        {
            await Queue.DeleteQueue(queueName);
            return null;

        }


    }
}
