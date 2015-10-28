using System;
using Microsoft.SPOT;
using ndSysKukaDiplomska.Interfejsi;
using ndSysKukaDiplomska.Klasi;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace ndSysKukaDiplomska.Senzori
{
    public class LedSvetlo : IAkciSensor, ISensor, IObserver
    {
        private TipSenzor _kakovSum;
        private bool _daliUkluceno = false;
        OutputPort ExternalLED;
        private KadePostaven _kadeSum;

        public KadePostaven KadeSum
        {
            get { return _kadeSum; }
            set { _kadeSum = value; }
        }
        public LedSvetlo(Cpu.Pin kojPin,KadePostaven kadeE)
        {
            _kakovSum = TipSenzor.SVETLO_LED;
            ExternalLED = new OutputPort(kojPin, false);
            this.KadeSum = kadeE;
        }
        public void Ukluci()
        {
            ExternalLED.Write(true);
            _daliUkluceno = true;
        }

        public void Iskluci()
        {
            ExternalLED.Write(false);
            _daliUkluceno = false;
        }

        public bool DaliUkluceno
        {
            get { return _daliUkluceno; }
        }

        public bool ZacuvajPodatoci(int kojEvent)
        {
            return WService.ZapisiPrekuWS("LED_Svetlo", kojEvent.ToString(), _daliUkluceno.ToString());
        }

        public string PecatiPodatoci()
        {
            Debug.Print("LED_Svetlo e " + _daliUkluceno.ToString());

            return "LED_Svetlo e " + _daliUkluceno.ToString();
        }

        public Hashtable VratiPodatoci()
        {
            return null;
        }

        public TipSenzor kakovSenzor
        {
            get { return _kakovSum; }
        }

        public void Akcija()
        {
            Ukluci();
        }


        public void Reset()
        {
            Iskluci();
        }
    }
}
