using System;
using System.Data;
using System.Linq;
using System.Net;
using Ical.Net.Serialization;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HTMLTableWebScraper
{


    public static class CalendarExtention
    {
        public static void WriteCalendar(this Ical.Net.Calendar calendar, string filename)
        {
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar).Replace("PRODID:-//github.com/rianjs/ical.net//NONSGML ical.net 4.0//EN", "PRODID:-//  //NONSGML //EN");
            string docPath = "D:\\dev-TryOuts\\HTMLTableWebScraper\\Export";
            File.WriteAllText(Path.Combine(docPath, filename +  ".ics"), serializedCalendar);
        }
    }



    internal class Program
    {
        private static string timeZone;
        private static string uidPrefix;
        private static int fileSeqeunce = Convert.ToInt32((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0)).TotalSeconds); 


        static void Main(string[] args)
        {
            wbscCompetition();
            knbsbCompetition();
            mlbCompetition();
            //mlbTransactions();
        }

        static void mlbTransactions()
        {


            using (WebClient client = new WebClient()) 
            {
                string jsonCode = "";
                jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/transactions?teamId=142&startDate=2018-01-01&endDate=2023-03-01");
                TransactionsRoot myDeserializedClass = JsonConvert.DeserializeObject<TransactionsRoot>(jsonCode);
                var listType = myDeserializedClass.transactions.GroupBy(t => (t.typeCode, t.typeDesc),
                                                                         (key, group) => new { 
                                                                                                key.typeCode, 
                                                                                                key.typeDesc,
                                                                                                Count = group.Count(),
                                                                                                transactions = group
                                                                                             }
                                                                       );
                var list = myDeserializedClass.transactions.Where( t => t.person != null)
                                                           .GroupBy(t => (t.personId, t.personFullname), 
                                                                    (key, group) => new { 
                                                                                        key.personId,
                                                                                        key.personFullname,
                                                                                        group.Last().date,
                                                                                        group.Last().description,
                                                                                        transactions = group.OrderByDescending(t => t.date).ToList()
                                                                                       }
                                                                   )
                                                           .OrderByDescending(p => p.date)
                                                           .ToList();
                                                           

            }

        }

        static void mlbCompetition()
        {
            //

            //string filename = "";
            //timeZone = "Etc/UTC";


                //mlb
                /*
                jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=1&startDate=2022-10-06&endDate=2022-11-10");
                filename = "MLB-Playoff-2022" + "_" + fileSeqeunce.ToString();
                uidPrefix = "NL-HH-Reg-2022-";
                */

                //abl

                //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01");
                //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData");
                //filename = "ALB-Regular-2022-2023" + "_" + fileSeqeunce.ToString();
                //uidPrefix = "AU-ABL-Reg-2022-2023-";


                //wbc

                //jsonCode = client.DownloadString();
                //filename = "WBC-2023" + "_" + fileSeqeunce.ToString();
                //uidPrefix = "WBC-2023-";


            //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2023-03-11&endDate=2023-03-11&gameTypes=F,D,L,W&teamId=944&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData");
            //filename = "WBC-2023" + "_" + fileSeqeunce.ToString();
            //uidPrefix = "WBC-2023-";




            try
            {
                // WBC 2023 
                MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2022-07-01&endDate=2023-04-01&gameTypes=F,D,L,W&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                                                  "WBC-2023-",
                                                  "Etc/UTC",
                                                  "WBC-2023"
                                                 );

                // WBC 2023 -Final
                MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2023-03-11&endDate=2023-03-11&gameTypes=F,D,L,W&teamId=944&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                                                  "WBC-2023-",
                                                  "Etc/UTC",
                                                  "WBC-2023-filter"
                                                 );


                //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01");
                //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData");
                //filename = "ALB-Regular-2022-2023" + "_" + fileSeqeunce.ToString();
                //uidPrefix = "AU-ABL-Reg-2022-2023-";

                MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                                                  "AU-ABL-Reg-2022-2023-",
                                                  "Etc/UTC",
                                                  "ALB-Regular-2022-2023"
                                                 );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void wbscCompetition()
        {
            try
            {

                // EK 2023 
                //WbscCompetition.WriteICalCalender("https://www.wbsceurope.org/en/events/2023-european-baseball-championship/schedule-and-results",
                //                                  "WBSC-EU-EC-2023-",
                //                                  "Europe/Prague",
                //                                  "EC-2023"
                //                                 );

                // HHW 2022
                //WbscCompetition.WriteICalCalender("https://stats.knbsbstats.nl/en/events/2022-honkbalweek-haarlem/schedule-and-results",
                //                                  "NL-HH-Reg-2022-",
                //                                  "Europe/Amsterdam",
                //                                  "HHW-2022"
                //                                 );

                // WK U18 2023
                //WbscCompetition.WriteICalCalender("https://www.wbsc.org/en/events/2023-u18-baseball-world-cup/schedule-and-results",
                //                                  "WBSC-WCU18-2023-",
                //                                  "Asia/Taipei",
                //                                  "WC-U18-2023"
                //                                 );

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void knbsbCompetition()
        {


            try
            {
                //knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6399/programma/",
                //                                   "NL-HH-WCTop-2023-",
                //                                   "Europe/Amsterdam",
                //                                   "KNBSB-WCTop-2023"
                //                                  );
                //knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6400/programma/",
                //                                   "NL-HH-WCBottom-2023-",
                //                                   "Europe/Amsterdam",
                //                                   "KNBSB-WCBottom-2023"
                //                                  );
                //knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6401/programma/",
                //                                   "NL-HH-PO-2023-",
                //                                   "Europe/Amsterdam",
                //                                   "KNBSB-PO-2023"
                //                                  );

                KnbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6402/programma/",
                                                   "NL-HH-HS-2023-",
                                                   "Europe/Amsterdam",
                                                   "KNBSB-HS-2023"
                                                  );

                knbsbInWbscCompetition.WriteICalCalender("https://stats.knbsbstats.nl/en/events/2023-hoofdklasse-honkbal/schedule-and-results",
                                                         "NL-HH-{RoundRef}-2023-",
                                                         "Europe/Amsterdam",
                                                         "KNBSB-2023"
                                                        );

            }
            catch (Exception ex)
            {
                throw;
            }

            #region old code
            //using (WebClient client = new WebClient())
            //{
            //    client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5616/programma/");
            //    //filename = "HonkbalHoofdklasse - 2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5781/programma/");
            //    //filename = "Top4-2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5783/programma/");
            //    //filename = "WildCard-2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5784/programma/");
            //    //filename = "WildCardBottum-2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5786/programma/");
            //    //filename = "Playoffs-2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/5785/programma/");
            //    //filename = "HollandSeries-2022";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6336/programma/");
            //    //filename = "HonkbalHoofdklasse - 2023";

            //    //htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6397/programma/");
            //    //filename = "Top4-2023";

            //    htmlCode = client.DownloadString("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6399/programma/");
            //    filename = "Top4-2023";

            //}
            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();


            ////HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(htmlCode);

            //var calendar = new Ical.Net.Calendar();
            //calendar.AddTimeZone(timeZone);

            //try
            //{
            //    var headers = doc.DocumentNode.SelectSingleNode("//div[@class='container-responsive']");
            //    foreach (var speldag in headers.Descendants("h4"))
            //    {
            //        Console.WriteLine(speldag.InnerText.Trim());
            //        CultureInfo provider = CultureInfo.CreateSpecificCulture("nl-NL");
            //        DateTime date = DateTime.Parse(speldag.InnerText.Trim(),provider);
            //        foreach (var wedstrijd in speldag.NextSibling.NextSibling.Descendants("tr"))
            //        {
            //            var data = wedstrijd.Descendants("td");
            //            #region old code
            //            //var home = System.Web.HttpUtility.HtmlDecode(data.ElementAt(0).InnerText.Trim());
            //            //var away = System.Web.HttpUtility.HtmlDecode(data.ElementAt(1).InnerText.Trim());
            //            //var field = System.Web.HttpUtility.HtmlDecode(data.ElementAt(2).InnerText.Trim());
            //            //var matchNr = data.ElementAt(3).Descendants("span").Where(n => n.HasClass("external-match-id")).First().InnerText.Trim();
            //            //var matchTime = data.ElementAt(3).Descendants("span").Where(n => n.HasClass("match-time")).First().InnerText.Trim();
            //            //TimeSpan time = TimeSpan.Parse(matchTime);
            //            //DateTime MatchDateTime = date + time;
            //            //Console.WriteLine($"{matchNr} : {home} - {away} om {matchTime}");
            //            //var game = CreateCalenderEvents.CreateBaseballCalendarEvent(matchNr, 
            //            //                                                            home, 
            //            //                                                            away, 
            //            //                                                            field, 
            //            //                                                            date, 
            //            //                                                            time,
            //            //                                                            timeZone,
            //            //                                                            uidPrefix,
            //            //                                                            fileSeqeunce);
            //            #endregion
            //            calendar.Events.Add(knbsbCompetition.GameCalenderEvent(data, date, timeZone, uidPrefix));
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}



            ////DataTable table = new DataTable();
            ////foreach (HtmlNode header in headers)
            ////    table.Columns.Add(header.InnerText); // create columns from th
            ////                                         // select rows with td elements 
            ////foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
            ////    table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
            ////calendar.WriteCalendar("HonkbalHoofdklasse-2022");
            //calendar.WriteCalendar(filename);

            //Console.WriteLine("Hello World!");
            #endregion
        }
        /*
         * 
         * 
         *             List<WebAction> actions = new List<WebAction>();
                    //goto home url - https://www.bing.com/
                    actions.Add(new SimpleWebAction(new UrlWebStep("open search", "https://www.bing.com/"),
                                                    new LocatorCheckValidator(



        (           actions.Add(new SimpleWebAction(new UrlWebStep("open search", "https://www.bing.com/"),
                        new LocatorCheckValidator(new SimpleHtmlElementLocator("q search box",
                        new AttributeHtmlElementMatcher("q search box", "name", "q"))), waitForEvent: true));

                    //submit a search
                    actions.Add(new SimpleWebAction(new FormWebStep("submit search", new IdElementLocator("locate form to submit", "sb_form"), new Dictionary<String, String>
                                {
                                    {"sb_form_q", "WebScraper.NET github"}
                                }
                    ), waitForEvent: true));
                    //load results
                    actions.Add(new ExtractWebAction<String>(new StringHtmlElementDataExtractor("href"), "firstNavLink",
                        new TagElementLocator("match results", "ol", false, "firstResultLink",
                        new SimpleChildHtmlElementLocator("find first link",
                        filter: new SimpleChildHtmlElementLocator("get first a", new TagHtmlElementMatcher("match first a", "a"))),
                        new IdHtmlElementMatcher("match results ol", "b_results"))));

                    //goto first result
                    actions.Add(new SimpleWebAction(new UrlWebStep("open search", "firstNavLink"), new TitleWebValidator("GitHub - perusworld/WebScraper.NET: A .Net based Web Scraper using the WebBrowser control"), waitForEvent: true));
        */

    }
}
