import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DataTemplatePassInfo extends Base {
	static $t: Type = markType(DataTemplatePassInfo, 'DataTemplatePassInfo');
	renderContext: any = null;
	context: any = null;
	viewportTop: number = 0;
	viewportLeft: number = 0;
	viewportWidth: number = 0;
	viewportHeight: number = 0;
	isHitTestRender: boolean = false;
	passID: string = null;
}


