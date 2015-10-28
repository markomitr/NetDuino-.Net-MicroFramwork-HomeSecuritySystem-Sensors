using System;
using Microsoft.SPOT;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using ndSysKukaDiplomska.Interfejsi;
using ndSysKukaDiplomska.Klasi;

namespace ndSysKukaDiplomska.Senzori
{
    public class Zvucnik : IAkciSensor, ISensor, IObserver
    {
        private TipSenzor _kakovSum;
        private bool _daliUkluceno = false;
        private PWM _zvucnik;
        private KadePostaven _kadeSum;

        public KadePostaven KadeSum
        {
            get { return _kadeSum; }
            set { _kadeSum = value; }
        }
        public Zvucnik(Cpu.PWMChannel kojPin,KadePostaven kadeE)
        {
            _zvucnik = new PWM(kojPin, 100, 5, false);
            _zvucnik.DutyCycle = 50;
            _zvucnik.Frequency = 50;
            _zvucnik.Period = 1000;
            _kakovSum = TipSenzor.ZVUCNIK;
            this.KadeSum = kadeE;
        }
        public bool ZacuvajPodatoci(int kojEvent)
        {
          return WService.ZapisiPrekuWS("ZVUCNIK", kojEvent.ToString(), _daliUkluceno.ToString());
        }

        public string PecatiPodatoci()
        {
            Debug.Print("SPEAKER_ALARM e " + _daliUkluceno.ToString());

            return "SPEAKER_ALARM e " + _daliUkluceno.ToString();
        }

        public Hashtable VratiPodatoci()
        {
            return null;
        }

        public TipSenzor kakovSenzor
        {
            get { return _kakovSum; }
        }

        public void Ukluci()
        {
            _zvucnik.Start();
            _daliUkluceno = true;
        }

        public void Iskluci()
        {
            _zvucnik.Stop();
            _daliUkluceno = false;
        }

        public bool DaliUkluceno
        {
            get { return _daliUkluceno; }
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
