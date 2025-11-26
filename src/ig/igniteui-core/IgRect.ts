import { typeCast } from "./type";
import { Rect } from "./Rect";

export interface IgRect {
    left: number,
    top: number,
    width: number,
    height: number
}

export function isRect(r: any): boolean {
    if (r == null)
        return false;

    if (typeCast(Rect.$t, r) != null)
        return true;

    if (typeof r.x === "number" && typeof r.y === "number" &&
        typeof r.width === "number" && typeof r.height === "number")
        return true;

    //if (typeof r.left === "number" && typeof r.top === "number" &&
    //    typeof r.right === "number" && typeof r.bottom === "number")
    //    return true;

    return false;
}

export function rectFromLiteral(r: IgRect): Rect {
    if (r == null)
        return new Rect(0, 0, 0, 0, 0);

    var cast = typeCast<Rect>(Rect.$t, r);
    if (cast != null)
        return cast;

    return new Rect(0, r.left, r.top, r.width, r.height);
}

export function rectToLiteral(r: Rect): IgRect {
    if (r == null)
        return null;

    return { left: r.left, top: r.top, width: r.width, height: r.height };
}
