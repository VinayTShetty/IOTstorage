using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IotHubStorage.Repository;
using IotHubStorage.Models;
using System;
using Azure.Data.Tables;

namespace IotHubStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStorageController : ControllerBase
    {
        [HttpPost("AddTable")]
        public async Task<string> AddTable(string tableName)
        {
            await TableStorage.AddTable(tableName);
            return null;

        }

        [HttpPost("UpdateTable")]
        public async Task<Details> UpdateTable(Details student,string tableName)
        {
            await TableStorage.UpdateTable(student,tableName);
            return null;

        }


        [HttpGet("GetTableData")]
        public async Task<Details> GetTableData(string tableName, string partitionKey, string rowKey)
        {
            var data=await TableStorage.GetTableData(tableName, partitionKey, rowKey);
            return data;

        }

        [HttpGet("GetTable")]
        public async Task<TableClient> GetTable(string tableName)
        {
            var data = await TableStorage.GetTable(tableName);
            return data;

        }


        [HttpDelete("DeleteTableData")]
        public  async Task DeleteTableData(string tableName, string partitionKey, string rowKey)
        {
            await TableStorage.DeleteTableData(tableName, partitionKey, rowKey);
            return;

        }

        [HttpDelete("DeleteTable")]
        public async Task DeleteTable(string tableName)
        {
            await TableStorage.DeleteTable( tableName);
            return;

        }

    }
}
