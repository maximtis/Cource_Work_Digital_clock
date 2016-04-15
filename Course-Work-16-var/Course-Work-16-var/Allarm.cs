using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_16_var
{
    public class Allarm : RunTimeClock, IComparable
    {
        public Allarm() : base() 
        {
            second = 0; sleepTime = 5; activate = true; IsThisSleepTimer = false; //1 минута
        }
        public bool IsThisSleepTimer { get; set; }
        public Allarm(int Hours, int Minutes, int Seconds, int _number, int _allarmTime) : base(Hours, Minutes, Seconds) 
        {
            activate = true;
            number = _number;
            sleepTime = _allarmTime;
        }
        public static IComparer<Allarm> SortByTheme()
        {
            return (IComparer<Allarm>)new Allarm();
        }
        public bool activate;
        private int number;
        public int sleepTime { get; set; }
        public Allarm DeepCopy()
        {
            return new Allarm(Hour, Minute, Second, number,sleepTime);
        }
        public int AllarmTime { get; set; }
        public int Number { get; set; }

        public event EventHandler TimeChanged;
        public event EventHandler runAllarm;
        public void Sleep_Next()
        {
            sleepTime += 1;
        }
        public void Sleep_Prev()
        {
            sleepTime -= 1;
        }
        public override void Hour_Next()
        {
            if (this.hour == 23)
                hour = 0;
            else hour++;
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
        }
        public static bool operator >(Allarm a, Allarm b)
        {
            if ((a.hour * 100) + a.minute > (b.hour + 100) + b.minute)
                return true;
            else return false;
        }
        public static bool operator <(Allarm a, Allarm b)
        {
            if (a > b)
                return false;
            else return true;
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
            
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
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
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
        }
        public void RunAllarm()
        {
            if (runAllarm != null) // Вызываем событие
                runAllarm(this, new EventArgs());
        }
        public override void Hour_Prev()
        {
            if (hour == 0)
                hour = 23;
            else hour--;
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
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
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
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
            if (TimeChanged != null) // Вызываем событие
                TimeChanged(this, new EventArgs());
        }
        public int CompareTo(object obj)
        {
            Allarm Obg = obj as Allarm;
            if (obj == null) return 1;
            if (obj is Allarm)
            {
                return (hour * 100 + minute).CompareTo((Obg.hour * 100 + Obg.minute));
            }
            else
                throw new ArgumentException("Object is not a Temperature");
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
