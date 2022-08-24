using System;

namespace MindWaveDevice_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var mwDev = new MindWaveDevice();
            try
            {
                mwDev.ConnectDevice();
            }
            catch (Exception) 
            {
                Console.WriteLine("Подключение не удалось");
                return;
            }
            
            while (mwDev.status)
            {
                var data = mwDev.brainData;
                Console.WriteLine($"poorLevelSignal: {data?.poorSignalLevel}");
                if (data?.eSense != null)
                    Console.WriteLine($"attention: {data?.eSense?.attention}");
                if (data?.blinkStrength >= 0) 
                    Console.WriteLine($"blink: {data?.blinkStrength}");
                if (data?.mentalEffort >= 0)
                    Console.WriteLine($"mental: {data?.mentalEffort}");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
