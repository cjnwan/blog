using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using Domain.Model;
using Service;
using WebUI.ViewModel;

namespace WebUI.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
       



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Day()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Week()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Month()
        {
            return new Dpm().CallBack(this);
        }

        //public ActionResult Backend()
        //{
        //    return new Dpc().CallBack(this);
        //}

        private class Dpc : DayPilotCalendar
        {
            protected override void OnInit(InitArgs initArgs)
            {

                UpdateWithMessage("Welcome!", CallBackUpdateType.Full);
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                new EventManager().MoveEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                new EventManager().MoveEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                new EventManager().CreateEvent(e.Start, e.End, "New event");
                Update();
            }

            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                e.Areas.Add(
                    new Area().Right(3)
                              .Top(3)
                              .Width(15)
                              .Height(15)
                              .CssClass("event_action_delete")
                              .JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"));
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime) e.Data["day"];
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                    case "delete":
                        new EventManager().RemoveEvent((string) e.Data["e"]["id"]);
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                }
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events =
                    new EventManager().GetEvent(StartDate, StartDate.AddDays(Days));

                DataIdField = "Id";
                DataTextField = "Title";
                DataStartField = "Start";
                DataEndField = "End";
            }
        }


        private class Dpm : DayPilotMonth
        {
            protected override void OnInit(DayPilot.Web.Mvc.Events.Month.InitArgs e)
            {
                
                Update();
            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Month.EventResizeArgs e)
            {
                new EventManager().MoveEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Month.EventMoveArgs e)
            {
                new EventManager().MoveEvent(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnTimeRangeSelected(DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedArgs e)
            {
                new EventManager().CreateEvent(e.Start, e.End, "New event");
                Update();
            }

            protected override void OnBeforeEventRender(DayPilot.Web.Mvc.Events.Month.BeforeEventRenderArgs e)
            {
                e.Areas.Add(
                    new Area().Right(3)
                              .Top(3)
                              .Width(15)
                              .Height(15)
                              .CssClass("event_action_delete")
                              .JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"));
            }

            protected override void OnCommand(DayPilot.Web.Mvc.Events.Month.CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime) e.Data["day"];
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                    case "delete":
                        new EventManager().RemoveEvent((string)e.Data["e"]["id"]);
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                }
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = new EventManager().GetEvent(VisibleStart, VisibleEnd);

                DataIdField = "Id";
                DataTextField = "title";
                DataStartField = "Start";
                DataEndField = "End";
            }
        }
    }

}

