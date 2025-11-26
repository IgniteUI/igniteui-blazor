import { Description } from "./Description";
import { WebStepDescription } from "./WebStepDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebStepperDescription extends Description {
	static $t: Type = markType(WebStepperDescription, 'WebStepperDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebStepper";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _steps: WebStepDescription[] = null;
	get steps(): WebStepDescription[] {
		return this._steps;
	}
	set steps(value: WebStepDescription[]) {
		this._steps = value;
		this.markDirty("Steps");
	}
	private _orientation: string = null;
	get orientation(): string {
		return this._orientation;
	}
	set orientation(value: string) {
		this._orientation = value;
		this.markDirty("Orientation");
	}
	private _stepType: string = null;
	get stepType(): string {
		return this._stepType;
	}
	set stepType(value: string) {
		this._stepType = value;
		this.markDirty("StepType");
	}
	private _linear: boolean = false;
	get linear(): boolean {
		return this._linear;
	}
	set linear(value: boolean) {
		this._linear = value;
		this.markDirty("Linear");
	}
	private _contentTop: boolean = false;
	get contentTop(): boolean {
		return this._contentTop;
	}
	set contentTop(value: boolean) {
		this._contentTop = value;
		this.markDirty("ContentTop");
	}
	private _verticalAnimation: string = null;
	get verticalAnimation(): string {
		return this._verticalAnimation;
	}
	set verticalAnimation(value: string) {
		this._verticalAnimation = value;
		this.markDirty("VerticalAnimation");
	}
	private _horizontalAnimation: string = null;
	get horizontalAnimation(): string {
		return this._horizontalAnimation;
	}
	set horizontalAnimation(value: string) {
		this._horizontalAnimation = value;
		this.markDirty("HorizontalAnimation");
	}
	private _animationDuration: number = 0;
	get animationDuration(): number {
		return this._animationDuration;
	}
	set animationDuration(value: number) {
		this._animationDuration = value;
		this.markDirty("AnimationDuration");
	}
	private _titlePosition: string = null;
	get titlePosition(): string {
		return this._titlePosition;
	}
	set titlePosition(value: string) {
		this._titlePosition = value;
		this.markDirty("TitlePosition");
	}
	private _activeStepChanging: string = null;
	get activeStepChangingRef(): string {
		return this._activeStepChanging;
	}
	set activeStepChangingRef(value: string) {
		this._activeStepChanging = value;
		this.markDirty("ActiveStepChangingRef");
	}
	private _activeStepChanged: string = null;
	get activeStepChangedRef(): string {
		return this._activeStepChanged;
	}
	set activeStepChangedRef(value: string) {
		this._activeStepChanged = value;
		this.markDirty("ActiveStepChangedRef");
	}
}


