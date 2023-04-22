using AltkamulAlteqani.Entities.DataContext;
using AltkamulAlteqani.Repository.Repositories;
using AltkamulAlteqani.Service.Services;
using BaseRepository.Repositories;
using BaseRepository.UnitOfWork;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Unity;

namespace AltkamulAlteqani.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<DbContext, LibraryContext>();
            container.RegisterType<IStoredProcedureService, StoredProcedureService>();
            RegisterEntities(container);
        }

        private static void RegisterEntities(IUnityContainer container)
        {
            Assembly assemblerEntity = Assembly.GetAssembly(typeof(LibraryContext));
            IEnumerable<Type> entityTypes = assemblerEntity.GetTypes()
                .Where(e => e.BaseType != null && e.BaseType.Name.Equals("Entity"))
                .ToList();

            foreach (var type in entityTypes)
            {
                var typeFrom = (typeof(IRepositoryAsync<>)).MakeGenericType(type);
                var typeTo = (typeof(Repository<>)).MakeGenericType(type);
                container.RegisterType(typeFrom, typeTo);
            }

            Assembly serviceAssembly = Assembly.GetAssembly(typeof(IStoredProcedureService));
            IEnumerable<Type> serviceTypes = serviceAssembly.GetTypes()
                .Where(e => e.BaseType != null && e.BaseType.Name.IndexOf("Service") == 0)
                .ToList();

            foreach (var type in serviceTypes)
            {
                Type typeTo = type;
                var interfaceTypes = type.GetInterfaces();
                Type typeFrom = null;

                foreach (var t in interfaceTypes)
                {
                    typeFrom = serviceAssembly.GetTypes()
                        .FirstOrDefault(x => x.Name == t.Name);
                    if (typeFrom != null)
                        break;
                }
                if (typeFrom != null)
                {
                    container.RegisterType(typeFrom, typeTo);
                }
            }

        }
    }
}