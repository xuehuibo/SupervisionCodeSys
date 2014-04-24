using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class SysResModel
    {
        public SysResModel(short ID, string Text)
        {
            this.ID = ID;
            this.Text = Text;
        }
        public short ID
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
    }
}