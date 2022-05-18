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

            DateTime timeSlotJour = orderDicoViewModels.Order.timeSlot.Day;

            TimeSlot timeSlot = TimeSlot.GetTimeSlot(_timeSlotDAL, ts);
            timeSlot.Day = timeSlotJour;

            orderDicoViewModels.Order.timeSlot = timeSlot;

            HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            return Redirect("Validate");
        }

        public IActionResult Validate()
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            Shop shop = orderDicoViewModels.Order.shop;
            orderDicoViewModels.Order.timeSlot.shop = shop;

            int nbr = orderDicoViewModels.Order.timeSlot.CheckIfAvalaible(_timeSlotDAL, shop);

            if ( nbr < 10)
            {
                return Redirect("/Order/SelectDay");
            }

            return View();
        }

    }
}
