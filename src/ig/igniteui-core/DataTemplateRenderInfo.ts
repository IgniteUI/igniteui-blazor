import { Base, Type, markType } from "./type";
import { DataTemplatePassInfo } from "./DataTemplatePassInfo";

/**
 * @hidden 
 */
export class DataTemplateRenderInfo extends Base {
	static $t: Type = markType(DataTemplateRenderInfo, 'DataTemplateRenderInfo');
	renderContext: any = null;
	context: any = null;
	xPosition: number = 0;
	yPosition: number = 0;
	availableWidth: number = 0;
	availableHeight: number = 0;
	data: any = null;
	isHitTestRender: boolean = false;
	passInfo: DataTemplatePassInfo = null;
	renderOffsetX: number = 0;
	renderOffsetY: number = 0;
}


