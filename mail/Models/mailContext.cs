using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;


namespace mail.Models
{
    public class mailContext:DbContext
    {
        public mailContext(DbContextOptions<mailContext> options)
            : base(options)
        {
        }
        public DbSet<EmailMessage> EmailMessagese { get; set; }
        public DbSet<EmailAddress> EmailAddresse { get; set; }
    }
}
