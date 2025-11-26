let _typeIdentifierCache: { [val: string]: number } = {};
let _nextTypeIdentifier = 0;

// interface Function {
//     $type?: Type;
// }

export function getInstanceType(obj: any): Type {
    if (obj.$type) {
        return obj.$type;
    }
    else if (typeof obj === 'number') {
        return Number_$type;
    }
    else if (typeof obj === 'string') {
        return String_$type;
    }
    else if (typeof obj === 'boolean') {
        return Boolean_$type;
    }
    else if (obj instanceof Date) {
        return Date_$type;
    }

    return (<any>Base).prototype.$type;
}

export class Base {
    constructor () {

    }
    equals(other: any) : boolean {
        return this === other;
    }
    static equalsStatic(a: any, b: any): boolean {
        var aIsNull = (a == null) || (!!(<any>a).isNullable && !(<any>a).hasValue);
        var bIsNull = (b == null) || (!!(<any>b).isNullable && !(<any>b).hasValue);

        if (aIsNull || bIsNull) {
            return aIsNull && bIsNull;
        }

        if (a.equals) {
            return a.equals(b);
        }

        if (b.equals) {
            return b.equals(a);
        }

        if (Number.isNaN(<any>a) && Number.isNaN(<any>b)) {
            return true;
        }

        if (a instanceof Date) {
            return b instanceof Date && +a === +b;
        }

        return a == b && typeof a == typeof b;
    }

    static equalsSimple(item1: any, item2: any): boolean {
		return item1 == item2;
	};

	static compareSimple(item1: any, item2: any): number {
		if (item1 == item2) {
			return 0;
		}

		if (item1 < item2) {
			return -1;
		}
		return 1;
	};

	static compare(item1: any, item2: any): number {
		if (item1 === item2) {
			return 0;
		}

		var xComparable = typeCast<IComparable>(IComparable_$type, item1);
		if (xComparable !== null && xComparable.compareToObject) {
			return xComparable.compareToObject(item2);
		}

		var yComparable = typeCast<IComparable>(IComparable_$type, item2);
		if (yComparable !== null && yComparable.compareToObject) {
			return -yComparable.compareToObject(item1);
		}

		return Base.compareSimple(item1, item2);
	};

    private $hashCode: number;
    static nextHashCode: number = 0;
    getHashCode(): number {
        if (this.$hashCode === undefined) {
            this.$hashCode = Base.nextHashCode++;
        }
        return this.$hashCode;
    }
    static getHashCodeStatic(obj: any): number {
        if (obj.getHashCode) {
            return obj.getHashCode();
        }
        if (obj.$hashCode !== undefined) {
            return obj.$hashCode;
        }
        if (!(typeof obj == "object")) {
            return Type.getPrimitiveHashCode(obj);
        } else {
            obj.$hashCode = Base.nextHashCode++;
            return obj.$hashCode;
        }
    }
    memberwiseClone(): Base {
        var clone: any;
        try {
            clone = Object.create(this.$type.InstanceConstructor.prototype);
        }
        catch (e) {
            class Cons {

            }
            Cons.prototype = this.$type.InstanceConstructor.prototype;
            clone = new Cons();
        }

        for (var prop in this) {
            if (this.hasOwnProperty(prop)) {
                clone[ prop ] = this[ prop ];
            }
        }

        return clone;
    }
    static referenceEquals(a: any, b: any): boolean {
        return a === b || (a == null && b == null);
    }
    static getArrayOfValues(obj: any): any[] {
		var result = [ ];
		for (var i in obj) {
			if (obj.hasOwnProperty(i)) {
				result.push(obj[ i ]);
			}
		}

		return result;
	}

	static getArrayOfProperties(obj: any): any[] {
		var result = [ ];
		for (var i in obj) {
			if (obj.hasOwnProperty(i)) {
				result.push(i);
			}
		}

		return result;
	};
    $type: Type
}

export type InstanceConstructor = 
    Function;

export class Type extends Base {
    public specializationCache: { [val: string]: Type } = null;
    private _staticInitializer: () => void = null;
    private _fullName: string;
    public name: string = null;
    public typeArguments: (number | Type)[] = null;
    public baseType: Type = null;
    public interfaces: Type[] = null;
    public identifier: number;
    public isEnumType: boolean = false;
    private _isGenericType: boolean = undefined;
    private _staticFields: object = null;
    InstanceConstructor: InstanceConstructor;
    public isNullable: boolean = false;
    public stringId: string;

    _$nullNullable: any = null;

    enumInfo: EnumInfo = null;

