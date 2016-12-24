using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVocabulary.Model.EntityFramework
{
    public class Log
    {
        public int Id { get; set; }

        public Word Word { get; set; }

        public DateTime Date { get; set; }
    }
}
