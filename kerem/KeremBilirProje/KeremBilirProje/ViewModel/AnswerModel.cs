using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeremBilirProje.ViewModel
{
    public class AnswerModel
    {
        public string aId { get; set; }
        public string aBody { get; set; }
        public string aUserId { get; set; }
        public string aQId { get; set; }

        public UserModel UserInfo { get; set; }
        public QuestionModel QuestionInfo { get; set; }
    }
}