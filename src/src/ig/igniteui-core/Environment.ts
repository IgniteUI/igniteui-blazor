import { Base, BaseError, Type, markType } from "./type";

/**
 * @hidden 
 */
export class Environment extends Base {
	static $t: Type = markType(Environment, 'Environment');
	static get newLine(): string {
		return "\n";
	}
	static get stackTrace(): string {
		try {
			throw new Error();
		}
		catch (e) {
			return <string>(e.stack);
		}
		return "";
	}
}


