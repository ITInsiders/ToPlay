using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP.ML.Entities;
using TP.BL.Services;
using TP.BL.Extensions;

namespace TP
{
    public class Identity
    {
        private HttpContext HC => HttpContext.Current;
        public User User;
        public long UserId => User.Id;

        public Identity()
        {
           Check(HC.Request.Cookies["_usk"]?.Value);
        }

        private bool Check(string Key, string Password)
        {
            this.User = Service<User>.I
                .Get(x => x.Login.ToUpper() == Key.ToUpper() || x.PhoneNumber == Key || x.Email.ToUpper() == Key.ToUpper())
                .FirstOrDefault(x => Password == x.Password);

            return User != null;
        }

        private bool Check(string UserSecurityKey)
        {
            if (UserSecurityKey != null)
            {
                int USKLenght = UserSecurityKey.Length;
                if (USKLenght > 128)
                {
                    string HashLogin = UserSecurityKey.Substring(0, UserSecurityKey.Length - 128);

                    string Login = AES.Decrypt(HashLogin);
                    string Password = UserSecurityKey.Substring(UserSecurityKey.Length - 128);

                    return Check(Login, Password);
                }
            }

            User = null;
            return false;
        }

        public bool Authentication(string Login, string Password)
        {
            clearAuthentication();

            if (Check(Login, Hash.GetHash(Hash.TypeHash.SHA512, Password, Hash.GenerateSalt(Login))))
            {
                HttpCookie Cookie = new HttpCookie("_usk");
                Cookie.Expires = DateTime.Now.AddDays(7);

                Cookie.Value = AES.Encrypt(Login) + Hash.GetHash(Hash.TypeHash.SHA512, Password, Hash.GenerateSalt(Login));

                HC.Response.Cookies.Add(Cookie);

                return true;
            }

            else return false;
        }

        public bool isAuthentication => Check(HC.Request.Cookies["_usk"]?.Value);
        public bool isAuth => User != null;

        public void clearAuthentication()
        {
            if (HC.Request.Cookies["_usk"] != null)
            {
                HC.Response.Cookies["_usk"].Expires = DateTime.Now.AddYears(-1);
            }
            User = null;
        }
    }
}