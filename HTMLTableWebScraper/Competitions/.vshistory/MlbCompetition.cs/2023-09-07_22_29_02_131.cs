using Ical.Net;
using Ical.Net.CalendarComponents;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;

namespace HTMLTableWebScraper
{
    public static class MlbCompetition
    {
        private static int fileSeqeunce = Convert.ToInt32((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0)).TotalSeconds);


        private static CalendarEvent GameCalenderEvent(dynamic game,
                                                      string timeZone,
                                                      string uidPrefix)
        {
            string home = game.teams.home.team.name;
            string away = game.teams.away.team.name;
            string field = game.venue.name;
            string matchNr = game.gamePk;
            string description = game.description;
            DateTime MatchDateTime;
            if (game.status.startTimeTBD.Value)
            {
                MatchDateTime = new DateTime(game.gameDate.Value.Year, game.gameDate.Value.Month, game.gameDate.Value.Day,
                                              18, 0, 0);
            }
            else
            {
                MatchDateTime = game.gameDate;

                #region Auckland Tuatara Timezone Fix
                if (game.venue.timeZone?.id?.Value == "Asia/Taipei" && game.teams.home?.team?.name?.Value == "Auckland Tuatara")
                {
                    MatchDateTime = MatchDateTime.AddHours(-5); // +8 minus +13

                    // Asia/Taipei = +8 
                    /*
                     # Rule	NAME	FROM	TO	 -	IN	ON		AT		SAVE	LETTER/S
                     Rule	Taiwan	1979	only -	Jul 1	    0:00	1:00	D
                     Rule	Taiwan	1979	only -	Oct	1	    0:00	0	    S               <-- DURING ABL SEASON  (CST)
                     # Zone	NAME		    STDOFF	    RULES	FORMAT	[UNTIL]
                     Zone	Asia/Taipei	    8:00	    Taiwan	C%sT

                     */

                    // Pacific/Auckland = +12 + 1 
                    /*
                     # Rule	NAME	FROM	TO	-	IN	ON		AT		SAVE	LETTER/S
                     Rule	NZ		2007	max	-	Sep	lastSun	2:00s	1:00	D               <-- DURING ABL SEASON  (NZDT)
                     Rule	NZ		2008	max	-	Apr	Sun>=1	2:00s	0		S
                     # Zone	NAME			STDOFF		RULES	FORMAT	[UNTIL]
                     Zone Pacific/Auckland	12:00		NZ		NZ%sT

                     */
                }
                #endregion
            }

            string EventSuffix = "";
            bool isPpd = false;
            if (game.status.detailedState.Value == "Postponed")
            {
                EventSuffix = game.officialDate.Value.ToString();
                isPpd = true;
            }

            Console.WriteLine($"{matchNr} : {home} - {away} om {MatchDateTime.ToShortDateString()} {MatchDateTime.ToShortTimeString()}");
            Debug.Print($"{matchNr} : {home} - {away} om {MatchDateTime.ToShortDateString()} {MatchDateTime.ToShortTimeString()}");
            return CreateCalenderEvents.CreateBaseballCalendarEvent(matchNr,
                                                                    home,
                                                                    away,
                                                                    field,
                                                                    MatchDateTime.Date,
                                                                    MatchDateTime.TimeOfDay,
                                                                    timeZone,
                                                                    uidPrefix,
                                                                    fileSeqeunce,
                                                                    3,
                                                                    description,
                                                                    game.status.startTimeTBD.Value,
                                                                    isPpd,
                                                                    EventSuffix);
        }

        private static Calendar GetICalCalender(string mlbUrl,
                                                string uidPrefix,
                                                string timeZone)
        {
            var calendar = new Calendar();
            calendar.AddTimeZone(timeZone);

            string jsonCode = "";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                jsonCode = client.DownloadString(mlbUrl);
            }

            dynamic schedule = JsonConvert.DeserializeObject<dynamic>(jsonCode);
            foreach (dynamic date in schedule.dates)
            {
                foreach (dynamic game in date.games)
                {
                    var gameCalendarEvent = GameCalenderEvent(game,
                                                              timeZone,
                                                              uidPrefix);
                    calendar.Events.Add(gameCalendarEvent);
                }
            }
            return calendar;
        }

        public static void WriteICalCalender(string mlbUrl,
                                             string uidPrefix,
                                             string timeZone,
                                             string fileNamePrefix)
        {
            var calendar = GetICalCalender(mlbUrl,
                                           uidPrefix,
                                           timeZone);
            calendar.WriteCalendar(fileNamePrefix + "_" + fileSeqeunce.ToString());
        }



    }
}
