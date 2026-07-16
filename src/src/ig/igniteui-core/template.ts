//This is deliberately fashioned after lit-html's public interface seam
// so that a customer can drop in lit-html for more efficient templates.
// or perhaps we'll just take on lit-html as a dependency.
// The current templating is pretty brain dead and inefficient, but will
// probably serve us fine in the short term.

import { html as lit_html, render as lit_render, nothing as lit_nothing } from 'lit-html';

export interface ITemplateObject {
    strings: TemplateStringsArray;
    values: ReadonlyArray<any>;
}

export let nothing = lit_nothing;

export type TemplateFunction = (context: any) => ITemplateObject;

export class TemplateImplementation {
    public static useLitHtml(litHtml: any) {
        TemplateImplementation.createTemplate = litHtml.html;
        TemplateImplementation.renderTemplate = litHtml.render;
    }

    public static createTemplate: (strings: TemplateStringsArray, ...values) => ITemplateObject;
    public static renderTemplate: (template: ITemplateObject, target: Element | DocumentFragment) => void;
}

TemplateImplementation.createTemplate = lit_html;
TemplateImplementation.renderTemplate = lit_render;

export function html(strings: TemplateStringsArray, ...values: any[]): ITemplateObject {
    if (TemplateImplementation.createTemplate) {
        return TemplateImplementation.createTemplate(strings, ...values);
    }
    return {
        strings: strings,
        values: values
    };
}

function renderValue(val: any): string {
    if (!val) {
        return '';
    }

    if (val.strings && val.values) {
        return renderToString(val as ITemplateObject);
    }

    return val.toString();
}

function renderToString(template: ITemplateObject): string {
    let ret = "";
    for (let i = 0; i < template.strings.length; i++) {
        ret += template.strings[i];
        ret += template.values[i] ? template.values[i] : '';
    }
    return ret;
}

export function render(template: ITemplateObject, target: Element | DocumentFragment) {
    if (TemplateImplementation.renderTemplate) {
        TemplateImplementation.renderTemplate(template, target);
        return;
    }
    let ret = renderToString(template);

    if (target instanceof HTMLElement) {
        target.innerHTML = ret;
    } else {
        let div = document.createElement("div");
        div.innerHTML = ret;
        target.appendChild(div);
    }
}
