using Microsoft.EntityFrameworkCore;
using PKHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Run()
        {
            // Read();
            // Create("Test title 6", "PKH", "Test Body 6");
            // Edit(1);
            // Edit(100);
            // Delete(7);
            Update(6, "Test title 4", "PKH", "Test Body 4");
        }   

        public void Read()
        {
            var list = _context.Blogs.AsNoTracking().ToList();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Blog_Id} {item.Blog_Title} {item.Blog_Author} {item.Blog_Content}");
            }
        }

        public void Create(string title, string author, string content)
        {
            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            _context.Blogs.Add(blog);
            int result = _context.SaveChanges();
            var message = result > 0 ? "Create success" : "Create fail";
            Console.WriteLine(message);
        }

        public void Edit(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(blog is null)
            {
                Console.WriteLine("Blog not found");
                return;
            }
            Console.WriteLine($"{blog.Blog_Id} {blog.Blog_Title} {blog.Blog_Author} {blog.Blog_Content}");
        }

        public void Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("Blog not found");
                return;
            }
            _context.Blogs.Remove(blog);
            int result = _context.SaveChanges();
            var message = result > 0 ? "Delete success" : "Delete fail";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("Blog not found");
                return;
            }
            blog.Blog_Title = title;
            blog.Blog_Author = author;
            blog.Blog_Content = content;
            int result = _context.SaveChanges();
            var message = result > 0 ? "Update success" : "Update fail";
            Console.WriteLine(message);
        }
    }
}
