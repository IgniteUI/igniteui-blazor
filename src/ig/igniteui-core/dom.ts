import { Type } from './type';

export interface DomRenderer {
    readonly rootWrapper: DomWrapper;
    querySelector(selector: string): DomWrapper;
    supportsAnimation(): boolean;
    getRequestAnimationFrame(): (act: () => void) => void;
    getSetTimeout(): (act: () => void, millisecondsDelay: number) => number;
    getClearTimeout(): (timerId: number) => void;
    getSubRenderer(root: DomWrapper): DomRenderer;
    createElement(elementName: string): DomWrapper;
    createElementNS(elementName: string, ns: string): DomWrapper;
    get2DCanvasContext(canvas: DomWrapper): CanvasRenderingContext2D;
    hasWindow(): boolean;
    hasBody(): boolean;
    append(child: DomWrapper): DomRenderer;
    globalListen(element: string, eventName: string, hander: (ev: any) => void): () => void;
    appendToBody(element: DomWrapper): void;
    expandTemplate(template: any, args: any): DomWrapper;
    startCSSQuery(): void;
    endCSSQuery(): void;
    getCssDefaultPropertyValue(className: string, propertyName: string): string;
    getCssDefaultValuesForClassCollection(classPrefix: string, propertyNames: string[]): string[][];
    setCssQueryFontString(fontString: string): void;
    getHeightForFontString(fontString: string, text: string, useOffsetHeight: boolean): number;
    supportsDOMEvents(): boolean;
    getResourceString(resourceId: string): string;
    setTimeout(act: () => void, millisecondsDelay: number): number;
    clearTimeout(timerId: number): void;
    destroy(): void;
    runInMainZone(action: () => void): void;
    getWrapper(element: any): DomWrapper;
    getPortal(hostElement: DomWrapper, elementTag: string, portalCallback: (portal: DomPortal) => void, isContentPortal: boolean);
    getExternal(internalComponent: any, styleSourceElement?: DomWrapper, optionalParent?: any);
    setResourceBundleId(bundle: string): void;
    setCultureId(culture: string): void;
}
export let DomRenderer_$type = new Type(null, "DomRenderer");

export interface DomWrapper {
    getNativeElement(): any;
    addClass(className: string): DomWrapper
    removeClass(className: string): DomWrapper
    setProperty(propertyName: string, value: any): DomWrapper;
    getProperty(propertyName: string): any;
    setStyleProperty(propertyName: string, value: string): DomWrapper
    getStyleProperty(propertyName: string): string;
    setRawStyleProperty(propertyName: string, value: string): DomWrapper;
    setRawPosition(x: number, y: number): DomWrapper;
    setRawXPosition(x: number): DomWrapper;
    setRawYPosition(y: number): DomWrapper;
    setRawSize(width: number, height: number): DomWrapper;
    clone(): DomWrapper;
    setAttribute(propertyName: string, value: string): DomWrapper;
    getAttribute(propertyName: string): string;
    setText(value: string): DomWrapper;
    setRawText(value: string): DomWrapper;
    getText(): string;
    remove(): DomWrapper;
    append(child: DomWrapper): DomWrapper;
    before(child: DomWrapper): DomWrapper;
    removeChild(child: DomWrapper): DomWrapper;
    hide(): DomWrapper;
    show(): DomWrapper;
    width(): number;
    height(): number;
    outerWidth(): number;
    outerHeight(): number;
    outerWidthWithMargin(): number;
    outerHeightWithMargin(): number;
    getOffset(): DomWrapperPosition;
    setOffset(x: number, y: number): DomWrapper;
    removeChildren(): void;
    listen(eventName: string, handler: (ev: any) => void): () => void;
    unlistenAll(): void;
    focus(preventScroll?: boolean): void;
    getChildCount(): number;
    getChildAt(i: number): DomWrapper;
    findByClass(className: string): DomWrapper[];
    parent(): DomWrapper;
    querySelectorAll(selector: string): DomWrapper[];
    destroy(): void;
}
export let DomWrapper_$type = new Type(null, "DomWrapper");

export interface DomPortal {
    componentRef: any;
    portalContainer: DomWrapper;
    destroy(): void;
}
export let DomPortal_$type = new Type(null, "DomPortal");

export interface DomWrapperPosition {
    readonly left: number;
    readonly top: number;
}
let DomWrapperPosition_$type = new Type(null, "DomWrapperPosition");

export interface LegacyGestureEvent extends UIEvent {
    scale: number;
}

export interface NormalizedEvent extends MouseEvent {
    originalEvent: MouseEvent & TouchEvent & PointerEvent & LegacyGestureEvent & WheelEvent;
    pageX: number;
    pageY: number;
    button: number;
    wheelDelta?: number;
    target: Node;
}
let NormalizedEvent_$type = new Type(null, "NormalizedEvent");