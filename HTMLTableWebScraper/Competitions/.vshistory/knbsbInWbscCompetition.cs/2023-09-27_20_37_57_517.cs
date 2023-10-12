using Ical.Net;
using Ical.Net.CalendarComponents;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace HTMLTableWebScraper
{
    public static class knbsbInWbscCompetition
    {
        private static int fileSeqeunce = Convert.ToInt32((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0)).TotalSeconds);

        private static CalendarEvent GameCalenderEvent(dynamic game,
                                                      string timeZone,
                                                      string uidPrefix)
        {
            string home = game.homelabel.ToString();
            string away = game.awaylabel.ToString();
            string field = game.stadium.ToString();
            string knbsbNr = game.gamecode.ToString();
            string compRound = game.gametypelabel.ToString();
            string compRef;
            switch (compRound)
            {
                case "Play-offs A":
                case "Play-offs B":
                    compRef = "PO";
                    break;
                case "Wildcard Bottom":
                    compRef = "WCBottom";
                    break;
                case "Wildcard Top":
                    compRef = "WCTop";
                    break;
                case "Bovenste-4":
                    compRef = "Reg";
                    break;
                case "Holland Series":
                    compRef = "Reg";
                    break;
                default:
                    return null;
            }

            uidPrefix = uidPrefix.Replace("{RoundRef}", compRef);
            DateTime MatchDateTime = Convert.ToDateTime(game.start.ToString());
            Console.WriteLine($"{knbsbNr} : {home} - {away} om {MatchDateTime.ToShortDateString()} {MatchDateTime.ToShortTimeString()}");
            return  CreateCalenderEvents.CreateBaseballCalendarEvent(knbsbNr,
                                                                     home,
                                                                     away,
                                                                     field,
                                                                     MatchDateTime.Date,
                                                                     MatchDateTime.TimeOfDay,
                                                                     timeZone,
                                                                     uidPrefix,
                                                                     fileSeqeunce);

        }

        private static Calendar GetICalCalender(string wbscUrl, 
                                               string uidPrefix, 
                                               string timeZone)
        {
            var calendar = new Calendar();
            calendar.AddTimeZone(timeZone);

            string htmlCode;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                htmlCode = client.DownloadString(wbscUrl);
            }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlCode);
            var dataEncoded = doc.DocumentNode.Descendants("div").Where(x => x.Id == "app").First().Attributes.Where(a => a.Name == "data-page").First().Value;
            var dataUncoded = WebUtility.HtmlDecode(dataEncoded);
            dynamic dataDeserialized = JsonConvert.DeserializeObject(dataUncoded);
            dynamic props = dataDeserialized.props;
            dynamic games = props.games;

            foreach (dynamic game in games)
            {
                var gameCalendarEvent = knbsbInWbscCompetition.GameCalenderEvent(game,
                                                                                 timeZone,
                                                                                 uidPrefix);
                if (gameCalendarEvent != null)
                {
                    calendar.Events.Add(gameCalendarEvent);
                }
            }

            return calendar; 
        }

        public static void WriteICalCalender(string wbscUrl,
                                             string uidPrefix,
                                             string timeZone,
                                             string fileNamePrefix)
        {
            var calendar = GetICalCalender(wbscUrl,
                                           uidPrefix,
                                           timeZone);
            calendar.WriteCalendar(fileNamePrefix + "_" + fileSeqeunce.ToString());
        }
    }
}
