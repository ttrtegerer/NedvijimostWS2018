using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace App1
{
    public class Eventcs : INotifyPropertyChanged
    {
        private int id;
        private string dateTime;
        private int duration;
        private string typeEvent;
        private string comment;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("id");
                }
            }
        }
        public string DateTime
        {
            get { return dateTime; }
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    OnPropertyChanged("dateTime");
                }
            }
        }
        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("duration");
                }
            }
        }
        public string TypeEvent
        {
            get { return typeEvent; }
            set
            {
                if (typeEvent != value)
                {
                    typeEvent = value;
                    OnPropertyChanged("typeEvent");
                }
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("comment");
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
