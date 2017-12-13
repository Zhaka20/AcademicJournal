using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Test
    {
        public int TestId { get; set; }
 
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int TotalScore { get; set; }
        public Mentor Mentor { get; set; }
        public Student Student { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }

    public class Question
    {
        public QuestionType Type { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int MyProperty { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<int> Answers { get; set; }
    }

    public class Choice
    {
        public int ChoiceId { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
    }

    public enum QuestionType
    {
        MultiChoice,
        SingleChoice
    }
}

//Constructor?
// journal model?
// assignment in files? 
// comments for assignment?
