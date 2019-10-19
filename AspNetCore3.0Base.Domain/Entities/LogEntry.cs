using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace AspNetCore3._0Base.Domain.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Exception { get; set; }

        public string LogEvent { get; set; }
        [Column(TypeName = "xml")]
        public String Properties { get; set; }


        [NotMapped]
        public XDocument DetailsElement
        {
            get { return Properties != null ? XDocument.Parse(Properties) : null; }
            set { Properties = value.ToString(); }
        }

    }
}
