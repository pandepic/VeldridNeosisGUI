using System;
using Noesis;

namespace VNGUI
{
    public class VN
    {
        public static void Init(string licenceName, string licenceKey)
        {
            Console.WriteLine("Initializing Noeasis");

            Noesis.GUI.Init(licenceName, licenceKey);
        }
    }
}
