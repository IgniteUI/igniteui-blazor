import { Type, Base } from "./type";

export class CompareUtil {
    static compareToObject(obj1: any, obj2: any): number {
        if (obj1.compareToObject) {
            return <number>obj1.compareToObject(obj2);
        }
        return Base.compare(obj1, obj2);
    }
    static compareTo(obj1: any, obj2: any) {
        if (obj1.compareTo) {
            return <number>obj1.compareTo(obj2);
        }
        return Base.compare(obj1, obj2);
    }
}