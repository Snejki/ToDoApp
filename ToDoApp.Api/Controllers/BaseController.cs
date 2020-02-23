using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Api.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ApiController]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        protected Guid AuthUserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected void AddPaginationInfo(int currentpage, int pagesCount)
        {
            Response.Headers.Add("Pagination-CurrentPage", $"{currentpage}");
            Response.Headers.Add("Pagination-PagesCount", $"{pagesCount}");
        }
    }
}