
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
                            public partial class IgbNumberFormatSpecifier: IgbFormatSpecifier {
                                public override string Type { get { return "NumberFormatSpecifier"; } }


                                protected override void EnsureModulesLoaded()
                                {
                                    if (!IgbNumberFormatSpecifierModule.IsLoadRequested(IgBlazor))
                                    {
                                        IgbNumberFormatSpecifierModule.Register(IgBlazor);
                                    }
                                }
	
	                    private static bool _marshalByValue = true;
	
	    public IgbNumberFormatSpecifier(): base() {
	        OnCreatedIgbNumberFormatSpecifier();
	
	        
	    }
	
	    partial void OnCreatedIgbNumberFormatSpecifier();
	    
	private string _locale;
	
	partial void OnLocaleChanging(ref string newValue);
	[Parameter]
	public string Locale 
	{
	get { return this._locale; }
	set { 
	                if (this._locale != value || !IsPropDirty("Locale")) {
	                        MarkPropDirty("Locale");
	                } 
	                this._locale = value;
	                 
	                }
	}
	private string _compactDisplay;
	
	partial void OnCompactDisplayChanging(ref string newValue);
	[Parameter]
	public string CompactDisplay 
	{
	get { return this._compactDisplay; }
	set { 
	                if (this._compactDisplay != value || !IsPropDirty("CompactDisplay")) {
	                        MarkPropDirty("CompactDisplay");
	                } 
	                this._compactDisplay = value;
	                 
	                }
	}
	private string _currency;
	
	partial void OnCurrencyChanging(ref string newValue);
	[Parameter]
	public string Currency 
	{
	get { return this._currency; }
	set { 
	                if (this._currency != value || !IsPropDirty("Currency")) {
	                        MarkPropDirty("Currency");
	                } 
	                this._currency = value;
	                 
	                }
	}
	private string _currencyDisplay;
	
	partial void OnCurrencyDisplayChanging(ref string newValue);
	[Parameter]
	public string CurrencyDisplay 
	{
	get { return this._currencyDisplay; }
	set { 
	                if (this._currencyDisplay != value || !IsPropDirty("CurrencyDisplay")) {
	                        MarkPropDirty("CurrencyDisplay");
	                } 
	                this._currencyDisplay = value;
	                 
	                }
	}
	private string _currencySign;
	
	partial void OnCurrencySignChanging(ref string newValue);
	[Parameter]
	public string CurrencySign 
	{
	get { return this._currencySign; }
	set { 
	                if (this._currencySign != value || !IsPropDirty("CurrencySign")) {
	                        MarkPropDirty("CurrencySign");
	                } 
	                this._currencySign = value;
	                 
	                }
	}
	private string _currencyCode;
	
	partial void OnCurrencyCodeChanging(ref string newValue);
	[Parameter]
	public string CurrencyCode 
	{
	get { return this._currencyCode; }
	set { 
	                if (this._currencyCode != value || !IsPropDirty("CurrencyCode")) {
	                        MarkPropDirty("CurrencyCode");
	                } 
	                this._currencyCode = value;
	                 
	                }
	}
	private string _localeMatcher;
	
	partial void OnLocaleMatcherChanging(ref string newValue);
	[Parameter]
	public string LocaleMatcher 
	{
	get { return this._localeMatcher; }
	set { 
	                if (this._localeMatcher != value || !IsPropDirty("LocaleMatcher")) {
	                        MarkPropDirty("LocaleMatcher");
	                } 
	                this._localeMatcher = value;
	                 
	                }
	}
	private string _notation;
	
	partial void OnNotationChanging(ref string newValue);
	[Parameter]
	public string Notation 
	{
	get { return this._notation; }
	set { 
	                if (this._notation != value || !IsPropDirty("Notation")) {
	                        MarkPropDirty("Notation");
	                } 
	                this._notation = value;
	                 
	                }
	}
	private string _numberingSystem;
	
	partial void OnNumberingSystemChanging(ref string newValue);
	[Parameter]
	public string NumberingSystem 
	{
	get { return this._numberingSystem; }
	set { 
	                if (this._numberingSystem != value || !IsPropDirty("NumberingSystem")) {
	                        MarkPropDirty("NumberingSystem");
	                } 
	                this._numberingSystem = value;
	                 
	                }
	}
	private string _signDisplay;
	
	partial void OnSignDisplayChanging(ref string newValue);
	[Parameter]
	public string SignDisplay 
	{
	get { return this._signDisplay; }
	set { 
	                if (this._signDisplay != value || !IsPropDirty("SignDisplay")) {
	                        MarkPropDirty("SignDisplay");
	                } 
	                this._signDisplay = value;
	                 
	                }
	}
	private string _style;
	
	partial void OnStyleChanging(ref string newValue);
	[Parameter]
	public string Style 
	{
	get { return this._style; }
	set { 
	                if (this._style != value || !IsPropDirty("Style")) {
	                        MarkPropDirty("Style");
	                } 
	                this._style = value;
	                 
	                }
	}
	private string _unit;
	
	partial void OnUnitChanging(ref string newValue);
	[Parameter]
	public string Unit 
	{
	get { return this._unit; }
	set { 
	                if (this._unit != value || !IsPropDirty("Unit")) {
	                        MarkPropDirty("Unit");
	                } 
	                this._unit = value;
	                 
	                }
	}
	private string _unitDisplay;
	
	partial void OnUnitDisplayChanging(ref string newValue);
	[Parameter]
	public string UnitDisplay 
	{
	get { return this._unitDisplay; }
	set { 
	                if (this._unitDisplay != value || !IsPropDirty("UnitDisplay")) {
	                        MarkPropDirty("UnitDisplay");
	                } 
	                this._unitDisplay = value;
	                 
	                }
	}
	private bool _useGrouping = false;
	
	partial void OnUseGroupingChanging(ref bool newValue);
	[Parameter]
	public bool UseGrouping 
	{
	get { return this._useGrouping; }
	set { 
	                if (this._useGrouping != value || !IsPropDirty("UseGrouping")) {
	                        MarkPropDirty("UseGrouping");
	                } 
	                this._useGrouping = value;
	                 
	                }
	}
	private int _minimumIntegerDigits = 0;
	
	partial void OnMinimumIntegerDigitsChanging(ref int newValue);
	[Parameter]
	public int MinimumIntegerDigits 
	{
	get { return this._minimumIntegerDigits; }
	set { 
	                if (this._minimumIntegerDigits != value || !IsPropDirty("MinimumIntegerDigits")) {
	                        MarkPropDirty("MinimumIntegerDigits");
	                } 
	                this._minimumIntegerDigits = value;
	                 
	                }
	}
	private int _minimumFractionDigits = 0;
	
	partial void OnMinimumFractionDigitsChanging(ref int newValue);
	[Parameter]
	public int MinimumFractionDigits 
	{
	get { return this._minimumFractionDigits; }
	set { 
	                if (this._minimumFractionDigits != value || !IsPropDirty("MinimumFractionDigits")) {
	                        MarkPropDirty("MinimumFractionDigits");
	                } 
	                this._minimumFractionDigits = value;
	                 
	                }
	}
	private int _maximumFractionDigits = 0;
	
	partial void OnMaximumFractionDigitsChanging(ref int newValue);
	[Parameter]
	public int MaximumFractionDigits 
	{
	get { return this._maximumFractionDigits; }
	set { 
	                if (this._maximumFractionDigits != value || !IsPropDirty("MaximumFractionDigits")) {
	                        MarkPropDirty("MaximumFractionDigits");
	                } 
	                this._maximumFractionDigits = value;
	                 
	                }
	}
	private int _minimumSignificantDigits = 0;
	
	partial void OnMinimumSignificantDigitsChanging(ref int newValue);
	[Parameter]
	public int MinimumSignificantDigits 
	{
	get { return this._minimumSignificantDigits; }
	set { 
	                if (this._minimumSignificantDigits != value || !IsPropDirty("MinimumSignificantDigits")) {
	                        MarkPropDirty("MinimumSignificantDigits");
	                } 
	                this._minimumSignificantDigits = value;
	                 
	                }
	}
	private int _maximumSignificantDigits = 0;
	
	partial void OnMaximumSignificantDigitsChanging(ref int newValue);
	[Parameter]
	public int MaximumSignificantDigits 
	{
	get { return this._maximumSignificantDigits; }
	set { 
	                if (this._maximumSignificantDigits != value || !IsPropDirty("MaximumSignificantDigits")) {
	                        MarkPropDirty("MaximumSignificantDigits");
	                } 
	                this._maximumSignificantDigits = value;
	                 
	                }
	}
	
	    partial void FindByNameNumberFormatSpecifier(string name, ref object item);
	    public override object FindByName(string name)
	    {
	        
	    var baseResult = base.FindByName(name);
	    if (baseResult != null)
	    {
	        return baseResult;
	    }
	
	        object item = null;
	        FindByNameNumberFormatSpecifier(name, ref item);
	        if (item != null)
	        {
	            return item;          
	        }
	
	        return null;
	    }
	
	    partial void SerializeCoreIgbNumberFormatSpecifier(RendererSerializer ser);
	
	    internal override void SerializeCore(RendererSerializer ser) 
	    {
	        base.SerializeCore(ser);
	
	        SerializeCoreIgbNumberFormatSpecifier(ser);
	
	if (IsPropDirty("Locale")) { ser.AddStringProp("locale", this._locale); }
	if (IsPropDirty("CompactDisplay")) { ser.AddStringProp("compactDisplay", this._compactDisplay); }
	if (IsPropDirty("Currency")) { ser.AddStringProp("currency", this._currency); }
	if (IsPropDirty("CurrencyDisplay")) { ser.AddStringProp("currencyDisplay", this._currencyDisplay); }
	if (IsPropDirty("CurrencySign")) { ser.AddStringProp("currencySign", this._currencySign); }
	if (IsPropDirty("CurrencyCode")) { ser.AddStringProp("currencyCode", this._currencyCode); }
	if (IsPropDirty("LocaleMatcher")) { ser.AddStringProp("localeMatcher", this._localeMatcher); }
	if (IsPropDirty("Notation")) { ser.AddStringProp("notation", this._notation); }
	if (IsPropDirty("NumberingSystem")) { ser.AddStringProp("numberingSystem", this._numberingSystem); }
	if (IsPropDirty("SignDisplay")) { ser.AddStringProp("signDisplay", this._signDisplay); }
	if (IsPropDirty("Style")) { ser.AddStringProp("style", this._style); }
	if (IsPropDirty("Unit")) { ser.AddStringProp("unit", this._unit); }
	if (IsPropDirty("UnitDisplay")) { ser.AddStringProp("unitDisplay", this._unitDisplay); }
	if (IsPropDirty("UseGrouping")) { ser.AddBooleanProp("useGrouping", this._useGrouping); }
	if (IsPropDirty("MinimumIntegerDigits")) { ser.AddNumberProp("minimumIntegerDigits", this._minimumIntegerDigits); }
	if (IsPropDirty("MinimumFractionDigits")) { ser.AddNumberProp("minimumFractionDigits", this._minimumFractionDigits); }
	if (IsPropDirty("MaximumFractionDigits")) { ser.AddNumberProp("maximumFractionDigits", this._maximumFractionDigits); }
	if (IsPropDirty("MinimumSignificantDigits")) { ser.AddNumberProp("minimumSignificantDigits", this._minimumSignificantDigits); }
	if (IsPropDirty("MaximumSignificantDigits")) { ser.AddNumberProp("maximumSignificantDigits", this._maximumSignificantDigits); }
	
	    }
	
	
	protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
	    {
	        base.ToEventJson(control, args);
	
	if (IsPropDirty("Locale")) { args["locale"] = this._locale; }
	if (IsPropDirty("CompactDisplay")) { args["compactDisplay"] = this._compactDisplay; }
	if (IsPropDirty("Currency")) { args["currency"] = this._currency; }
	if (IsPropDirty("CurrencyDisplay")) { args["currencyDisplay"] = this._currencyDisplay; }
	if (IsPropDirty("CurrencySign")) { args["currencySign"] = this._currencySign; }
	if (IsPropDirty("CurrencyCode")) { args["currencyCode"] = this._currencyCode; }
	if (IsPropDirty("LocaleMatcher")) { args["localeMatcher"] = this._localeMatcher; }
	if (IsPropDirty("Notation")) { args["notation"] = this._notation; }
	if (IsPropDirty("NumberingSystem")) { args["numberingSystem"] = this._numberingSystem; }
	if (IsPropDirty("SignDisplay")) { args["signDisplay"] = this._signDisplay; }
	if (IsPropDirty("Style")) { args["style"] = this._style; }
	if (IsPropDirty("Unit")) { args["unit"] = this._unit; }
	if (IsPropDirty("UnitDisplay")) { args["unitDisplay"] = this._unitDisplay; }
	if (IsPropDirty("UseGrouping")) { args["useGrouping"] = (this._useGrouping).ToString().ToLower(); }
	if (IsPropDirty("MinimumIntegerDigits")) { args["minimumIntegerDigits"] = (this._minimumIntegerDigits).ToString(); }
	if (IsPropDirty("MinimumFractionDigits")) { args["minimumFractionDigits"] = (this._minimumFractionDigits).ToString(); }
	if (IsPropDirty("MaximumFractionDigits")) { args["maximumFractionDigits"] = (this._maximumFractionDigits).ToString(); }
	if (IsPropDirty("MinimumSignificantDigits")) { args["minimumSignificantDigits"] = (this._minimumSignificantDigits).ToString(); }
	if (IsPropDirty("MaximumSignificantDigits")) { args["maximumSignificantDigits"] = (this._maximumSignificantDigits).ToString(); }
	
	        
	    }
	
	
	    protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args) {
	        base.FromEventJson(control, args);
	        this.SuppressParentNotify = true;
	
	if (args.ContainsKey("locale")) { this.Locale = ReturnToString(args["locale"]); }
	if (args.ContainsKey("compactDisplay")) { this.CompactDisplay = ReturnToString(args["compactDisplay"]); }
	if (args.ContainsKey("currency")) { this.Currency = ReturnToString(args["currency"]); }
	if (args.ContainsKey("currencyDisplay")) { this.CurrencyDisplay = ReturnToString(args["currencyDisplay"]); }
	if (args.ContainsKey("currencySign")) { this.CurrencySign = ReturnToString(args["currencySign"]); }
	if (args.ContainsKey("currencyCode")) { this.CurrencyCode = ReturnToString(args["currencyCode"]); }
	if (args.ContainsKey("localeMatcher")) { this.LocaleMatcher = ReturnToString(args["localeMatcher"]); }
	if (args.ContainsKey("notation")) { this.Notation = ReturnToString(args["notation"]); }
	if (args.ContainsKey("numberingSystem")) { this.NumberingSystem = ReturnToString(args["numberingSystem"]); }
	if (args.ContainsKey("signDisplay")) { this.SignDisplay = ReturnToString(args["signDisplay"]); }
	if (args.ContainsKey("style")) { this.Style = ReturnToString(args["style"]); }
	if (args.ContainsKey("unit")) { this.Unit = ReturnToString(args["unit"]); }
	if (args.ContainsKey("unitDisplay")) { this.UnitDisplay = ReturnToString(args["unitDisplay"]); }
	if (args.ContainsKey("useGrouping")) { this.UseGrouping = ReturnToBoolean(args["useGrouping"]); }
	if (args.ContainsKey("minimumIntegerDigits")) { this.MinimumIntegerDigits = ReturnToInt(args["minimumIntegerDigits"]); }
	if (args.ContainsKey("minimumFractionDigits")) { this.MinimumFractionDigits = ReturnToInt(args["minimumFractionDigits"]); }
	if (args.ContainsKey("maximumFractionDigits")) { this.MaximumFractionDigits = ReturnToInt(args["maximumFractionDigits"]); }
	if (args.ContainsKey("minimumSignificantDigits")) { this.MinimumSignificantDigits = ReturnToInt(args["minimumSignificantDigits"]); }
	if (args.ContainsKey("maximumSignificantDigits")) { this.MaximumSignificantDigits = ReturnToInt(args["maximumSignificantDigits"]); }
	
	        this.SuppressParentNotify = false;
	    }
	
}

public class IgbNumberFormatSpecifierModule 
{
    public static void Register(IIgniteUIBlazor runtime) {
        ModuleLoader.Load(runtime, "NumberFormatSpecifierModule");
    }

    public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
        ModuleLoader.MarkIsLoadRequested(runtime, "NumberFormatSpecifierModule");
    }


    public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
        return ModuleLoader.IsLoadRequested(runtime, "NumberFormatSpecifierModule");
    }
}

}
