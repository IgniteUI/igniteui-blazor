import { IgcTemplateContainerComponent } from './igc-template-container-component';
import { TypeRegistrar } from './type';
import { IgcTemplateContentModule } from './igc-template-content-module';

export class IgcTemplateContainerModule {
    public static register() {
        IgcTemplateContainerComponent.register();
        IgcTemplateContentModule.register();
    }
}