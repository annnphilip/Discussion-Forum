using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discussion_Forum.Models
{
    public class QuestionAnswerViewModel
    {
        public Question Questions { get; set; }
       // public Topic Questtopic { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