    constructor (
        instanceConstructor: InstanceConstructor,
        identifier: string, 
        baseType: Type = Base.prototype.$type, 
        interfaces: Type[] = null, 
        staticInitializer: () => void = null) {

        super();
        this.specializationCache = {};
        this._staticInitializer = staticInitializer;
        this._fullName = identifier;
        this.name = identifier;

        this.InstanceConstructor = instanceConstructor;
        var lastDotIndex = this.name.lastIndexOf(".");
        if (lastDotIndex >= 0) {
            this.name = this.name.substr(lastDotIndex + 1);
        }
    
        this.typeArguments = null;
        this.baseType = null;
        this.interfaces = null;
        if (baseType) {
            this.baseType = baseType;
        }
        if (interfaces) {
            this.interfaces = interfaces;
        }

        if (_typeIdentifierCache[ identifier ]) {
            this.identifier = _typeIdentifierCache[ identifier ];
        } else {
            this.identifier = _nextTypeIdentifier++;
            _typeIdentifierCache[ identifier ] = this.identifier;
        }

        // rather than always evaluating a function on a type to see if it is an enum
        // we can just cache it once on the type
        // if (baseType && Enum && baseType == Enum.prototype.$type) {
        //     this.isEnumType = true;
        // }
    }
    get typeName(): string {
        return this.name;
    }
    get fullName(): string {
        return this._fullName;
    }
    getSpecId(types: (number | Type)[]): string {
        if (types.length === 1) {
            if (!types[ 0 ]) {
                return "undef";
            } else if (types[ 0 ] === -1) {
                return undefined;
            } else if (!(<any>types[ 0 ]).typeName) {
                return types[ 0 ].toString();
            } else if ((<any>types[ 0 ]).stringId) {
                return (<any>types[ 0 ]).stringId;
            } else {
                return (<Type>types[ 0 ]).identifier.toString();
            }
        }

        var ret = "";
        for (var i = 0; i < types.length; i++) {
            var type = types[ i ];
            if (!type) {
                ret += "undef";
            } else if (type == -1) {
                return undefined;
            } else if (!(<any>type).typeName) {
                ret += type.toString();
            } else if ((<any>type).stringId) {
                ret += (<any>type).stringId;
            } else {
                ret += (<Type>type).identifier.toString();
            }
        }
        return ret;
    }
    get isGenericType(): boolean {
        if (this._isGenericType === undefined) {
            this._isGenericType = this.name.indexOf("$") >= 0;
        }

        return this._isGenericType;
    }
    get isGenericTypeDefinition() : boolean {
        return this.typeArguments === null && this.isGenericType;
    }
    get genericTypeArguments(): (number | Type)[] {
        return this.typeArguments;
    }
    getStaticFields(type?: Type): any {
        if (type === undefined) {
            type = this;
        }

        var t: Type | string = this;

        while (t != null) {
            if (t === type || t._fullName == type._fullName) {
                if (t._staticFields == null && t._staticInitializer) {
                    t._staticFields = {};
                    t._staticInitializer.apply(t._staticFields, t.typeArguments);
                }

                return t._staticFields;
            }

            t = t.baseType;
        }

        return null;
    }
    initSelfReferences(replacement?: Type): Type {
        var i, j;
        if (replacement) {
            if (this.typeArguments) {
                var updateCache = false;

                for (j = 0; j < this.typeArguments.length; j++) {
                    var typeArg = this.typeArguments[ j ];
                    if (typeArg == -1) {
                        updateCache = true;
                        this.typeArguments[ j ] = replacement;
                    } else if (
                        typeArg &&
                        typeArg instanceof Type &&
                        typeArg.initSelfReferences) {
                        typeArg.initSelfReferences(replacement);
                    }
                }

                if (updateCache) {
                    var specId = this.getSpecId(this.typeArguments);
                    var ret = this.specializationCache[ specId ];

                    if (!ret) {
                        this.specializationCache[ specId ] = this;
                    }
                }
            }
        } else {
            if (this.baseType) {
                this.baseType.initSelfReferences(this);
            }

            if (this.interfaces) {
                for (i = 0; i < this.interfaces.length; i++) {
                    this.interfaces[ i ].initSelfReferences(this);
                }
            }
        }

        return this;
    }
    specialize(...rest: (Type | number | Function)[]): Type {
        var i;
        if (!this.isGenericType) {
            return this;
        }

        var specId = this.getSpecId(Array.from(arguments));
        var ret = this.specializationCache[ specId ];
        if (ret) {
            return ret;
        }
        ret = new Type(this.InstanceConstructor, this._fullName, this.baseType, this.interfaces, this._staticInitializer);
        ret.specializationCache = this.specializationCache;

        var placeholders = this.typeArguments;
        var hasPlaceholders = false;

        // Make sure the placeholders are actually numbers. If they are types, we are re-specializing an
        // already specialized type.
        if (placeholders && placeholders.length) {
            /* going back to how it used to be. we shouldn't assume that the number/order of the arguments
                relates to the typearguments. this may be an interface that has its type information already
                and either has placeholders or is a closed type
            // you can have a mixed bag where some are placeholders and others are not and the
            // placeholder doesn't have to be the first slot
            for (i = 0; i < placeholders.length; i++) {
                if (isFinite(placeholders[ i ])) {
                    hasPlaceholders = true;
                    break;
                }
            }*/
            hasPlaceholders = true;
        }

        ret.typeArguments = [ ];
        if (hasPlaceholders) {
            for (i = 0; i < placeholders.length; i++) {

                // if the argument being provided is a placeholder index and we already have
                // a placeholder then keep the index we have. otherwise we're taking the index
                // of the parent type
                if (typeof placeholders[i] === "number" &&
                    isFinite(<number>placeholders[ i ]) && !isFinite(arguments[<number>placeholders[ i ] ])) {
                    ret.typeArguments[ i ] = arguments[ <number>placeholders[ i ] ];
                } else {
                    ret.typeArguments[ i ] = placeholders[ i ];
                }
            }
        } else {
            for (i = 0; i < arguments.length; i++) {
                ret.typeArguments[ i ] = arguments[ i ];
            }
        }

        // since the placeholder indexes for the basetype and interfaces implemented are based
        // on the order of the type arguments for the defining types we should pass its typeargs
        // and not the outermost type's type arguments which may be different in number and order
        // than the base type of the base types and interfaces implemented
        if (this.baseType && this.baseType.typeArguments) {
            ret.baseType = this.specialize.apply(this.baseType, ret.typeArguments);
        }

        if (this.interfaces) {
            ret.interfaces = [ ];
            for (i = 0; i < this.interfaces.length; i++) {
                ret.interfaces[ i ] = this.specialize.apply(this.interfaces[ i ], ret.typeArguments);
            }
        }

        // rather than doing this check in various places we could just cache a field on the type
        if (this._fullName == "Nullable$1" && ret.typeArguments.length == 1) {
            ret.isNullable = true;
        }

        // if this was a self referencing type (e.g. IEquatable<Int32> for Int32 then we won't have the
        // specId yet because we don't know the type argument. we'll update the cache when we update
        // the self references. otherwise other types that use self references (but for a different type)
        // will get and use the wrong type arguments
        if (specId) {
            this.specializationCache[ specId ] = ret;
            ret.stringId = ret.generateString();
        } else {
            // the self referencing type needs to be able to put itself into the specialization cache
            // of the original type
            //ret.specializationCache = this.specializationCache;
        }

        if (this.InstanceConstructor != null) {
            var _self = this;
            ret.InstanceConstructor = function () {
                _self.InstanceConstructor.apply(this,
                    ret.typeArguments.concat(Array.prototype.slice.call(arguments, 0)));
                return this;
            };
            ret.InstanceConstructor.prototype = this.InstanceConstructor.prototype;
        }

        return ret;
    }
    equals(other: Type) : boolean {
        if (!(other instanceof Type)) {
            return false;
        }
        if (this.identifier !== other.identifier) {
            return false;
        }
        if (this.typeArguments === null && other.typeArguments === null) {
            return true;
        }
        if (this.typeArguments === null && other.typeArguments !== null) {
            return false;
        }
        if (this.typeArguments !== null && other.typeArguments === null) {
            return false;
        }
        if (this.typeArguments.length !== other.typeArguments.length) {
            return false;
        }
        for (var i = 0; i < this.typeArguments.length; i++) {

            //TODO: handle covariance case here.
            //if (!$.ig.util.canAssign(this.typeArguments[ i ], other.typeArguments[ i ])) {
            //    return false;
            //}
            if (!Type.checkEquals(this.typeArguments[ i ], other.typeArguments[ i ])) {
                return false;
            }
        }

        return true;
    }
    static checkEquals(type1: any, type2: any): boolean {
        if (type1 instanceof Type) {
            return type1.equals(type2);
        } else if (type2 instanceof Type) {
            return type2.equals(type1);
        } else {
            return type1 === type2;
        }
    }

    static op_Equality(type1: Type, type2: Type): boolean { 
        return type1.equals(type2);
    }
    static op_Inequality(type1: Type, type2: Type) { 
        return !type1.equals(type2);
    }

    generateString() : string {
        if (!this.typeArguments || !this.typeArguments.length) {
            return this.identifier.toString();
        } else {
            var ret = this.identifier.toString() + "[";
            var first = true;
            for (var i = 0; i < this.typeArguments.length; i++) {
                if (this.typeArguments[ i ] == undefined) {
                    continue;
                }
                if (first) { first = false; } else { ret += ","; }
                if (this.typeArguments[ i ].toString) {
                    ret += this.typeArguments[ i ].toString();
                } else {
                    ret += (<Type>this.typeArguments[ i ]).identifier.toString();
                }
            }
            ret += "]";
            return ret;
        }
    }
    get isValueType() : boolean {
        return this.baseType === ValueType.prototype.$type;
    }
    isAssignableFrom(tOther: Type): boolean {

        // TODO: Unit test and make sure this is right (especially with generics
        if (this === tOther) {
            return true;
        }

        if (tOther.baseType && this.isAssignableFrom(tOther.baseType)) {
            return true;
        }

        if (tOther.interfaces) {
            for (var i = 0; i < tOther.interfaces.length; i++) {
                if (this.isAssignableFrom(tOther.interfaces[ i ])) {
                    return true;
                }
            }
        }

        return false;
    }
    isInstanceOfType(value: Type | string): boolean {
        return typeCast(this, value) !== null;
    }
    get isPrimitive() {
        return this === Number_$type ||
            this === Boolean_$type;
    }
            
