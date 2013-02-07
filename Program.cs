using System;
using System.Collections.Generic;
using System.Text;

namespace NoZangyo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                checkTimeAndShutdown();
                // 5分ごとに処理。
                System.Threading.Thread.Sleep(600000);
            }
        }

        private static void checkTimeAndShutdown()
        {
            DateTime now = DateTime.Now;
            // 定時超えてたらシャットダウン確認ダイアログ。作者の定時19時をHardCodeしている。
            // TODO : 引数または設定ファイルによる定時指定。
            if (now.CompareTo(new DateTime(now.Year, now.Month, now.Day, 19, 0, 0)) == 1)
            {
                System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show("残業しないで下さい！", "残業しないで下さい！", System.Windows.Forms.MessageBoxButtons.YesNoCancel);
                // Yesなら10分待ってやる。それ以外ならあまり待たなくていい。
                if (res.Equals(System.Windows.Forms.DialogResult.Yes))
                {
                    executeShutdown("600");
                }
                else
                {
                    executeShutdown("120");
                }
            }
        }
        private static void executeShutdown(String shutdownDelay)
        {
            // シェル実行のコマンド作成
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = "shutdown";
            // もし-tオプションの時間指定なければ60秒待ってやる。
            String delay = (String.IsNullOrEmpty(shutdownDelay)) ? "60" : shutdownDelay;
            // 時間指定強制シャットダウン。
            psi.Arguments = "-s -f -t " + delay;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = false;
            // 指定コマンドライン実行。
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
        }
    }
}
