﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{nodeid}
	/// </pre>
	/// node id is optional
	/// </summary>
	public class NodeIdOptionalDescriptor<P, K> : BasePathDescriptor<P>
		where P : NodeIdOptionalDescriptor<P, K> 
		where K : FluentRequestParameters<K>, new()
	{
		internal string _NodeId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public P NodeId(string nodeId)
		{
			this._NodeId = nodeId;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				NodeId = this._NodeId
			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
