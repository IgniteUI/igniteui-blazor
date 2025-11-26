import { IgRect } from './IgRect'
import { IgSize } from './IgSize'
import { IgPoint } from './IgPoint'
import { IgDataTemplate } from './IgDataTemplate'
import { IgcHTMLElement } from './igc-html-element'
import { IgcFormatSpecifier } from "./igc-format-specifier";
import { NumberFormatSpecifier as NumberFormatSpecifier_internal } from "./NumberFormatSpecifier";
import { ensureBool } from "./componentUtil";

export class IgcNumberFormatSpecifier extends IgcFormatSpecifier
{

protected createImplementation() : NumberFormatSpecifier_internal
{
	return new NumberFormatSpecifier_internal();
}
	/**
	                             * @hidden 
	                             */
	                            public get i() : NumberFormatSpecifier_internal {
		return this._implementation;
	}
	constructor() {
		super();
	}
	get locale() : string {
		return (this.i.locale as string);
	}
	set locale(v: string) {
		this.i.locale = v;
	}
	get compactDisplay() : string {
		return (this.i.compactDisplay as string);
	}
	set compactDisplay(v: string) {
		this.i.compactDisplay = v;
	}
	get currency() : string {
		return (this.i.currency as string);
	}
	set currency(v: string) {
		this.i.currency = v;
	}
	get currencyDisplay() : string {
		return (this.i.currencyDisplay as string);
	}
	set currencyDisplay(v: string) {
		this.i.currencyDisplay = v;
	}
	get currencySign() : string {
		return (this.i.currencySign as string);
	}
	set currencySign(v: string) {
		this.i.currencySign = v;
	}
	get currencyCode() : string {
		return (this.i.currencyCode as string);
	}
	set currencyCode(v: string) {
		this.i.currencyCode = v;
	}
	get localeMatcher() : string {
		return (this.i.localeMatcher as string);
	}
	set localeMatcher(v: string) {
		this.i.localeMatcher = v;
	}
	get notation() : string {
		return (this.i.notation as string);
	}
	set notation(v: string) {
		this.i.notation = v;
	}
	get numberingSystem() : string {
		return (this.i.numberingSystem as string);
	}
	set numberingSystem(v: string) {
		this.i.numberingSystem = v;
	}
	get signDisplay() : string {
		return (this.i.signDisplay as string);
	}
	set signDisplay(v: string) {
		this.i.signDisplay = v;
	}
	get style() : string {
		return (this.i.style as string);
	}
	set style(v: string) {
		this.i.style = v;
	}
	get unit() : string {
		return (this.i.unit as string);
	}
	set unit(v: string) {
		this.i.unit = v;
	}
	get unitDisplay() : string {
		return (this.i.unitDisplay as string);
	}
	set unitDisplay(v: string) {
		this.i.unitDisplay = v;
	}
	get useGrouping() : boolean {
		return (this.i.useGrouping as boolean);
	}
	set useGrouping(v: boolean) {
		this.i.useGrouping = ensureBool(v);
	}
	get minimumIntegerDigits() : number {
		return (this.i.minimumIntegerDigits as number);
	}
	set minimumIntegerDigits(v: number) {
		this.i.minimumIntegerDigits = +v;
	}
	get minimumFractionDigits() : number {
		return (this.i.minimumFractionDigits as number);
	}
	set minimumFractionDigits(v: number) {
		this.i.minimumFractionDigits = +v;
	}
	get maximumFractionDigits() : number {
		return (this.i.maximumFractionDigits as number);
	}
	set maximumFractionDigits(v: number) {
		this.i.maximumFractionDigits = +v;
	}
	get minimumSignificantDigits() : number {
		return (this.i.minimumSignificantDigits as number);
	}
	set minimumSignificantDigits(v: number) {
		this.i.minimumSignificantDigits = +v;
	}
	get maximumSignificantDigits() : number {
		return (this.i.maximumSignificantDigits as number);
	}
	set maximumSignificantDigits(v: number) {
		this.i.maximumSignificantDigits = +v;
	}
}
