import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCarouselDescription extends Description {
	static $t: Type = markType(WebCarouselDescription, 'WebCarouselDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCarousel";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _disableLoop: boolean = false;
	get disableLoop(): boolean {
		return this._disableLoop;
	}
	set disableLoop(value: boolean) {
		this._disableLoop = value;
		this.markDirty("DisableLoop");
	}
	private _disablePauseOnInteraction: boolean = false;
	get disablePauseOnInteraction(): boolean {
		return this._disablePauseOnInteraction;
	}
	set disablePauseOnInteraction(value: boolean) {
		this._disablePauseOnInteraction = value;
		this.markDirty("DisablePauseOnInteraction");
	}
	private _hideNavigation: boolean = false;
	get hideNavigation(): boolean {
		return this._hideNavigation;
	}
	set hideNavigation(value: boolean) {
		this._hideNavigation = value;
		this.markDirty("HideNavigation");
	}
	private _hideIndicators: boolean = false;
	get hideIndicators(): boolean {
		return this._hideIndicators;
	}
	set hideIndicators(value: boolean) {
		this._hideIndicators = value;
		this.markDirty("HideIndicators");
	}
	private _vertical: boolean = false;
	get vertical(): boolean {
		return this._vertical;
	}
	set vertical(value: boolean) {
		this._vertical = value;
		this.markDirty("Vertical");
	}
	private _indicatorsOrientation: string = null;
	get indicatorsOrientation(): string {
		return this._indicatorsOrientation;
	}
	set indicatorsOrientation(value: string) {
		this._indicatorsOrientation = value;
		this.markDirty("IndicatorsOrientation");
	}
	private _indicatorsLabelFormat: string = null;
	get indicatorsLabelFormat(): string {
		return this._indicatorsLabelFormat;
	}
	set indicatorsLabelFormat(value: string) {
		this._indicatorsLabelFormat = value;
		this.markDirty("IndicatorsLabelFormat");
	}
	private _slidesLabelFormat: string = null;
	get slidesLabelFormat(): string {
		return this._slidesLabelFormat;
	}
	set slidesLabelFormat(value: string) {
		this._slidesLabelFormat = value;
		this.markDirty("SlidesLabelFormat");
	}
	private _interval: number = 0;
	get interval(): number {
		return this._interval;
	}
	set interval(value: number) {
		this._interval = value;
		this.markDirty("Interval");
	}
	private _maximumIndicatorsCount: number = 0;
	get maximumIndicatorsCount(): number {
		return this._maximumIndicatorsCount;
	}
	set maximumIndicatorsCount(value: number) {
		this._maximumIndicatorsCount = value;
		this.markDirty("MaximumIndicatorsCount");
	}
	private _animationType: string = null;
	get animationType(): string {
		return this._animationType;
	}
	set animationType(value: string) {
		this._animationType = value;
		this.markDirty("AnimationType");
	}
	private _slideChanged: string = null;
	get slideChangedRef(): string {
		return this._slideChanged;
	}
	set slideChangedRef(value: string) {
		this._slideChanged = value;
		this.markDirty("SlideChangedRef");
	}
	private _playing: string = null;
	get playingRef(): string {
		return this._playing;
	}
	set playingRef(value: string) {
		this._playing = value;
		this.markDirty("PlayingRef");
	}
	private _paused: string = null;
	get pausedRef(): string {
		return this._paused;
	}
	set pausedRef(value: string) {
		this._paused = value;
		this.markDirty("PausedRef");
	}
}


