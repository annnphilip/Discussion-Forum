using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        
        public string TopicName { get; set; }
    }
}
