using System;
using System.Threading;
using ndSysKukaDiplomska.Interfejsi;
using ndSysKukaDiplomska.Klasi;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using ndSysKukaDiplomska.NadvKlasi;

namespace ndSysKukaDiplomska.Senzori
{
    class TempSens: ISensor, IPrekinSensor 
    {

        private TipSenzor _kakovSum;
        private Predmet _kadeSum;
        private InterruptPort disSensEcho ;
        private OutputPort disSensTrig;
        private Dht11Sensor sensor;
        private  int ticks;
        private static double  _posledenPodatok = 0;

        public TempSens(Cpu.Pin kojPinEcho,Cpu.Pin kojPinTrig, Predmet stoPred)
        {
            _kakovSum = TipSenzor.TERMOMETAR;
            _kadeSum = stoPred;

            //disSensEcho = new InterruptPort(kojPinEcho, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            //disSensTrig = new OutputPort(kojPinTrig, false);
            sensor = new Dht11Sensor(kojPinTrig,kojPinEcho, PullUpResistor.External);
            //disSensEcho.OnInterrupt += new NativeEventHandler(NekojSePriblizuva);
            //disSensEcho.DisableInterrupt();
                     
        }

        public bool ZacuvajPodatoci(int kojEvent)
        {
            return WService.ZapisiPrekuWS("TERMOMETAR", kojEvent.ToString(), _posledenPodatok.ToString());
        }

        public string PecatiPodatoci()
        {
            Debug.Print("TEMPERATURA " + _posledenPodatok.ToString());

            return "TEMPERATURA " + _posledenPodatok.ToString();
        }

        public Hashtable VratiPodatoci()
        {
            return null;
        }

        public TipSenzor kakovSenzor
        {
            get { return this._kakovSum; }
        }
    
        public void  ImaPrekin()
        {
            
        }

        public void  Izvesti()
        {
            _kadeSum.Notify(KadePostaven.VNATRE);
        }
        public void MeriTemperatura()
        {
            if(sensor.Read())
            {
                _posledenPodatok = sensor.Temperature;
            }
        }
       
    }

}
