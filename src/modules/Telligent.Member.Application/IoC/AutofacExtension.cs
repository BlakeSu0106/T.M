using Autofac;

namespace Telligent.Member.Application.IoC;

public static class AutofacExtension
{
    public static void RegisterUnitOfWork(this ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().AsSelf();
    }
}