    static canAssign(targetType: Type, type: Type) : boolean {
		if (targetType.name === 'Nullable$1' && type.name !== 'Nullable$1') {
			targetType = Nullable.getUnderlyingType(targetType);
		}

		return Type.canAssignSimple(targetType, type);
	}

	static canAssignSimple(targetType: Type, type: Type): boolean {
		if (targetType === type || Type.checkEquals(targetType, type)) {
			return true;
		}
		if (type.interfaces) {
			for (var i = 0; i < type.interfaces.length; i++) {
				if (Type.canAssignSimple(targetType, type.interfaces[i])) {
					return true;
				}
			}
		}
		if (type.baseType) {
			return Type.canAssignSimple(targetType, type.baseType);
		}

		return false;
	}

	

    static createInstance<T>($t: Type | Function): T {
        if ($t === Number || $t == Number_$type ||
            (<Type>$t).isEnumType) {
            return <T><any>0;
        }

        if ($t == Boolean || $t == Boolean_$type) {
            return <T><any>false;
        }

        if ((<Type>$t).InstanceConstructor) {
            var result;
            //result = Object.create((<Type>$t).InstanceConstructor.prototype);

            let C: any = (<Type>$t).InstanceConstructor;
            //(<Type>$t).InstanceConstructor.apply(result, Array.prototype.slice.call(arguments, 1));
            result = new C(...Array.prototype.slice.call(arguments, 1));
            return <T>result;
        }

        throw new Error("Cannot find instance constructor for the type parameter");
    };

    static getDefaultValue<T>($t: Type): T {
		if ($t === Number_$type ||
			$t.isEnumType) {
			return <T><any>0;
		}

		if ($t == Boolean_$type) {
			return <T><any>false;
		}

		if ($t.baseType === (<any>ValueType).$type) {
			return Type.createInstance<T>($t);
		}

		return null;
	};
    
    static getPrimitiveHashCode(v: any): number {
        var val = typeof v;
        if (val === "string" || v instanceof String) {
            var hash = 0, i, chr, len;
			if (v.length === 0) {
				return hash;
			}
			for (i = 0, len = v.length; i < len; i++) {
				chr = v.charCodeAt(i);
				/*jslint bitwise: true */
				hash = ((hash << 5) - hash) + chr;
				hash |= 0; // Convert to 32bit integer
			}

			return hash;
        } else if (val === "boolean" || v instanceof Boolean) {
            return +v;
        } else {
            return v;
        }
    }

    

    // static mark(t: Function, name: string,
    //     baseType: Type = Base.prototype.$type, 
    //     interfaces: Type[] = null, 
    //     staticInitializer: () => void = null) {
    //     t.prototype.$type = new Type(t, name, baseType, interfaces, staticInitializer);
    //     (<any>t).$type = t.prototype.$type;
    // }

    public static decodePropType(val: any): Type {
        if (val === 0) {
            return Boolean_$type;
        }
        else if (val === 1) {
            return Number_$type;
        }
        else if (val === 2) {
            return String_$type;
        }
        else if (val === 3) {
            return Date_$type;
        }
        else {
            return <Type>val;
        }
    }
}

export function markDep(depProp: Function, PropMeta: Function, t: Function, changedFunction: string,
    props: any[]) {
    let names: string[] = [];
    let currName = "";
    let currOpts: any[] = [];
    let hasDefaultValue = false;
    let defaultValue: any = null;
    let propType: Type = null;
    let changeHandler : (sender: any, args: any) => void = null;
    let setterFunc: (value: any) => void = null;
    let getterFunc: () => any = null;
    let aliasName: string = null;
    let propertyAlias: string = null;

    //debugger;
    for (var i = 0; i < props.length; i++) {
        if (i % 2 == 0) {
            currName = props[i];
            if (currName.indexOf(":") >= 0) {
                let parts = currName.split(':');
                if (parts.length == 2) {
                    currName = parts[0];
                    aliasName = parts[1];
                    propertyAlias = currName.substring(0, 1).toLowerCase() + currName.substring(1) + "Property";
                } else {
                    currName = parts[0];
                    aliasName = parts[1];
                    if (aliasName.length == 0) {
                        aliasName = currName.substring(0, 1).toLowerCase() + currName.substring(1);
                    }
                    propertyAlias = parts[2];
                }
            } else {
                aliasName = currName.substring(0, 1).toLowerCase() + currName.substring(1);
                propertyAlias = aliasName + "Property";
            }
            names.push(currName);
        } else {
            currOpts = props[i];
            if (currOpts.length == 2) {
                hasDefaultValue = true;
                defaultValue = currOpts[1];
                propType = Type.decodePropType(currOpts[0]);
            } else {
                hasDefaultValue = false;
                propType = Type.decodePropType(currOpts[0]);
            }

            let changedName = currName;
            changeHandler = (o: any, a: any) => {
                o[changedFunction].call(o, changedName, a.oldValue, a.newValue);
            }

            let meta: any = null;
                
            if (hasDefaultValue) {
                meta = (<any>PropMeta).createWithDefaultAndCallback(defaultValue, changeHandler);
            } else { 
                meta = (<any>PropMeta).createWithCallback(changeHandler);
            }

            let dp = (<any>depProp).registerAlt(currName, propType, (<any>t).$type, meta);
                
            setterFunc = function (this: any, v: any): void {
                (<any>this).setValueAlt(dp, v);
            }

            if (propType.isEnumType) {
                getterFunc = function (this: any): any {
                    return typeGetValue((<any>this).getValueAlt(dp));
                }
            }
            else {
                getterFunc = function (this: any): any {
                    return (<any>this).getValueAlt(dp);
                }
            }

            Object.defineProperty(t.prototype, aliasName, {
                set: setterFunc,
                get: getterFunc,
                configurable: true
            });

            (<any>t)[propertyAlias] = dp; 
        }

    }

    // let superClass = (<any>t).$type.baseType;
    // if (superClass !== null && superClass !== undefined) {
    //     let superNames = superClass.InstanceConstructor.$$p;
    //     if (superNames) {
    //         for (var j = superNames.length - 1; j >= 0; j--) {
    //             names.unshift(superNames[j]);
    //         }
    //     }
    // }

    //(<any>t).$$p = names;
    return names;
}

export function typeGetValue(v: any): any {
    if (v !== null && v.$type && v.$type.isEnumType) {
        return v.value;
    }

    return v;
}

