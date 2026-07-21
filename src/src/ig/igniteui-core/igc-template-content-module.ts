import { IgcTemplateContentComponent } from './igc-template-content-component';
import { TypeRegistrar } from './type';

export class IgcTemplateContentModule {
    public static register() {
        IgcTemplateContentComponent.register();
        TypeRegistrar.registerCons("IgcTemplateContentComponent", IgcTemplateContentComponent);
    }
}

