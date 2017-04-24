using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestLogin.Models;

namespace TestLogin.Controllers
{
    public class LoginController : Controller
    {
        public const string SALT = @"zrRu^Wq>NI7?=]e1Y`@PjX/]+Kjl\)POEgIIl(B5%J:%ow&;<87e]2;Ske3>&+7[";
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// To hash code by MD5
        /// </summary>
        /// <param name="salt">salt string</param>
        /// <param name="code">code string</param>
        /// <returns>string</returns>
        public static string ToHashCodeByMD5(string salt, string code)
        {
            try
            {
                // Convert saft string to byte
                var saltByte = Encoding.UTF8.GetBytes(salt);

                // Convert data string to byte
                var codeByte = Encoding.UTF8.GetBytes(code);

                // Hash password with MD5
                var hmacMD5 = new HMACMD5(saltByte);
                var saltedHash = hmacMD5.ComputeHash(codeByte);
                var hashLoginKey = Encoding.UTF8.GetString(saltedHash);

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < saltedHash.Length; i++)
                {
                    sBuilder.Append(saltedHash[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            try
            {
                var db = new OnlineStoreEntities();
                user.Password = ToHashCodeByMD5(SALT, user.Password);
                var info = db.Users.FirstOrDefault(m => m.Email == user.Email && m.Password == user.Password);
                if (info != null)
                {
                    Session["auth"] = info;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                ViewBag.Notice = "Login failed.";
                return View();
            }
            ViewBag.Notice = "Login failed.";
            return View();

        }
        public ActionResult Logout()
        {
            Session["auth"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}