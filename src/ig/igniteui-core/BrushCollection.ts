import { ObservableCollection$1 } from "./ObservableCollection$1";
import { Brush } from "./Brush";
import { InterpolationMode } from "./InterpolationMode";
import { NotifyCollectionChangedEventArgs } from "./NotifyCollectionChangedEventArgs";
import { List$1 } from "./List$1";
import { Random } from "./Random";
import { Base, typeCast, Type, markType } from "./type";
import { NotifyCollectionChangedAction } from "./NotifyCollectionChangedAction";

/**
 * @hidden 
 */
export class BrushCollection extends ObservableCollection$1<Brush> {
	static $t: Type = markType(BrushCollection, 'BrushCollection', (<any>ObservableCollection$1).$type.specialize((<any>Brush).$type));
	constructor() {
		super((<any>Brush).$type, 0);
	}
	private static random: Random = new Random(0);
	selectRandom(): Brush {
		return this.item(BrushCollection.random.next1(this.count));
	}
	get interpolationMode(): InterpolationMode {
		return this._interpolationMode;
	}
	set interpolationMode(value: InterpolationMode) {
		if (this._interpolationMode != value) {
			this._interpolationMode = value;
			this.onCollectionChanged(new NotifyCollectionChangedEventArgs(0, NotifyCollectionChangedAction.Reset));
		}
	}
	private _interpolationMode: InterpolationMode = InterpolationMode.RGB;
	item(index: number, value?: Brush): Brush {
		if (arguments.length === 2) {
			super.item(index, value);
			return value;
		} else {
			if (index < 0 || index >= this.count) {
				return null;
			}
			return <Brush>super.item(index);
		}
	}
	equals(obj: any): boolean {
		if (obj == null || !(typeCast<BrushCollection>((<any>BrushCollection).$type, obj) !== null)) {
			return false;
		}
		let bc: BrushCollection = <BrushCollection>obj;
		if (bc.count != this.count) {
			return false;
		}
		for (let i: number = 0; i < bc.count; i++) {
			if (!bc.item(i).equals(this.item(i))) {
				return false;
			}
		}
		return true;
	}
}


