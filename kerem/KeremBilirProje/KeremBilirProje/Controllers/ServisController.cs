using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KeremBilirProje.Models;
using KeremBilirProje.ViewModel;

namespace KeremBilirProje.Controllers
{
    public class ServisController : ApiController
    {
        DB04Entities db = new DB04Entities ();
        SonucModel sonuc = new SonucModel();

        #region CATEGORY
        [HttpGet]
        [Route ("api/categorylist")]

        public List<CategoryModel> CategoryList()
        {
            List<CategoryModel> list = db.Category.Select(x => new CategoryModel()
            {
                catId=x.catId,
                catName=x.catName
            }).ToList();
            return list;
        }


        [HttpGet]
        [Route("api/categorybyid")]

        public CategoryModel CategoryById(string catId)
        {
            CategoryModel record = db.Category.Where(s =>s.catId == catId).Select(x=> new CategoryModel()
            {
                catId = x.catId,
                catName = x.catName
            }).FirstOrDefault();
            return record;
        }

        [HttpPost]
        [Route ("api/categoryadd")]

        public SonucModel CategoryAdd (CategoryModel model)
        {
            if (db.Category.Count(s=>s.catName== model.catName)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır.";
                return sonuc;
            }
            Category yeni = new Category();
            yeni.catId = Guid.NewGuid().ToString();
            yeni.catName = model.catName;
            db.Category.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = " Category Added";
            return sonuc;
        }


        [HttpPut]
        [Route ("api/categoryedit")]

        public SonucModel CategoryEdit (CategoryModel model)
        {
            Category record = db.Category.Where(s => s.catId == model.catId).FirstOrDefault();
            if (record==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Category Didn't Founded!";
                return sonuc;
            }
            record.catName = model.catName;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Category Edited";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/categorydelete/{catId}")]


        public SonucModel CategoryDelete (string catId)
        {
            Category record = db.Category.Where(s => s.catId == catId).FirstOrDefault();

            if (record==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Category bulanamadı!";
                return sonuc;
            }

            if (db.Question.Count(s=> s.qCatId==catId)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategoriye kayıtlı sorular olduğu için bu kategori silinemez";
                return sonuc;
            }

            db.Category.Remove(record);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Category Deleted";

            return sonuc;
        }


       #endregion

        #region USER

        [HttpGet]
        [Route("api/userlist")]

        public List<UserModel> UserList()
        {
            List<UserModel> list = db.User.Select(x => new UserModel()
            {
                userId = x.userId,
                userName = x.userName,
                userEmail = x.userEmail,
                userPassword = x.userPassword
            }).ToList();
            return list;
        }

        [HttpGet]
        [Route("api/userbyid")]

        public UserModel UserById(string userId)
        {
            UserModel record = db.User.Where(s => s.userId == userId).Select(x => new UserModel()
            {
                userId = x.userId,
                userName = x.userName,
                userEmail = x.userEmail,
                userPassword = x.userPassword
            }).FirstOrDefault();
            return record;
        }

        [HttpPost]
        [Route("api/useradd")]

        public SonucModel UserAdd(UserModel model)
        {
            if (db.User.Count(s => s.userId == model.userId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Kayıtlıdır.";
                return sonuc;
            }
            User yeni = new User();
            yeni.userId = Guid.NewGuid().ToString();
            yeni.userName = model.userName;
            yeni.userEmail = model.userEmail;
            yeni.userPassword = model.userPassword;
            db.User.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = " User Added";
            return sonuc;
        }

        [HttpPut]
        [Route("api/useredit")]

        public SonucModel UserEdit(UserModel model)
        {
            User record = db.User.Where(s => s.userId == model.userId).FirstOrDefault();
            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " User Didn't Founded!";
                return sonuc;
            }
            record.userName = model.userName;
            record.userEmail = model.userEmail;
            record.userPassword = model.userPassword;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "User Edited";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/userdelete/{userId}")]


        public SonucModel UserDelete(string userId)
        {
            User record = db.User.Where(s => s.userId == userId).FirstOrDefault();

            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " User bulanamadı!";
                return sonuc;

            }
            db.User.Remove(record);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "User Deleted";

            return sonuc;
        }

       


        #endregion

        #region QUESTION

        [HttpGet]
        [Route("api/questionlist")]

        public List<QuestionModel> QuestionList()
        {
            List<QuestionModel> list = db.Question.Select(x => new QuestionModel()
            {
                qId = x.qId,
                qTitle = x.qTitle,
                qBody = x.qBody
            }).ToList();
            return list;
        }

        [HttpGet]
        [Route("api/questionbyid")]

