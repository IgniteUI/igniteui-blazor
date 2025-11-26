import { BaseError, Base, Type, markType } from "./type";
import { NotImplementedException } from "./NotImplementedException";

/**
 * @hidden 
 */
export class ArgumentNullException extends BaseError {
	static $t: Type = markType(ArgumentNullException, 'ArgumentNullException', (<any>BaseError).$type);
	constructor(initNumber: number, argumentName: string);
	constructor(initNumber: number);
	constructor(initNumber: number, paramName: string, message: string);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				let argumentName: string = <string>_rest[0];
				super(1, argumentName + " cannot be null.");
			}
			break;

			case 1:
			{
				super(0);
				throw new NotImplementedException(0);
			}
			break;

			case 2:
			{
				let paramName: string = <string>_rest[0];
				let message: string = <string>_rest[1];
				super(1, message);
				throw new NotImplementedException(0);
			}
			break;

		}

	}
}


