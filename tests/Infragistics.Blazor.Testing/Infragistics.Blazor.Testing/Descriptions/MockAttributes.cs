using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Controls
{
    public abstract class WidgetAttributeBase : Attribute
    {
        internal WidgetAttributeBase()
        {
        }
    }
    public class MainWidget : WidgetAttributeBase
    {
    }

    public class XamWrapperAttribute : WidgetAttributeBase
    {
    }
    public class WebContainerInlineBlock : WidgetAttributeBase
    {
    }
    public class DontObfuscate : WidgetAttributeBase
    {
    }

    public class BlazorSupportsVisualChildren : WidgetAttributeBase
    {
    }

    public class JsonAPIMarshalByValue : WidgetAttributeBase
    {
    }

    public class BlazorPlainObjectAttribute : WidgetAttributeBase
    {
    }

    public class ReactWrapper : WidgetAttributeBase
    {
    }

    public class WCWrapperElement : WidgetAttributeBase
    {
    }

    public class BlazorElement : WidgetAttributeBase
    {
    }

    public class JsonAPIManageCollectionInMarkup : WidgetAttributeBase
    {
    }
    public class JsonAPIManageItemInMarkup : WidgetAttributeBase
    {
    }
    public class SingleInstanceIdentifierAttribute : WidgetAttributeBase
    {
    }

    public class ChildContentCollection : WidgetAttributeBase
    {
    }

    public class UwpWidget : WidgetAttributeBase
    {
    }

    public class WpfWidget : WidgetAttributeBase
    {
    }

    public class JsonAPIPrimitiveValue : WidgetAttributeBase
    {
    }

    public class TSIsCustomEvent : WidgetAttributeBase
    {
    }
    public class BlazorSuppressWidgetAttribute : WidgetAttributeBase
    {
    }
    public class JsonAPISkipSuffix : WidgetAttributeBase
    {
    }

    public class BlazorSynthesizeTwoWayBind : WidgetAttributeBase
    {
    }

    public class JsonAPIPlainObject : WidgetAttributeBase
    {
    }

    public class JsonAPITreatAsCollection : WidgetAttributeBase
    {
    }

    public class BlazorSynthesizeGetMethod : WidgetAttributeBase
    {
    }

    public class DoNotStringifyWidgetMember : WidgetAttributeBase
    {
    }
    public class WCWrapperSkipImport : WidgetAttributeBase
    {
    }
    public class PortalChildrenWC : WidgetAttributeBase
    {
    }
    public class WCWrapperDeferCreateAttribute : WidgetAttributeBase
    {
    }
    public class BlazorSuppressSerialize : WidgetAttributeBase
    {
    }
    public class JsonAPICanMarshalByValue : WidgetAttributeBase
    {
    }
    /// <summary>
    /// Attribute used for cross-platform translation.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Property)]
    public class BlazorNullableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the attribute
        /// </summary>
        public BlazorNullableAttribute()
        {

        }
    }
    public class BlazorSuppressWidgetMember : WidgetAttributeBase
    {
    }

    public class TSOptionalWidgetMember : WidgetAttributeBase
    {
    }
    public class JsonAPITreatAsRef : WidgetAttributeBase
    {
        /// <summary>
        /// Whether to avoid manipulating the property [well-known] type in any way. Primarily for Blazor ATM.
        /// TODO: Consider making this the default behavior and replacing JsonAPITreatAsCollectionAttribute?
        /// </summary>
        public bool PreserveType { get; set; }
    }

    public class BlazorSuppressNativeEvent : WidgetAttributeBase
    {
    }
    public class JsonAPISuppressWidgetMember : WidgetAttributeBase
    {
    }
    public class WCSkipComponentSuffix : WidgetAttributeBase
    {
    }
    public class ReactChildContentCollection : WidgetAttributeBase
    {
    }
    public class BlazorAlwaysWriteback : WidgetAttributeBase
    {
    }
    public class Weak : WidgetAttributeBase
    {
    }
    
    public class WidgetCreatableNonLeaf : WidgetAttributeBase
    {
    }
    public class JsonAPIMustUseNGParentAnchor : WidgetAttributeBase
    {
    }
    public class JsonAPIIgnore : WidgetAttributeBase
    {
    }

    public class JsonAPIRedirectWidget : WidgetAttributeBase
    {
    }
    public class JsonAPIMustCoerceToInt : WidgetAttributeBase
    {
    }
    

    public class TSForceCastDelegate : WidgetAttributeBase
    {
    }
    public class BlazorByValueArray : WidgetAttributeBase
    {
    }

    public class BlazorGenericType : WidgetAttributeBase
    {
    }

    public class TSPlainInterface : WidgetAttributeBase
    {
    }

    public class SkipWCPrefix : WidgetAttributeBase
    {
    }

    public class DontHide : WidgetAttributeBase
    {
    }
    public class WCSkipRegister : WidgetAttributeBase
    {
    }

    public class ManuallyReleasedAttribute : WidgetAttributeBase
    {
    }

    public class WidgetAttribute : Attribute
    {
        public WidgetAttribute()
        {
        }
        public WidgetAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The widget name.
        /// </summary>
        public string Name { get; private set; }
    }
        public class XamWidgetAttribute : WidgetAttribute
    {
        public XamWidgetAttribute()
        {
        }

        public XamWidgetAttribute(string name)
            : base(name)
        {
        }
    }
    public class WCComplexClassType : WidgetAttribute
    {
        public WCComplexClassType()
        {
        }

        public WCComplexClassType(string name)
            : base(name)
        {
        }
    }

    public class WCEnumName : WidgetAttribute
    {
        public WCEnumName()
        {
        }

        public WCEnumName(string name)
            : base(name)
        {
        }
    }
    public class JsonAPINGQueryListName : WidgetAttribute
    {
        public JsonAPINGQueryListName()
        {
        }

        public JsonAPINGQueryListName(string name)
            : base(name)
        {
        }
    }

    public class AdditionalIdentifierPropsAttribute : WidgetAttribute
    {
        public AdditionalIdentifierPropsAttribute()
        {
        }

        public AdditionalIdentifierPropsAttribute(string name)
            : base(name)
        {
        }
    }

    public class WCAttributeName : WidgetAttribute
    {
        public WCAttributeName()
        {
        }

        public WCAttributeName(string name)
            : base(name)
        {
        }
    }
    public class JsonAPIRedirect : WidgetAttribute
    {
        public JsonAPIRedirect()
        {
        }

        public JsonAPIRedirect(string name)
            : base(name)
        {
        }
    }
    
    public class JsonAPIMustSetInCodePlatforms : WidgetAttribute
    {
        public JsonAPIMustSetInCodePlatforms()
        {
        }

        public JsonAPIMustSetInCodePlatforms(string name)
            : base(name)
        {
        }
    }    

    public class WCElementTag : WidgetAttribute
    {
        public WCElementTag()
        {
        }

        public WCElementTag(string name)
            : base(name)
        {
        }
    }
    public class TSTwoWayPropertyAttribute : WidgetAttributeBase
    {
        public TSTwoWayPropertyAttribute(bool isTwoWay = true, string specificEvent = null, string specificProperty = null, bool isHighFrequency = false, bool isComplexObject = false)
        {
            this.IsTwoWay = isTwoWay;
            this.SpecificProperty = specificProperty;
            this.SpecificEvent = specificEvent;
            this.IsComplexObject = isComplexObject;
        }
        public bool IsTwoWay { get; set; }
        public string SpecificEvent { get; set; }

        public string SpecificProperty { get; set; }

        public bool IsComplexObject { get; set; }
    }

    public class BlazorTwoWayBindCondition : WidgetAttribute
    {
        public BlazorTwoWayBindCondition()
        {
        }

        public BlazorTwoWayBindCondition(string name)
            : base(name)
        {
        }
    }
    public class AutoModuleNameAttribute : WidgetAttribute
    {

        /// <summary>
        /// Initializes a new instance of the attribute
        /// </summary>
        /// <param name="name">The widget module name</param>
        public AutoModuleNameAttribute(string name)
            : base(name)
        {
        }
    }

    public class WCPackage : WidgetAttribute
    {
        public WCPackage()
        {
        }

        public WCPackage(string name)
            : base(name)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class TSImportModuleAttribute : WidgetAttributeBase
    {
        public TSImportModuleAttribute(string modulePath, string importedMember)
        {
            ModulePath = modulePath;
            ImportedMember = importedMember;
        }

        public string ModulePath { get; set; }

        public string ImportedMember { get; set; }
    }
    public class WCWidgetMemberName : WidgetAttribute
    {
        public WCWidgetMemberName()
        {
        }

        public WCWidgetMemberName(string name)
            : base(name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public class TSEmitModuleDefinitions : WidgetAttribute
    {
        public TSEmitModuleDefinitions()
        {
        }

        public TSEmitModuleDefinitions(string name)
            : base(name)
        {
        }
    }
    public class WebDeepImport : WidgetAttribute
    {
        public WebDeepImport()
        {
        }

        public WebDeepImport(string name)
            : base(name)
        {
        }
    }

    public class JsonAPIStringEnum : WidgetAttribute
    {
        public JsonAPIStringEnum()
        {
        }

        public JsonAPIStringEnum(string name)
            : base(name)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ContentParent : WidgetAttribute
    {
        public ContentParent()
        {
        }

        public ContentParent(string name)
            : base(name)
        {
        }
    }

    public class JsonAPIWidgetAttribute : WidgetAttribute
    {
        public JsonAPIWidgetAttribute()
        {
        }

        public JsonAPIWidgetAttribute(string name)
            : base(name)
        {
        }
    }

    public class ItemTypeNames : WidgetAttribute
    {
        public ItemTypeNames()
        {
        }

        public ItemTypeNames(string name)
            : base(name)
        {
        }
    }

    public class BlazorWidget : WidgetAttribute
    {
        public BlazorWidget()
        {
        }

        public BlazorWidget(string name)
            : base(name)
        {
        }
    }

    public class TSWidget : WidgetAttribute
    {
        public TSWidget()
        {
        }

        public TSWidget(string name)
            : base(name)
        {
        }
    }

    public class WCWrapperAttribute : WidgetAttributeBase
    {
        public WCWrapperAttribute(string name, bool isDockManager = false)
        {
            Name = name;
            IsDockManager = isDockManager;
        }

        public string Name { get; set; }

        public bool IsDockManager { get; set; }
    }

    public class BlazorUseDirectRender : WidgetAttribute
    {
        public BlazorUseDirectRender()
        {
        }

        public BlazorUseDirectRender(string name)
            : base(name)
        {
        }
    }

    public class TSClientName : WidgetAttribute
    {
        public TSClientName()
        {
        }

        public TSClientName(string name)
            : base(name)
        {
        }
    }

}
