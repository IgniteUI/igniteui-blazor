declare class IgcHTMLElement extends HTMLElement {
    constructor();

    createShadow(): ShadowRoot;
}

let shadowWrapTemp = (node: Node) => node;
if ((window as any).ShadyDOM && ((window as any).ShadyDOM).inUse && ((window as any).ShadyDOM).noPatch === true) {
    shadowWrapTemp = ((window as any).ShadyDOM).wrap;
}
export const shadowWrap = shadowWrapTemp;

var cons: typeof IgcHTMLElement = (function () {
function IgcHTMLElement() {
    return Reflect.construct(HTMLElement, [], this.constructor);
}
IgcHTMLElement.prototype = Object.create(HTMLElement.prototype);
IgcHTMLElement.prototype.constructor = IgcHTMLElement;
IgcHTMLElement.prototype.createShadow = function () {
    return (shadowWrap(this) as Element).attachShadow({ mode: "open" });
}
return IgcHTMLElement as any;
})();

export { cons as IgcHTMLElement };