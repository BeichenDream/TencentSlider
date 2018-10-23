using System;
using System.Drawing;
using System.Windows.Forms;
using Beichen.QQlogin;
namespace type
{
    public partial class Login : Form
    {
        public static Login F;
        public Login()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            F=this;

        }

        private void login_Load(object sender, EventArgs e)
        {
            

           

        }
        


        private void button1_Click_1(object sender, EventArgs e)
        {
            //作者QQ：1489154212     瞎几把交流群：303544938
            Login.F.Log.Text = "日志："+Environment.NewLine;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            string qq, pwd, cap_cd, sid, sess, vsig, rand_par, websig, p, ticket, verify, code, Trajectory,data,cookie;
              int x,y=0;
              qq = pwd = cap_cd = sid = sess = vsig = rand_par = websig = p = ticket = verify = Trajectory=cookie=code = string.Empty;
              qq = user.Text;
              pwd = password.Text;
              QQLogin a = new QQLogin();
            if (a.Is_Code(qq, ref cap_cd, ref code, ref verify))
            {
                Login.F.Log.Text += "需要验证码" + Environment.NewLine;
                a.GetSliderInfo(qq, cap_cd, ref sid, ref sess);
                a.LoadSlider(qq, sid, sess, cap_cd, ref vsig, ref rand_par, ref websig, ref y);
                x = QQcode.SliderCoordinate(sess, sid, vsig,y);
                Trajectory = a.TrajectoryEncryption(x);
                Console.WriteLine(y);
                code = a.SubmissionCode(qq, sess, sid, cap_cd, vsig, x.ToString(), y.ToString(), rand_par, Trajectory, websig, "30", ref ticket);
               data= a.Login(qq, pwd, code, ticket, ref cookie, true);
            }
            else {
            Login.F.Log.Text += "不需要验证码"+ Environment.NewLine;
            data=  a.Login(qq, pwd, code, verify, ref cookie, false);
            }
            Login.F.Log.Text += string.Format("登录总用时{0}s", stopwatch.Elapsed.TotalSeconds) + Environment.NewLine;
            Login.F.Log.Text += "返回数据:"+data+Environment.NewLine;
            Login.F.Log.Text += "返回cookie:" + cookie;
            stopwatch.Stop();
            /* string a, b, c;
          a = "SYQqqFdMyKsNKRSlRE5nak2-6iDS0v7xWZ9Q94Kp1bwhTSNW9gV62Der2xHjEQhbSL1Pe9BzesSxANrMDyX8UKo-Ra3Z_QPotIaRJslfN4OWVEav95T0j8LYJipMhuh_u1zVQmWLNRbQwfBx7sNdz0D-9fP75FnAtvmMEs3YYw5OIDEBEl74b30jWvmVXK1kfVRMwJ97wTU*";
          b = "6614133106531023678";
          c = "c01yOR862wP8JLNGAQKoXqbyIBjUnRhoSWdcjQdtdDP3sMQFTFUifZzg5pzfd3imzpJfraRmeDqCvJePmsr6ez0pJSru1_HospWfIymmESeEHimzzZZvyRj80uN9nNoXaGvukgAunErt-MpL6hACN6r_mJWZzkZZ_spULj5pO3n24eXHrL15mbNZg**";

          Console.WriteLine(QQcode.SliderCoordinate(a,b,c));*/
        }

        private void Log_TextChanged(object sender, EventArgs e)
        {

        }


    }




}