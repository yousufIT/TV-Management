using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;
using Attachment = TV.Domain.Models.Attachment;

namespace TV.Infrastructure
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageTVShow> LanguageTVShows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.TVShow)
                .WithOne(t => t.Attachment)
                .HasForeignKey<Attachment>(a => a.TVShowId);

        }

        public static void CreatingInitialTestingDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            // Create and add TV Shows
            var tvShow1 = new TVShow { Title = "Breaking Bad", ReleaseDate = new DateTime(2008, 1, 20), Rating = 9.5, URL = "https://www.imdb.com/title/tt0903747/" };
            var tvShow2 = new TVShow { Title = "Stranger Things", ReleaseDate = new DateTime(2016, 7, 15), Rating = 8.7, URL = "https://www.imdb.com/title/tt4574334/" };
            var tvShow3 = new TVShow { Title = "The Crown", ReleaseDate = new DateTime(2016, 11, 4), Rating = 8.6, URL = "https://www.imdb.com/title/tt4786824/" };

            context.TVShows.AddRange(tvShow1, tvShow2, tvShow3);
            context.SaveChanges();

            // Create and add Attachments
            var attachment1 = new Attachment { Name = "Breaking Bad Image", Path = $"wwwroot/img/{tvShow1.Id}.jpg", FileType = "image/jpeg", TVShowId = tvShow1.Id };
            var attachment2 = new Attachment { Name = "Stranger Things Image", Path = $"wwwroot/img/{tvShow2.Id}.jpg", FileType = "image/jpeg", TVShowId = tvShow2.Id };
            var attachment3 = new Attachment { Name = "The Crown Image", Path = $"wwwroot/img/{tvShow3.Id}.jpg", FileType = "image/jpeg", TVShowId = tvShow3.Id };

            context.Attachments.AddRange(attachment1, attachment2, attachment3);
            context.SaveChanges();

            // Update TV Shows with Attachment IDs
            tvShow1.AttachmentId = attachment1.Id;
            tvShow2.AttachmentId = attachment2.Id;
            tvShow3.AttachmentId = attachment3.Id;

            context.TVShows.UpdateRange(tvShow1, tvShow2, tvShow3);
            context.SaveChanges();

            // Create and add Languages
            var language1 = new Language { Name = "English" };
            var language2 = new Language { Name = "Spanish" };
            var language3 = new Language { Name = "French" };

            context.Languages.AddRange(language1, language2, language3);
            context.SaveChanges();

            attachment1.TVShow = tvShow1;
            attachment2.TVShow = tvShow2;
            attachment3.TVShow = tvShow3;
            context.SaveChanges();
            var languagetvshow1 = new LanguageTVShow
            {
                Language = language1,
                LanguageId = language1.Id,
                TVShow = tvShow1,
                TVShowId = tvShow1.Id
            };
            var languagetvshow2 = new LanguageTVShow
            {
                Language = language1,
                LanguageId = language1.Id,
                TVShow = tvShow2,
                TVShowId = tvShow2.Id
            };
            var languagetvshow3 = new LanguageTVShow
            {
                Language = language3,
                LanguageId = language3.Id,
                TVShow = tvShow3,
                TVShowId = tvShow3.Id
            };
            // Associate languages with TV Shows
            tvShow1.LanguageTVShows.Add(languagetvshow1);
            tvShow2.LanguageTVShows.Add(languagetvshow2);
            tvShow3.LanguageTVShows.Add(languagetvshow3);
            language1.LanguageTVShows.Add(languagetvshow1);
            language1.LanguageTVShows.Add(languagetvshow2);
            language3.LanguageTVShows.Add(languagetvshow3);
            context.SaveChanges();
        }
    }


}