export function typeCast<T>(targetType: Type | Function, obj: any): T {
    if (obj === undefined || obj === null) {
        return null;
    }

    if (targetType === Array) {
        return <T><any>((obj instanceof Array) ? obj : null);
    }

    if (targetType === String) {
        targetType = String_$type;
    }
    if (targetType === Number) {
        targetType = Number_$type;
    }
    if (targetType === Boolean) {
        targetType = Boolean_$type;
    }
    if (targetType === Date) {
        targetType = Date_$type;
    }

    var type = obj;

    if (obj.$type) {
        type = obj.$type;
    }
    else if (typeof obj === 'number') {
        type = targetType === Number ? Number : Number_$type;
    }
    else if (typeof obj === 'string') {
        type = String_$type;
    }
    else if (typeof obj === 'boolean') {
        type = Boolean_$type;
    }
    else if (obj instanceof Date) {
        type = Date_$type;
    }
    if (obj instanceof Array) {
        type = Array_$type;
    }

    if (Type.canAssignSimple(<Type>targetType, type)) {
        return obj;
    }

    if (targetType.name === 'Nullable$1' && type.name !== 'Nullable$1') {
        targetType = Nullable.getUnderlyingType(<Type>targetType);
        if (Type.canAssignSimple(<Type>targetType, type)) {
            return <T><any>toNullable(targetType, obj);
        }

        return <T><any>toNullable(targetType, null);
    }
    return null;
}

export function typeCastObjTo$t<T>($t: Type, v: any) {

    var shouldWrap = false;
    if ($t.isNullable) {
        $t = <Type>$t.typeArguments[ 0 ];
        shouldWrap = true;
    }

    if (v !== null && $t.isEnumType) {
        v = v.value;
    }

    return shouldWrap ? toNullable($t, v) : v;
}


let pendingStaticCtors = new Array();
export function markStruct(t: Function, name: string,
        baseType: Type = ValueType.prototype.$type, 
        interfaces: Type[] = null, 
        staticInitializer: () => void = null) {
    t.prototype.$type = new Type(t, name, baseType, interfaces, staticInitializer);

    //TODO: do we need/want a flag on the function like we have in js?
    if (typeof (<any>t).staticInit === "function") {
        pendingStaticCtors.push(t);
    }

    (<any>t).$type = t.prototype.$type;
    (<any>t).$ = t.prototype.$type;

    return t.prototype.$type;
}

export interface EnumInfo {
    names: string[];
    actualNames: string[];
    namesValuesMap: { [index: string]: number };
    actualNamesValuesMap: { [index: string]: number };
    mustCoerceToInt: Boolean
}

/* #__PURE__ */
export function markEnum(name: string, encodedDef: string, mustCoerceToInt: Boolean = false): Type {
    let t = new Type(null, name, Base.prototype.$type, [ IConvertible_$type ]);
    t.isEnumType = true;

    let parts = encodedDef.split("|");
    let names: string[] = [];
    let actualNames: string[] = [];
    let namesValuesMap: { [index: string]: number } = {};
    let actualNamesValuesMap: { [index: string]: number } = {};

    for (var i = 0; i < parts.length; i++) {
        let subParts = parts[i].split(",");
        let nameParts = subParts[0].split(":");

        let name = nameParts[0];
        let actualName = nameParts[0];
        if (nameParts.length > 1) {
            actualName = nameParts[1];
        }
        names.push(name);
        actualNames.push(actualName);
        //TODO: did we support string enums??
        namesValuesMap[name] = parseInt(subParts[1]);
        actualNamesValuesMap[actualName] = parseInt(subParts[1]);
    }

    let info: EnumInfo = {
        names: names,
        actualNames: actualNames,
        namesValuesMap: namesValuesMap,
        actualNamesValuesMap: actualNamesValuesMap,
        mustCoerceToInt: mustCoerceToInt
    };
    t.enumInfo = info;
    
    return t;
}

let markTypeInitialized = false;
export function markType(t: Function, name: string,
        baseType: Type = Base.prototype.$type, 
        interfaces: Type[] = null, 
        staticInitializer: () => void = null): Type {
    t.prototype.$type = new Type(t, name, baseType, interfaces, staticInitializer);
    if (!markTypeInitialized) {
        markTypeInitialized = true;
        markType(Type, "Type");
        markType(Base, "Base", null);
    }

    //TODO: do we need/want a flag on the function like we have in js?
    if (typeof (<any>t).staticInit === "function") {
        pendingStaticCtors.push(t);
    }

    //t.prototype.$ = t.prototype.$type;
    (<any>t).$type = t.prototype.$type;
    (<any>t).$ = t.prototype.$type;
    return t.prototype.$type;
}

export function callStaticConstructors() {
    if (pendingStaticCtors.length > 0) {
        //TODO: is the copy of the array needed? 
        let classes = Array.from(pendingStaticCtors);
        pendingStaticCtors.length = 0;
        for (let c of classes) {
            c.staticInit();
        }
    }
}
//markType(Base, "BaseObject");
//markType(Type, "Type");

export interface IConvertible {
    toBoolean(provider: IFormatProvider): boolean;
    toByte(provider: IFormatProvider): number;
    toChar(provider: IFormatProvider): string;
    toDateTime(provider: IFormatProvider): Date;
    toDecimal(provider: IFormatProvider): number;
    toDouble(provider: IFormatProvider): number;
    toInt16(provider: IFormatProvider): number;
    toInt32(provider: IFormatProvider): number;
    toInt64(provider: IFormatProvider): number;
    toSByte(provider: IFormatProvider): number;
    toSingle(provider: IFormatProvider): number;
    toString1(provider: IFormatProvider): string;
    toUInt16(provider: IFormatProvider): number;
    toUInt32(provider: IFormatProvider): number;
    toUInt64(provider: IFormatProvider): number;
}
export let IConvertible_$type = new Type(null, "IConvertible");


export class Enum extends Base {
    static $t: Type = markType(Enum, "Enum");
}
export class EnumBox extends Enum {
    constructor(public readonly value: number, type: Type) {
        super();
        this.$type = type;
    }
    getHashCode(): number {
        return this.value;
    }
    // TODO: Fill out remaining IConvertible implementation
    toDouble(provider: IFormatProvider): number {
        return this.value;
    }
    toString(): string {
        return EnumUtil.getName(this.$type, this.value);
    }

    getActualName(): string {
        const vals = EnumUtil.getValues<any>(this.$type);
        const actualNames = this.$type.isEnumType ? this.$type.enumInfo.actualNames : EnumUtil.getNames<any>(this.$type);
        return actualNames[vals.indexOf(this.value)];
    }
}

export function getBoxIfEnum<T>($t: Type, v: any) {
    if (v !== null && $t) { // TODO: Remove the $t check here and fix the null ref issue
        if ($t.isNullable) {
            $t = <Type>$t.typeArguments[ 0 ];
        }

        if ($t.isEnumType) {
            return enumGetBox<T>($t, v);
        }
    }

    return v;
};

export function enumGetBox<T>($t: Type, v: number): T {
    if (!(<any>$t)._boxes) {
        (<any>$t)._boxes = {};
    }

    if (!(<any>$t)._boxes[ v ]) {
        (<any>$t)._boxes[v] = new EnumBox(v, $t);
    }

    return (<any>$t)._boxes[ v ];
}

