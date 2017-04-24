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
    public class RegisterController : Controller
    {
        OnlineStoreEntities db = new OnlineStoreEntities();
        public const string SALT = @"zrRu^Wq>NI7?=]e1Y`@PjX/]+Kjl\)POEgIIl(B5%J:%ow&;<87e]2;Ske3>&+7[";
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
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(RegisterViewModel viewmodel)
        {
            try
            {
                var account = db.Users.Any(m => m.Email == viewmodel.Email);
                if (account)
                {
                    ViewBag.Notice = "Email already exists";
                    return View();
                }
                // create is allow
                var user = new User();
                user.Address = viewmodel.Address;
                user.Email = viewmodel.Email;
                user.Password = viewmodel.Password;
                user.UserName = viewmodel.UserName;
                user.Password = ToHashCodeByMD5(SALT, viewmodel.Password);
                user.CreatedAt = DateTime.Now;
                
                db.Users.Add( user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }

        }
    }
}