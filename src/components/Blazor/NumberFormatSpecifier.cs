using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Number formatting options for component values. The options mirror the number formatting
    /// options of the browser and are applied by the browser number formatter.
    /// </summary>
    public partial class IgbNumberFormatSpecifier : IgbFormatSpecifier
    {
        /// <inheritdoc />
        public override string Type { get { return "NumberFormatSpecifier"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbNumberFormatSpecifierModule.IsLoadRequested(IgBlazor))
            {
                IgbNumberFormatSpecifierModule.Register(IgBlazor);
            }
        }

        private static bool _marshalByValue = true;

        private string _locale;

        /// <summary>
        /// The culture used to format the number. When not set, the browser culture returned by
        /// <see cref="IgbFormatSpecifier.GetLocalCulture"/> is used.
        /// </summary>
        [Parameter]
        public string Locale
        {
            get { return this._locale; }
            set
            {
                if (this._locale != value || !IsPropDirty("Locale"))
                {
                    MarkPropDirty("Locale");
                }
                this._locale = value;

            }
        }
        private string _compactDisplay;

        /// <summary>
        /// The form of the compact notation, either <c>short</c> or <c>long</c>. Applies only when
        /// <see cref="Notation"/> is <c>compact</c>.
        /// </summary>
        [Parameter]
        public string CompactDisplay
        {
            get { return this._compactDisplay; }
            set
            {
                if (this._compactDisplay != value || !IsPropDirty("CompactDisplay"))
                {
                    MarkPropDirty("CompactDisplay");
                }
                this._compactDisplay = value;

            }
        }
        private string _currency;

        /// <summary>
        /// The currency used in currency formatting, given as an ISO 4217 currency code.
        /// </summary>
        [Parameter]
        public string Currency
        {
            get { return this._currency; }
            set
            {
                if (this._currency != value || !IsPropDirty("Currency"))
                {
                    MarkPropDirty("Currency");
                }
                this._currency = value;

            }
        }
        private string _currencyDisplay;

        /// <summary>
        /// How the currency is shown, one of <c>symbol</c>, <c>narrowSymbol</c>, <c>code</c> or
        /// <c>name</c>.
        /// </summary>
        [Parameter]
        public string CurrencyDisplay
        {
            get { return this._currencyDisplay; }
            set
            {
                if (this._currencyDisplay != value || !IsPropDirty("CurrencyDisplay"))
                {
                    MarkPropDirty("CurrencyDisplay");
                }
                this._currencyDisplay = value;

            }
        }
        private string _currencySign;

        /// <summary>
        /// How negative currency amounts are rendered, either <c>standard</c> or <c>accounting</c>.
        /// </summary>
        [Parameter]
        public string CurrencySign
        {
            get { return this._currencySign; }
            set
            {
                if (this._currencySign != value || !IsPropDirty("CurrencySign"))
                {
                    MarkPropDirty("CurrencySign");
                }
                this._currencySign = value;

            }
        }
        private string _currencyCode;

        /// <summary>
        /// The currency code applied when <see cref="Style"/> is <c>currency</c>. It takes precedence
        /// over <see cref="Currency"/>; when not set, the code is resolved from the culture.
        /// </summary>
        [Parameter]
        public string CurrencyCode
        {
            get { return this._currencyCode; }
            set
            {
                if (this._currencyCode != value || !IsPropDirty("CurrencyCode"))
                {
                    MarkPropDirty("CurrencyCode");
                }
                this._currencyCode = value;

            }
        }
        private string _localeMatcher;

        /// <summary>
        /// The locale matching algorithm, either <c>lookup</c> or <c>best fit</c>.
        /// </summary>
        [Parameter]
        public string LocaleMatcher
        {
            get { return this._localeMatcher; }
            set
            {
                if (this._localeMatcher != value || !IsPropDirty("LocaleMatcher"))
                {
                    MarkPropDirty("LocaleMatcher");
                }
                this._localeMatcher = value;

            }
        }
        private string _notation;

        /// <summary>
        /// The formatting notation, one of <c>standard</c>, <c>scientific</c>, <c>engineering</c> or
        /// <c>compact</c>.
        /// </summary>
        [Parameter]
        public string Notation
        {
            get { return this._notation; }
            set
            {
                if (this._notation != value || !IsPropDirty("Notation"))
                {
                    MarkPropDirty("Notation");
                }
                this._notation = value;

            }
        }
        private string _numberingSystem;

        /// <summary>
        /// The numbering system used to render the digits.
        /// </summary>
        [Parameter]
        public string NumberingSystem
        {
            get { return this._numberingSystem; }
            set
            {
                if (this._numberingSystem != value || !IsPropDirty("NumberingSystem"))
                {
                    MarkPropDirty("NumberingSystem");
                }
                this._numberingSystem = value;

            }
        }
        private string _signDisplay;

        /// <summary>
        /// When the sign is shown, one of <c>auto</c>, <c>never</c>, <c>always</c> or
        /// <c>exceptZero</c>.
        /// </summary>
        [Parameter]
        public string SignDisplay
        {
            get { return this._signDisplay; }
            set
            {
                if (this._signDisplay != value || !IsPropDirty("SignDisplay"))
                {
                    MarkPropDirty("SignDisplay");
                }
                this._signDisplay = value;

            }
        }
        private string _style;

        /// <summary>
        /// The formatting style, one of <c>decimal</c>, <c>currency</c>, <c>percent</c> or
        /// <c>unit</c>.
        /// </summary>
        [Parameter]
        public string Style
        {
            get { return this._style; }
            set
            {
                if (this._style != value || !IsPropDirty("Style"))
                {
                    MarkPropDirty("Style");
                }
                this._style = value;

            }
        }
        private string _unit;

        /// <summary>
        /// The unit used when <see cref="Style"/> is <c>unit</c>.
        /// </summary>
        [Parameter]
        public string Unit
        {
            get { return this._unit; }
            set
            {
                if (this._unit != value || !IsPropDirty("Unit"))
                {
                    MarkPropDirty("Unit");
                }
                this._unit = value;

            }
        }
        private string _unitDisplay;

        /// <summary>
        /// How the unit is shown, one of <c>short</c>, <c>narrow</c> or <c>long</c>.
        /// </summary>
        [Parameter]
        public string UnitDisplay
        {
            get { return this._unitDisplay; }
            set
            {
                if (this._unitDisplay != value || !IsPropDirty("UnitDisplay"))
                {
                    MarkPropDirty("UnitDisplay");
                }
                this._unitDisplay = value;

            }
        }
        private bool _useGrouping = false;

        /// <summary>
        /// Whether grouping separators, such as thousands separators, are used.
        /// </summary>
        [Parameter]
        public bool UseGrouping
        {
            get { return this._useGrouping; }
            set
            {
                if (this._useGrouping != value || !IsPropDirty("UseGrouping"))
                {
                    MarkPropDirty("UseGrouping");
                }
                this._useGrouping = value;

            }
        }
        private int _minimumIntegerDigits = 0;

        /// <summary>
        /// The minimum number of integer digits to use.
        /// </summary>
        [Parameter]
        public int MinimumIntegerDigits
        {
            get { return this._minimumIntegerDigits; }
            set
            {
                if (this._minimumIntegerDigits != value || !IsPropDirty("MinimumIntegerDigits"))
                {
                    MarkPropDirty("MinimumIntegerDigits");
                }
                this._minimumIntegerDigits = value;

            }
        }
        private int _minimumFractionDigits = 0;

        /// <summary>
        /// The minimum number of fraction digits to use.
        /// </summary>
        [Parameter]
        public int MinimumFractionDigits
        {
            get { return this._minimumFractionDigits; }
            set
            {
                if (this._minimumFractionDigits != value || !IsPropDirty("MinimumFractionDigits"))
                {
                    MarkPropDirty("MinimumFractionDigits");
                }
                this._minimumFractionDigits = value;

            }
        }
        private int _maximumFractionDigits = 0;

        /// <summary>
        /// The maximum number of fraction digits to use.
        /// </summary>
        [Parameter]
        public int MaximumFractionDigits
        {
            get { return this._maximumFractionDigits; }
            set
            {
                if (this._maximumFractionDigits != value || !IsPropDirty("MaximumFractionDigits"))
                {
                    MarkPropDirty("MaximumFractionDigits");
                }
                this._maximumFractionDigits = value;

            }
        }
        private int _minimumSignificantDigits = 0;

        /// <summary>
        /// The minimum number of significant digits to use.
        /// </summary>
        [Parameter]
        public int MinimumSignificantDigits
        {
            get { return this._minimumSignificantDigits; }
            set
            {
                if (this._minimumSignificantDigits != value || !IsPropDirty("MinimumSignificantDigits"))
                {
                    MarkPropDirty("MinimumSignificantDigits");
                }
                this._minimumSignificantDigits = value;

            }
        }
        private int _maximumSignificantDigits = 0;

        /// <summary>
        /// The maximum number of significant digits to use.
        /// </summary>
        [Parameter]
        public int MaximumSignificantDigits
        {
            get { return this._maximumSignificantDigits; }
            set
            {
                if (this._maximumSignificantDigits != value || !IsPropDirty("MaximumSignificantDigits"))
                {
                    MarkPropDirty("MaximumSignificantDigits");
                }
                this._maximumSignificantDigits = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Locale"))
            { ser.AddStringProp("locale", this._locale); }
            if (IsPropDirty("CompactDisplay"))
            { ser.AddStringProp("compactDisplay", this._compactDisplay); }
            if (IsPropDirty("Currency"))
            { ser.AddStringProp("currency", this._currency); }
            if (IsPropDirty("CurrencyDisplay"))
            { ser.AddStringProp("currencyDisplay", this._currencyDisplay); }
            if (IsPropDirty("CurrencySign"))
            { ser.AddStringProp("currencySign", this._currencySign); }
            if (IsPropDirty("CurrencyCode"))
            { ser.AddStringProp("currencyCode", this._currencyCode); }
            if (IsPropDirty("LocaleMatcher"))
            { ser.AddStringProp("localeMatcher", this._localeMatcher); }
            if (IsPropDirty("Notation"))
            { ser.AddStringProp("notation", this._notation); }
            if (IsPropDirty("NumberingSystem"))
            { ser.AddStringProp("numberingSystem", this._numberingSystem); }
            if (IsPropDirty("SignDisplay"))
            { ser.AddStringProp("signDisplay", this._signDisplay); }
            if (IsPropDirty("Style"))
            { ser.AddStringProp("style", this._style); }
            if (IsPropDirty("Unit"))
            { ser.AddStringProp("unit", this._unit); }
            if (IsPropDirty("UnitDisplay"))
            { ser.AddStringProp("unitDisplay", this._unitDisplay); }
            if (IsPropDirty("UseGrouping"))
            { ser.AddBooleanProp("useGrouping", this._useGrouping); }
            if (IsPropDirty("MinimumIntegerDigits"))
            { ser.AddNumberProp("minimumIntegerDigits", this._minimumIntegerDigits); }
            if (IsPropDirty("MinimumFractionDigits"))
            { ser.AddNumberProp("minimumFractionDigits", this._minimumFractionDigits); }
            if (IsPropDirty("MaximumFractionDigits"))
            { ser.AddNumberProp("maximumFractionDigits", this._maximumFractionDigits); }
            if (IsPropDirty("MinimumSignificantDigits"))
            { ser.AddNumberProp("minimumSignificantDigits", this._minimumSignificantDigits); }
            if (IsPropDirty("MaximumSignificantDigits"))
            { ser.AddNumberProp("maximumSignificantDigits", this._maximumSignificantDigits); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Locale"))
            { args["locale"] = this._locale; }
            if (IsPropDirty("CompactDisplay"))
            { args["compactDisplay"] = this._compactDisplay; }
            if (IsPropDirty("Currency"))
            { args["currency"] = this._currency; }
            if (IsPropDirty("CurrencyDisplay"))
            { args["currencyDisplay"] = this._currencyDisplay; }
            if (IsPropDirty("CurrencySign"))
            { args["currencySign"] = this._currencySign; }
            if (IsPropDirty("CurrencyCode"))
            { args["currencyCode"] = this._currencyCode; }
            if (IsPropDirty("LocaleMatcher"))
            { args["localeMatcher"] = this._localeMatcher; }
            if (IsPropDirty("Notation"))
            { args["notation"] = this._notation; }
            if (IsPropDirty("NumberingSystem"))
            { args["numberingSystem"] = this._numberingSystem; }
            if (IsPropDirty("SignDisplay"))
            { args["signDisplay"] = this._signDisplay; }
            if (IsPropDirty("Style"))
            { args["style"] = this._style; }
            if (IsPropDirty("Unit"))
            { args["unit"] = this._unit; }
            if (IsPropDirty("UnitDisplay"))
            { args["unitDisplay"] = this._unitDisplay; }
            if (IsPropDirty("UseGrouping"))
            { args["useGrouping"] = (this._useGrouping).ToString().ToLower(); }
            if (IsPropDirty("MinimumIntegerDigits"))
            { args["minimumIntegerDigits"] = (this._minimumIntegerDigits).ToString(); }
            if (IsPropDirty("MinimumFractionDigits"))
            { args["minimumFractionDigits"] = (this._minimumFractionDigits).ToString(); }
            if (IsPropDirty("MaximumFractionDigits"))
            { args["maximumFractionDigits"] = (this._maximumFractionDigits).ToString(); }
            if (IsPropDirty("MinimumSignificantDigits"))
            { args["minimumSignificantDigits"] = (this._minimumSignificantDigits).ToString(); }
            if (IsPropDirty("MaximumSignificantDigits"))
            { args["maximumSignificantDigits"] = (this._maximumSignificantDigits).ToString(); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("locale"))
            { this.Locale = ReturnToString(args["locale"]); }
            if (args.ContainsKey("compactDisplay"))
            { this.CompactDisplay = ReturnToString(args["compactDisplay"]); }
            if (args.ContainsKey("currency"))
            { this.Currency = ReturnToString(args["currency"]); }
            if (args.ContainsKey("currencyDisplay"))
            { this.CurrencyDisplay = ReturnToString(args["currencyDisplay"]); }
            if (args.ContainsKey("currencySign"))
            { this.CurrencySign = ReturnToString(args["currencySign"]); }
            if (args.ContainsKey("currencyCode"))
            { this.CurrencyCode = ReturnToString(args["currencyCode"]); }
            if (args.ContainsKey("localeMatcher"))
            { this.LocaleMatcher = ReturnToString(args["localeMatcher"]); }
            if (args.ContainsKey("notation"))
            { this.Notation = ReturnToString(args["notation"]); }
            if (args.ContainsKey("numberingSystem"))
            { this.NumberingSystem = ReturnToString(args["numberingSystem"]); }
            if (args.ContainsKey("signDisplay"))
            { this.SignDisplay = ReturnToString(args["signDisplay"]); }
            if (args.ContainsKey("style"))
            { this.Style = ReturnToString(args["style"]); }
            if (args.ContainsKey("unit"))
            { this.Unit = ReturnToString(args["unit"]); }
            if (args.ContainsKey("unitDisplay"))
            { this.UnitDisplay = ReturnToString(args["unitDisplay"]); }
            if (args.ContainsKey("useGrouping"))
            { this.UseGrouping = ReturnToBoolean(args["useGrouping"]); }
            if (args.ContainsKey("minimumIntegerDigits"))
            { this.MinimumIntegerDigits = ReturnToInt(args["minimumIntegerDigits"]); }
            if (args.ContainsKey("minimumFractionDigits"))
            { this.MinimumFractionDigits = ReturnToInt(args["minimumFractionDigits"]); }
            if (args.ContainsKey("maximumFractionDigits"))
            { this.MaximumFractionDigits = ReturnToInt(args["maximumFractionDigits"]); }
            if (args.ContainsKey("minimumSignificantDigits"))
            { this.MinimumSignificantDigits = ReturnToInt(args["minimumSignificantDigits"]); }
            if (args.ContainsKey("maximumSignificantDigits"))
            { this.MaximumSignificantDigits = ReturnToInt(args["maximumSignificantDigits"]); }

            this.SuppressParentNotify = false;
        }

    }

    public class IgbNumberFormatSpecifierModule
    {
        public static void Register(IIgniteUIBlazor runtime)
        {
            ModuleLoader.Load(runtime, "NumberFormatSpecifierModule");
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime)
        {
            ModuleLoader.MarkIsLoadRequested(runtime, "NumberFormatSpecifierModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime)
        {
            return ModuleLoader.IsLoadRequested(runtime, "NumberFormatSpecifierModule");
        }
    }

}