export class EnumUtil {
    private static getValueFromName(values: any, enumType: Type, value: string, ignoreCase: boolean) {
        if (values.hasOwnProperty(value)) {
            return enumGetBox(enumType, values[value]);
        } else if (ignoreCase) {
            var upper = value.toUpperCase();

            for (var x in values) {
                if (x.toUpperCase() === upper) {
                    return enumGetBox(enumType, values[x]);
                }
            }
        } else {
            // A.S. Nov 4, 2016 Adjusted to handle case where leading char is _.
            var firstChar = value.charAt(0);
            if (firstChar != "_") {
                value = firstChar.toLowerCase() + value.substr(1);
            } else {
                value = "_" + value.charAt(1).toLowerCase() + value.substr(2);
            }
            if (values.hasOwnProperty(value)) {
                return enumGetBox(enumType, values[value]);
            }
        }

        return null;
    }

    static parse(enumType: Type, value: string, ignoreCase: boolean) {
        //var info = Type.getDefinedNameAndNamespace(enumType.fullName);

        //if (Type.canAssign(this.$type, enumType)) 
        {
            //var p = info.namespace[ info.name ].prototype;
            var values = enumType.isEnumType ? enumType.enumInfo.actualNamesValuesMap : enumType.InstanceConstructor.prototype;

            let val = EnumUtil.getValueFromName(values, enumType, value, ignoreCase);
            if (val !== null) {
                return val;
            }

            if (enumType.isEnumType) {
                values = enumType.enumInfo.namesValuesMap;

                let val = EnumUtil.getValueFromName(values, enumType, value, ignoreCase);
                if (val !== null) {
                    return val;
                }
            }
        }

        throw new Error("Invalid " + enumType.name + " value: " + value);
    }
    
    // static $getName(value: number): string {

    // }
    // static $value(): number {

    // }
    //private _v: number;

    static enumHasFlag(value: number, flag: number): boolean {
		/*jslint bitwise: true */
		return (value & flag) === flag;
	}

    
    static toString(enumType: Type, value: any): string {
        return EnumUtil.getName<any>(enumType, value);
    }



    static getName<T>(enumType: Type, v: number): string {
        if ((<any>enumType)._nameMap == undefined) {
            let vals = EnumUtil.getValues<T>(enumType);
            let names = enumType.isEnumType ? enumType.enumInfo.names : EnumUtil.getNames<T>(enumType);

            let map: any = {};
            for (let i = 0; i < vals.length; i++) {
                map[vals[i]] = names[i];
            }
            (<any>enumType)._nameMap = map;
        }
        let lookup = (<any>enumType)._nameMap;
        return lookup[v];
    }

    static getFlaggedName(enumType: Type, v: number, getName: (v: number) => string): string {
        var names = [ ];
        var original = v;
        var zeroValueName;
        var value;

        var values: string[] = [ ];
        for (var p in this) {
            if (this.hasOwnProperty(p)) {
                value = (<any>enumType)[ p ];
                if (typeof (<any>enumType)[ p ] == "number") {
                    values.push(p);
                }
            }
        }

        values.sort((a, b) => { return (<any>this)[ a ] - (<any>this)[ b ]; });

        for (var i = values.length - 1; i >= 0; i--) {
            value = <number>(<any>this)[ values[ i ] ];
            if (value === 0) {
                zeroValueName = getName(0);
            }
                /*jslint bitwise: true */
            else if ((v & value) === value) {
                v -= value;
                names.unshift(getName(value));
            }
        }

        if (v !== 0) {
            return original.toString();
        }

        if (original !== 0) {
            return names.join(", ");
        }

        return zeroValueName || "0";
    }
    static getValues<T>($t: Type): number[] {
        var result = [ ];

        if ($t.isEnumType) {
            for (let i = 0; i < $t.enumInfo.actualNames.length; i++) {
                result.push($t.enumInfo.actualNamesValuesMap[$t.enumInfo.actualNames[i]]);
            }
            return result;
        }

        var p = $t.isEnumType ?
            $t.enumInfo.actualNames : $t.InstanceConstructor.prototype;
        for (var member in p) {
            if (p.hasOwnProperty(member)) {
                if (typeof p[ member ] === "number") {
                    result.push(p[ member ]);
                }
            }
        }

        return result;
    }
    static getNames<T>($t: Type): string[] {
        var result = [ ];

        if ($t.isEnumType) {
            for (let i = 0; i < $t.enumInfo.actualNames.length; i++) {
                result.push($t.enumInfo.actualNames[i]);
            }
            return result;
        }

        var p = $t.isEnumType ?
            $t.enumInfo.actualNames : $t.InstanceConstructor.prototype;
        for (var member in p) {
            if (p.hasOwnProperty(member)) {
                if (typeof p[ member ] === "number") {
                    result.push(member);
                }
            }
        }

        return result;
    }
    
    static getEnumValue<T>($t: Type, v: any): T {
		if (v !== null && v !== undefined) {
			if (typeof v === "number") {
				return <T><any>v;
			} else {
				return v.value;
			}
		}

		return <T><any>0;
	}

    static isDefined<T>($t: Type, value: T) {
        value = typeGetValue(value);
        var p = $t.isEnumType ?
            $t.enumInfo.actualNamesValuesMap : $t.InstanceConstructor.prototype;
        for (var member in p) {
            if (p.hasOwnProperty(member)) {
                if (p[ member ] === value) {
                    return true;
                }
            }
        }

        return false;
    }

    // TODO: Fill out remaining IConvertible implementation
    static toDouble(enumType: Type, value: any, provider: any): number {
        return value.value;
    }
    static toObject<T>($t: Type, value: T): any {
        if (typeof value == "number") {
            return getBoxIfEnum($t, value);
        }

        return value;
    }
    static tryParse$1<TEnum>($tEnum: Type, value: string, ignoreCase: boolean, result: TEnum): { ret: boolean, p2: TEnum } {
        try {
            return {
                ret: true,
                p2: typeGetValue(EnumUtil.parse($tEnum, value, ignoreCase))
            };
        } catch (e) {
            result = Type.createInstance<TEnum>($tEnum);
            return {
                ret: false,
                p2: result
            };
        }
    }
}   

export class ValueType extends Base {
    static $t: Type = markType(ValueType, "ValueType");
}

export class Nullable extends Base {
    static getUnderlyingType(nullableType: Type): Type {
        if (nullableType.isGenericType !== undefined && nullableType.isGenericType &&
            !nullableType.isGenericTypeDefinition &&
            Nullable$1.prototype.$type.typeName == nullableType.typeName) {
            return <Type>nullableType.genericTypeArguments[ 0 ];
        }

        return null;
    }
    static $t: Type = markType(Nullable, "Nullable");
}

export class Nullable$1<T> extends Base {
    protected $t: Type = null;
    private _value: T = null;
    constructor($t: Type, value: T) {
        super();
        this.$t = $t;
        this.$type = this.$type.specialize(this.$t);
        
        if (value !== undefined) {
            this._value = value;
        }
    }

    static nullableEquals(v1: any, v2: any): boolean {
		/*jshint eqnull:true */
		var v1IsNull = (v1 == null) || (!!v1.isNullable && !v1.hasValue);
		var v2IsNull = (v2 == null) || (!!v2.isNullable && !v2.hasValue);

		if (v1IsNull && v2IsNull) {
			return true;
		}
		if (v1IsNull != v2IsNull) {
			return false;
		}

		var val1 = v1;
		var val2 = v2;
		if (v1.isNullable) {
			val1 = v1.value;
		}
		if (v2.isNullable) {
			val2 = v2.value;
		}

		return val1 == val2;
	}

