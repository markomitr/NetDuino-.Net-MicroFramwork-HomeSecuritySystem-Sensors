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
    public class DISsens : ISensor, IPrekinSensor 
    {

        private TipSenzor _kakovSum;
        private Predmet _kadeSum;
        private InterruptPort disSensEcho ;
        private OutputPort disSensTrig;
        private HC_SR04 sensor;
        private  int ticks;
        private static double  _posledenPodatok = 0;

        public DISsens(Cpu.Pin kojPinEcho,Cpu.Pin kojPinTrig, Predmet stoPred)
        {
            _kakovSum = TipSenzor.DISTANCE;
            _kadeSum = stoPred;

            //disSensEcho = new InterruptPort(kojPinEcho, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            //disSensTrig = new OutputPort(kojPinTrig, false);
            sensor  = new HC_SR04(kojPinTrig,kojPinEcho);
            //disSensEcho.OnInterrupt += new NativeEventHandler(NekojSePriblizuva);
            //disSensEcho.DisableInterrupt();
                     
        }

        public bool ZacuvajPodatoci(int kojEvent)
        {
            return WService.ZapisiPrekuWS("DISTANCE", kojEvent.ToString(), _posledenPodatok.ToString());
        }

        public string PecatiPodatoci()
        {
            Debug.Print("Dalecina se priblizi na " + _posledenPodatok.ToString());

            return "Dalecina se priblizi na " + _posledenPodatok.ToString();
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
            _kadeSum.Notify(KadePostaven.NADVOR);
        }

        public void NekojSePriblizuva(uint port, uint state, DateTime time)
        {
            if (state == 0) // falling edge, end of pulse
            {
                int pulseWidth = (int)time.Ticks - ticks;

                int distance = (pulseWidth * 10 / (int)578.29);
                double voMetri = 0;
                voMetri = (distance / 1000.0);
                _posledenPodatok = voMetri;
                if (voMetri < 0.2)
                {
                    Debug.Print("Dalecina ALARM se priblizi na " + voMetri.ToString() + " metri!");
                    this.Izvesti();
                }
                else
                {
                   // Debug.Print("Dalecina se priblizi na " + voMetri.ToString() + " metri!");
                }               
            }
            else
            {
                ticks = (int)time.Ticks;
            }
            disSensEcho.ClearInterrupt();
   
        }

        public void Distance()
        {
            //mojDis.Distance();
            long ticks = sensor.Ping();
            if (ticks > 0L)
            {
                double metri = sensor.TicksToInches(ticks) * 0.0254;
                _posledenPodatok = metri;
                Debug.Print((metri).ToString());
                if (metri <0.15)
                {
                    this.Izvesti();
                }
                
            }
        }   
         public void DistanceStar()
        {
            disSensEcho.EnableInterrupt();
            disSensTrig.Write(false);
            Thread.Sleep(2);
            disSensTrig.Write(true);
            Thread.Sleep(10);
            disSensTrig.Write(false);
            Thread.Sleep(2);
        }
       
    }
}
