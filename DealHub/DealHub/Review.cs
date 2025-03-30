using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub
{
    public class Review
    {
        public RegisteredUser Author { get; }
        public string Content { get; }
        public DateTime CreatedAt { get; }

        public Review(RegisteredUser author, string content)
        {
            Author = author;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}
