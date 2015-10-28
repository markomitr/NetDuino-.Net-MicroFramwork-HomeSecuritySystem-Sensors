using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using ndSysKukaDiplomska.Klasi;
using ndSysKukaDiplomska.NadvKlasi;
using ndSysKukaDiplomska.Senzori;

namespace ndSysKukaDiplomska
{
    public class Program
    {
        private static Kuka moja;
        private static DISsens mojDis;
        private static InterruptPort button1;
        private static DateTime poslednenPat;
        public static void Main()
        {
            SetirajTESTkopce();
            moja = new Kuka();
            PIRSens mojPir = new PIRSens(Pins.GPIO_PIN_D11, moja);
            mojDis = new DISsens(Pins.GPIO_PIN_D6, Pins.GPIO_PIN_D7, moja);
            LedSvetlo mojLed = new LedSvetlo(Pins.GPIO_PIN_D1, Interfejsi.KadePostaven.NADVOR);
            LedSvetlo mojLed2 = new LedSvetlo(Pins.GPIO_PIN_D0, Interfejsi.KadePostaven.VNATRE);
            LedSvetlo mojLedAlarm = new LedSvetlo(Pins.GPIO_PIN_D2,Interfejsi.KadePostaven.VNATRE);
            Zvucnik mojZvucnik = new Zvucnik(PWMChannels.PWM_PIN_D5,Interfejsi.KadePostaven.VNATRE);
          // mojTermo = new TempSens(SecretLabs.NETMF.Hardware.Netduino.Pins.GPIO_PIN_D3, SecretLabs.NETMF.Hardware.Netduino.Pins.GPIO_PIN_D4, moja);
            moja.DodadiPS(mojLed);
            moja.DodadiPS(mojLed2);
            moja.DodadiPS(mojLedAlarm);
            moja.DodadiPS(mojZvucnik);
            moja.DodadiSenzor(mojLed);
            moja.DodadiSenzor(mojLed2);
            moja.DodadiSenzor(mojPir);
            moja.DodadiSenzor(mojLedAlarm);
            moja.DodadiSenzor(mojZvucnik);
            moja.DodadiSenzor(mojDis);
            //moja.DodadiSenzor(mojTermo);

            // Create a delegate of type "ThreadStart", which is a pointer to the worker thread's main function
            ThreadStart delegateWorkerMain = new ThreadStart(Distance);

            // Next create the actual thread, passing it the delegate to the main function
            Thread threadWorker = new Thread(delegateWorkerMain);

            // Now start the thread.
            threadWorker.Start();

            while (true)
            {

                
            }


        }
        public static void SetirajTESTkopce()
        {
            button1 = new InterruptPort(Pins.GPIO_PIN_D10, false, Port.ResistorMode.PullUp,Port.InterruptMode.InterruptEdgeLow);
            button1.OnInterrupt += new NativeEventHandler(RestirajSens);
        }
        public static void RestirajSens(uint data1, uint data2, DateTime time)
        {
            if (poslednenPat.AddMilliseconds(200) > time)
                return;
           
             if (data2 == 0)
             {
                   moja.ResetSenzori();
             }
            poslednenPat= time;
        }
        public static void Distance()
        {
            while (true)
            {
                mojDis.Distance();
            }
            
        }
        public static void ZacuvajPodatOdSensori(object sender)
        {
            moja.ZacuvajPodatociOdSenzori();
        }


    }
}
