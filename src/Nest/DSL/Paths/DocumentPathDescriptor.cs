﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{

	public class DocumentPathDescriptorBase<P, T, K>
		where P : DocumentPathDescriptorBase<P, T, K>, new() where T : class
		where K : FluentQueryString<K>, new()
	{

		internal string _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		internal string _Id { get; set; }
		internal T _Object { get; set; }

		public P Index(string index)
		{
			this._Index = index;
			return (P)this;
		}
		
		public P Type(string type)
		{
			this._Type = type;
			return (P)this;
		}
		public P Type(Type type)
		{
			this._Type = type;
			return (P)this;
		}
		public P Id(int id)
		{
			return this.Id(id.ToString());
		}
		public P Id(string id)
		{
			this._Id = id;
			return (P)this;
		}
		public P Object(T @object)
		{
			this._Object = @object;
			return (P)this;
		}
		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings) 
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
            var index = this._Index ?? inferrer.IndexName<T>();
            var type = this._Type != null ? this._Type.Resolve(settings) : inferrer.TypeName<T>();
            var id = this._Id ?? inferrer.Id(this._Object);
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
				Type = type,
				Id = id
			};
			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}