using Ical.Net;
using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;

namespace GenerateBaseballCalendars.Competitions
{

    public class SchaatsEvent
    {
        public DateTime Date;
        public TimeSpan Start;
        public TimeSpan End;
        public string TimeZone;
    }

    public static class SchaatsenKalender
    {
        private static int fileSeqeunce = Convert.ToInt32((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0)).TotalSeconds);

        private static List<CalendarEvent> GameCalenderEvent(string titel,
                                                             string EventId,
                                                             string field,
                                                             List<SchaatsEvent> schaatsEvents,
                                                             string uidPrefix)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            int i = 0;
            foreach (var schaatsEvent in schaatsEvents)
            {
                events.Add(CreateCalenderEvents.CreateCalendarEvent(EventId,
                                                                titel,
                                                                field,
                                                                schaatsEvent.Date,
                                                                schaatsEvent.Start,
                                                                schaatsEvent.TimeZone,
                                                                uidPrefix,
                                                                fileSeqeunce,
                                                                (schaatsEvent.End - schaatsEvent.Start).TotalHours,
                                                                "",
                                                                i.ToString()));
                i++;
            }
            return events;
        }


        private static Calendar GetICalCalender(string uidPrefix)
        {
            var calendar = new Calendar();
            calendar.AddTimeZone("Europe/Amsterdam");
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("World Cup Kwalificatietoernooi", "KNSB-WCKT", "Heerenveen, NED",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 10, 27),
                                                                    Start = new TimeSpan(17, 0, 0),
                                                                    End = new TimeSpan(21, 42, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 10, 28),
                                                                    Start = new TimeSpan(13, 35, 0),
                                                                    End = new TimeSpan(17, 33, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 10, 29),
                                                                    Start = new TimeSpan(12, 55, 0),
                                                                    End = new TimeSpan(18, 50, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                }
                                                            }
                                                            , uidPrefix));
            var StartTime = new TimeSpan(14, 30, 0);
            var TimeZone = "Asia/Tokyo";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 1 Langebaan", "ISUWC1", "Obihiro, JPN",
                                                            new List<SchaatsEvent>()  {  
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 10),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 11),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 12),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));
            StartTime = new TimeSpan(17, 00, 0);
            TimeZone = "Asia/Shanghai";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 2 Langebaan", "ISUWC2", "Beijing, CHN",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 17),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 18),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 11, 19),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));

            StartTime = new TimeSpan(14, 00, 0);
            TimeZone = "Europe/Oslo";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 3 Langebaan", "ISUWC3", "Stavanger, NOR",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 01),
                                                                    Start = new TimeSpan(20, 00, 0),
                                                                    End = new TimeSpan(20, 00, 0).Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 02),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 03),
                                                                    Start = StartTime,
                                                                    End = StartTime.Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));

            StartTime = new TimeSpan(17, 00, 0);
            TimeZone = "Europe/Warsaw";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 4 Langebaan", "ISUWC4", "Thomaszów Mazowiecki, POL",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 08),
                                                                    Start = new TimeSpan(18, 30, 0),
                                                                    End = new TimeSpan(18, 30, 0).Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 09),
                                                                    Start = new TimeSpan(14, 00, 0),
                                                                    End = new TimeSpan(14, 00, 0).Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 10),
                                                                    Start = new TimeSpan(14, 45, 0),
                                                                    End = new TimeSpan(14, 45, 0).Add(new TimeSpan(3,0,0)),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));
            TimeZone = "Europe/Amsterdam";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("Daikin NK Afstanden", "KNSB-NKA", "Heerenveen, NED",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 28),
                                                                    Start = new TimeSpan(18, 30, 0),
                                                                    End = new TimeSpan(21, 59, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 29),
                                                                    Start = new TimeSpan(18, 30, 0),
                                                                    End = new TimeSpan(21, 22, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2023, 12, 30),
                                                                    Start = new TimeSpan(12, 45, 0),
                                                                    End = new TimeSpan(17, 41, 0),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));

            TimeZone = "Europe/Amsterdam";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU EK Afstanden", "ISUEKA", "Heerenveen, NED",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 01, 5),
                                                                    Start = new TimeSpan(19, 30, 0),
                                                                    End = new TimeSpan(22, 15, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 01, 6),
                                                                    Start = new TimeSpan(14, 30, 0),
                                                                    End = new TimeSpan(18, 00, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 01, 7),
                                                                    Start = new TimeSpan(14, 15, 0),
                                                                    End = new TimeSpan(18, 00, 0),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));


            TimeZone = "America/Denver";
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 5 Langebaan", "ISUWC5", "Salt Lake City, USA",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 01, 26),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 01, 27),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 1, 28),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                }
                                                            }
                                                            , uidPrefix));


            TimeZone = "America/Toronto";
            calendar.Events.AddRange(GameCalenderEvent("ISU World Cup 6 Langebaan", "ISUWC6", "Québec, CAN",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 02, 2),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 3),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 4),
                                                                    Start = new TimeSpan(20, 30, 0),
                                                                    End = new TimeSpan(23, 30, 0),
                                                                    TimeZone = "Europe/Amsterdam"
                                                                }
                                                            }
                                                            , uidPrefix));

            TimeZone = "America/Edmonton";
            StartTime = new TimeSpan(12, 30, 0);
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU WK Afstanden", "ISUWKA", "Calgary, CAN",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 02, 15),
                                                                    Start = StartTime,
                                                                    End = new TimeSpan(16, 15, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 02, 16),
                                                                    Start = StartTime,
                                                                    End = new TimeSpan(15, 00, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 17),
                                                                    Start = StartTime,
                                                                    End = new TimeSpan(16, 30, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 18),
                                                                    Start = StartTime.Add(new TimeSpan(0,30,0)),
                                                                    End = new TimeSpan(17, 00, 0),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));

            TimeZone = "Europe/Amsterdam";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("Daikin NK Allround & Sprint", "KNSB-NKALLSP", "Heerenveen, NED",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 24),
                                                                    Start = new TimeSpan(11, 00, 0),
                                                                    End = new TimeSpan(17, 45, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 2, 25),
                                                                    Start = new TimeSpan(11, 00, 0),
                                                                    End = new TimeSpan(17, 15, 0),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));
            TimeZone = "Europe/Berlin";
            //Localized
            calendar.Events.AddRange(GameCalenderEvent("ISU WK Allround en Sprint Langebaan", "ISUWKALLSP", "Inzell, GER",
                                                            new List<SchaatsEvent>()  {
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 3, 7),
                                                                    Start = new TimeSpan(19, 00, 0),
                                                                    End = new TimeSpan(22, 15, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 3, 8),
                                                                    Start = new TimeSpan(18, 30, 0),
                                                                    End = new TimeSpan(22, 00, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 3, 9),
                                                                    Start = new TimeSpan(12, 45, 0),
                                                                    End = new TimeSpan(17, 30, 0),
                                                                    TimeZone = TimeZone
                                                                },
                                                                new SchaatsEvent() {
                                                                    Date = new DateTime(2024, 3, 10),
                                                                    Start = new TimeSpan(13, 15, 0),
                                                                    End = new TimeSpan(17, 17, 0),
                                                                    TimeZone = TimeZone
                                                                }
                                                            }
                                                            , uidPrefix));

            return calendar;
        }




        public static void WriteICalCalender(string uidPrefix,
                                             string fileNamePrefix)
        {
            var calendar = GetICalCalender(uidPrefix);
            calendar.WriteCalendar(fileNamePrefix + "_" + fileSeqeunce.ToString());
        }
    }
}
