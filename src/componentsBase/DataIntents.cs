using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{

    
    public interface IDataIntentAttribute
	{
		string Intent { get; }
	}

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
	public class DataIntentAttribute
		: Attribute, IDataIntentAttribute
	{
		public DataIntentAttribute(string intent)
		{
			Intent = intent;
		}

		public string Intent { get; private set; }
	}
}