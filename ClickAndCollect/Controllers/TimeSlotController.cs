using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using ClickAndCollect.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotDAL _timeSlotDAL;

        public TimeSlotController(ITimeSlotDAL timeSlotDAL)
        {
            _timeSlotDAL = timeSlotDAL;
        }


        public IActionResult GetCanva(TimeSlot ts)//Gerer l'afficher des dispo
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            Shop shop = orderDicoViewModels.Order.shop;

            DateTime timeSlotJour = orderDicoViewModels.Order.timeSlot.Day;

            TimeSlot timeSlot = TimeSlot.GetTimeSlot(_timeSlotDAL, ts);
            timeSlot.Day = timeSlotJour;

            orderDicoViewModels.Order.timeSlot = timeSlot;

            int nbr = orderDicoViewModels.Order.timeSlot.CheckIfAvalaible(_timeSlotDAL, shop);

            if (nbr >= 10)
            {
                TempData["ErrorNbr"] = "Plus de place disponible !!";
                return Redirect("/Shop/SelectCanva");
            }

            HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            return Redirect("summary");
        }

        public IActionResult summary()
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            




            return View();
        }

    }
}
