using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i18n
{
    public partial class Form1 : Form
    {
        private ResourceManager _localResource = null;

        private ComponentResourceManager _ResourceManager = new ComponentResourceManager();

        private CultureInfo ci;
        private string lang = "zh-TW";

        public Form1()
        {
            InitializeComponent();
            this._localResource = new ResourceManager("i18n.Form1", Assembly.GetExecutingAssembly());

            this._localResource.IgnoreCase = false;
        }

        private void btnChangeLang_Click(object sender, EventArgs e)
        {
            //取得語系資料 zh-TW etc..
            string UICulture = Thread.CurrentThread.CurrentUICulture.Name;
            //更換語系
            UICulture = UICulture == "zh-TW" ? "en-US" : "zh-TW";
            //設定文化特性
            ci = new CultureInfo(UICulture);
            //變更文化特性
            Thread.CurrentThread.CurrentUICulture = ci;
            //取得資源檔
            this._ResourceManager = new ComponentResourceManager(this.GetType());
            //套用語系
            this._ResourceManager.ApplyResources(this, "$this");
            SetLang(this);
        }

        //更換語系
        private void SetLang(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                this._ResourceManager.ApplyResources(ctrl, ctrl.Name);
                if (ctrl.HasChildren)
                {
                    SetLang(ctrl);
                }
            }
        }
    }
}
