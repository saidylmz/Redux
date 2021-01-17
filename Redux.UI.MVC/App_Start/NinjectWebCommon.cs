[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Redux.UI.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Redux.UI.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace Redux.UI.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MusicStore.UI.MVC.Tools;
    using Ninject;
    using Ninject.Web.Common;
    using Redux.BLL.Abstract;
    using Redux.BLL.Concrete;
    using Redux.BLL.IoC;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IMediaService>().To<MediaService>();
            kernel.Bind<IModService>().To<ModService>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Load<ReduxDALModule>();
        }        
    }
}
