using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public Topic Topic { get; set; }
        
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        
        public UserAccount UserAccount { get; set; }
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        
        public string Questions { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime  CreatedDate{ get; set; }
    }
}
