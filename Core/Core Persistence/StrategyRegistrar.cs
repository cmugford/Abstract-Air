using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using AbstractAir.Persistence.Properties;

using NHibernate;
using NHibernate.Metadata;

using StructureMap;

namespace AbstractAir.Persistence
{
	public class StrategyRegistrar : IStrategyRegistrar
	{
		private readonly ISessionFactory _sessionFactory;

		public StrategyRegistrar(ISessionFactory sessionFactory)
		{
			_sessionFactory = ArgumentValidation.IsNotNull(sessionFactory, "sessionFactory");
		}

		public void Register()
		{
			_sessionFactory.GetAllClassMetadata()
				.Values
				.Apply(ProcessClassMetadata);
		}

		private static void ProcessClassMetadata(IClassMetadata classMetadata)
		{
			var mappedClass = classMetadata.GetMappedClass(EntityMode.Poco);
			if (mappedClass == null || !typeof(IEntity).IsAssignableFrom(mappedClass))
			{
				return;
			}

			RegisterDefaultStrategies(mappedClass);

			ConfigureRoles(mappedClass, typeof(IRepository<>), typeof(Repository<,>));
			ConfigureRoles(mappedClass, typeof(ICreationStrategy<>), typeof(DefaultCreationStrategy<,>));
		}

		private static void RegisterDefaultStrategies(Type mappedClass)
		{
			ObjectFactory.Configure(configure =>
				{
					configure.For(typeof(ICreationStrategy<>).MakeGenericType(mappedClass))
						.Use(typeof(DefaultCreationStrategy<,>).MakeGenericType(new[] {mappedClass, mappedClass}));
					configure.For(typeof(IRepository<>).MakeGenericType(mappedClass))
						.Use(typeof(Repository<,>).MakeGenericType(new[] {mappedClass, mappedClass}));
				});
		}

		private static void ConfigureRoles(Type mappedClass, Type requestedType, Type defaultType)
		{
			var roles = RegisterRoleInterfaces(mappedClass, requestedType);

			ObjectFactory.Configure(configure =>
				roles.Keys.Apply(key =>
					configure.For(requestedType.MakeGenericType(key))
						.Use(defaultType.MakeGenericType(new[] {key, roles[key]}))));
		}

		private static IDictionary<Type, Type> RegisterRoleInterfaces(Type mappedClass, Type requestedType)
		{
			var roles = new Dictionary<Type, Type>();

			foreach (var roleInterface in mappedClass.GetInterfaces()
				.Where(interfaceType => interfaceType != typeof(IEntity) && typeof(IEntity).IsAssignableFrom(interfaceType)))
			{
				RegisterRoleInterface(mappedClass, roles, roleInterface, requestedType);
			}

			return roles;
		}

		private static void RegisterRoleInterface(Type mappedClass, IDictionary<Type, Type> roles, Type roleInterface, Type requestedType)
		{
			if (roles.ContainsKey(roleInterface))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
					Resources.MultipleTypesImplementTheSameRoleErrorFormat,
					roleInterface));
			}

			if (ObjectFactory.TryGetInstance(requestedType.MakeGenericType(roleInterface)) != null)
			{
				return;
			}
			roles.Add(roleInterface, mappedClass);
		}
	}
}