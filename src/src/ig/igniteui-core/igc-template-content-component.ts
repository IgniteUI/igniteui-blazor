import { TypeRegistrar } from './type';
import { render, html, TemplateFunction } from './template';
import { createGuid } from './string';
import { IgcHTMLElement } from './igc-html-element';

export class IgcTemplateContentComponent extends IgcHTMLElement {
    constructor() {
        super();

        this.template = (context) => {
            return html`
               <div></div>  
            `;
        };
    }

    private _template: TemplateFunction = null;
    set template(value: TemplateFunction) {
        var oldValue = this._template;
        this._template = value;
        this.onTemplateChanged(oldValue, this._template);
    }
    get template(): TemplateFunction {
        return this._template;
    }

    connectedCallback() {
        this.attachedCallback();
    }

    private _attached: boolean = false;
    attachedCallback() {
        this._attached = true;
        this.render();
    }

    diconnectedCallBack() {
        if (this.template && (this.template as any).___isBridged) {
            this.teardownBridgedTemplate(this.template);
        }
    }

    private _context: any = null;
    private _isContextPopulated: boolean = false;
    set context(value: any) {
        this._isContextPopulated = true;
        if (this._context == null && value == null) {
            return;
        }
        this._context = value;
        this.onContextChanged();
    }
    get context(): any {
        return this._context;
    }

    markChanged() {
        this.onContextChanged();
    }

    private onContextChanged() {
        this.render();
    }

    private _id: string = null;
    private _isBridged: boolean = false;
    private teardownBridgedTemplate(value: TemplateFunction) {
        (value as any).___onTemplateTeardown(this._template, this, this._template);
    }
    private setupBridgedTemplate(value: TemplateFunction) {
        this._id = createGuid();
        (value as any).___onTemplateInit(this._template, this, this._template);
    }

    private onTemplateChanged(oldValue: TemplateFunction, newValue: TemplateFunction) {
        if (newValue && (newValue as any).___isBridged && !this._isBridged) {
            this.setupBridgedTemplate(newValue);
            this._isBridged = true;
        }
        if (this._attached) {
            this.render();
        }
    }

    private render() {
        if (this._isBridged) {
            if (this.context) {
                this.context.___contentId = this._id;
            }
        }
        if (!this.context || !this.template) {
            if (this.template) {
                render(this.template(this.context), this);
                if (this._isBridged && this._isContextPopulated) {
                    (this._template as any).___onTemplateContextChanged(this._template, this, this.context);
                }
            } else {
                this.innerHTML = '';
            }
        } else {
            render(this.template(this.context), this);
            if (this._isBridged && this._isContextPopulated) {
                (this._template as any).___onTemplateContextChanged(this._template, this, this.context);
            }
        }
    }

    private _owner: any;
    public get owner(): any {
        return this._owner;
    }
    public set owner(v: any) {
        this._owner = v;
    }

    public static htmlTagName: string = "igc-template-content";
    protected static _registered: boolean = false;
    static register(): void {
        if (!IgcTemplateContentComponent._registered) {
            IgcTemplateContentComponent._registered = true;
            if (window.customElements) {
                window.customElements.define(IgcTemplateContentComponent.htmlTagName, IgcTemplateContentComponent);
            } else {
                (<any>document).registerElement(IgcTemplateContentComponent.htmlTagName, IgcTemplateContentComponent);
            }
            TypeRegistrar.registerCons("IgcTemplateContentComponent", IgcTemplateContentComponent);
        }
    }
}