using System.Linq;
using System.Reflection;
using Autofac;
using DemoLibrary;
using DemoLibrary.Utilities;

namespace ConsoleUI
{
    /// <summary>
    /// 无所谓这个class 在 ConsoleUI 或 DemoLibrary
    /// DI testable 
    /// </summary>
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<Application>().As<IApplication>();
            builder.Register(c=> new Application(c.Resolve<IBusinessLogic>())).As<IApplication>();

            //builder.RegisterType<RefactorBusinessLogic>().As<IBusinessLogic>(); //等同于 IBusinessLogic xx = new BusinessLogic()
            builder.Register(c => new RefactorBusinessLogic(c.Resolve<ILogger>(), c.Resolve<IDataAccess>())).As<IBusinessLogic>();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(DemoLibrary)))
                //.Where(t => t.Namespace != null && t.Namespace.Contains("Utilities"))
                .Where(t => t.Namespace != null && t.Namespace.Contains(nameof(DemoLibrary.Utilities)))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name)); // 找到所有Interface

            //上面的 RegisterType 顺序是没关系的
            return builder.Build();// build container
        }
    }
}
