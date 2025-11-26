import { BaseError, Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class IndexOutOfRangeException extends BaseError {
	static $t: Type = markType(IndexOutOfRangeException, 'IndexOutOfRangeException', (<any>BaseError).$type);
	constructor(initNumber: number);
	constructor(initNumber: number, message: string);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				super(0);
			}
			break;

			case 1:
			{
				let message: string = <string>_rest[0];
				super(1, message);
			}
			break;

		}

	}
}


