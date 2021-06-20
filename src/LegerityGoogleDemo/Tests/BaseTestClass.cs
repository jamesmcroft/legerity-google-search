namespace LegerityGoogleDemo.Tests
{
    using System;
    using System.IO;
    using Legerity;
    using Legerity.Web;
    using NUnit.Framework;

    public abstract class BaseTestClass
    {
        public const string EdgeTools = "Tools";

        public static WebAppManagerOptions GetEdgeOptions(string url)
        {
            return new(
                WebAppDriverType.Edge,
                Path.Combine(Environment.CurrentDirectory, EdgeTools)) {Url = url};
        }

        [TearDown]
        public virtual void Cleanup()
        {
            AppManager.StopApp();
        }
    }
}