using DryIoc;

namespace MaoTouGu.Shells
{
    public static partial class Ioc
    {
        static Ioc()
        {
            Container = new Container(x => x.With(FactoryMethod.ConstructorWithResolvableArguments)
                                            .WithTrackingDisposableTransients());
        }

        public static Container Container { get; }
    }
}