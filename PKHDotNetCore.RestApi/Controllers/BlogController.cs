using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PKHDotNetCore.RestApi.EFCoreExamples;
using PKHDotNetCore.RestApi.Models;

namespace PKHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogs = _context.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                return NotFound("Blog not found.");
            }
            return Ok(blog);
        }

        [HttpGet("{pageNo}/{rowPerPage}")]
        public IActionResult GetBlogsWithPaginate(int pageNo, int rowPerPage)
        {
            var blogs = _context.Blogs
                .Skip((pageNo - 1) * rowPerPage)
                .Take(rowPerPage)
                .ToList();
            var totalRows = _context.Blogs.Count();
            var totalPages = totalRows / rowPerPage;
            if (totalRows % rowPerPage > 0)
            {
                totalPages++;
            }
            return Ok(new { isLastPage = pageNo >= totalPages, pageNo = pageNo, rowPerPage = rowPerPage, totalPages = totalPages, data = blogs });
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();
            var message = result > 0 ? "Blog created successfully." : "Blog creation failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            if(string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog title is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog author is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog content is required.");
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;
            var result = _context.SaveChanges();
            var message = result > 0 ? "Blog updated successfully." : "Blog update failed.";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            if(!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }
            var result = _context.SaveChanges();
            var message = result > 0 ? "Blog updated successfully." : "Blog update failed.";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                return NotFound("Blog not found.");
            }
            _context.Blogs.Remove(blog);
            var result = _context.SaveChanges();
            var message = result > 0 ? "Blog deleted successfully." : "Blog deletion failed.";
            return Ok(message);
        }
    }
}
