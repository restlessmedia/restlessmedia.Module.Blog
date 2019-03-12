using Autofac;
using restlessmedia.Module.Blog.Data;

namespace restlessmedia.Module.Blog
{
  internal class Module : IModule
  {
    public void RegisterComponents(ContainerBuilder containerBuilder)
    {
      containerBuilder.RegisterType<BlogDataProvider>().As<IBlogDataProvider>().SingleInstance();
      containerBuilder.RegisterType<BlogService>().As<IBlogService>().SingleInstance();
    }
  }
}