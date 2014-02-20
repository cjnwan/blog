using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc.Json;
using Domain.Model;
using Service;
using WebUI.ViewModel;

namespace WebUI.Areas.Admin.Controllers
{
    public class EventController : Controller
    {      
        public ActionResult Edit(string id)
        {
            var e = new EventManager().Get(id) ?? new CalendarEvent();
            return View(e);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            new EventManager().EditEvent(form["Id"], form["Text"], start, end);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


        public ActionResult Create()
        {
            return View(new CalendarEvent
                {
                    Start = Convert.ToDateTime(Request.QueryString["start"]),
                    End = Convert.ToDateTime(Request.QueryString["end"])
                });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            new EventManager().CreateEvent(start, end, form["Text"]);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }
    }
}
