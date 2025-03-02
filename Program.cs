using Microsoft.EntityFrameworkCore;
using WB_labb3_API_new_.Data;
using WB_labb3_API_new_.Models;

namespace WB_labb3_API_new_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //COrs
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /* TEST Locla db 
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDB"));
            */

            //Connection
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            var app = builder.Build();

            // Använd CORS-policy
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            // ------- Endpoints för certifikat --------


            // POST: Skapa ett nytt certifikat
            app.MapPost("/api/certificates", async (AppDbContext db, Certificate certificate) =>
            {
                db.Certificates.Add(certificate);
                await db.SaveChangesAsync();
                return Results.Ok(certificate);
            });

            // GET ALL: Hämta alla certifikat
            app.MapGet("/api/certificates", async (AppDbContext db) =>
                await db.Certificates.ToListAsync());

            // GET BY ID: Hämta ett certifikat via id
            app.MapGet("/api/certificates/{id}", async (AppDbContext db, int id) =>
            {
                var certificate = await db.Certificates.FindAsync(id);
                if (certificate == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(certificate);
            });

            // PUT: Uppdatera ett certifikat
            app.MapPut("/api/certificates/{id}", async (AppDbContext db, int id, Certificate updatedCertificate) =>
            {
                var existingCertificate = await db.Certificates.FindAsync(id);
                if (existingCertificate == null)
                {
                    return Results.NotFound();
                }

                existingCertificate.Title = updatedCertificate.Title;
                existingCertificate.ImageUrl = updatedCertificate.ImageUrl;
                existingCertificate.Status = updatedCertificate.Status;
                existingCertificate.DateAchieved = updatedCertificate.DateAchieved;
                existingCertificate.CredentialUrl = updatedCertificate.CredentialUrl;

                await db.SaveChangesAsync();
                return Results.Ok(updatedCertificate);
            });

            // DELETE: Ta bort ett certifikat
            app.MapDelete("/api/certificates/{id}", async (AppDbContext db, int id) =>
            {
                var certificate = await db.Certificates.FindAsync(id);
                if (certificate == null)
                {
                    return Results.NotFound();
                }
                db.Certificates.Remove(certificate);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // ------- Endpoints för erfarenhet --------


            // POST: Skapa en ny erfarenhet
            app.MapPost("/api/experiences", async (AppDbContext db, Experience experience) =>
            {
                db.Experiences.Add(experience);
                await db.SaveChangesAsync();
                return Results.Ok(experience);
            });

            // GET ALL: Hämta alla erfarenheter
            app.MapGet("/api/experiences", async (AppDbContext db) =>
                await db.Experiences.ToListAsync());

            // GET BY ID: Hämta en erfarenhet via id
            app.MapGet("/api/experiences/{id}", async (AppDbContext db, int id) =>
            {
                var experience = await db.Experiences.FindAsync(id);
                if (experience == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(experience);
            });

            // PUT: Uppdatera en erfarenhet
            app.MapPut("/api/experiences/{id}", async (AppDbContext db, int id, Experience updatedExperience) =>
            {
                var existingExperience = await db.Experiences.FindAsync(id);
                if (existingExperience == null)
                {
                    return Results.NotFound();
                }

                existingExperience.Company = updatedExperience.Company;
                existingExperience.Role = updatedExperience.Role;
                existingExperience.Date = updatedExperience.Date;
                existingExperience.ImageUrl = updatedExperience.ImageUrl;

                await db.SaveChangesAsync();
                return Results.Ok(updatedExperience);
            });

            // DELETE: Ta bort en erfarenhet
            app.MapDelete("/api/experiences/{id}", async (AppDbContext db, int id) =>
            {
                var experience = await db.Experiences.FindAsync(id);
                if (experience == null)
                {
                    return Results.NotFound();
                }
                db.Experiences.Remove(experience);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // ------- Endpoints för Technology --------

            //Post
            app.MapPost("/api/technologies", async (AppDbContext db, Technology tech) =>
            {
                if (string.IsNullOrEmpty(tech.ImageUrl))
                {
                    tech.ImageUrl = "images/Default.png"; // standardbild
                }
                
                db.Technologies.Add(tech);

                await db.SaveChangesAsync();
                return Results.Ok(tech);
            });

            //Get all
            app.MapGet("/api/technologies", async (AppDbContext db) =>
                await db.Technologies.ToListAsync());

            //GetById
            app.MapGet("/api/technologies/{id}", async (AppDbContext db,int id) =>
            {
                var tech = await db.Technologies.FindAsync(id);

                if (tech == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(tech);
            });

            //Put
            app.MapPut("/api/technologies/{id}", async (AppDbContext db, int id, Technology updatedTech) =>
            {
                var existingTech = await db.Technologies.FindAsync(id);

                if (existingTech == null)
                {
                    return Results.NotFound();
                }

                existingTech.Name = updatedTech.Name;
                existingTech.YearsOfExperience = updatedTech.YearsOfExperience;
                existingTech.SkillLevel = updatedTech.SkillLevel;

                await db.SaveChangesAsync();

                return Results.Ok(existingTech);
            });

            //Delete
            app.MapDelete("/api/technologies/{id}", async (AppDbContext db, int id) =>
            {
                var tech = await db.Technologies.FindAsync(id);

                if (tech == null)
                {
                    return Results.NotFound();
                }

                db.Technologies.Remove(tech);

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
