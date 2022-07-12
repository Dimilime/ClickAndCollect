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


        public IActionResult GetCanva(int idCanva)
        {
            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
                TimeSlot timeSlot = TimeSlot.GetTimeSlot(_timeSlotDAL, idCanva);
                
                orderDicoViewModels.Order.TimeSlot.Start = timeSlot.Start;
                orderDicoViewModels.Order.TimeSlot.End = timeSlot.End;

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
