import { DataTemplateRenderInfo } from './DataTemplateRenderInfo';
import { DataTemplateMeasureInfo } from './DataTemplateMeasureInfo';
import { DataTemplatePassInfo } from './DataTemplatePassInfo';

export interface IgDataTemplate {   
    render: (info: DataTemplateRenderInfo) => void; 
    measure: (info: DataTemplateMeasureInfo) => void; 
    passStarting: (info: DataTemplatePassInfo) => void; 
    passCompleted: (info: DataTemplatePassInfo) => void; 
}