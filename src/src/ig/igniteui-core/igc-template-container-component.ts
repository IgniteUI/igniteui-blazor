import { IgcTemplateContentComponent } from './igc-template-content-component';
import { TypeRegistrar } from './type';
import { html, render, TemplateFunction } from './template';
import { createGuid } from './string';
import { IgcHTMLElement } from './igc-html-element';
import { FontDefaults } from 'igniteui-core/FontDefaults';

export class IgcTemplateContainerComponent extends IgcTemplateContentComponent {
    
    protected static _registered: boolean = false;
    public static htmlTagName: string = "igc-template-container";
    static register(): void {
        if (!IgcTemplateContainerComponent._registered) {
            IgcTemplateContainerComponent._registered = true;
            if (window.customElements) {
                window.customElements.define(IgcTemplateContainerComponent.htmlTagName, IgcTemplateContainerComponent);
            } else {
                (<any>document).registerElement(IgcTemplateContainerComponent.htmlTagName, IgcTemplateContainerComponent);
            }
            TypeRegistrar.registerCons("IgcTemplateContainerComponent", IgcTemplateContainerComponent);
        }
    }
}