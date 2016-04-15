using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_16_var
{

    class Radio
    {
        string[] stations = { @"searchFM.mp3", @"xitfm.mp3", @"avtoradio.mp3", @"perecfm.mp3" };
        public int current_station=0;
        public double[] stationsValue = { 103.5,106.6 , 107.3 };
        public int Volume
        { 
            set 
            {
                for(int i=0;i<stations.Length;i++)
                {
                    StationsWMP[i].settings.volume = value;
                }

            } 
        }
        public int minute{ get; set; }
        public int second { get; set; }

        public event EventHandler ChangedState;
        public void RunChangedState()
        {
            if (ChangedState != null) // Вызываем событие
                ChangedState(this, new EventArgs());
        }
        public WMPLib.WindowsMediaPlayer[] StationsWMP = new WMPLib.WindowsMediaPlayer[4];


        List<double> StationList = new List<double>(0);
        public int TimerAllarm=30;
        public double Frecuency{get;set;}
        public Radio()
        {
            StationsWMP[1] = new WMPLib.WindowsMediaPlayer();
            StationsWMP[2] = new WMPLib.WindowsMediaPlayer();
            StationsWMP[3] = new WMPLib.WindowsMediaPlayer();
            StationsWMP[0] = new WMPLib.WindowsMediaPlayer();
            StationsWMP[1].URL = stations[1];
            StationsWMP[2].URL = stations[2];
            StationsWMP[3].URL = stations[3];
            StationsWMP[1].controls.stop();
            StationsWMP[2].controls.stop();
            StationsWMP[3].controls.stop();
            Frecuency = 0;
        }

        public void PlaySearchVoice(bool choose)
        {
            StationsWMP[0].URL = stations[0];
            if(choose)
                StationsWMP[0].controls.play();
            else
                StationsWMP[0].controls.stop();
        }
        public void Play(int station)
        {
            StationsWMP[current_station].controls.pause();
            current_station = station;
            StationsWMP[station].controls.play();
        }
        public void Stop()
        {
            StationsWMP[current_station].controls.pause();
        }
        public void Frec_Plus()
        {
            if (Frecuency == 145.0)
                Frecuency = 45.0;
            else
            Frecuency += 0.1;
        }
        public void Frec_Prev()
        {
            if (Frecuency == 45.0)
                Frecuency = 145.0;
            else
            Frecuency -= 0.1;
        }
        public virtual void Minute_Prev()
        {
            if (minute == 0)
            {
                return;
            }
            else
                minute--;
        }
        public virtual void Second_Prev()
        {
            if (second == 0)
            {
                second = 59;
                Minute_Prev();
            }
            else
                second--;
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
