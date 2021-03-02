using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        
        public Question Question { get; set; }
        [ForeignKey("Question")]
        public int QuestId { get; set; }
       
        public UserAccount UserAccount { get; set; }
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        
        public string Answers { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime AnsDate { get; set; }
    }
}
