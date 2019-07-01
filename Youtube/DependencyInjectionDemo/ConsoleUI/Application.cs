using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary;

namespace ConsoleUI
{
    public interface IApplication
    {
        void Run();
    }

    /// <summary>
    /// 因为static main, 所以我们用这个class 去DI businessLogic 
    /// </summary>
    public class Application : IApplication
    {
       private readonly IBusinessLogic _businessLogic;

        public Application(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        public void Run()
        {
            _businessLogic.ProcessData();
        }
    }
}
