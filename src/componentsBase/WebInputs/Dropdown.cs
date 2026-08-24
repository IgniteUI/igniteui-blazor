using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{

    public partial class IgbDropdown
    {
        /// <summary>
        /// Shows the dropdown, anchored to the specified target.
        /// </summary>
        /// <param name="target_">
        /// The element to anchor the dropdown to: either an Ignite UI component whose host
        /// element is the anchor (e.g. an <see cref="IgbButton"/>), or an
        /// <see cref="ElementReference"/> captured from plain markup with <c>@ref</c>.
        /// Any other value is ignored and the dropdown stays anchored to the element in
        /// its <c>target</c> slot.
        /// </param>
        /// <returns><see langword="true"/> when the dropdown was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public async Task<bool> ShowAsync(Object target_)
        {
            //Console.WriteLine(ComponentToJson(target_));
            var iv = await InvokeMethod("show", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" },
            target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Shows the dropdown, anchored to the specified target.
        /// </summary>
        /// <param name="target_">
        /// The element to anchor the dropdown to: either an Ignite UI component whose host
        /// element is the anchor (e.g. an <see cref="IgbButton"/>), or an
        /// <see cref="ElementReference"/> captured from plain markup with <c>@ref</c>.
        /// Any other value is ignored and the dropdown stays anchored to the element in
        /// its <c>target</c> slot.
        /// </param>
        /// <returns><see langword="true"/> when the dropdown was successfully opened,
        /// or <see langword="false"/> if it was already open.</returns>
        public bool Show(Object target_)
        {
            var iv = InvokeMethodSync("show", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" },
            target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the open state of the dropdown, anchoring it to the specified target.
        /// </summary>
        /// <param name="target_">
        /// The element to anchor the dropdown to: either an Ignite UI component whose host
        /// element is the anchor (e.g. an <see cref="IgbButton"/>), or an
        /// <see cref="ElementReference"/> captured from plain markup with <c>@ref</c>.
        /// Any other value is ignored and the dropdown stays anchored to the element in
        /// its <c>target</c> slot.
        /// </param>
        /// <returns><see langword="true"/> when the open state was changed.</returns>
        public async Task<bool> ToggleAsync(Object target_)
        {
            var iv = await InvokeMethod("toggle", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" },
            target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Toggles the open state of the dropdown, anchoring it to the specified target.
        /// </summary>
        /// <param name="target_">
        /// The element to anchor the dropdown to: either an Ignite UI component whose host
        /// element is the anchor (e.g. an <see cref="IgbButton"/>), or an
        /// <see cref="ElementReference"/> captured from plain markup with <c>@ref</c>.
        /// Any other value is ignored and the dropdown stays anchored to the element in
        /// its <c>target</c> slot.
        /// </param>
        /// <returns><see langword="true"/> when the open state was changed.</returns>
        public bool Toggle(Object target_)
        {
            var iv = InvokeMethodSync("toggle", new object[] { ComponentToJson(target_, 0) }, new string[] { "Component" },
            target_ is ElementReference ? new ElementReference[] { (ElementReference)target_ } : null);
            return ReturnToBoolean(iv);
        }

        /// <inheritdoc />
        protected override string ParentTypeName
        {
            get
            {
                return "DropdownParent";
            }
        }

        private IgbDropdownItemCollection? _contentItems = null;

        public IgbDropdownItemCollection ContentItems
        {

            get
            {
                if (this._contentItems == null)
                {
                    this._contentItems = new IgbDropdownItemCollection(this, "Items");
                }
                return this._contentItems;
            }
        }

    }

}
