using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_16_var
{
    public class RunTimeClock : Clock
    {
        protected int hour;
        protected int minute;
        protected int second;
        public System.Windows.Threading.DispatcherTimer Timer;
        public int Hour 
        {
            get
            {
                return hour;
            }
        }
        public int Minute
        {
            get
            {
                return minute;
            }
        }
        public int Second
        {
            get
            {
                return second;
            }
        }

        public override void Hour_Next()
        {
            if (hour == 23)
                hour = 0;
            else hour++;
        }
        public override void Minute_Next()
        {
            if (minute == 59)
            {
                minute = 0;
                Hour_Next();
            }
            else
                minute++;
            if (Check_Event != null) // Вызываем событие
                Check_Event(this, new EventArgs());
        }
        public override void Second_Next()
        {
            if (second == 59)
            {

                second = 0;
                Minute_Next();
            }
            else
                second++;
        }

        public override void Hour_Prev()
        {
            if (hour == 0)
                hour = 23;
            else hour--;
        }
        public override void Minute_Prev()
        {
            if (minute == 0)
            {
                minute = 59;
                Hour_Prev();
            }
            else
                minute--;
        }
        public override void Second_Prev()
        {
            if (second == 0)
            {
                second = 59;
                Minute_Prev();
            }
            else
                second--;
        }

        public RunTimeClock()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            SetNowTime();
        }
        public RunTimeClock(int Hours, int Minutes, int Seconds)
        {
            hour = Hours;
            minute = Minutes;
            second = Seconds;
        }
        public void SetNowTime()
        {
            DateTime Now = DateTime.Now;
            hour = Now.Hour;
            minute = Now.Minute;
            second = Now.Second;
        }
        public override void Start()
        {
            Timer.Tick+=new EventHandler(Timer_Ticks);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }
        public override void Stop()
        {
            Timer.Stop();
        }
        public override string ToString()
        {
            return new TimeSpan(hour, minute, second).ToString();
        }

        public event EventHandler Ticks;
        public event EventHandler Check_Event;
        private void Timer_Ticks(object sender, EventArgs e)
        {
            Second_Next();
            if (Ticks != null) // Вызываем событие
                Ticks(this, new EventArgs());
        }

        public MainWindow MainWindow
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        
    }
}