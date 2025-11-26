import { BaseError, Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class NotImplementedException extends BaseError {
	static $t: Type = markType(NotImplementedException, 'NotImplementedException', (<any>BaseError).$type);
	constructor(initNumber: number);
	constructor(initNumber: number, message: string);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				super(1, "not implemented");
			}
			break;

			case 1:
			{
				let message: string = <string>_rest[0];
				super(1, message);
				throw new NotImplementedException(0);
			}
			break;

		}

	}
}


