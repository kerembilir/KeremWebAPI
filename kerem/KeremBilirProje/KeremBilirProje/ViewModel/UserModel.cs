using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeremBilirProje.ViewModel
{
    public class UserModel
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }

       
        public AnswerModel AnswerInfo { get; set; }
    }
}