using MyPocApplication.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MyPocApplication.Controllers
{
    public class UserController : Controller
    {
        private DataContext db = new DataContext();
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User usr)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(usr);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Data has been Inserted!";
            }
            else
            {
                ModelState.AddModelError("", "Some Error Occured");
            }
            return View(usr);

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User Users)
        {
            if
                (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please Enter Valid UserName and Password");
                using (DataContext db = new DataContext())
                {
                    var obj = db.Users.Where(u => u.UserName.Equals(Users.UserName) &&
                        u.Password.Equals(Users.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserId"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                }
            }
            return View(Users);

        }
        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult DashBoard()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int UserID)
        {
            var obj = db.Users.Single(u => u.UserId == UserID);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User Users)
        {
            string CS = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUpdateUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Users.UserId);
                cmd.Parameters.AddWithValue("@UserName", Users.UserName);
                cmd.Parameters.AddWithValue("@Password", Users.Password);
                cmd.Parameters.AddWithValue("@FirstName", Users.FirstName);
                cmd.Parameters.AddWithValue("@LastName", Users.LastName);
                cmd.Parameters.AddWithValue("@EmailID", Users.Email);
                cmd.Parameters.AddWithValue("@Mobile", Users.Mobile);
                ViewBag.SuccessMessage = "Data Updated  Successfully!";
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("LoggedIn");
        }


        [HttpGet]
        public ActionResult Delete(int UserID)
        {
            var obj = db.Users.Single(u => u.UserId == UserID);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete(User Users)
        {
            string CS = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spDeleteUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Users.UserId);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("LoggedIn");
        }


    }




}