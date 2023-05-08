using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeremBilirProje.ViewModel
{
    public class QuestionModel
    {
        public string qId { get; set; }
        public string qTitle { get; set; }
        public string qBody { get; set; }
        public string qUserId { get; set; }
        public string qCatId { get; set; }


        public UserModel UserInfo { get; set; }
        public CategoryModel CategoryInfo { get; set; }
    }
}