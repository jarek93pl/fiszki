using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class User
    {

        public Dictionary<int, ResponseDetails> AnswersByFicheId { get; set; } = new Dictionary<int, ResponseDetails>();
        public int LastIndexComand { get; set; }
        private string SetedLogin { get; set; }
        public ResponseDetails LastResponseDetails
        {
            get
            {
                DateTime maxTime = AnswersByFicheId.Values.FirstOrDefault().DateCreated;
                ResponseDetails responseDetails = AnswersByFicheId.Values.FirstOrDefault();
                foreach (var item in AnswersByFicheId.Values)
                {
                    if (item.DateCreated > maxTime)
                    {
                        maxTime = item.DateCreated;
                        responseDetails = item;
                    }
                }
                return responseDetails;
            }
        }
        public User()
        {

        }
        public User(string login)
        {
            SetedLogin = login;
        }
        public User(Logic.UserFiche userFiche)
        {
            this.UserFiche = userFiche;
        }
        public string LoginToProcess { get => UserFiche?.Name ?? SetedLogin; }
        Logic.UserFiche UserFiche { get; }
        public int Point = 0;
        public override bool Equals(object obj)
        {
            if (obj is User userB)
            {
                return userB.LoginToProcess == LoginToProcess;
            }
            return false;
        }
        public override int GetHashCode() => LoginToProcess.GetHashCode();
        public bool UserCanStart { get; internal set; }


        public override string ToString()
        {
            return LoginToProcess;
        }
    }
}