    equals(value: Nullable$1<T>): boolean {
        return Nullable$1.nullableEquals(this, value);
    }
    getHashCode(): number {
        if (this._value === null) {
            return 0;
        }
        if ((<any>this._value)["getHashCode"] !== undefined) {
            return (<any>this._value).getHashCode();
        }
        return Type.getPrimitiveHashCode(this._value);
    }
    get hasValue(): boolean {
        return this._value !== null;
    }
    toString(): string {
        return this._value === null ? "" : this._value.toString();
    }
    get value(): T {
        return this._value;
    }
    set value(value: T) {
        this._value = value;
    }
    getValueOrDefault(): T {
        if (this.hasValue) {
            return this._value;
        } else {
            return this.getDefaultValue();
        }
    }
    getDefaultValue(): T {
        if (Type.canAssign(Number_$type, this.$t)) {
            return <T><any>0;
        } else if (Type.canAssign(Boolean_$type, this.$t)) {
            return <T><any>false;
        } else if (this.$t.baseType == ValueType.prototype.$type) {
            return Type.createInstance<T>(this.$t);
        } else {
            return null;
        }
    }
    getValueOrDefault1(defaultValue: T): T {
        if (this.hasValue) {
            return this._value;
        } else {
            return defaultValue;
        }
    }
    preIncrement(): Nullable$1<T> {
        if (!this.hasValue) {
            return this;
        }

        this._value = <T><any>((<number><any>this._value) + 1);
        return this;
    }
    preDecrement(): Nullable$1<T> {
        if (!this.hasValue) {
            return this;
        }

        this._value = <T><any>((<number><any>this._value) - 1);
        return this;
    }
    postIncrement(): Nullable$1<T> {
        if (!this.hasValue) {
            return this;
        }

        var originalValue = this._value;
        this._value = <T><any>((<number><any>this._value) + 1);
        return new Nullable$1(this.$t, originalValue);
    }
    postDecrement(): Nullable$1<T> {
        if (!this.hasValue) {
            return this;
        }

        var originalValue = this._value;
        this._value = <T><any>((<number><any>this._value) - 1);
        return new Nullable$1(this.$t, originalValue);
    }
    readonly isNullable = true;
    static $t: Type = markType(Nullable$1, "Nullable$1");
}

export function toNullable<T>(t: Type, value: T | Nullable$1<T>): Nullable$1<T> {

    if (value == null) {
        return t._$nullNullable || (t._$nullNullable = new Nullable$1<T>(t, null));
    } else if ((<any>value).isNullable) {
        return <Nullable$1<T>>value;
    }

    return new Nullable$1<T>(t, <T>value);
}

export interface IComparable {
    compareToObject(other: object): number;
}
export let IComparable_$type = new Type(null, "IComparable");
export interface IComparable$1<T> {
    compareTo(other: T): number;
}
export let IComparable$1_$type = new Type(null, "IComparable$1");
export interface IEquatable$1<T> {
    equals(other: T): boolean;
}
export let IEquatable$1_$type = new Type(null, "IEquatable$1");

export interface INotifyPropertyChanged {
    propertyChanged: (sender: object, args: PropertyChangedEventArgs) => void;
}
export let INotifyPropertyChanged_$type = new Type(null, "INotifyPropertyChanged");

export class PropertyChangedEventArgs extends Base {
    constructor(propertyName: string) {
        super();
        this._propertyName = propertyName;
    }
    private _propertyName: string = null;
    get propertyName() : string {
        return this._propertyName;
    }
    set propertyName(value: string) {
        this._propertyName = value;
    }

    static $t: Type = markType(PropertyChangedEventArgs, "PropertyChangedEventArgs");
}
    
export class IteratorWrapper<T> implements IEnumerator$1<T> {
    private _inner: Iterator<T> = null;
    private _getNew: () => Iterator<T> = null;
    constructor (inner: Iterator<T>, getNew: () => Iterator<T>) {
        this._inner = inner;
        this._getNew = getNew;
    }
    private _hasNext: boolean = true;
    private _current: T = null;
    moveNext(): boolean {
        let next = this._inner.next();
        this._hasNext = !next.done;
        this._current = next.value;
        return this._hasNext;
    }
    get current(): T {
        return this._current;
    }
    get currentObject(): T {
        return this._current;
    }
    dispose(): void {
    }
    reset(): void {
        this._inner = this._getNew();
        this._current = null;
        this._hasNext = true;
    }
}

export class IterableWrapper<T> implements IEnumerable$1<T>, IEnumerable {
    private _inner: () => Iterable<T> = null;
    constructor (inner: () => Iterable<T>) {
        this._inner = inner;
    }
    getEnumerator() : IEnumerator$1<T> {
        return new IteratorWrapper<T>(this._inner()[Symbol.iterator](), () => this._inner()[Symbol.iterator]());
    }
    getEnumeratorObject(): IEnumerator {
        return new IteratorWrapper<T>(this._inner()[Symbol.iterator](), () => this._inner()[Symbol.iterator]());
    }
}

export class EnumeratorWrapper<T> implements Iterator<T> {
    private _inner: IEnumerator$1<T> = null;
    
    constructor (inner: IEnumerator$1<T>) {
        this._inner = inner;
    }
    
    next() : IteratorResult<T> {
        let done = !this._inner.moveNext();
        let value: T = null;
        if (!done) {
            value = this._inner.current;
        }

        return {
            done: done,
            value: value
        };
    }
}

export class EnumeratorWrapperObject<T> implements Iterator<T> {
    private _inner: IEnumerator = null;

    constructor(inner: IEnumerator) {
        this._inner = inner;
    }

    next(): IteratorResult<T> {
        let done = !this._inner.moveNext();
        let value: T = null;
        if (!done) {
            value = this._inner.currentObject;
        }

        return {
            done: done,
            value: value
        };
    }
}

export function* getEn(arr: any[]) {
    for (let item of arr) {
        yield item;
    }
}

export function getEnumeratorObject(en: any): IEnumerator {
    if ((en instanceof Array || Array.isArray(en))) {
        let arr = <any[]>en;
        return new IteratorWrapper<any>(getEn(arr), () => getEn(arr));
    }
    return en.getEnumeratorObject();
}

export function getEnumerator(en: any): IEnumerator$1<any> {
    if ((en instanceof Array || Array.isArray(en))) {
        let arr = <any[]>en;
        return new IteratorWrapper<any>(getEn(arr), () => getEn(arr));
    }
    return en.getEnumerator();
}

export class EnumerableWrapper<T> implements Iterable<T> {
    private _inner: IEnumerable$1<T> = null;
    constructor (inner: IEnumerable$1<T>) {
        this._inner = inner;
    }
    [Symbol.iterator]() {
        return new EnumeratorWrapper<T>(
            getEnumerator(
            this._inner));
    }
}

