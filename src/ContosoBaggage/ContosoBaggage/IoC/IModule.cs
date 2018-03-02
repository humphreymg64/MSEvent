
namespace ContosoBaggage
{
    using Autofac;

    /// <summary>
    /// Module.
    /// </summary>
    public interface IModule
    {
        #region Methods

        /// <summary>
        /// Register the specified builder.
        /// </summary>
        /// <param name="builder">builder.</param>
        void Register(ContainerBuilder builder);

        #endregion
    }
}