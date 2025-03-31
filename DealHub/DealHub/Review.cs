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
        [JsonIgnore]
        public RegisteredUser Author { get; }
        public string AuthorName { get; set;}
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Review(){ }
        public Review(RegisteredUser author, string content)
        {
            AuthorName = author.Nickname;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}
