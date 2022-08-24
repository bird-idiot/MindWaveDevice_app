using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MindWaveDevice_app
{
    class MindWaveDevice
    {
        TcpClient client;
        Stream stream;
        int bytesRead;
        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": false,""format"": ""Json""}");
        //создать мьютекс
        Mutex mutex;
        //переменная для потока
        Thread thread;
        //контейнер данных
        public BrainData brainData { get; set; }

        byte[] buffer = new byte[2048];
        public bool status { get; private set; }
        public MindWaveDevice() //конструктор
        {
            status = false;
            mutex = new Mutex();
        }
        public void ConnectDevice()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 13854);
                stream = client.GetStream();

                // отправка пакета конфигурации на TGC
                if (stream.CanWrite)
                {
                    stream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                }

                //запуск потока
                thread = new Thread(ReadData);
                thread.Start();
            }
            catch (SocketException) { }

            status = true;
        }
        public void DisconnectDevice()
        {
            //остановить поток
            thread.Abort();
            //разорвать соединение с устройством
            client.Close();
            //обновить данные на форме
            status = false;
        }

        void ReadData()
        {
            while (true)
            {
                bytesRead = stream.Read(buffer, 0, 2048);
                //разделить на массив пакетов
                string[] packets = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('\r');

                foreach (string packet in packets)
                {
                    try
                    {
                        //запись данных в контейнер
                        mutex.WaitOne();
                        brainData = JsonSerializer.Deserialize<BrainData>(packet);
                        mutex.ReleaseMutex();
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
