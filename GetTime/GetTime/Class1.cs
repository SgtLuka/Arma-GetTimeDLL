using RGiesecke.DllExport;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace WGGetTime
{
    public class DllEntry
    {
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            outputSize--;

            string WG_Input = function;

            if (WG_Input.Equals("hour") || WG_Input.Equals("minute") || WG_Input.Equals("second") || WG_Input.Equals("time"))
            {
                if (WG_Input.Equals("hour"))
                {
                    string WG_Time = DateTime.Now.ToString("hh");
                    output.Append(WG_Time);
                    return;
                }
                if (WG_Input.Equals("minute"))
                {
                    string WG_Time = DateTime.Now.ToString("mm");
                    output.Append(WG_Time);
                    return;
                }
                if (WG_Input.Equals("second"))
                {
                    string WG_Time = DateTime.Now.ToString("ss");
                    output.Append(WG_Time);
                    return;
                }
                if (WG_Input.Equals("time"))
                {
                    string WG_Time = DateTime.Now.ToString("hh:mm:ss");
                    output.Append(WG_Time);
                    return;
                }
            }
            else
            {
                int WG_Input1 = WG_Input.IndexOf(',');
                int WG_Input2 = WG_Input.LastIndexOf(',');
                int RestartHours = int.Parse(WG_Input.Substring(0, WG_Input1));
                int RestartMinute = int.Parse(WG_Input.Substring(WG_Input1 + 1, WG_Input2 - WG_Input1 - 1));
                int RestartEvery = int.Parse(WG_Input.Substring(WG_Input2 + 1));
                int RestartMaths = 24 / RestartEvery;
                int[] ArrayForMaths = Enumerable.Range(1, RestartMaths + 1).ToArray();
                string WG_FullTime = DateTime.Now.ToString("hh:mm:ss");
                DateTime dFrom;
                DateTime dTo;
                int hour = 0;
                int mins = 0;
                int secs = 0;
                string timeDiff = "";
                int ProperRestartHour;

                foreach (int element in ArrayForMaths)
                {
                    ProperRestartHour = RestartHours + (RestartEvery * element);

                    if (ProperRestartHour > 24)
                    {
                        ProperRestartHour = ProperRestartHour - 24;
                    }

                    foreach (int element2 in new[] { ProperRestartHour })
                    {
                        string NextRestart = element2 + ":" + RestartMinute + ":00";
                        if (DateTime.TryParse(WG_FullTime, out dFrom) && DateTime.TryParse(NextRestart, out dTo))
                        {
                            TimeSpan TS = dTo - dFrom;
                            hour = TS.Hours;
                            mins = TS.Minutes;
                            secs = TS.Seconds;
                            if (hour < 0)
                            {
                                hour = hour + RestartEvery;
                            }
                            if (mins < 0)
                            {
                                mins = mins + 60;
                                hour = hour - 1;
                            }
                            timeDiff = "[" + hour.ToString("00") + "," + mins.ToString("00") + "," + secs.ToString("00") + "]";
                        }
                        int Maths180 = (hour * 60) + mins;
                        if (hour <= RestartEvery && hour >= 0 && timeDiff != "00:00:00" && Maths180 <= 180 && secs > 0)
                        {
                            output.Append(timeDiff);
                            return;
                        }
                    }
                }
            }
        }
    }
}