export class EnumerableWrapperObject implements Iterable<any> {
    private _inner: IEnumerable = null;
    constructor (inner: IEnumerable) {
        this._inner = inner;
    }
    [Symbol.iterator]() {
        return new EnumeratorWrapperObject<any>(
            getEnumeratorObject(
            this._inner));
    }
}



export function toEnum<T>(v: () => Iterable<T>): IEnumerable$1<T> {
    return new IterableWrapper<T>(v);
}
    
export function fromEnum<T>(v: IEnumerable$1<T>): Iterable<T> {
    return new EnumerableWrapper<T>(v);
}

export function toEn(v: () => Iterable<any>): IEnumerable {
    return new IterableWrapper<any>(v);
}
    
export function fromEn<T>(v: IEnumerable): Iterable<T> {
    return new EnumerableWrapperObject(v);
}

export function *fromDict<T>(v: Map<string, any>): Iterable<{ key: string, value: any }> {
    for (let item of v) {
        let [key, value] = item;
        yield { key: key, value: value };
    }
}


export let Number_$type: Type = new Type(Number, "Number", Base.prototype.$type, [IComparable_$type, IConvertible_$type]);
export let String_$type: Type = new Type(String, "String", Base.prototype.$type, [IComparable_$type, IConvertible_$type]);
export let Date_$type: Type = new Type(Date, "Date", Base.prototype.$type, [IComparable_$type, IConvertible_$type]);
export let Boolean_$type: Type = new Type(Boolean, "Boolean", Base.prototype.$type, [IComparable_$type, IConvertible_$type]);
export let Void_$type: Type = new Type(null, "Void", Base.prototype.$type);
export let n$ = Number_$type;
export let s$ = String_$type;
export let d$ = Date_$type;
export let b$ = Boolean_$type;
export let v$ = Void_$type;

export interface Delegate extends Function {
    enumerate?(arr: any[]): void;
    original?: any;
    target?: any;
}

export let Delegate_$type = new Type(null, "Delegate");

export function runOn<T>(target: any, func: T): T {
    var self = <Function><any>func;
    var ret: any = function () {
        return self.apply(target, arguments);
    };
    ret.original = self;
    ret.target = target;

    return <T>ret;
}

export function delegateCombine<T extends Delegate>(del1: T, del2: T): T {
    if (!del1) {
        return del2;
    }

    if (!del2) {
        return del1;
    }

    var ret = function () {
        del1.apply(null, arguments);
        return del2.apply(null, arguments);
    };
    (<any>ret).enumerate = function (arr: any[]) {
        if (del1) {
            if (del1.enumerate) {
                del1.enumerate(arr);
            } else {
                arr.push(del1);
            }
        }
        if (del2) {
            if (del2.enumerate) {
                del2.enumerate(arr);
            } else {
                arr.push(del2);
            }
        }
    };

    return <T><any>ret;
};


export function delegateRemove<T extends Delegate>(del1: T, del2: T): T {
    if (!del1) {
        return null;
    }
    if (!del2) {
        return del1;
    }

    var arr: any[] = [ ];
    var del = null;
    if (del1.enumerate) {
        del1.enumerate(arr);
    } else {
        arr.push(del1);
    }

    for (var i = 0; i < arr.length; i++) {
        if (del2.original) {
            if (arr[ i ].original == del2.original &&
                arr[ i ].target == del2.target) {
                continue;
            }
        }

        if (arr[ i ] == del2) {
            continue;
        }

        del = delegateCombine(del, arr[ i ]);
    }

    return del;
};


export interface IDisposable {
    dispose(): void
}
export let IDisposable_$type = new Type(null, 'IDisposable');

export interface IEnumerable {
    getEnumeratorObject() : IEnumerator
}
export let IEnumerable_$type = new Type(null, "IEnumerable");

export interface IEnumerator {
    currentObject: any;
    moveNext(): boolean;
    reset(): void;
}
export let IEnumerator_$type = new Type(null, "IEnumerator");

export interface IEqualityComparer$1<T> {   
    equalsC(x: T, y: T): boolean;
    getHashCodeC(obj: T): number;
}
export let IEqualityComparer$1_$type = new Type(null, "IEqualityComparer$1");

export interface IEqualityComparer {   
    equals(x: any, y: any): boolean;
    getHashCode(obj: any): number;
}
export let IEqualityComparer_$type = new Type(null, "IEqualityComparer");


export interface ICollection extends IEnumerable {
    readonly count: number;
    copyTo(array: any[], index: number): void;
    readonly isSynchronized: boolean;
    readonly syncRoot: any;  
}
export let ICollection_$type = new Type(null, "ICollection", null, [ IEnumerable_$type ]);
export interface IList extends ICollection {
    readonly isFixedSize: boolean;
    readonly isReadOnly: boolean;
    isSynchronized: boolean;
    item(index: number, value?: any): any;
    add(item: any): void;
    clear(): void;
    contains(item: any): boolean;
    indexOf(item: any): number;
    insert(index: number, item: any): void;
    remove(item: any): boolean;
    removeAt(index: number): void;
}
export let IList_$type = new Type(null, "IList", null, [ IEnumerable_$type, ICollection_$type ]);

export interface IEnumerable$1<T> extends IEnumerable {
    getEnumerator() : IEnumerator$1<T>
}
export let IEnumerable$1_$type = new Type(null, "IEnumerable$1", null, [ IEnumerable_$type ]);

export interface ICollection$1<T> extends IEnumerable$1<T>, IEnumerable {
        readonly count: number;
        readonly isReadOnly: boolean;
        add(item: T): void;   
        clear(): void;
        contains(item: T): boolean;
        copyTo(array: T[], arrayIndex: number): void;
		remove(item: T): boolean;
}
export let ICollection$1_$type = new Type(null, "ICollection$1", null, [ IEnumerable$1_$type.specialize(0), IEnumerable_$type ]);

export interface IList$1<T> extends ICollection$1<T>, IEnumerable$1<T>, IEnumerable {
        item(index: number, value?: T): T;
        indexOf(item: T): number;
        insert(index: number, item: T): void;
        removeAt(index: number): void;
}
export let IList$1_$type = new Type(null, "IList$1", null,  [ 
    ICollection$1_$type.specialize(0),
    IEnumerable$1_$type.specialize(0),
    IEnumerable_$type ]);

export interface IEnumerator$1<T> extends IEnumerator, IDisposable {
    current: T;
    moveNext(): boolean;
    reset(): void;
}
export let IEnumerator$1_$type = new Type(null, "IEnumerator$1", null, [ IEnumerator_$type, IDisposable_$type ]);

export interface IDictionary {
    // count: number;
    // isFixedSize: boolean;
    // isReadOnly: boolean;
    // isSynchronized: boolean;
    // item(key: any): object;
    
    // add(key: any, item: any): void;
    // clear(): void;
    // contains(key: any): boolean;
    // remove(key: any): boolean;
}
export let IDictionary_$type = new Type(null, "IDictionary");

