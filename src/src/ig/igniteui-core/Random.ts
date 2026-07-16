import { Base, Type, markType } from "./type";
import { truncate } from "./number";

/**
 * @hidden 
 */
export class Random extends Base {
	static $t: Type = markType(Random, 'Random');
	constructor(initNumber: number);
	constructor(initNumber: number, Seed: number);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		super();
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0: break;
			case 1:
			{
				let Seed: number = <number>_rest[0];
			}
			break;

		}

	}
	nextDouble(): number {
		return Math.random();
	}
	next(): number {
		return this.next1(0x7FFFFFFF);
	}
	next1(value: number): number {
		return <number>truncate(Math.round(this.nextDouble() * (value - 1)));
	}
	next2(low: number, high: number): number {
		return low + <number>truncate(Math.round(this.nextDouble() * ((high - low) - 1)));
	}
}


