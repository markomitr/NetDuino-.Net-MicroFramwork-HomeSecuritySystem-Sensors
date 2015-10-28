using System;
using Microsoft.SPOT;
using System.Collections;
namespace ndSysKukaDiplomska.Interfejsi
{
    public interface  ISensor
    {
         
         bool ZacuvajPodatoci(int kojEvent);
         string PecatiPodatoci();
         Hashtable VratiPodatoci();
         TipSenzor kakovSenzor { get; }
    }
    public enum TipSenzor
    {
        PIR,
        DISTANCE,
        ALARM_LED,
        SVETLO_LED,
        ZVUCNIK,
        TERMOMETAR
    }
    public enum KadePostaven
    {
        NADVOR,
        VNATRE
    }
}
