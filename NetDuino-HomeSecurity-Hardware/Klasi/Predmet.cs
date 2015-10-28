using System;
using Microsoft.SPOT;
using System.Collections;
using ndSysKukaDiplomska.Interfejsi;
using System.Threading;

namespace ndSysKukaDiplomska.Klasi
{


    public abstract class Predmet
    {


        private ArrayList _prekinSenzori = new ArrayList();
        private ArrayList _siteSenzori = new ArrayList();
        private  int _nastanId = 0;
        private bool _ImaAlarm = false;
        private Timer _restTimer = new Timer(new TimerCallback(Program.ZacuvajPodatOdSensori), null, 5000, 60000);

        public void DodadiPS(IObserver senzor)
        {
            _prekinSenzori.Add(senzor);
          
        }

        public void IzbrisiPS(IObserver senzor)
        {
            _prekinSenzori.Remove(senzor);
        }

        public void DodadiSenzor(ISensor senzor)
        {
            _siteSenzori.Add(senzor);
        }

        public void IzbrisiSenzor(ISensor senzor)
        {
            _siteSenzori.Remove(senzor);
        }

        public void Notify(KadePostaven stoDaIzvestam)
        {
            //if (_restTimer != null)
            //{
            //    _restTimer = null;
            //}
            
            
            _ImaAlarm= true;
            _nastanId++;
            foreach (IObserver o in _prekinSenzori)
            {
                if (o.KadeSum == stoDaIzvestam)
                {
                    o.Akcija();
                }
                
            }
            _ImaAlarm = false;
            ZacuvajPodatociOdPriAlarm();
            
        }
        public void ResetSenzori()
        {
           if (!_ImaAlarm)
           {
                foreach (IObserver o in _prekinSenzori)
                {
                    o.Reset();
                }
           }

        }
        private void ZacuvajPodatociOdPriAlarm()
        {
            foreach (ISensor o in _siteSenzori)
            {
                o.ZacuvajPodatoci(_nastanId);
                o.PecatiPodatoci();
            }
        }
        public void ZacuvajPodatociOdSenzori()
        {
            foreach (ISensor o in _siteSenzori)
            {
                o.ZacuvajPodatoci(0);
                o.PecatiPodatoci();
            }
        }

        internal void IsklucTimer()
        {
            if (_restTimer != null)
            {
                _restTimer = null;
            }
        }
    }
}
