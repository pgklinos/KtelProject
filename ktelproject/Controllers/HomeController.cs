using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KtelProject.Models;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace KtelProject.Controllers
{
    public class HomeController : Controller
    {


        private KtelModels db = new KtelModels();



        public ActionResult Index()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View();
        }

       
        [Route("Home/Edit")]
        [HttpGet]
        public ActionResult Edit()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                TempData["Cemail"] = email;
                Passenger k = db.Passengers.First(x => x.Email == email);
                ViewBag.FirstName = k.FirstName;
                ViewBag.LastName = k.LastName;
                ViewBag.PhoneNumber = k.PhoneNumber;

                return View();
            }
            else { return View(); }
           
        }


        [Route("Home/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProfile editprofile)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            

            var email = TempData["Cemail"];
            Passenger e = db.Passengers.Where(x => x.Email == (string)email).Single();
            e.FirstName = editprofile.FirstName;
            e.LastName = editprofile.LastName;
            e.PhoneNumber = editprofile.PhoneNumber;

            if (ModelState.IsValid)
            {

                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View("Index");
        }


        public ActionResult BookingHistory()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            if (Session["Email"] != null)
            {

                string email = (string)Session["Email"];
                Passenger p = db.Passengers.First(x => x.Email == email);
                
                int PassID = p.PassengerID;

                ViewBag.reservations = db.Reservations.Where(x => x.PassengerID == PassID).ToList();



                List<Reservation> reservationlist = db.Reservations.Where(x => x.PassengerID == PassID).ToList();

                ReservationViewModel reservationVM = new ReservationViewModel();

                List<ReservationViewModel> reservationVMList = reservationlist.Select(
                    x => new ReservationViewModel
                    {
                        Departure = x.Route.Departure,
                        Destination = x.Route.Destination,
                        DateOfReservation = x.DateOfReservation,
                        SeatsOfReservation = x.SeatsOfReservation,
                        PriceOfReservation = x.PriceOfReservation,
                        TripTime = x.DateTimeOfReservation
                    }).ToList();

                return View(reservationVMList);
            }
        
   
            else { return View("Login"); }
        }


        //SignUp Get Method
        [Route("Home/Signup")]
        public ActionResult Signup()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View();
        }

        //Signup from user
        [Route("Home/Signup")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignUp signup)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            Passenger p1 = new Passenger();
            p1.Email = signup.Email;
            p1.Password = signup.Password;
            p1.FirstName = signup.FirstName;
            p1.LastName = signup.LastName;
            p1.PhoneNumber = signup.PhoneNumber;
            if (db.Passengers.Count(x => x.Email == signup.Email) == 0)
            {
                db.Passengers.Add(p1);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.EmailExists = true;
                return View(signup);
            }

        }



        //Login Get
        [Route("Home/Login")]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            if (Session["Email"] != null)
            {
                
                return RedirectToAction("LoggedIn");
            }
            return View();
        }




        //Login Post
        [Route("Home/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            Passenger p = db.Passengers.SingleOrDefault(i => i.Email == login.Email);
            if (p == null)
            {
                ViewBag.EmailNotExists = true;
                return View(login);
            }
            if (p.Password == login.Password)
            {
                //Login Successful
                Session.Add("Email", login.Email);
                Session.Add("FirstName", p.FirstName);

                
                return RedirectToAction("LoggedIn");
            }

            //Login Unsuccessful
            else
            {
                ViewBag.NotMatching = true;
                return View(login);
            }
        }



        //Μια LoggedIn με redirect στο LogIn
        public ActionResult LoggedIn()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                Passenger p = db.Passengers.First(x => x.Email == email);
                ViewBag.FirstName = p.FirstName;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


        }


        //Logout
        public ActionResult LogOut()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            Session.Clear();
            return RedirectToAction("Index");
        }



        //Reservations (1st Way)
        public ActionResult ForATicket(string search)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View(db.Routes.Where(x => x.Departure.StartsWith(search) || x.Destination.StartsWith(search)));
        }



        //Reservations (2nd Way)
        [HttpGet]
        public ActionResult ForATicket2(int RouteID)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            int id = RouteID;
            TempData["id"] = id;
            TempData["id2"] = id;
            ViewBag.Message = id;

            KtelModels dbday = new KtelModels();

            ViewBag.Trip = new SelectList(dbday.Trips.Where(z => z.RouteID == id).Select(z => z.TripTime).ToList());

            return View();
        }


        //Reservations (2nd Way) Post
        [HttpPost]
        public ActionResult ForATicket2(RouteInfo routeinfo)
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();


            var id = TempData["id"];
            
            Capacity r1 = new Capacity();

            routeinfo.SpecificRouteID = id + ";" + routeinfo.RouteDay + routeinfo.RouteTime;
            r1.SpecificRouteID = routeinfo.SpecificRouteID;
            r1.RouteDay = routeinfo.RouteDay;
            r1.RouteTime = routeinfo.RouteTime;
            int CBusID = db.Trips.Where(x => x.TripTime == routeinfo.RouteTime).Select(z => z.BusID).FirstOrDefault();
            r1.BusID = CBusID;
            TempData["CBusID"] = CBusID;
            TempData["CBusID2"] = CBusID;

            int TakenSeats = routeinfo.Adults + routeinfo.Childrens + routeinfo.Unemployed;

            double RoutePrice = db.Routes.Where(x => x.RouteID == (int)id).Select(z => z.Price).FirstOrDefault();
            double TotalPrice = (routeinfo.Adults * RoutePrice) + ((routeinfo.Childrens * RoutePrice) * 0.6) + ((routeinfo.Unemployed * RoutePrice) * 0.4);

            TempData["TotalPrice"] = TotalPrice;
            TempData["TotalPrice2"] = TotalPrice;
            TempData["TakenSeats"] = TakenSeats;
            TempData["TakenSeats2"] = TakenSeats;
            string SpecificRID = routeinfo.SpecificRouteID;
            TempData["SpecificRID"] = SpecificRID;
            TempData["SpecificRID2"] = SpecificRID;
            TempData["RouteTime"] = routeinfo.RouteTime;

            if (db.Capacities.Count(x => x.SpecificRouteID == routeinfo.SpecificRouteID) == 0)
            {

                db.Capacities.Add(r1);
                db.SaveChanges();

                int TotalBusSeats = db.Buses.Where(x => x.BusID == (int)id).Select(z => z.Capacity).FirstOrDefault();
                int RouteAvailableSeats = (int)db.Capacities.Where(x => x.SpecificRouteID == routeinfo.SpecificRouteID).Select(x => x.AvailableSeats + TotalBusSeats - TakenSeats).FirstOrDefault();
                r1.AvailableSeats = RouteAvailableSeats;

                if (ModelState.IsValid)
                {
                    db.Entry(r1).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ReservationConfirm", "Home", new { routeinfo.SpecificRouteID });
            }
            else
            {
                Capacity c = db.Capacities.Where(x => x.SpecificRouteID == routeinfo.SpecificRouteID).Single();
                c.AvailableSeats -= TakenSeats;


                if (ModelState.IsValid)
                {

                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ReservationConfirm", "Home", new { routeinfo.SpecificRouteID });
            }


        }


        //Reservation Confirmation
        [HttpGet]
        public ActionResult ReservationConfirm()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            var id = TempData["id"];
            var TotalPrice = TempData["TotalPrice"];
            var TakenSeats = TempData["TakenSeats"];
            var SpecificRID = TempData["SpecificRID"];
            var RouteTime = TempData["RouteTime"];
            var BusID = TempData["CBusID"];

            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                Passenger p = db.Passengers.First(x => x.Email == email);
                int userID = p.PassengerID;

                ViewBag.UserFirstName = db.Passengers.Where(x => x.PassengerID == userID).Select(z => z.FirstName).FirstOrDefault();
                ViewBag.UserLastName = db.Passengers.Where(x => x.PassengerID == userID).Select(z => z.LastName).FirstOrDefault();
                ViewBag.UserPhoneNumber = db.Passengers.Where(x => x.PassengerID == userID).Select(z => z.PhoneNumber).FirstOrDefault();


                ViewBag.Departure = db.Routes.Where(x => x.RouteID == (int)id).Select(z => z.Departure).FirstOrDefault();
                ViewBag.Destination = db.Routes.Where(x => x.RouteID == (int)id).Select(z => z.Destination).FirstOrDefault();
                //ViewBag.TripTime = db.Trips.Where(x => x.RouteID == (int)id && x.BusID == (int)id).Select(z => z.TripTime).FirstOrDefault();
                ViewBag.TripTime = RouteTime;
                ViewBag.Duration = db.Routes.Where(x => x.RouteID == (int)id).Select(z => z.Duration).FirstOrDefault();
                ViewBag.DriverFirstName = db.Drivers.Where(x => x.BusID == (int)BusID).Select(z => z.FirstName).FirstOrDefault();
                ViewBag.DriverLastName = db.Drivers.Where(x => x.BusID == (int)BusID).Select(z => z.LastName).FirstOrDefault();

                ViewBag.RouteDay = db.Capacities.Where(x => x.SpecificRouteID == (string)SpecificRID).Select(z => z.RouteDay).FirstOrDefault();
                //ViewBag.RouteDay = Convert.ToString(CRouteTime);
                ViewBag.TotalSeats = TakenSeats;
                ViewBag.FinalPrice = TotalPrice + "Euro";

            }
            return View();
        }



        // Choose The Seats 5-6-2017(In Progress)
        [HttpPost]
        public ActionResult ReservationConfirm(FinalReservation finalreservation)
        {


            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            var id = TempData["id2"];
            var TotalPrice = TempData["TotalPrice2"];
            var TakenSeats = TempData["TakenSeats2"];
            var SpecificRID = TempData["SpecificRID2"];
            var BusID = TempData["CBusID2"];

            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                Passenger p = db.Passengers.First(x => x.Email == email);
                int userID = p.PassengerID;

                var RouteDay = db.Capacities.Where(x => x.SpecificRouteID == (string)SpecificRID).Select(z => z.RouteDay).FirstOrDefault();
                var RouteTime = db.Trips.Where(x => x.BusID == (int)BusID).Select(z => z.TripTime).FirstOrDefault();

                Route r = db.Routes.Where(x => x.RouteID == (int)id).Single();
                int routeid = r.RouteID;

                DateTime routedate = Convert.ToDateTime(RouteDay);
              
                var routetotalprice = TotalPrice;
                var routeseatsreservation = TakenSeats;

                Reservation reservation = new Reservation();

                reservation.PassengerID = userID;
                reservation.RouteID = routeid;
                reservation.DateOfReservation = routedate;
                reservation.DateTimeOfReservation = RouteTime;
                reservation.PriceOfReservation = Convert.ToInt32(routetotalprice);
                reservation.SeatsOfReservation = Convert.ToInt32(routeseatsreservation);
                

                db.Reservations.Add(reservation);
                db.SaveChanges();

            }

            return View("Thx");
        }


        //User Profile
        public ActionResult MyProfile()
        {
            

            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();


            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                Passenger p = db.Passengers.First(x => x.Email == email);
                ViewBag.FirstName = p.FirstName;
                ViewBag.LastName = p.LastName;
                ViewBag.PhoneNumber = p.PhoneNumber;
                ViewBag.Email = p.Email;
                ViewBag.Password = p.Password;
                int PassID = p.PassengerID;

                return View();
            }
            else { return View(); }
           
        }

        public ActionResult Thx()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View();
        }


        //HistoricalFacts
        public ActionResult _HistoricalFacts()
        {

            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();


            return View();

        }

        //CorporatePolicy
        public ActionResult _CorporatePolicy()
        {

            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();


            return View();
        }

        //Contact 
        public ActionResult Contact()
        {

            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();


            return View();
        }

        //TripInfo
        public ActionResult _TripInfo()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View();
        }    

        //FAQ
        public ActionResult _FAQ()
        {
            ViewBag.Pirroutes = db.Routes.Where(x => x.Departure == "Piraeus").ToList();
            ViewBag.Athroutes = db.Routes.Where(x => x.Departure == "Athens").ToList();

            return View();
        }
        
    }

}




