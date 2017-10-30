using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dna.core.service.Services.Abstract;
using dna.core.libs.Validation;
using dna.core.data.Infrastructure;

namespace articlemaker.Controllers
{
    /// <summary>
    /// Controller for article
    /// </summary>
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="articleService"></param>
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;

        }
        private void AssignModelState()
        {
            _articleService.Initialize(new ModelStateWrapper(ModelState));
        }
        /// <summary>
        /// Get newest article
        /// </summary>
        /// <returns></returns>
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetNewestArticle(int pageIndex, int pageSize)
        {
            var status = new List<ArticleStatus>() { ArticleStatus.Confirmed };
            var result = await _articleService.GetNewestArticleAsync(status, pageIndex, pageSize);
            if ( result.Success )
            {
                return Ok(result.Item);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Get article by medical staff
        /// </summary>
        /// <param name="id">Staff Id</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Width</param>
        /// <returns></returns>
        [HttpGet("Staff/{id}/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetArticleByStaff(int id, int pageIndex, int pageSize)
        {
            var status = new List<ArticleStatus>() { ArticleStatus.Confirmed };
            var result = await _articleService.GetArticleByStaff(id, pageIndex, pageSize, status.ToArray());
            if ( result.Success )
            {
                return Ok(result.Item);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Get article detail
        /// </summary>
        /// <param name="id">Article Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _articleService.GetSingleDetailAsync(id);
            if ( response.Success )
            {
                return Ok(response.Item);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _articleService.Dispose();

        }
    }
}
