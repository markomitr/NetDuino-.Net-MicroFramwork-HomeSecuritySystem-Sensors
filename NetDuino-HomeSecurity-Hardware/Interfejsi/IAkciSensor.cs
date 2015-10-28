using System;
using Microsoft.SPOT;

namespace ndSysKukaDiplomska.Interfejsi
{
    public interface IAkciSensor
    {
         void Ukluci();
         void Iskluci();
         bool DaliUkluceno { get; }
    }
}
