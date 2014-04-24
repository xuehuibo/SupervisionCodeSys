using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Scan
{
    public delegate void ReceivedDataDelegate(string[] strs);
    public class Scanner
    {
        SerialPort Port = new SerialPort();

        ReceivedDataDelegate ReceiveData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PortName">端口号</param>
        /// <param name="receivedDataDelegate">收到数据后的处理函数</param>
        public Scanner(SerialPort Port, ReceivedDataDelegate receivedDataDelegate)
        {
            this.Port = Port;
            ReceiveData = receivedDataDelegate;//收到条码后,外部响应
            Port.DataReceived+=new SerialDataReceivedEventHandler(dataReceived);//收到条码后,触发事件
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            string data = Port.ReadExisting();
            byte[] b = { 2};
            string header = Encoding.ASCII.GetString(b);
            ReceiveData(data.Replace(header,string.Empty).Replace("\r\n",";").Replace("&",";").Replace("\r",string.Empty).Split(';'));
        }
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                Port.Open();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (Port.IsOpen)
                {
                    Port.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
