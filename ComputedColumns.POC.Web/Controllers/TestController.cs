using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComputedColumns.POC.EntityFramework;
using ComputedColumns.POC.EntityFramework.Entities;
using ComputedColumns.POC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputedColumns.POC.Web.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        private readonly ComputedColumnsDbContext _dbContext;
        
        public TestController(ComputedColumnsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("search/{searchText}")]
        public async Task<IActionResult> Search([FromRoute]string searchText)
        {
            var resultsList = new List<SearchResultDto>(2);
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Animal> results;
            var formattedSearchText = $"%{searchText}%";
            
            results = await _dbContext.Animals.Where(a
                => EF.Functions.Like(a.Name.ToLower(), formattedSearchText)
                   || EF.Functions.Like(a.Breed.ToLower(), formattedSearchText)
                   || EF.Functions.Like(a.RegistrationCode.ToLower(), formattedSearchText)
                   || EF.Functions.Like(a.OwnerName.ToLower(), formattedSearchText)
                   || EF.Functions.Like(a.Description.ToLower(), formattedSearchText)).ToListAsync();
                
            stopWatch.Stop();

            var elapsedTime = stopWatch.Elapsed;
            var elapsedTimeStr = $"{elapsedTime.Hours}HH:{elapsedTime.Minutes}mm:{elapsedTime.Seconds}ss:{elapsedTime.Milliseconds}ms";

            var notComputedResponseModel = new SearchResultDto
            {
                ResultsCount = results.Count,
                ElapsedTimeStr = elapsedTimeStr,
                IsComputedUsed = false
            };

            resultsList.Add(notComputedResponseModel);
            
            stopWatch.Restart();
            
            results = await _dbContext.Animals
                .Where(a => EF.Functions.Like(a.ComputedProperty.ToLower(), formattedSearchText))
                .ToListAsync();
                
            stopWatch.Stop();
            
            elapsedTime = stopWatch.Elapsed;
            elapsedTimeStr = $"{elapsedTime.Hours}HH:{elapsedTime.Minutes}mm:{elapsedTime.Seconds}ss:{elapsedTime.Milliseconds}ms";
            
            var computedResponseModel = new SearchResultDto
            {
                ResultsCount = results.Count,
                ElapsedTimeStr = elapsedTimeStr,
                IsComputedUsed = true
            };

            resultsList.Add(computedResponseModel);
            
            return Ok(resultsList);
        }
    }
}