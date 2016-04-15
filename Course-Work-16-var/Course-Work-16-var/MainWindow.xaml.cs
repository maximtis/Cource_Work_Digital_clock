using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using Microsoft.VisualBasic;
using Microsoft.CSharp;
using System.ComponentModel;
using System.Diagnostics;


namespace Course_Work_16_var
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Allarm> Object_List_Of_AllarmsAllarmsList = new List<Allarm>();
        public int On_Valid_Index=0;
        public Allarm Object_AllarmTime = new Allarm();
        Radio MyRadio = new Radio();
        public RunTimeClock Object_NowTime = new RunTimeClock();
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        public bool IsAllarmed = true;
        public MainWindow()
        {
            
            InitializeComponent();
            String path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "clock.jpg");
            Wrapper.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            
            Object_NowTime.Ticks+=new EventHandler(Timer_Ticks);
            Object_NowTime.Start();
            Object_NowTime.Check_Event += IsAllarmed_Change;
            off.IsChecked = true;
            Main_ListBox_Of_AllarmTime.Items.Add("<-- No Allarms Set -->");
        }

        /* 
          public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
         
          WMP.URL = @"sound.mp3" // файл музыкальный
          WMP.settings.volume = 100; // меняя значение можно регулировать громкость
          WMP.controls.play(); // Старт
          WMP.controls.stop(); // Стоп
          WMP.close();
       */


        #region main Grid


        private void Timer_Ticks(object sender, EventArgs e)
        {
            clock.Text = Object_NowTime.ToString();
            date.Text = DateTime.Now.ToShortDateString();
        }

        private void setTime_Click(object sender, RoutedEventArgs e)
        {
            Grid_Timer_Allarm.Visibility = Visibility.Hidden;
            MainClock.Visibility = Visibility.Visible;
            setAllarmGrid.Visibility = Visibility.Hidden;
            setTimeGrid.Visibility = Visibility.Visible;
        }

        private void setAllarm_Click(object sender, RoutedEventArgs e)
        {
            
            Grid_Timer_Allarm.Visibility = Visibility.Hidden;
            MainClock.Visibility = Visibility.Hidden;
            setTimeGrid.Visibility = Visibility.Hidden;
            setAllarmGrid.Visibility = Visibility.Visible;
            Clock_On_The_Grid_Allarm.Text = Object_AllarmTime.ToString();
            Object_AllarmTime.TimeChanged += new EventHandler(Allarm_Set_Clock_Changed);
            
        }

        private void Allarm_Set_Clock_Changed(object sender, EventArgs e)
        {
            Clock_On_The_Grid_Allarm.Text = Object_AllarmTime.ToString();
        }

        private void mainClock_Click(object sender, RoutedEventArgs e)
        {
            MainClock.Visibility = Visibility.Visible;
            setTimeGrid.Visibility = Visibility.Hidden;
            setAllarmGrid.Visibility = Visibility.Hidden;
        }

        private void reSet_Click(object sender, RoutedEventArgs e)
        {
            
            Main_ListBox_Of_AllarmTime.Items.Clear();
            SetAllarm_ListBox_AlarmList.Items.Clear();
            Object_List_Of_AllarmsAllarmsList.Clear();
            Main_ListBox_Of_AllarmTime.Items.Add("<-- No Allarms Set -->");

        }
