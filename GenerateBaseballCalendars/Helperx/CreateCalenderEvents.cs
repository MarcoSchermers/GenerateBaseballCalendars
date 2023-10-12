using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;

namespace GenerateBaseballCalendars
{
    public static class CreateCalenderEvents
    {
        public static CalendarEvent CreateBaseballCalendarEvent(string id,
                                                                string home,
                                                                string away,
                                                                string stadium,
                                                                DateTime Datum,
                                                                TimeSpan? tijd,
                                                                string timeZone,
                                                                string uidPrefix,
                                                                int fileSeqeunce,
                                                                double Duur = 3,
                                                                string description = "",
                                                                bool tbd = false,
                                                                bool isPpd = false,
                                                                string EventSuffix = "")
        {
            bool isAllDay;
            DateTime BeginTijd;
            int uur = (int)Math.Truncate(Duur);
            int minute = (int)((Duur - uur) * 60);

            string Titel = home + " - " + away;
            if (tbd)
            {
                Titel += " (TBD)";
            }
            else if (isPpd)
            {
                Titel += " (ppd)";
            }

            if (tijd == null)
            {
                isAllDay = true;
                BeginTijd = Datum;
            }
            else
            {
                isAllDay = false;
                BeginTijd = Datum + tijd.Value;
            }

            var StartTime = new CalDateTime(BeginTijd, timeZone);

            var ev = new CalendarEvent
            {
                Uid = uidPrefix + id + EventSuffix,
                Sequence = fileSeqeunce,
                Start = StartTime,
                End = StartTime.AddHours(uur).AddMinutes(minute),
                Summary = Titel,
                Location = stadium,
                Description = description,
                IsAllDay = isAllDay
            };

            return ev;
        }
    }
}
