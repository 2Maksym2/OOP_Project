using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
namespace DealHub
{
    public class Review
    {
        public string AuthorName { get; set;}
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Review(){ }
        public Review(string author, string content)
        {
            AuthorName = author;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}
