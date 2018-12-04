using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CapsOff
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Made by DcPacky\nThanks 4 using!\n\nIf you have any problems, try running this application as an administrator!\nPress CTRL+C to exit!\n");
            
            //ToDo: Do something with args to set time for Thread.Sleep()
            if (args != null && args.Length > 0)
                Console.WriteLine(args[0]);

            while (true) 
            {
                if (GetCapsLockState())
                {
                    Console.WriteLine("Caps On! Shutting it down.");
                    SetCapsLockKey(true);
                    SetCapsLockKey(false);
                }
                Thread.Sleep(1000);
            } 
            
        }

        private const byte VK_CAPITAL = 0x14;
        private const uint KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
        static extern short GetKeyState(uint nVirtKey);

        public static void SetCapsLockKey(bool newState)
        {
            bool scrollLockSet = GetKeyState(VK_CAPITAL) != 0;
            if (scrollLockSet != newState)
            {
                keybd_event(VK_CAPITAL, 0, 0, 0);
                keybd_event(VK_CAPITAL, 0, KEYEVENTF_KEYUP, 0);
            }
        }

        public static bool GetCapsLockState() // true = set, false = not set
        {
            return GetKeyState(VK_CAPITAL) != 0;
        }
    }
}
