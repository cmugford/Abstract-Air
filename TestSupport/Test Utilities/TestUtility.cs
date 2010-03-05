using System;
using System.Collections.Generic;

using AutoMapper;

using Rhino.Mocks;

namespace AbstractAir.TestUtilities
{
	public static class TestUtility
	{
		public static IList<TObject> InstanceCreator<TObject>(int numberOfObjects)
			where TObject : new()
		{
			var instances = new List<TObject>();
			numberOfObjects.Times(() => instances.Add(new TObject()));

			return instances;
		}

		public static IList<TObject> InstanceCreator<TObject>(int numberOfObjects, Func<int, TObject> creator)
		{
			var instances = new List<TObject>();
			numberOfObjects.Times(count => instances.Add(creator(count)));

			return instances;
		}

		public static void MapCollections<TSource, TDestination>(this IMappingEngine mappingEngine, IList<TSource> sourceItems, IList<TDestination> destinationItems)
		{
			for (var itemCount = 0; itemCount < sourceItems.Count; itemCount++)
			{
				var count = itemCount;
				mappingEngine.Stub(engine => engine.Map<TSource, TDestination>(sourceItems[count])).Return(destinationItems[itemCount]);
			}
		}
	}
}