#endregion

        #region SetTime_Grid
        private void hour_plus_Click(object sender, RoutedEventArgs e)
        {
            Object_NowTime.Hour_Next();
        }

        private void hour_minus_Click(object sender, RoutedEventArgs e)
        {
            Object_NowTime.Hour_Prev();
        }

        private void minut_plus_Click(object sender, RoutedEventArgs e)
        {
            Object_NowTime.Minute_Next();
        }

        private void minute_minus_Click(object sender, RoutedEventArgs e)
        {
            Object_NowTime.Minute_Prev();
        }

        #endregion

        #region Set_Allarm_Grid

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainClock.Visibility = Visibility.Visible;
            setTimeGrid.Visibility = Visibility.Hidden;
            setAllarmGrid.Visibility = Visibility.Hidden;
        }
        private void hour_plus_a_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Hour_Next();
        }

        private void hour_minus_a_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Hour_Prev();
        }

        private void minut_plus_a_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Minute_Next();
        }

        private void minute_minus_a_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Minute_Prev();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            IsAllarmed = false;
            SetAllarm_ListBox_AlarmList.Items.Clear();
            Main_ListBox_Of_AllarmTime.Items.Clear();
            Object_List_Of_AllarmsAllarmsList.Add(Object_AllarmTime.DeepCopy());
            Object_List_Of_AllarmsAllarmsList.Sort();
            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count;i++)
                SetAllarm_ListBox_AlarmList.Items.Add((i + 1).ToString() + ". " + Object_List_Of_AllarmsAllarmsList[i]);
            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count; i++)
                Main_ListBox_Of_AllarmTime.Items.Add((i + 1).ToString() + ". " + Object_List_Of_AllarmsAllarmsList[i]);
        }

        private void plus_sleep_time_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Sleep_Next();
            TextBox_Sleep_Time.Text = Object_AllarmTime.sleepTime.ToString();
        }

        private void minus_sleep_time_Click(object sender, RoutedEventArgs e)
        {
            Object_AllarmTime.Sleep_Prev();
            TextBox_Sleep_Time.Text = Object_AllarmTime.sleepTime.ToString();
        }

        #endregion

        #region Allarm_View_Grid

        private void on_Checked(object sender,  RoutedEventArgs e)
        {
            MyRadio.Stop();
            Main_ListBox_Of_AllarmTime.Visibility = Visibility.Visible;
            TextBox_RadioIndicator.Visibility = Visibility.Hidden;
            TextBox_FM_Indicator.Visibility = Visibility.Hidden;
            Grid_Timer_Allarm.Visibility = Visibility.Hidden;
            Object_NowTime.Ticks += Allarm_Enable;
            Object_NowTime.Check_Event += Check_Event_Event;
            Object_NowTime.Ticks -= Allaert_Indicator;
        }

        private void IsAllarmed_Change(object sender,  EventArgs e)
        {
            IsAllarmed = false;
        }

        private void Check_Event_Event(object sender, EventArgs e)
        {
            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count; i++)
            {
                if (Object_List_Of_AllarmsAllarmsList[i].Hour == Object_NowTime.Hour &&
                Object_List_Of_AllarmsAllarmsList[i].Minute < Object_NowTime.Minute)
                    Object_List_Of_AllarmsAllarmsList[i].activate = false;
                if(Object_List_Of_AllarmsAllarmsList[i].Hour<Object_NowTime.Hour)
                    Object_List_Of_AllarmsAllarmsList[i].activate = false;
            }
        }
        private int Get_Valid_Allarm_Index()
        {
            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count; i++)
                if (Object_List_Of_AllarmsAllarmsList[i].activate)
                    return i;
            return 0;
        }
        private void On_Signal(bool button)
        {
            if(button)
            {
                WMP.URL = @"back.mp3"; // файл музыкальный
            WMP.settings.volume = 100; // меняя значение можно регулировать громкость
            WMP.controls.play(); // Старт
            }
            else
            {
                WMP.controls.stop();
            }
        }

        private void Allarm_Enable(object sender, EventArgs e)
        {
            if (Object_List_Of_AllarmsAllarmsList.Count!=0)
            if (!IsAllarmed)
            {
                On_Valid_Index = Get_Valid_Allarm_Index();
                if (Object_NowTime.Hour == Object_List_Of_AllarmsAllarmsList[On_Valid_Index].Hour)
                    if (Object_NowTime.Minute == Object_List_Of_AllarmsAllarmsList[On_Valid_Index].Minute)
                        if (Object_NowTime.Second == Object_List_Of_AllarmsAllarmsList[On_Valid_Index].Second)
                        {

                            Object_NowTime.Ticks += Run_Allarm_Clock;
                            On_Signal(true);
                            Grid_Allarm_View.Visibility = Visibility.Visible;

                        }
            }
        }

        private void off_Checked(object sender, RoutedEventArgs e)
        {
            MyRadio.Stop();
            Main_ListBox_Of_AllarmTime.Visibility = Visibility.Hidden;
            TextBox_RadioIndicator.Visibility = Visibility.Hidden;
            TextBox_FM_Indicator.Visibility = Visibility.Hidden;
            Grid_Timer_Allarm.Visibility = Visibility.Hidden;

            Object_NowTime.Ticks -= Allarm_Enable;
            Object_NowTime.Ticks -= Run_Allarm_Clock;
            IsAllarmed = true;
            Object_NowTime.Ticks -= Allaert_Indicator;
            
        }

        private void Run_Allarm_Clock(object sender, EventArgs e)
        {
            Allarm_time.Text = Object_NowTime.ToString();
            
        }

        private void off_allarm_Click(object sender, RoutedEventArgs e)
        {
            On_Signal(false);
            Grid_Allarm_View.Visibility = Visibility.Hidden;
            MainClock.Visibility = Visibility.Visible;
        }
        #endregion

        #region Set_Radio_Grid
        //Set_Radio_Grid
        private void set_Radio_Click(object sender, RoutedEventArgs e)
        {
            MainClock.Visibility = Visibility.Hidden;
            Set_Radio.Visibility = Visibility.Visible;
        }

        private void back_to_main_Click(object sender, RoutedEventArgs e)
        {
            MainClock.Visibility = Visibility.Visible;
            Set_Radio.Visibility = Visibility.Hidden;
        }

        private void fm_plus_Click(object sender, RoutedEventArgs e)
        {
            MyRadio.RunChangedState();
            MyRadio.Frec_Plus();
            Slider_State_Control.Value += 0.1;
            radio_frequency.Text = "" + String.Format("{0:F1}", MyRadio.Frecuency) + " FM";
        }

        private void fm_minus_Click(object sender, RoutedEventArgs e)
        {
            MyRadio.RunChangedState();
            MyRadio.Frec_Prev();
            Slider_State_Control.Value -= 0.1;
            radio_frequency.Text = "" + String.Format("{0:F1}", MyRadio.Frecuency) + " FM"; ;
            TextBox_FM_Indicator.Text = "" + String.Format("{0:F1}", MyRadio.Frecuency) + " FM";
        }

        private void Slider_State_Control_ValueChenged(object sender, RoutedEventArgs e)
        {
            MyRadio.RunChangedState();
            MyRadio.Frecuency = Slider_State_Control.Value;
            radio_frequency.Text = "" + String.Format("{0:F1}", MyRadio.Frecuency) + " FM";
            TextBox_FM_Indicator.Text = "" + String.Format("{0:F1}", MyRadio.Frecuency) + " FM";
        }
        private void State_Control_Voice(object sender, EventArgs e)
        {
            if (Convert.ToDouble(String.Format("{0:F1}", MyRadio.Frecuency)) == 106.6)
            {
                MyRadio.PlaySearchVoice(false);
                MyRadio.Play(1);
            }
            else if (Convert.ToDouble(String.Format("{0:F1}", MyRadio.Frecuency)) == 103.5)
            {
                MyRadio.PlaySearchVoice(false);
                MyRadio.Play(2);
            }
            else if (Convert.ToDouble(String.Format("{0:F1}", MyRadio.Frecuency)) == 107.3)
            {
                MyRadio.PlaySearchVoice(false);
                MyRadio.Play(3);
            }
            else
            {
                MyRadio.Stop();
                MyRadio.PlaySearchVoice(true);
            }
            
        }





        #endregion

        private void sleep_Click(object sender, RoutedEventArgs e)
        {
            ;
            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList[On_Valid_Index].sleepTime; i++)
                Object_List_Of_AllarmsAllarmsList[On_Valid_Index].Minute_Next();

            SetAllarm_ListBox_AlarmList.Items.Clear();
            Main_ListBox_Of_AllarmTime.Items.Clear();

            Object_List_Of_AllarmsAllarmsList.Sort();

            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count; i++)
                    SetAllarm_ListBox_AlarmList.Items.Add((i + 1).ToString() + ". " + Object_List_Of_AllarmsAllarmsList[i]);

            for (int i = 0; i < Object_List_Of_AllarmsAllarmsList.Count; i++)
                    Main_ListBox_Of_AllarmTime.Items.Add((i + 1).ToString() + ". " + Object_List_Of_AllarmsAllarmsList[i]);

            Grid_Allarm_View.Visibility = Visibility.Hidden;
            On_Signal(false);
            Object_NowTime.Ticks -= Run_Allarm_Clock;
            IsAllarmed = true;
        }

        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            MyRadio.ChangedState -= State_Control_Voice;
            MyRadio.ChangedState += State_Control_Voice;
            MyRadio.RunChangedState();
            Main_ListBox_Of_AllarmTime.Visibility = Visibility.Hidden;
            TextBox_RadioIndicator.Visibility = Visibility.Visible;
            TextBox_FM_Indicator.Visibility = Visibility.Visible;
            Object_NowTime.Ticks -= Allaert_Indicator;
            Grid_Timer_Allarm.Visibility = Visibility.Hidden;
            Object_NowTime.Ticks -= Allarm_Enable;
        }

        private void timer_Checked(object sender, RoutedEventArgs e)
        {
            MyRadio.ChangedState -= State_Control_Voice;
            MyRadio.ChangedState += State_Control_Voice;
            MyRadio.RunChangedState();
            Main_ListBox_Of_AllarmTime.Visibility = Visibility.Hidden;
            TextBox_FM_Indicator.Visibility = Visibility.Visible;
            MyRadio.TimerAllarm=30;
            Object_NowTime.Ticks += Allaert_Indicator;
            MyRadio.minute = 29;
            MyRadio.second = 59;
            Grid_Timer_Allarm.Visibility = Visibility.Visible;


        }
        
        private void Allaert_Indicator(object sender, EventArgs e)
        {
            TextBox_Time_To_Allert.Text = MyRadio.minute + " min " + MyRadio.second + " sec";
            MyRadio.Second_Prev();
            if(MyRadio.minute==0 &&MyRadio.second==0)
            {
                Grid_Timer_ShowAllarmScreen.Visibility = Visibility.Visible;
            }
        }

        private void Slider_Volume_Control_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyRadio.Volume = (int)Slider_Volume_Control.Value;
        }

        private void off_allert_Click(object sender, RoutedEventArgs e)
        {
            Grid_Timer_ShowAllarmScreen.Visibility = Visibility.Hidden;
        }

        private void Find_Station_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MyRadio.stationsValue.Length; i++)
                if (MyRadio.Frecuency < MyRadio.stationsValue[i])
                {
                    Slider_State_Control.Value = MyRadio.stationsValue[i];
                    MyRadio.RunChangedState();
                    break;
                }
        }

        private void On_Radio_Click(object sender, RoutedEventArgs e)
        {
            TextBox_FM_Indicator.Visibility = Visibility.Visible;
            MyRadio.ChangedState -= State_Control_Voice;
            MyRadio.ChangedState += State_Control_Voice;
            MyRadio.RunChangedState();
        }  

        private void Off_Radio_Click(object sender, RoutedEventArgs e)
        {

            MyRadio.Stop();
            TextBox_FM_Indicator.Visibility = Visibility.Hidden;
        }

     

       


    }
}