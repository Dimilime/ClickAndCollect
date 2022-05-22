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
            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                Shop shop = orderDicoViewModels.Order.Shop;

                DateTime timeSlotJour = orderDicoViewModels.Order.TimeSlot.Day;

                TimeSlot timeSlot = TimeSlot.GetTimeSlot(_timeSlotDAL, ts);
                timeSlot.Day = timeSlotJour;

                orderDicoViewModels.Order.TimeSlot = timeSlot;

                int nbr = orderDicoViewModels.Order.TimeSlot.CheckIfAvalaible(_timeSlotDAL, shop);

                if (nbr >= 10)
                {
                    TempData["ErrorNbr"] = "Plus de place disponible !!";
                    return Redirect("/Shop/SelectCanva");
                }

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

                return Redirect("/Product/Summary");
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }
        }

    }
}
