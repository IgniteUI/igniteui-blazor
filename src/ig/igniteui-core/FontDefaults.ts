import { Base, Type, markType } from "./type";
import { BrushUtil } from "./BrushUtil";
import { Brush } from "./Brush";
import { DeviceUtils } from "./DeviceUtils";

/**
 * @hidden 
 */
export class FontDefaults extends Base {
	static $t: Type = markType(FontDefaults, 'FontDefaults');
	static toFontFamily(defaultFont: string): string {
		return "Titillium Web, " + defaultFont + ", Arial, sans-serif";
	}
	static readonly legendLabelsBrush: Brush = BrushUtil.fromArgb(255, 37, 37, 37);
	static readonly legendLabelsFontSizeConst: number = 13;
	static readonly legendLabelsFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.legendLabelsFontSizeConst);
	static readonly legendLabelsFontConst: string = "Verdana";
	static readonly legendLabelsFontFamily: string = FontDefaults.toFontFamily(FontDefaults.legendLabelsFontConst);
	static readonly dataLegendFontBrush: Brush = BrushUtil.fromArgb(255, 37, 37, 37);
	static readonly dataLegendFontSizeConst: number = 13;
	static readonly dataLegendFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.dataLegendFontSizeConst);
	static readonly dataLegendFontFamilyConst: string = "Verdana";
	static readonly dataLegendFontFamily: string = FontDefaults.toFontFamily(FontDefaults.dataLegendFontFamilyConst);
	static readonly dataLegendFontWeight: string = "400";
	static readonly dataLegendFontStyle: string = "Normal";
	static readonly dataLegendFontStretch: string = "Normal";
	static readonly dataLegendFontVariant: string = "Normal";
	static readonly dataLegendHeaderFontWeight: string = "600";
	static readonly chartAxisLabelBrush: Brush = BrushUtil.fromArgb(255, 78, 78, 78);
	static readonly chartFontSizeConst: number = 13;
	static readonly chartFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.chartFontSizeConst);
	static readonly chartFontNameConst: string = "Verdana";
	static readonly chartXAxisLabelsMarginTop: number = 5;
	static readonly chartXAxisLabelsMarginBottom: number = 5;
	static readonly chartXAxisLabelsMarginLeft: number = 2;
	static readonly chartXAxisLabelsMarginRight: number = 2;
	static readonly chartYAxisLabelsMarginTop: number = 2;
	static readonly chartYAxisLabelsMarginBottom: number = 2;
	static readonly chartYAxisLabelsMarginLeft: number = 5;
	static readonly chartYAxisLabelsMarginRight: number = 5;
	static readonly chartFontFamily: string = FontDefaults.toFontFamily(FontDefaults.chartFontNameConst);
	static readonly chartFontWeight: string = "400";
	static readonly chartFontStyle: string = "Normal";
	static readonly chartFontStretch: string = "Normal";
	static readonly chartFontVariant: string = "Normal";
	static readonly chartTitleFontSizeConst: number = 20;
	static readonly chartTitleFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.chartTitleFontSizeConst);
	static readonly chartTitleNameConst: string = "Verdana";
	static readonly chartTitleFontWeight: string = "600";
	static readonly chartSubtitleFontSizeConst: number = 20;
	static readonly chartSubtitleFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.chartSubtitleFontSizeConst);
	static readonly chartSubtitleNameConst: string = "Verdana";
	static readonly chartSubtitleFontWeight: string = "600";
	static readonly tooltipLabelsBrush: Brush = BrushUtil.fromArgb(255, 78, 78, 78);
	static readonly tooltipLabelsFontSizeConst: number = 13;
	static readonly tooltipLabelsFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.tooltipLabelsFontSizeConst);
	static readonly tooltipFontNameConst: string = "Verdana";
	static readonly tooltipLabelsFontFamily: string = FontDefaults.toFontFamily(FontDefaults.tooltipFontNameConst);
	static readonly gaugesFontBrush: Brush = BrushUtil.fromArgb(255, 97, 97, 97);
	static readonly gaugesFontSizeConst: number = 12;
	static readonly gaugesFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.gaugesFontSizeConst);
	static readonly gaugesFontFamilyConst: string = "Verdana";
	static readonly gaugesFontWeight: string = "600";
	static readonly gaugesFontFamily: string = FontDefaults.toFontFamily(FontDefaults.gaugesFontFamilyConst);
	static readonly gaugesFontStyle: string = "Normal";
	static readonly gaugesFontStretch: string = "Normal";
	static readonly gaugesFontVariant: string = "Normal";
	static readonly gaugesTitleSizeConst: number = FontDefaults.gaugesFontSizeConst * 2;
	static readonly gaugesTitleSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.gaugesTitleSizeConst);
	static readonly gaugesSubtleSizeConst: number = FontDefaults.gaugesFontSizeConst * 2;
	static readonly gaugesSubtitleSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.gaugesSubtleSizeConst);
	static readonly gaugesHighlightLabelSizeConst: number = FontDefaults.gaugesFontSizeConst * 2;
	static readonly gaugesHighlightLabelSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.gaugesHighlightLabelSizeConst);
	static readonly treemapFontSizeConst: number = 13;
	static readonly treemapFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.treemapFontSizeConst);
	static readonly treemapHeaderFontSizeConst: number = 13;
	static readonly treemapHeaderFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.treemapHeaderFontSizeConst);
	static readonly treemapFontFamilyConst: string = "Verdana";
	static readonly treemapFontFamily: string = FontDefaults.toFontFamily(FontDefaults.treemapFontFamilyConst);
	static readonly treemapHeaderFontFamily: string = FontDefaults.toFontFamily(FontDefaults.treemapFontFamilyConst);
	static readonly zoomSliderFontSizeConst: number = 10;
	static readonly zoomSliderFontSize: number = DeviceUtils.toFontPixelUnits(FontDefaults.zoomSliderFontSizeConst);
	static readonly zoomSliderNameConst: string = "Verdana";
}


