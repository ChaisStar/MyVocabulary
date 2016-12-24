using System.Data.Entity;
namespace MyVocabulary.Model.EntityFramework
{

    public class VocabularyContext : DbContext
    {
        public VocabularyContext()
            : base("name=VocabularyContext")
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}