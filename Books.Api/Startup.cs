using System;
using System.Collections.Generic;
using System.Linq;
using Books.Api.Controllers.Filters;
using Books.Api.Data;
using Books.Api.Models;
using Books.Api.Services;
using FluentValidation.AspNetCore;
using JsonApiDotNetCore.Extensions;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });;
            services.AddDbContext<AppDataContext>(opt => opt.UseSqlite("Data Source=BooksDB"));
            services.AddJsonApi<AppDataContext>(opts => opts.Namespace = "api");

            services.AddScoped<IResourceService<Book>, BookService>();
            services.AddScoped<IResourceService<Review>, ReviewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJsonApi();
            
            context.Database.EnsureCreated();

            GenerateFakeData(context);
        }

        private void GenerateFakeData(AppDataContext context)
        {
            if (context.Authors.Any() == false)
                        {
                            context.Authors.AddRange(
                                new Author
                                {
                                    Id = 1,
                                    Books = new List<Book>(),
                                    FirstName = "Terry",
                                    LastName = "Brooks"
                                },
                                new Author
                                {
                                    Id = 2,
                                    Books = new List<Book>(),
                                    FirstName = "User",
                                    LastName = "One"
                                },
                                new Author
                                {
                                    Id = 3,
                                    Books = new List<Book>(),
                                    FirstName = "User",
                                    LastName = "Two"
                                }
                            );
                        }
                        
                        if(context.Books.Any() == false)
                        {
                            context.Books.AddRange(
                                new Book
                                {
                                    Id = 1,
                                    AuthorId = 1,
                                    ISBN = "9781904233404",
                                    Title = "The Sword of Shannara",
                                    PublishDate = DateTime.Today
                                },
                                new Book
                                {
                                    Id = 2,
                                    AuthorId = 1,
                                    ISBN = "9783442249794",
                                    Title = "The Elfstone of Shannara",
                                    PublishDate = DateTime.Today
                                },
                                new Book
                                {
                                    Id = 3,
                                    AuthorId = 2,
                                    ISBN = "1234",
                                    Title = "Book Test",
                                    PublishDate = DateTime.Today
                                }
                            );
                        }
            
                        if (context.Reviews.Any() == false)
                        {
                            context.Reviews.AddRange(
                                new Review
                                {
                                    Id = 1,
                                    BookId = 1,
                                    ReviewerName = "Bad reviewer",
                                    Body = "This book sucks!"
                                },
                                new Review
                                {
                                    Id = 2,
                                    BookId = 1,
                                    ReviewerName = "Good reviewer",
                                    Body = "This book is awesome!"
                                },
                                new Review
                                {
                                    Id = 3,
                                    BookId = 2,
                                    ReviewerName = "Neutral reviewer",
                                    Body = "This book is soso."
                                }
                            );
                        }
                        context.SaveChanges();
        }
    }
}