
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{

public partial class IgbDropdown
{
	/// <summary>
	/// Shows the dropdown.
	/// </summary>
	public async  Task ShowAsync(Object target_) 
	                    {
							//Console.WriteLine(ComponentToJson(target_));
		await InvokeMethod("show", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" }, 
        target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
	}
	                    public  void Show(Object target_) 
	                    {
		InvokeMethodSync("show", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" }, 
        target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
	}
	/// <summary>
	/// Toggles the open state of the dropdown.
	/// </summary>
	public async  Task ToggleAsync(Object target_) 
	                    {
		await InvokeMethod("toggle", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" }, 
        target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
	}
	                    public  void Toggle(Object target_) 
	                    {
		InvokeMethodSync("show", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" }, 
        target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
	}


	protected override string ParentTypeName
        {
            get
            {
                return "DropdownParent";
            }
        }

        

        private IgbDropdownItemCollection _contentItems = null;
	
        public IgbDropdownItemCollection ContentItems
        {
        
            get 
            {
                if (this._contentItems == null) {
                    this._contentItems  = new IgbDropdownItemCollection(this, "Items");
                }
                return this._contentItems;
            }
        }

        partial void FindByNameDropdown(string name, ref object item)
        {
            foreach (var it in ContentItems) 
            {
                if (it.Name == name || it.ContainerId == name) {
                    item = it;
                    return;
                }
            }
        }
}

}