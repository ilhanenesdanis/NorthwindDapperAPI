using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindDapperAPI.Models;
using System.Data.SqlClient;

namespace NorthwindDapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CrudController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            using (var con = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                var Categorys = await con.QueryAsync<Categories>("select CategoryID ,CategoryName ,Description from Categories");
                return Ok(Categorys);
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            using (var con = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                var category = await con.QueryAsync<Categories>($"select CategoryID ,CategoryName ,Description from Categories where CategoryID={id}");
                return Ok(category);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewCategory(Categories categories)
        {
            using (var con = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                await con.ExecuteAsync($"insert into Categories (CategoryName,Description) values ('{categories.CategoryName}','{categories.Description}')");
                return Ok(categories);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Categories categories)
        {
            using (var con =new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                await con.ExecuteAsync($"update Categories set CategoryName='{categories.CategoryName}',Description='{categories.Description}' where CategoryID={categories.CategoryID}");
                return Ok();
            }
        }
        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            using (var con = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                await con.ExecuteAsync($"delete from Categories where CategoryID={CategoryId}");
                return Ok();
            }
        }
    }
}