        public QuestionModel QuestionById(string qId)
        {
            QuestionModel record = db.Question.Where(s => s.qId == qId).Select(x => new QuestionModel()
            {
                qId = x.qId,
                qBody = x.qBody,
                qTitle = x.qTitle
                
            }).FirstOrDefault();

            return record;
        }

        [HttpPost]
        [Route("api/questionadd")]

        public SonucModel QuestionAdd (QuestionModel model)
        {
            if (db.Question.Count(s => s.qTitle == model.qTitle) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Kayıtlıdır.";
                return sonuc;
            }
            Question yeni = new Question();
            yeni.qId = Guid.NewGuid().ToString();
            yeni.qTitle = model.qTitle;
            yeni.qBody = model.qBody;
            db.Question.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = " Question Added";
            return sonuc;
        }


        [HttpPut]
        [Route("api/questionedit")]

        public SonucModel QuestionEdit (QuestionModel model)
        {
            Question record = db.Question.Where(s => s.qId == model.qId).FirstOrDefault();
            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Question Didn't Founded!";
                return sonuc;
            }
            record.qTitle = model.qTitle;
            record.qBody = model.qBody;
            record.qCatId = model.qCatId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Question Edited";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/questiondelete/{qId}")]


        public SonucModel QuestionDelete(string qId)
        {
            Question record = db.Question.Where(s => s.qId == qId).FirstOrDefault();

            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Question bulanamadı!";
                return sonuc;

            }

            if (db.Answer.Count(s => s.aQId == qId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu soruya ait cevaplar olduğu için silinemez!";
                return sonuc;
            }

            db.Question.Remove(record);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Question Deleted";

            return sonuc;
        }


        // kullanıcını soruları ve soruların kategorilerinin listelendiği kısım 
        [HttpGet]
        [Route("api/userquestionlist/{userId}")]

        public List<QuestionModel> UserQuestionList(string userId)
        {
            List<QuestionModel> list = db.Question.Where(s => s.qUserId == userId).Select(x => new QuestionModel()
            {
                qUserId = x.qUserId,
                qCatId = x.qCatId,
                qId = x.qId,
                qTitle = x.qTitle,
                qBody = x.qBody,

            }).ToList();

            foreach (var question in list)
            {
                question.UserInfo = UserById (question.qUserId);
                question.CategoryInfo = CategoryById (question.qCatId);
            }

            return list;
        }

        #endregion

        #region ANSWER

        [HttpGet]
        [Route("api/answerlist")]

        public List<AnswerModel> AnswerList()
        {
            List<AnswerModel> list = db.Answer.Select(x => new AnswerModel()
            {
                aId = x.aId,
                aBody = x.aBody
            }).ToList();
            return list;
        }

        [HttpGet]
        [Route("api/answerbyid")]

        public AnswerModel AnswerById(string aId)
        {
            AnswerModel record = db.Answer.Where(s => s.aId == aId).Select(x => new AnswerModel()
            {
                aId = x.aId,
                aBody = x.aBody

            }).FirstOrDefault();

            return record;
        }

        [HttpPost]
        [Route("api/answeradd")]

        public SonucModel AnswerAdd (AnswerModel model)
        {
            if (db.Answer.Count(s => s.aBody == model.aBody) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Cevap Kayıtlıdır.";
                return sonuc;
            }
            Answer yeni = new Answer();
            yeni.aId = Guid.NewGuid().ToString();
            yeni.aBody = model.aBody;
            db.Answer.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = " Answer Added";
            return sonuc;
        }

        [HttpPut]
        [Route("api/answeredit")]

        public SonucModel AnswerEdit(AnswerModel model)
        {
            Answer record = db.Answer.Where(s => s.aId == model.aId).FirstOrDefault();
            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Answer Didn't Founded!";
                return sonuc;
            }
            record.aBody = model.aBody;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Answer Edited";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/answerdelete/{aId}")]


        public SonucModel AnswerDelete(string aId)
        {
            Answer record = db.Answer.Where(s => s.aId == aId).FirstOrDefault();

            if (record == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Answer bulanamadı!";
                return sonuc;

            }
            db.Answer.Remove(record);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Answer Deleted";

            return sonuc;
        }


        // kullanıcının cevaplarının ve soruların listelendiği kısım
        [HttpGet]
        [Route("api/useranswerlist/{userId}")]

        public List<AnswerModel> UserAnswerList(string userId)
        {
            List<AnswerModel> list = db.Answer.Where(s => s.aUserId == userId).Select(x => new AnswerModel()
            {
                aUserId = x.aUserId,
                aQId = x.aQId,
                aBody = x.aBody,

            }).ToList();

            foreach (var answer in list)
            {
                answer.UserInfo = UserById(answer.aUserId);
                answer.QuestionInfo = QuestionById(answer.aQId);
            }

            return list;
        }

        #endregion
    }
}
