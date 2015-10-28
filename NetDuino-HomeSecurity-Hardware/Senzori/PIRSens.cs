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
    public class PIRSens : ISensor,IPrekinSensor 
    {
        private TipSenzor _kakovSum;
        private Predmet _kadeSum;

        InterruptPort pirSens;
        private bool _ImaDvizenje = false;

        public PIRSens(Cpu.Pin kojPin, Predmet stoPred)
        {
            _kakovSum = TipSenzor.PIR;
            _kadeSum = stoPred;
            pirSens  = new InterruptPort(kojPin, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
            pirSens.OnInterrupt += new NativeEventHandler(NekojVleze);
        }

        public bool ZacuvajPodatoci(int kojEvent)
        {
            string strAkc = "NEMA_Dvizenje";
                if (_ImaDvizenje)
                {
                    strAkc = "DVIZENJE";
                }
            return WService.ZapisiPrekuWS("PIR", kojEvent.ToString(), strAkc);
        }

        public string PecatiPodatoci()
        {
            Debug.Print("PIR ima dvizenje vo " + DateTime.Now.ToString());

            return "PIR ima dvizenje vo " + DateTime.Now.ToString();
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

        public void NekojVleze(uint data1, uint data2, DateTime time)
        {
            _ImaDvizenje = true;
            this.Izvesti();
            _ImaDvizenje = false;
        }
    }
}
