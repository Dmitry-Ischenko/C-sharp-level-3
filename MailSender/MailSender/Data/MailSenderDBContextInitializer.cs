using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace MailSender.Data
{
    class MailSenderDBContextInitializer : IDesignTimeDbContextFactory<MailSenderDB>
    {
        public MailSenderDB CreateDbContext(string[] args)
        {
            string sql_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database1.mdf;Integrated Security=True";
            var optionsBuilder = new DbContextOptionsBuilder<MailSenderDB>();
            optionsBuilder.UseSqlServer(sql_string);
            return new MailSenderDB(optionsBuilder.Options);
        }
    }
}
