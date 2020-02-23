using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using System.Diagnostics;
using System.Drawing;

namespace ConsoleApplication1
{
    class Program
    {

        static bool is_debug = false;

        static void Debugmsg(string value)
        {
            if(is_debug)
                Console.WriteLine(value);
        }
            

        static void Main(string[] args)
        {
            if (args.GetLength(0) != 0)
                is_debug = true;

            /*input*/
            int limit = 5;  // minimum mouse movement 
            int timeout = 5; //seconds of inactivity to set user idle
            int timer = timeout;

            /*status*/
            bool moveDetected = false;
            bool userIdle = false;
            bool userwasIdle = false;
            System.Drawing.Point oldpos = Cursor.Position;

            while (true)
            {
                System.Drawing.Point pos = Cursor.Position;
                int dx = pos.X - oldpos.X;
                int dy = pos.Y - oldpos.Y;
                oldpos = pos;
                
                if( (Math.Abs(dx)>limit)  || (Math.Abs(dy) > limit))
                {
                    moveDetected = true;
                    userIdle = false;
                    timer = timeout;
                }
                else
                {
                    moveDetected = false;
                    if (timer > 0)
                        timer--;
                    else
                    {
                        userIdle = true;
                        timer = timeout;
                        Debugmsg("Wakeup computer by pressing shift key or move mouse");
                        Cursor.Position = new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10);
                        Thread.Sleep(10);
                        Cursor.Position = new Point(Cursor.Position.X + 10, Cursor.Position.Y + 10);
                    }

                }

                userwasIdle = userIdle;

                Debugmsg("Cursor change[" + dx + ":" + dy + "]");
                Debugmsg("Move detected:[" + moveDetected + "]");
                Debugmsg("User Idle:[" + userIdle + "]");
                Debugmsg("Inactivity counter:[" + timer + "]");

                Thread.Sleep(1000);

            }
        }
    }
}
