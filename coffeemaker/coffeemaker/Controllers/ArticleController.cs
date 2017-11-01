using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dna.core.service.Services.Abstract;
using dna.core.libs.Validation;
using dna.core.data.Infrastructure;

namespace coffeemaker.api.Controllers
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
        /// <param name="pageIndex">Current page, start from 1</param>
        /// <param name="pageSize">Page size, start from 1</param>
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

        



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _articleService.Dispose();

        }
    }
}
