﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.InMemory;

namespace Data.InMemory
{
    public class Program
    {
        public static async Task Main()
        {
            using (var db = new MyContext())
            {
                db.Add(new Blog {BlogId = 1, Name = "ADO.NET", Url = "http://blogs.msdn.com/adonet"});
                db.Add(new Blog {BlogId = 2, Name = ".NET Framework", Url = "http://blogs.msdn.com/dotnet"});
                db.Add(new Blog {BlogId = 3, Name = "Visual Studio", Url = "http://blogs.msdn.com/visualstudio"});
                await db.SaveChangesAsync();
            }

            using (var db = new MyContext())
            {
                var blogs = db.Blogs.OrderBy(b => b.Name);
                foreach (var item in blogs)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }

    public class MyContext : EntityContext
    {
        public EntitySet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(EntityConfigurationBuilder builder)
        {
            builder.UseInMemoryStore();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Blog>()
                .Key(b => b.BlogId);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}