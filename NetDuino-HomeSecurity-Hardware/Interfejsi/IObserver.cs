using System;
using Microsoft.SPOT;

namespace ndSysKukaDiplomska.Interfejsi
{
    public interface  IObserver
    {
        void Akcija();
        void Reset();
        KadePostaven KadeSum { get; set; }
    }
}
