using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WorkCenterViewer
{
    public class WorkViewer
    {
        SerialPort Port = new SerialPort();
        public WorkViewer(SerialPort Port)
        {
            this.Port=Port;
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

        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="msg"></param>
        public void Write(string msg)
        {
            byte[] buff= Encoding.Default.GetBytes(msg);
            Port.Write(buff,0,buff.Length);
        }

        /// <summary>
        /// 打开语音
        /// </summary>
        public void SpeakOn()
        {
            byte[] buff = { 0x1B, 0x3D, 0x01};
            Port.Write(buff, 0, 3);
        }
        /// <summary>
        /// 关闭语音
        /// </summary>
        public void SpeakOff()
        {
            byte[] buff = {0x1B ,0x3D, 0x02 };
            Port.Write(buff, 0, 3);
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void CLS()
        {
            byte cls = 0x0c;
            Port.Write(new byte[] { cls }, 0, 1);
        }

        /// <summary>
        /// 播放语音
        /// </summary>
        /// <param name="msg"></param>
        public void Speak(SpeakMsg msg)
        {
            byte[] buff = {Convert.ToByte(msg) };
            Port.Write(buff, 0, 1);
        }
    }
    public enum SpeakMsg
    {
        错误=32,
        成功=34,
        重复=46,
        任务已启动=48,
        不存在=39,
        药监码=45
    }
}
