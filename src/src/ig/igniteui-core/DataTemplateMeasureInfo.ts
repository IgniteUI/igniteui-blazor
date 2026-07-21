import { Base, Type, markType } from "./type";
import { DataTemplatePassInfo } from "./DataTemplatePassInfo";

/**
 * @hidden 
 */
export class DataTemplateMeasureInfo extends Base {
	static $t: Type = markType(DataTemplateMeasureInfo, 'DataTemplateMeasureInfo');
	renderContext: any = null;
	context: any = null;
	width: number = 0;
	height: number = 0;
	isConstant: boolean = false;
	data: any = null;
	passInfo: DataTemplatePassInfo = null;
	renderOffsetX: number = 0;
	renderOffsetY: number = 0;
}


