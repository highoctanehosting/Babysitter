using System;

namespace Babysitter_Kata___C_Sharp
{
    class Program
    {
        DateTime startTime;
        DateTime endTime;
        DateTime bedTime;
        DateTime midNight = DateTime.Today.AddDays(1).AddHours(0);

        readonly System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Babysitter Kata\n");

            var job = new Program();

            job.SetStartTime();
            job.SetEndTime();
            job.SetBedTime();
            job.CalculateWages(job.startTime, job.endTime, job.bedTime);
        }

        public void SetStartTime()
        {
            string startTimeCorrect = "NO";

            while (startTimeCorrect == "NO")
            {
                Console.WriteLine("What time do you start babysitting? (Hour + AM/PM Format)\n");
                DateTime.TryParseExact(Console.ReadLine(), "h tt", ci, System.Globalization.DateTimeStyles.AssumeLocal, out startTime);
                Console.Clear();
                Console.WriteLine("You entered " + startTime.ToString("h tt") + ". Is that correct?\n");
                string s_correct = Console.ReadLine().ToUpper();

                if (s_correct == "YES" && startTime >= DateTime.Parse("5 PM"))
                {
                    startTimeCorrect = "YES";
                    Console.Clear();
                    Console.WriteLine("Great! Let's move on.\n\n");
                }
                else if (startTime < DateTime.Parse("5 PM"))
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, you're not allowed to start before 5 PM. Please try again.\n\n");
                }

            }
        }

        public void SetBedTime()
        {
            string bedTimeCorrect = "NO";
            string tempBedTimeString;

            while (bedTimeCorrect == "NO")
            {       
                Console.WriteLine("What time is bedtime? (Hour + AM/PM Format)\n");
                tempBedTimeString = Console.ReadLine();
                int intHour = int.Parse(tempBedTimeString.Substring(0, 1));
                DateTime.TryParseExact(tempBedTimeString, "h tt", ci, System.Globalization.DateTimeStyles.AssumeLocal, out bedTime);

                if (intHour >= 12 || intHour <= 4)
                {
                    DateTime tempDate = bedTime.AddDays(1);
                    bedTime = tempDate;
                    Console.Clear();
                    Console.WriteLine("You entered " + bedTime.ToString("h tt") + ", which is tomorrow. Is that correct?\n");
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("You entered " + bedTime.ToString("h tt") + ". Is that correct?\n");
                }

                string b_correct = Console.ReadLine().ToUpper();

                if (b_correct == "YES" && bedTime >= startTime && bedTime <= endTime)
                {
                    bedTimeCorrect = "YES";
                    Console.Clear();
                    Console.WriteLine("Great! Let's move on.\n\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, the bedtime (" + bedTime.ToString("h tt") + ") " +
                        "must be after your start time (" + startTime.ToString("h tt") + ") " +
                        "and before your shift is over (" + endTime.ToString("h tt") + ")\n\n");
                }
            }
        }

        public void SetEndTime()
        {
            string endTimeCorrect = "NO";
            string tempEndTimeString;
            
            while (endTimeCorrect == "NO")
            {
                Console.WriteLine("What time will you be done babysitting? (Hour + AM/PM Format)\n");
                tempEndTimeString = Console.ReadLine();
                int intHour = int.Parse(tempEndTimeString.Substring(0,1));
                DateTime.TryParseExact(tempEndTimeString, "h tt", ci, System.Globalization.DateTimeStyles.AssumeLocal, out endTime);

                if (intHour >= 12 || intHour <= 4)
                {
                    DateTime tempDate = endTime.AddDays(1);
                    endTime = tempDate;
                    Console.Clear();
                    Console.WriteLine("You entered " + endTime.ToString("h tt") + ", which is tomorrow. Is that correct?\n");
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("You entered " + endTime.ToString("h tt") + ". Is that correct?\n");
                }

                string e_correct = Console.ReadLine().ToUpper();

                if (e_correct == "YES" && endTime > startTime)
                {
                    endTimeCorrect = "YES";
                    Console.Clear();
                    Console.WriteLine("Great! Let's move on.\n\n");
                }

            }
        }

        public void CalculateWages(DateTime s, DateTime e, DateTime b)
        {
            int STBWage = 12;
            int BTMWage = 8;
            int MTEWage = 16;
            int totalWages = 0;
            
            TimeSpan startToBed = s - b;
            TimeSpan bedToMid = b - midNight;
            TimeSpan midToEnd = midNight - e;

            string stringStartToBedHours = startToBed.Duration().ToString().Substring(0,2);
            string stringBedToMidHours = bedToMid.Duration().ToString().Substring(0, 2);
            string stringMidToEndHours = midToEnd.Duration().ToString().Substring(0, 2);

            int totalSTBSpan = int.Parse(stringStartToBedHours);
            int totalBTMSpan = int.Parse(stringBedToMidHours);
            int totalMTESpan = int.Parse(stringMidToEndHours);

            Console.Clear();

            Console.WriteLine("Here is the breakdown of your wages for this shift:\n\n");

            Console.WriteLine("You worked from " + s.ToString("h tt") + " to bedtime, which was at " + b.ToString("h tt") + " for a total of " + totalSTBSpan + " hours.\n");
            Console.WriteLine("For this part of your shift, you made $" + (STBWage * totalSTBSpan) + ".\n\n");

            totalWages += (STBWage * totalSTBSpan);

            Console.WriteLine("Bedtime to Midnight was " + totalBTMSpan + " hours, so you made $" + (BTMWage * totalBTMSpan) + " for this period.\n\n");

            totalWages += (BTMWage * totalBTMSpan);

            Console.WriteLine("In the wee hours of the night from midnight 'til the end of your shift. You worked " + totalMTESpan + " hours.\n");
            Console.WriteLine("That earned you a whopping $" + (MTEWage * totalMTESpan) + ".\n\n");

            totalWages += (MTEWage * totalMTESpan);

            Console.WriteLine("Your grand total is $" + totalWages + "!!! Congrats, baller!");

        }
    }
}
