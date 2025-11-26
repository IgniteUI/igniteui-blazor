import { Base, Type, markType } from "./type";
import { JsonWriter } from "./JsonWriter";
import { StringBuilder } from "./StringBuilder";
import { stringReplace } from "./string";

/**
 * @hidden 
 */
export class JsonDictionaryItem extends Base {
	static $t: Type = markType(JsonDictionaryItem, 'JsonDictionaryItem');
	toJson(): string {
		let writer = new JsonWriter();
		this.toJsonHelper(writer);
		return writer.toString();
	}
	toJsonHelper(writer: JsonWriter): void {
	}
	protected escape(val: string): string {
		if (val == null) {
			return null;
		}
		return stringReplace(stringReplace(stringReplace(stringReplace(stringReplace(val, "\\", "\\\\"), "\t", "\\t"), "\"", "\\\""), "\r", "\\r"), "\n", "\\n");
	}
	static unescape(val: string): string {
		if (val == null) {
			return null;
		}
		let currStart: number = -1;
		let sb: StringBuilder = new StringBuilder(0);
		for (let i = 0; i < val.length; i++) {
			if (val.charAt(i) == '\\' && currStart == -1) {
				currStart = i;
			} else {
				if (currStart == -1) {
					sb.append1(val.charAt(i));
				} else {
					if (val.charAt(i) == 'n') {
						sb.append5("\n");
						currStart = -1;
					} else if (val.charAt(i) == 'r') {
						sb.append5("\r");
						currStart = -1;
					} else if (val.charAt(i) == 't') {
						sb.append5("\t");
						currStart = -1;
					} else if (val.charAt(i) == '\"') {
						sb.append5("\"");
						currStart = -1;
					} else if (val.charAt(i) == '\\') {
						sb.append5("\\");
						currStart = -1;
					} else {
						sb.append5("\\" + val.charAt(i));
						currStart = -1;
					}
				}
			}
		}
		return sb.toString();
	}
}