export class BaseError extends Base {
    static $t: Type = markType(BaseError, "BaseError");
    constructor(initNumber: number = -1, ...rest: any[]) {
        super();
        if (initNumber >= 0) {
            switch (initNumber) {
                case 1:
                    this.init1.apply(this, arguments);
                    break;
                case 2:
                    this.init2.apply(this, arguments);
                    break;
            }
        }
        return;
    }   
    protected get_message(): string {
        return this._message;
    }
    get message() : string {
        return this.get_message();
    }
    get innerException(): any {
        return this._innerException;
    }
    private _message: string = null;
    private _innerException: any = null;
    init1(initNumber: number, message: string) {
        this._message = message;
    }
    init2(initNumber: number, message: string, innerException: any) {
        this._message = message;
        this._innerException = innerException;
    }
    toString() : string {
        return this._message;
    }
}

export class SystemException extends BaseError {
	constructor(initNumber: number, ...rest: any[]) {
        super(0);
        if (initNumber > 0) {
            switch (initNumber) {
                case 1:
                    this.init1.apply(this, arguments);
                    break;
                case 2:
                    this.init2.apply(this, arguments);
                    break;
            }
            return;
        }
        //super(0);
    }
    init1(initNumber: number, message: string) {
        super.init1(1, message);
    }
    init2(initNumber: number, message: string, innerException: any) {
        super.init2(2, message, innerException);
    }
    static $t: Type = markType(SystemException, "SystemException", BaseError.prototype.$type);
}

export class NotSupportedException extends SystemException {
    constructor (initNumber: number, ...rest: any[]) {
        super(0);
        if (initNumber > 0) {
            switch (initNumber) {
                case 1:
                    this.init1.apply(this, arguments);
                    break;
                case 2:
                    this.init2.apply(this, arguments);
                    break;
            }
            return;
        }
    }
    init1(initNumber: number, message: string) {
        super.init1(1, message);
    }
    init2(initNumber: number, message: string, innerException: any) {
        super.init2(2, message, innerException);
    }
    static $t: Type = markType(NotSupportedException, "NotSupportedException", SystemException.prototype.$type);
}

export class FormatException extends SystemException {
    constructor (initNumber: number, ...rest: any[]) {
        super(0);
        if (initNumber > 0) {
            switch (initNumber) {
                case 1:
                    this.init1.apply(this, arguments);
                    break;
                case 2:
                    this.init2.apply(this, arguments);
                    break;
            }
            return;
        }
    }
    init1(initNumber: number, message: string) {
        super.init1(1, message);
    }
    init2(initNumber: number, message: string, innerException: any) {
        super.init2(2, message, innerException);
    }
    static $t: Type = markType(FormatException, "FormatException", SystemException.prototype.$type);
}

export class PointUtil {
    static equals(p1: Point, p2: Point): boolean {
        if (p1 == null && p2 == null) {
            return true;
        }
        if (p1 == null && p2 != null) {
            return false
        }
        if (p1 != null && p2 == null) {
            return false;
        }
        return p1.x == p2.x && p1.y == p2.y;
    }
    static notEquals(p1: Point, p2: Point): boolean {
        return !PointUtil.equals(p1, p2);
    }
    static create() {
        return { x: 0, y: 0, $type: Point_$type };
    }
    static createXY(x: number, y: number) {
        return { x: x, y: y, $type: Point_$type };
    }
}
export interface Point {
    x: number;
    y: number;
}
export let Point_$type = new Type(null, "Point");

export enum SeekOrigin {
    Begin,
    Current,
    End
}

export abstract class Stream extends Base {
		close(): void {
			this.disposeCore(true);
		}
		dispose(): void {
			this.close();
		}
		protected disposeCore(disposing: boolean): void {
		}
		abstract flush(): void;
		readByte(): number {
			var bytes = [ 0 ];
			var count = (<any>this).read(bytes, 0, 1);
			if (count === 0) {
				return -1;
			}

			return bytes[ 0 ];
		}
		writeByte(value: number) {
			(<any>this).write([ value ], 0, 1);
        }
        abstract read(bytes: number[], offset: number, count: number): number;
        abstract seek(offset: number, origin: SeekOrigin): number;
        abstract setLength(value: number): void;
        abstract write(buffer: number[], offset: number, count: number): void;
        abstract get canRead(): boolean;
        abstract get canSeek(): boolean;
        abstract get canWrite(): boolean;
        abstract get length(): number;
        abstract get position(): number;
        abstract set position(value: number);

        static $t: Type = markType(Stream, "Stream");
    }

    export class EventArgs extends Base {
        private static _empty: EventArgs;
        static get empty(): EventArgs {
            if (EventArgs._empty) {
                return EventArgs._empty;
            }
            EventArgs._empty = new EventArgs;
            return EventArgs._empty;
        }
        static $t: Type = markType(EventArgs, "EventArgs");
    }

    export interface IFormatProvider {
        getFormat(formatType: Type): any;
    }
    export let IFormatProvider_$type = new Type(null, 'IFormatProvider');

export let Array_$type: Type = new Type(Array, "Array", Base.prototype.$type, [IEnumerable_$type, ICollection_$type]);
export let a$ = Array_$type;

export class TypeRegistrar {
    static _registrar: Map<string, Function> = new Map<string, Function>();
    static create(typeName: string, ...rest: any[]): any {
        if (TypeRegistrar.isRegistered(typeName)) {
            let C: any = TypeRegistrar.get(typeName);
            if (C.htmlTagName) {
                return document.createElement(C.htmlTagName);
            } else {
                return new C(...rest);
            }
        } else {
            return null;
        }
    }

    static register(typeName: string, type: Type): void {
        TypeRegistrar._registrar.set(typeName, type.InstanceConstructor);
    }

    static registerCons(typeName: string, type: Function): void {
        TypeRegistrar._registrar.set(typeName, type);
    }

    static callRegister(typeName: string): void {
        let type = TypeRegistrar.get(typeName);
        if (type !== undefined && type !== null &&
            type["register"] !== undefined) {
            type["register"]();
        }
    }

    static isRegistered(typeName: string): boolean {
        return TypeRegistrar.get(typeName) !== null;
    }

    static get(typeName: string): any {
        if (TypeRegistrar._registrar.has(typeName)) {
            return TypeRegistrar._registrar.get(typeName);
        }

        if (typeName.indexOf("Igx") == 0) {
            let igc = typeName.replace("Igx", "Igc");
            if (TypeRegistrar._registrar.has(igc)) {
                return TypeRegistrar._registrar.get(igc);
            }
            let igr = typeName.replace("Igx", "Igr");
            if (igr.indexOf("Component") == igr.length - 9) {
                igr = igr.substring(0, igr.length - 9);
            }
            if (TypeRegistrar._registrar.has(igr)) {
                return TypeRegistrar._registrar.get(igr);
            }
        }

        return null;
    }

    static createFromInternal(internal: any, prefix: string, postfix: string): any {
        if (!internal) {
            return null;
        }
        if (!internal.$type) {
            return null;
        }
        let name = internal.$type.name;
        let externalName = prefix + name + postfix;
        if (!TypeRegistrar.isRegistered(externalName)) {
            return null;
        }
        return TypeRegistrar.create(externalName);
    }
}

export function createMutationObserver(...args: any[]) {
    let origObserver: any = MutationObserver;
    if ((window as any).Zone && (window as any).Zone.__symbol__) {
        origObserver = (window as any)[(window as any).Zone.__symbol__('MutationObserver')]
            || (window as any)[(window as any).Zone.__symbol__('WebKitMutationObserver')];
    }
    return new origObserver(...args);
}

